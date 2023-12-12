namespace JadeX.MRP.Tests.API;

using System.Threading.Tasks;
using Shouldly;
using Xunit;

public class CENEO0 : ApiTest
{
    [SkippableFact]
    public async Task GetPrices()
    {
        var response = await this.MrpApi.CENEO0(x => x
            .Filter("cisloSkladu", "1")
            .Filter("SKKAR.CISLO", "1..10")
            .Filter("cenovaSkupina", "1")
        );

        await CheckResponseForErrors(response);

        _ = response.Prices.ShouldNotBeNull();
    }
}
