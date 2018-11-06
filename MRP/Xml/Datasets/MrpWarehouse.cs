using System.Xml.Serialization;

namespace MRP.Xml.Datasets
{
    [XmlRoot("fields")]
    public class MrpWarehouse
    {
        [XmlElement("cisloskl")]
        public int CisloSkl { get; set; }

        [XmlElement("nazevskl")]
        public string NazevSkl { get; set; }
    }
}
