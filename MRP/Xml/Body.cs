using System.Xml.Serialization;

namespace MRP.Xml
{
    public class Body
    {
        [XmlElement("mrpRequest")]
        public MrpRequest MrpRequest { get; set; }

        [XmlElement("mrpResponse")]
        public MrpResponse MrpResponse { get; set; }
    }
}
