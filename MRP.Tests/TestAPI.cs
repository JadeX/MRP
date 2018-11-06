using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MRP.Commands;
using MRP.Xml;
using Xunit;
using Xunit.Abstractions;

namespace MRP.Tests
{
    public class TestAPI
    {
        private readonly ITestOutputHelper _output;

        public TestAPI(ITestOutputHelper output)
        {
            _output = output;

            Configuration = new ConfigurationBuilder().AddUserSecrets<TestAPI>().AddEnvironmentVariables().Build();

            SecretKey = Configuration["SecretKey"];
            ApiUrl = Configuration["ApiUrl"];
        }

        public string ApiUrl { get; set; }

        public IConfiguration Configuration { get; set; }

        public int MyProperty { get; set; }

        public string SecretKey { get; set; }

        [Fact]
        public async Task CompressedRequest()
        {
            var mrpApi = new MrpApi(new MrpApiConfig() { Url = ApiUrl });

            var response = await Send(mrpApi);

            Assert.True(!response.HasError || response.ErrorCode == -1);
        }

        [Fact]
        public async Task EncryptedAndCompressedRequest()
        {
            var mrpApi = new MrpApi(new MrpApiConfig() { Url = ApiUrl, SecretKey = SecretKey });

            var response = await Send(mrpApi);

            Assert.True(!response.HasError || response.ErrorCode == -1);
        }

        [Fact]
        public async Task EncryptedRequest()
        {
            var mrpApi = new MrpApi(new MrpApiConfig() { Url = ApiUrl, SecretKey = SecretKey, UseCompression = false });

            var response = await Send(mrpApi);

            Assert.True(!response.HasError || response.ErrorCode == -1);
        }

        [Fact]
        public async Task PlainRequest()
        {
            var mrpApi = new MrpApi(new MrpApiConfig() { Url = ApiUrl, UseCompression = false });

            var response = await Send(mrpApi);

            _output.WriteLine($"{response.ErrorCode} {response.ErrorClass} {response.ErrorMessage}");

            Assert.True(!response.HasError || response.ErrorCode == -1);
        }

        private async Task<EXPEO0> Send(MrpApi mrpApi)
        {
            var response = await mrpApi.EXPEO0(new List<NameValueItem>() {
                new NameValueItem() { Name = "cisloSkladu", Value = "1" },
                new NameValueItem() { Name = "SKKAR.CISLO", Value = "1..10" },
                new NameValueItem() { Name = "stavy", Value = "T" }
            });

            if (response.HasError)
            {
                _output.WriteLine($"{response.ErrorCode} {response.ErrorClass} {response.ErrorMessage}");
            }

            return response;
        }
    }
}
