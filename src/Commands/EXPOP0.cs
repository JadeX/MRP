namespace JadeX.MRP.Commands;

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

public class EXPOP0 : Response
{
    public EXPOP0Order? Order { get; set; }
}

[XmlRoot("objednavka")]
public class EXPOP0Order
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
    public List<EXPOP0OrderItem>? Polozky { get; set; }
}

public class EXPOP0OrderItem
{
    [XmlAttribute("stav")]
    public int Stav { get; set; }

    [XmlAttribute("vybavitMJ")]
    public int VybavitMJ { get; set; }

    [XmlAttribute("text")]
    public string? Text { get; set; }

    [XmlAttribute("cisloKarty")]
    public int CisloKarty { get; set; }

    [XmlAttribute("eanKarty")]
    public string? EanKarty { get; set; }

    [XmlAttribute("kodKarty")]
    public string? KodKarty { get; set; }
}
