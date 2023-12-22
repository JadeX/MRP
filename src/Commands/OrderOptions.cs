namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using System.Xml.Serialization;
using MRP.Xml;

public class OrderOptions
{
    public List<NameValueItem> ParamItems { get; private set; } = [];
    public List<IMPEO0Order> OrderItems { get; private set; } = [];

    public OrderOptions Param(string name, string value)
    {
        this.ParamItems.Add(new NameValueItem() { Name = name, Value = value });

        return this;
    }

    public OrderOptions Order(IMPEO0Order order)
    {
        this.OrderItems.Add(order);

        return this;
    }

    public class IMPEO0Order
    {
        [XmlAttribute("stredisko")]
        public string? Stredisko { get; set; }

        [XmlAttribute("cisloZakazky")]
        public string? CisloZakazky { get; set; }

        [XmlAttribute("formaUhrady")]
        public string? FormaUhrady { get; set; }

        [XmlAttribute("zpusobDopravy")]
        public string? ZpusobDopravy { get; set; }

        [XmlAttribute("variabilniSymbol")]
        public int VariabilniSymbol { get; set; }

        [XmlAttribute("puvodniCislo")]
        public string? PuvodniCislo { get; set; }

        [XmlAttribute("datum")]
        public string? Datum { get; set; }

        [XmlAttribute("datumDodani")]
        public string? DatumDodani { get; set; }

        [XmlAttribute("cenySDPH")]
        public string? CenySDPH { get; set; }

        [XmlAttribute("fixniCena")]
        public string? FixniCena { get; set; }

        [XmlAttribute("typDPH")]
        public string? TypDPH { get; set; }

        [XmlElement("adresa")]
        public IMPEO0Address? Adresa { get; set; }

        [XmlArray("polozky")]
        [XmlArrayItem("polozka")]
        public List<IMPEO0OrderItem>? Polozky { get; set; }
    }

    public class IMPEO0OrderItem
    {
        [XmlAttribute("cisloKarty")]
        public int CisloKarty { get; set; }

        [XmlAttribute("eanKarty")]
        public string? EanKarty { get; set; }

        [XmlAttribute("kodKarty")]
        public string? KodKarty { get; set; }

        [XmlAttribute("text")]
        public string? Text { get; set; }

        [XmlAttribute("pocetMJ")]
        public int PocetMJ { get; set; }

        [XmlAttribute("cenaMJ")]
        public int CenaMJ { get; set; }

        [XmlAttribute("slevaMJ")]
        public int SlevaMJ { get; set; }

        [XmlAttribute("sleva")]
        public int Sleva { get; set; }

        [XmlAttribute("sazbaDPH")]
        public int SazbaDPH { get; set; }

        [XmlAttribute("typPolozky")]
        public string? TypPolozky { get; set; }

        [XmlAttribute("fixniCena")]
        public string? FixniCena { get; set; }

        [XmlAttribute("poznamkaPolozky")]
        public string? PoznamkaPolozky { get; set; }
    }

    public class IMPEO0Address
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("ulice")]
        public string? Ulice { get; set; }

        [XmlAttribute("mesto")]
        public string? Mesto { get; set; }

        [XmlAttribute("psc")]
        public string? PSC { get; set; }

        [XmlAttribute("kodStatu")]
        public string? KodStatu { get; set; }

        [XmlAttribute("fyzickaOsoba")]
        public string? FyzickaOsoba { get; set; }
    }

    public class IMPEO0Params
    {
        [XmlElement("paramvalue")]
        public List<NameValueItem>? Items { get; set; }
    }
}
