using System.Xml.Serialization;

namespace MRP.Xml.Datasets
{
    [XmlRoot("fields")]
    public class MrpCategory
    {
        [XmlElement("idr")]
        public int Idr { get; set; }

        [XmlElement("ciskat")]
        public int Ciskat { get; set; }

        [XmlElement("uciskat")]
        public int? Uciskat { get; set; }

        [XmlElement("popis")]
        public string Popis { get; set; }

        [XmlElement("poradi")]
        public int Poradi { get; set; }
    }
}
