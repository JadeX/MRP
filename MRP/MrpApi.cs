using System;
using System.Collections.Generic;
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

namespace MRP
{
    public class MrpApi
    {
        private readonly Cryptography _crypto;
        private readonly MrpApiConfig _config;

        public MrpApi(MrpApiConfig config)
        {
            _config = config;

            _crypto = new Cryptography(_config.SecretKey);
        }

        private async Task<T> PostFilteredAsync<T>(List<NameValueItem> filters) where T : IResponse => await PostAsync<T>(new Data()
        {
            Filter = new Filter()
            {
                Items = filters
            }
        });

        public async Task<EXPEO0> EXPEO0(List<NameValueItem> filters) => await PostFilteredAsync<EXPEO0>(filters);

        public async Task<EXPEO1> EXPEO1(List<NameValueItem> filters) => await PostFilteredAsync<EXPEO1>(filters);

        public async Task<IMPEO0> IMPEO0(Params @params, Objednavka objednavka) => throw new NotImplementedException();

        public async Task<CENEO0> CENEO0(List<NameValueItem> filters) => await PostFilteredAsync<CENEO0>(filters);

        public async Task<ADREO0> ADREO0() => throw new NotImplementedException();

        public async Task<EXPFV0> EXPFV0() => throw new NotImplementedException();

        public async Task<EXPFV1> EXPFV1() => throw new NotImplementedException();

        public async Task<EXPFP0> EXPFP0() => throw new NotImplementedException();

        public async Task<EXPFP1> EXPFP1() => throw new NotImplementedException();

        public async Task<T> PostAsync<T>(Data requestData) where T : IResponse => await PostAsync<T>(XDocument.Parse(SerializeToXmlString<Data>(requestData)));

        public async Task<T> PostAsync<T>(XDocument requestData) where T : IResponse
        {
            var mrpEnvelope = new MrpEnvelope();

            var mrpRequest = new MrpRequest()
            {
                Request = new Request()
                {
                    Command = (MrpCommands)Enum.Parse(typeof(MrpCommands), typeof(T).Name)
                },
                Data = requestData.Root
            };

            if (_config.UseEncryption || _config.UseCompression)
            {
                var data = Encoding.UTF8.GetBytes(SerializeToXmlString<MrpRequest>(mrpRequest));

                var mrpEncodingParams = new MrpEncodingParams();

                if (_config.UseCompression)
                {
                    mrpEncodingParams.Compression = "zlib";
                    data = Compression.Deflate(data, _config.CompressionLevel);
                }

                if (_config.UseEncryption)
                {
                    mrpEncodingParams.Encryption = "aes";
                    data = _crypto.EncryptData(data);
                    mrpEncodingParams.VarKey = Convert.ToBase64String(_crypto.VariantKey);
                }

                var encodingParams = Encoding.UTF8.GetBytes(SerializeToXmlString<MrpEncodingParams>(mrpEncodingParams));

                mrpEnvelope.EncodedBody = new EncodedBody()
                {
                    Authentication = _config.UseEncryption ? "hmac_sha256" : null,
                    EncodingParams = Convert.ToBase64String(encodingParams),
                    EncodedData = Convert.ToBase64String(data),
                    AuthCode = _config.UseEncryption ? _crypto.SignData(encodingParams.Concat(data).ToArray()) : null
                };
            }
            else
            {
                mrpEnvelope.Body = new Body { MrpRequest = mrpRequest };
            }

            return (T)await ProcessResponseAsync<T>(
                await new HttpClient().PostAsync(
                    _config.Url,
                    new StringContent(SerializeToXmlString<MrpEnvelope>(mrpEnvelope), Encoding.UTF8, "application/xml")));
        }

        private static T DeserializeFromXmlString<T>(string xmlData)
        {
            using (var sr = new StringReader(xmlData))
            {
                var s = new XmlSerializer(typeof(T));

                return (T)s.Deserialize(sr);
            }
        }

