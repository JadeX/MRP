using System.Xml;
using System.Xml.Serialization;

namespace MRP.Xml
{
    [XmlRoot("mrpEnvelope")]
    public class MrpEnvelope
    {
        [XmlElement("encodedBody")]
        public EncodedBody EncodedBody { get; set; }

        [XmlElement("body")]
        public Body Body { get; set; }
    }
}
