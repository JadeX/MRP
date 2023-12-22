namespace JadeX.MRP.Tests.API;

using System;
using System.Globalization;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using static JadeX.MRP.Commands.OrderOptions;

public class IMPEO0 : ApiTest
{
    [SkippableFact]
    public async Task NewOrders()
    {
        var response = await this.MrpApi.IMPEO0(x => x
            .Order(new IMPEO0Order()
            {
                PuvodniCislo = "123456",
                Datum = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Adresa = new IMPEO0Address() { Id = "123456", Ulice = "Vonaskova 123", Mesto = "Zlín", PSC = "76001" },
                Polozky =
                [
                    new IMPEO0OrderItem { CisloKarty = 6425, PocetMJ = 5, CenaMJ = 150 }
                ]
            })
        );

        await CheckResponseForErrors(response);
        response.OrderIds.ShouldNotBeEmpty();
    }
}
