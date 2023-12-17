namespace JadeX.MRP.Xml.Datasets;

using System.Xml.Serialization;

[XmlRoot("fields")]
public class MrpOrderId
{
    [XmlElement("puvodniCislo")]
    public string? PuvodniCislo { get; set; }

    [XmlElement("cislo")]
    public string? Cislo { get; set; }
}
