namespace MRP.Xml
{
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("mrpEnvelope")]
    public class MrpEnvelope
    {
        [XmlElement("body")]
        public Body Body { get; set; }

        [XmlElement("encodedBody")]
        public EncodedBody EncodedBody { get; set; }
    }
}
