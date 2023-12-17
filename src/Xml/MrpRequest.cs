namespace JadeX.MRP.Xml;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using JadeX.MRP.Commands;

public class Data
{
    [XmlElement("filter")]
    public Filter? Filter { get; set; }

    [XmlElement("objednavka")]
    public List<Order>? Orders { get; set; }

    [XmlElement("params")]
    public Params? Params { get; set; }
}

public class Filter
{
    [XmlElement("fltvalue")]
    public List<NameValueItem>? Items { get; set; }
}

[XmlRoot("mrpRequest")]
public class MrpRequest
{
    [XmlElement("data")]
    public Data? Data { get; set; }

    [XmlElement("request")]
    public Request? Request { get; set; }
}

public class NameValueItem
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlText]
    public string? Value { get; set; }
}

public class Order
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
    public Address? Adresa { get; set; }

    [XmlArray("polozky")]
    [XmlArrayItem("polozka")]
    public List<MrpOrderItem>? Polozky { get; set; }
}

public class MrpOrderItem
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

public class Address
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

public class Params
{
    [XmlElement("paramvalue")]
    public List<NameValueItem>? Items { get; set; }
}

public class Request
{
    [XmlAttribute("command")]
    public MrpCommands Command { get; set; }

    [XmlAttribute("requestId")]
    public string RequestId { get; set; } = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
}
