namespace JadeX.MRP.Tests.API;

using System;
using System.Globalization;
using System.Threading.Tasks;
using JadeX.MRP.Xml;
using Shouldly;
// using Shouldly;
using Xunit;

public class IMPEO0 : ApiTest
{
    [SkippableFact]
    public async Task NewOrders()
    {
        var response = await this.MrpApi.IMPEO0(x => x
            .Order(new Order()
            {
                PuvodniCislo = "123456",
                Datum = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Adresa = new Address() { Id = "123456", Ulice = "Vonaskova 123", Mesto = "Zlín", PSC = "76001" },
                Polozky =
                [
                    new MrpOrderItem { CisloKarty = 6425, PocetMJ = 5, CenaMJ = 150 }
                ]
            })
        );

        await CheckResponseForErrors(response);
        response.OrderIds.ShouldNotBeEmpty();
    }
}
