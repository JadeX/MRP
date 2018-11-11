using System.Xml.Serialization;

namespace MRP.Xml.Datasets
{
    [XmlRoot("fields")]
    public class MrpPrice
    {
        [XmlElement("cislo")]
        public float Cislo { get; set; }

        [XmlElement("cisloceny")]
        public int CisloCeny { get; set; }

        [XmlElement("mena")]
        public string Mena { get; set; }

        [XmlElement("cenamj")]
        public float CenaMJ { get; set; }

        [XmlElement("sleva_p")]
        public float SlevaP { get; set; }

        [XmlElement("slevamj")]
        public float SlevaMJ { get; set; }
    }
}