        private static string SerializeToXmlString<T>(object xmlData)
        {
            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true }))
                {
                    var s = new XmlSerializer(typeof(T));
                    s.Serialize(writer, xmlData, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                }

                return sw.ToString();
            }
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

            var mrpEnvelope = DeserializeFromXmlString<MrpEnvelope>(await httpResponse.Content.ReadAsStringAsync());

            MrpResponse responseData;

            if (mrpEnvelope.EncodedBody != null)
            {
                var data = Convert.FromBase64String(mrpEnvelope.EncodedBody.EncodedData);

                if (mrpEnvelope.EncodedBody.EncodingParams != null)
                {
                    var responseParams = DeserializeFromXmlString<MrpEncodingParams>(Encoding.UTF8.GetString(Convert.FromBase64String(mrpEnvelope.EncodedBody.EncodingParams)));

                    if (!string.IsNullOrEmpty(responseParams.VarKey))
                    {
                        _crypto.VariantKey = Convert.FromBase64String(responseParams.VarKey);
                    }

                    if (responseParams.Encryption == "aes")
                    {
                        if (_crypto.SecretKey == null)
                        {
                            //TODO: BUG, unencrypted/compressed request to server with encryption enabled returns encrypted/uncompressed result
                            response.ErrorCode = -1;
                            response.ErrorClass = "ESvcClientError";
                            response.ErrorMessage = "Je vyžadována autentizace.";

                            return response;
                        }

                        if (!_crypto.HasValidSignature(mrpEnvelope))
                        {
                            response.ErrorCode = -1;
                            response.ErrorClass = "ESvcClientError";
                            response.ErrorMessage = "Neplatný autentizační kód v elementu \"authCode\".";

                            return response;
                        }

                        data = _crypto.DecryptData(data);
                    }

                    if (responseParams.Compression == "zlib")
                    {
                        data = Compression.Inflate(data);
                    }
                }

                responseData = DeserializeFromXmlString<MrpResponse>(Encoding.UTF8.GetString(data));
            }
            else
            {
                responseData = mrpEnvelope.Body.MrpResponse;
            }

            if (responseData.Status.Error != null)
            {
                response.ErrorCode = int.Parse(responseData.Status.Error.ErrorCode);
                response.ErrorClass = responseData.Status.Error.ErrorClass;
                response.ErrorMessage = responseData.Status.Error.ErrorMessage;

                return response;
            }

            var xdoc = XDocument.Parse(responseData.Data.OuterXml);

            switch (responseData.Status.Request.Command)
            {
                case MrpCommands.EXPEO0:
                    response = new EXPEO0()
                    {
                        Categories = xdoc.Descendants("katalog").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpCategory>(x.ToString())).ToList(),
                        Products = xdoc.Descendants("karty").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpProduct>(x.ToString())).ToList(),
                        Replacements = xdoc.Descendants("nahrady").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpReplacement>(x.ToString())).ToList(),
                    };
                    goto default;

                case MrpCommands.EXPEO1:
                    response = new EXPEO1()
                    {
                        Categories = xdoc.Descendants("katalog").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpCategory>(x.ToString())).ToList(),
                        Products = xdoc.Descendants("karty").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpProduct>(x.ToString())).ToList(),
                        Replacements = xdoc.Descendants("nahrady").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpReplacement>(x.ToString())).ToList(),
                        Warehouses = xdoc.Descendants("sklady").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpWarehouse>(x.ToString())).ToList(),
                        Stocks = xdoc.Descendants("stavy").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpStock>(x.ToString())).ToList()
                    };
                    goto default;

                case MrpCommands.IMPEO0:
                    response = new IMPEO0();
                    goto default;

                case MrpCommands.CENEO0:
                    response = new CENEO0()
                    {
                        Prices = xdoc.Descendants("ceny").FirstOrDefault()?.Descendants("fields").Select(x => DeserializeFromXmlString<MrpPrice>(x.ToString())).ToList()
                    };
                    goto default;

                case MrpCommands.ADREO0:
                    response = new ADREO0();
                    goto default;

                case MrpCommands.EXPFV0:
                    response = new EXPFV0();
                    goto default;

                case MrpCommands.EXPFV1:
                    response = new EXPFV1();
                    goto default;

                case MrpCommands.EXPFP0:
                    response = new EXPFP0();
                    goto default;

                case MrpCommands.EXPFP1:
                    response = new EXPFP1();
                    goto default;

                default:
                    response.Data = xdoc;
                    return response;
            }
        }
    }
}
