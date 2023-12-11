namespace MRP.Xml.Datasets;

using System.Xml.Serialization;

[XmlRoot("fields")]
public class MrpReplacement
{
    [XmlElement("cislo")]
    public float Cislo { get; set; }

    [XmlElement("cislo_z")]
    public float CisloZ { get; set; }

    [XmlElement("kod")]
    public string Kod { get; set; }

    [XmlElement("kod_z")]
    public string KodZ { get; set; }

    [XmlElement("kod1")]
    public string Kod1 { get; set; }

    [XmlElement("kod1_z")]
    public string Kod1Z { get; set; }
}
