namespace MRP.Xml.Datasets
{
    using System.Xml.Serialization;

    [XmlRoot("fields")]
    public class MrpCategory
    {
        [XmlElement("ciskat")]
        public int Ciskat { get; set; }

        [XmlElement("idr")]
        public int Idr { get; set; }

        [XmlElement("popis")]
        public string Popis { get; set; }

        [XmlElement("poradi")]
        public int Poradi { get; set; }

        [XmlElement("uciskat")]
        public int? Uciskat { get; set; }
    }
}
