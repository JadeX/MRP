using System.Xml;
using System.Xml.Serialization;

namespace MRP.Xml
{
    [XmlRoot("mrpResponse")]
    public class MrpResponse
    {
        [XmlElement("status")]
        public Status Status { get; set; }

        [XmlElement("data")]
        public XmlNode Data { get; set; }
    }

    public class Status
    {
        [XmlElement("request")]
        public Request Request { get; set; }

        [XmlElement("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [XmlAttribute("errorCode")]
        public string ErrorCode { get; set; }

        [XmlAttribute("errorClass")]
        public string ErrorClass { get; set; }

        [XmlElement("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
