namespace MRP;

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MRP.Commands;
using MRP.Xml;
using MRP.Xml.Datasets;
using SharpCompress.Compressors.Deflate;

public class MrpApi
{
    private readonly MrpApiConfig config;
    private HttpClient httpClient;

    public MrpApi(string url) => this.config = new MrpApiConfig() { Url = url };

    public Task<CENEO0> CENEO0(Action<RequestFilterOptions> requestFilterOptions) => this.PostFilteredAsync<CENEO0>(requestFilterOptions);

    public Task<EXPEO0> EXPEO0(Action<RequestFilterOptions> requestFilterOptions) => this.PostFilteredAsync<EXPEO0>(requestFilterOptions);

    public Task<EXPEO1> EXPEO1(Action<RequestFilterOptions> requestFilterOptions) => this.PostFilteredAsync<EXPEO1>(requestFilterOptions);

    public Task<T> PostAsync<T>(XDocument requestData) where T : IResponse => this.PostAsync<T>(DeserializeFromXmlString<Data>(requestData.ToString()));

    public async Task<T> PostAsync<T>(Data requestData) where T : IResponse
    {
        this.httpClient ??= new HttpClient();

        if (this.config.Timeout != default)
        {
            this.httpClient.Timeout = this.config.Timeout;
        }

        var crypto = new Cryptography(this.config.SecretKey);
        var mrpEnvelope = new MrpEnvelope();
        var mrpRequest = new MrpRequest()
        {
            Request = new Request()
            {
                Command = (MrpCommands)Enum.Parse(typeof(MrpCommands), typeof(T).Name)
            },
            Data = requestData
        };

        if (this.config.UseEncryption || this.config.UseCompression)
        {
            var data = Encoding.UTF8.GetBytes(SerializeToXmlString<MrpRequest>(mrpRequest));

            var mrpEncodingParams = new MrpEncodingParams();

            if (this.config.UseCompression)
            {
                mrpEncodingParams.Compression = "zlib";
                data = Compression.Deflate(data, this.config.CompressionLevel);
            }

            if (this.config.UseEncryption)
            {
                mrpEncodingParams.Encryption = "aes";
                data = crypto.EncryptData(data);
                mrpEncodingParams.VarKey = crypto.GetVariantKey();
            }

            var encodingParams = Encoding.UTF8.GetBytes(SerializeToXmlString<MrpEncodingParams>(mrpEncodingParams));

            mrpEnvelope.EncodedBody = new EncodedBody()
            {
                EncodingParams = Convert.ToBase64String(encodingParams),
                EncodedData = Convert.ToBase64String(data),
            };

            if (this.config.UseEncryption)
            {
                mrpEnvelope.EncodedBody.Authentication = "hmac_sha256";
                mrpEnvelope.EncodedBody.AuthCode = crypto.SignData([.. encodingParams, .. data]);
            }
        }
        else
        {
            mrpEnvelope.Body = new Body { MrpRequest = mrpRequest };
        }

        return (T)await this.ProcessResponseAsync<T>(
            await this.httpClient.PostAsync(
                this.config.Url,
                new StringContent(SerializeToXmlString<MrpEnvelope>(mrpEnvelope), Encoding.UTF8, "application/xml")));
    }

    public MrpApi WithCompression(CompressionLevel level = CompressionLevel.Default)
    {
        this.config.UseCompression = true;
        this.config.CompressionLevel = level;
        return this;
    }

    public MrpApi WithEncryption(string secretKey)
    {
        this.config.SecretKey = secretKey;
        return this;
    }

    public MrpApi WithTimeout(TimeSpan timeout)
    {
        this.config.Timeout = timeout;
        return this;
    }

    private static T DeserializeFromXmlString<T>(string xmlData)
    {
        using var sr = new StringReader(xmlData);
        var s = new XmlSerializer(typeof(T));

        return (T)s.Deserialize(sr);
    }

