namespace MRP.Xml;

using System.Xml.Serialization;

public class Body
{
    [XmlElement("mrpRequest")]
    public MrpRequest MrpRequest { get; set; }

    [XmlElement("mrpResponse")]
    public MrpResponse MrpResponse { get; set; }
}
