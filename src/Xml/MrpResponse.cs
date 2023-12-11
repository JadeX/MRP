namespace MRP.Xml
{
    using System.Xml;
    using System.Xml.Serialization;

    public class MrpError
    {
        [XmlAttribute("errorClass")]
        public string ErrorClass { get; set; }

        [XmlAttribute("errorCode")]
        public string ErrorCode { get; set; }

        [XmlElement("errorMessage")]
        public string ErrorMessage { get; set; }
    }

    [XmlRoot("mrpResponse")]
    public class MrpResponse
    {
        [XmlElement("data")]
        public XmlNode Data { get; set; }

        [XmlElement("status")]
        public Status Status { get; set; }
    }

    public class Status
    {
        [XmlElement("error")]
        public MrpError Error { get; set; }

        [XmlElement("request")]
        public Request Request { get; set; }
    }
}
