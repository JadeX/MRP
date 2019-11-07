namespace MRP.Xml.Datasets
{
    using System.Xml.Serialization;

    [XmlRoot("fields")]
    public class MrpPrice
    {
        [XmlElement("cenamj")]
        public float CenaMJ { get; set; }

        [XmlElement("cislo")]
        public float Cislo { get; set; }

        [XmlElement("cisloceny")]
        public int CisloCeny { get; set; }

        [XmlElement("mena")]
        public string Mena { get; set; }

        [XmlElement("slevamj")]
        public float SlevaMJ { get; set; }

        [XmlElement("sleva_p")]
        public float SlevaP { get; set; }
    }
}