    private static string SerializeToXmlString<T>(object xmlData)
    {
        using var sw = new StringWriter();
        using (var writer = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true }))
        {
            var s = new XmlSerializer(typeof(T));
            s.Serialize(writer, xmlData, new XmlSerializerNamespaces([XmlQualifiedName.Empty]));
        }

        return sw.ToString();
    }

    private MrpResponse ParseResponseData(MrpEnvelope mrpEnvelope)
    {
        if (mrpEnvelope.Body?.MrpResponse != null)
        {
            return mrpEnvelope.Body.MrpResponse;
        }

        var data = Convert.FromBase64String(mrpEnvelope.EncodedBody.EncodedData);

        var responseParams = DeserializeFromXmlString<MrpEncodingParams>(Encoding.UTF8.GetString(Convert.FromBase64String(mrpEnvelope.EncodedBody.EncodingParams)));

        if (responseParams.Encryption == "aes")
        {
            if (!this.config.UseEncryption)
            {
                /* BUG: Response is encrypted, but we don't have secret key to decrypt it.
                 * Happens when server is set to require encryption, but request was plaintext.
                 * Let's just assume response contained error requesting encrypted/authenticated communication. */
                return new MrpResponse() { Status = new Status() { Error = new MrpError() { ErrorCode = "-1", ErrorClass = "", ErrorMessage = "Je vyžadována autentizace." } } };
            }

            var crypto = new Cryptography(this.config.SecretKey, responseParams.VarKey);

            if (!crypto.VerifyEnvelopeSignature(mrpEnvelope))
            {
                // Signature doesn't match, assume forged response
                throw new InvalidOperationException("Neplatný autentizační kód v elementu \"authCode\"!");
            }

            data = crypto.DecryptData(data);
        }

        if (responseParams.Compression == "zlib")
        {
            data = Compression.Inflate(data);
        }

        return DeserializeFromXmlString<MrpResponse>(Encoding.UTF8.GetString(data));
    }

    private Task<T> PostFilteredAsync<T>(Action<RequestFilterOptions> requestFilterOptions) where T : IResponse
    {
        var filters = new RequestFilterOptions();

        requestFilterOptions(filters);

        return this.PostAsync<T>(new Data()
        {
            Filter = new Filter()
            {
                Items = filters.FilterItems
            }
        });
    }

    private async Task<IResponse> ProcessResponseAsync<T>(HttpResponseMessage httpResponse) where T : IResponse
    {
        IResponse response = Activator.CreateInstance<T>();

        if (!httpResponse.IsSuccessStatusCode)
        {
            response.ErrorCode = (int)httpResponse.StatusCode;
            response.ErrorMessage = httpResponse.ReasonPhrase;
            return response;
        }

        var responseXml = Encoding.UTF8.GetString(await httpResponse.Content.ReadAsByteArrayAsync());
        var mrpEnvelope = DeserializeFromXmlString<MrpEnvelope>(responseXml);

        var responseData = this.ParseResponseData(mrpEnvelope);

        if (responseData.Status.Error != null)
        {
            response.ErrorCode = int.Parse(responseData.Status.Error.ErrorCode, CultureInfo.InvariantCulture);
            response.ErrorClass = responseData.Status.Error.ErrorClass;
            response.ErrorMessage = responseData.Status.Error.ErrorMessage;
            return response;
        }

        var dataXml = XDocument.Parse(responseData.Data.OuterXml);

        switch (responseData.Status.Request.Command)
        {
            case MrpCommands.EXPEO0:
                response = new EXPEO0()
                {
                    Categories = dataXml.Descendants("katalog").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpCategory>(x.ToString())).ToList(),
                    Products = dataXml.Descendants("karty").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpProduct>(x.ToString())).ToList(),
                    Replacements = dataXml.Descendants("nahrady").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpReplacement>(x.ToString())).ToList(),
                };
                goto default;

            case MrpCommands.EXPEO1:
                response = new EXPEO1()
                {
                    Categories = dataXml.Descendants("katalog").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpCategory>(x.ToString())).ToList(),
                    Products = dataXml.Descendants("karty").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpProduct>(x.ToString())).ToList(),
                    Replacements = dataXml.Descendants("nahrady").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpReplacement>(x.ToString())).ToList(),
                    Warehouses = dataXml.Descendants("sklady").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpWarehouse>(x.ToString())).ToList(),
                    Stocks = dataXml.Descendants("stavy").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpStock>(x.ToString())).ToList()
                };
                goto default;

            case MrpCommands.CENEO0:
                response = new CENEO0()
                {
                    Prices = dataXml.Descendants("ceny").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpPrice>(x.ToString())).ToList()
                };
                goto default;

            default:
                response.Data = dataXml;
                return response;
        }
    }
}
