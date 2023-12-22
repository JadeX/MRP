namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using System.Xml.Serialization;

public class IMPEO0 : Response
{
    public List<IMPEO0OrderIds>? OrderIds { get; set; }
}

[XmlRoot("fields")]
public class IMPEO0OrderIds
{
    [XmlElement("puvodniCislo")]
    public string? PuvodniCislo { get; set; }

    [XmlElement("cislo")]
    public string? Cislo { get; set; }
}
