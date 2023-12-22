namespace JadeX.MRP.Tests.API;

using System.Threading.Tasks;
using Shouldly;
using Xunit;

public class ADREO0 : ApiTest
{
    [SkippableFact]
    public async Task GetAddress()
    {
        var response = await this.MrpApi.ADREO0(x => x
            .Filter("ADRES.PSC", "7*")
        );

        await CheckResponseForErrors(response);

        response.Addresses.ShouldNotBeEmpty();
    }
}
