namespace JadeX.MRP.Xml.Datasets;

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("objednavka")]
public class MrpOrder
{
    [XmlAttribute("cisloObj")]
    public string? CisloObj { get; set; }

    [XmlAttribute("puvodniCislo")]
    public string? PuvodniCislo { get; set; }

    [XmlAttribute("datum")]
    public DateTime Datum { get; set; }

    [XmlAttribute("stav")]
    public int Stav { get; set; }

    [XmlAttribute("usrLock")]
    public string? UsrLock { get; set; }

    [XmlAttribute("ico")]
    public string? Ico { get; set; }

    [XmlAttribute("nabidka")]
    public string? Nabidka { get; set; }

    [XmlArray("polozky")]
    [XmlArrayItem("polozka")]
    public List<MrpOrderItem>? Polozky { get; set; }
}

public class MrpOrderItem
{
    [XmlAttribute("stav")]
    public int Stav { get; set; }

    [XmlAttribute("vybavitMJ")]
    public int VybavitMJ { get; set; }

    [XmlAttribute("text")]
    public string? Text { get; set; }

    [XmlAttribute("cisloKarty")]
    public int CisloKarty { get; set; }

    [XmlAttribute(attributeName: "eanKarty")]
    public string? EanKarty { get; set; }

    [XmlAttribute("kodKarty")]
    public string? KodKarty { get; set; }
}
