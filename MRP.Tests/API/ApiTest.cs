namespace MRP.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using MRP.Commands;
    using Xunit;

    public class ApiTest
    {
        public ApiTest()
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets<ApiTest>().AddEnvironmentVariables().Build();

            this.MrpApi = new MrpApi(new Uri(configuration["ApiUrl"]).AbsoluteUri)
                .WithEncryption(configuration["SecretKey"])
                .WithCompression()
                .WithTimeout(TimeSpan.FromSeconds(10));
        }

        public MrpApi MrpApi { get; private set; }

        public static Task CheckResponseForErrors(IResponse response)
        {
            if (!response.HasError)
            {
                return Task.CompletedTask;
            }

            Skip.If(response.ErrorMessage.Contains("nemá povoleno obsloužení"), response.ErrorMessage);

            throw new Exception(response.ErrorMessage);
        }
    }
}
