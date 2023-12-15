namespace JadeX.MRP.Tests.API;

using System.Threading.Tasks;
using Shouldly;
using Xunit;

public class EXPOP0 : ApiTest
{
    [SkippableFact]
    public async Task GetOrder()
    {
        var response = await this.MrpApi.EXPOP0(x => x
            .Filter("OBJPR.CISLO", "*")
            .Filter("polozky", "T")
            .Filter("typDokladu", "X")
        );

        await CheckResponseForErrors(response);

        _ = response.Order.ShouldNotBeNull();
        response.Order.Polozky.Count.ShouldBeGreaterThanOrEqualTo(1);
    }
}
