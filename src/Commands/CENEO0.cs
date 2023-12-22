namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using System.Xml.Serialization;

public class CENEO0 : Response
{
    public List<CENEO0Price>? Prices { get; set; }
}

[XmlRoot("fields")]
public class CENEO0Price
{
    [XmlElement("cenamj")]
    public float CenaMJ { get; set; }

    [XmlElement("cislo")]
    public float Cislo { get; set; }

    [XmlElement("cisloceny")]
    public int CisloCeny { get; set; }

    [XmlElement("mena")]
    public string? Mena { get; set; }

    [XmlElement("slevamj")]
    public float SlevaMJ { get; set; }

    [XmlElement("sleva_p")]
    public float SlevaP { get; set; }
}
