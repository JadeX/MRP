namespace MRP.Tests.API
{
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    public class EXPEO0 : ApiTest
    {
        [SkippableFact]
        public async Task GetCategories()
        {
            var response = await this.MrpApi.EXPEO0(x => x
                .Filter("cisloSkladu", "1")
                .Filter("SKKAR.CISLO", "1..100")
            );

            await CheckResponseForErrors(response);

            _ = response.Categories.ShouldNotBeNull();
        }

        [SkippableFact]
        public async Task GetProducts()
        {
            var response = await this.MrpApi.EXPEO0(x => x
                .Filter("cisloSkladu", "1")
                .Filter("SKKAR.CISLO", "1..100")
            );

            await CheckResponseForErrors(response);

            _ = response.Products.ShouldNotBeNull();
        }

        [SkippableFact]
        public async Task GetReplacements()
        {
            var response = await this.MrpApi.EXPEO0(x => x
                .Filter("cisloSkladu", "1")
                .Filter("SKKAR.CISLO", "1..100")
            );

            await CheckResponseForErrors(response);

            // _ = response.Replacements.ShouldNotBeNull();
        }
    }
}
