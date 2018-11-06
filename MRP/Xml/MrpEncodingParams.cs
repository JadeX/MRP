using System.Xml.Serialization;

namespace MRP.Xml
{
    [XmlRoot("mrpEncodingParams")]
    public class MrpEncodingParams
    {
        [XmlAttribute("compression")]
        public string Compression { get; set; }

        [XmlAttribute("encryption")]
        public string Encryption { get; set; }

        [XmlElement("varKey")]
        public string VarKey { get; set; }
    }
}
