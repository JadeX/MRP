namespace MRP.Xml.Datasets;

using System.Xml.Serialization;

[XmlRoot("fields")]
public class MrpWarehouse
{
    [XmlElement("cisloskl")]
    public int CisloSkl { get; set; }

    [XmlElement("nazevskl")]
    public string NazevSkl { get; set; }
}
