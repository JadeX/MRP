namespace JadeX.MRP.Tests.API;

using System.Threading.Tasks;
using Shouldly;
using Xunit;

public class EXPEO1 : ApiTest
{
    [SkippableFact]
    public async Task GetCategories()
    {
        var response = await this.MrpApi.EXPEO1(x => x
            .Filter("cisloSkladu", "1")
            .Filter("SKKAR.CISLO", "1..100")
        );

        await CheckResponseForErrors(response);

        _ = response.Categories.ShouldNotBeNull();
    }

    [SkippableFact]
    public async Task GetProducts()
    {
        var response = await this.MrpApi.EXPEO1(x => x
            .Filter("cisloSkladu", "1")
            .Filter("SKKAR.CISLO", "1..100")
        );

        await CheckResponseForErrors(response);

        _ = response.Products.ShouldNotBeNull();
    }

    [SkippableFact]
    public async Task GetReplacements()
    {
        var response = await this.MrpApi.EXPEO1(x => x
            .Filter("cisloSkladu", "1")
            .Filter("SKKAR.CISLO", "1..100")
        );

        await CheckResponseForErrors(response);

        // _ = response.Replacements.ShouldNotBeNull();
    }

    [SkippableFact]
    public async Task GetStocks()
    {
        var response = await this.MrpApi.EXPEO1(x => x
            .Filter("cisloSkladu", "1")
            .Filter("SKKAR.CISLO", "1..100")
        );

        await CheckResponseForErrors(response);

        _ = response.Stocks.ShouldNotBeNull();
    }

    [SkippableFact]
    public async Task GetWarehouses()
    {
        var response = await this.MrpApi.EXPEO1(x => x
            .Filter("cisloSkladu", "1")
            .Filter("SKKAR.CISLO", "1..100")
        );

        await CheckResponseForErrors(response);

        _ = response.Warehouses.ShouldNotBeNull();
    }
}
