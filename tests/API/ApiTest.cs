namespace JadeX.MRP.Tests;

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using JadeX.MRP.Commands;
using Xunit;

public class ApiTest
{
    public ApiTest()
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets<ApiTest>().AddEnvironmentVariables().Build();

        this.MrpApi = new MrpApi(new Uri(configuration["ApiUrl"] ?? throw new InvalidOperationException()).AbsoluteUri)
            .WithEncryption(configuration["SecretKey"] ?? throw new InvalidOperationException())
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

        var commandDisabled = response.ErrorMessage?.Contains("nemá povoleno obsloužení");

        Skip.If(commandDisabled.HasValue && commandDisabled.Value, response.ErrorMessage);

        throw new InvalidOperationException(response.ErrorMessage);
    }
}
