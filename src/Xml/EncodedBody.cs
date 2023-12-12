namespace JadeX.MRP.Xml;

using System.Xml;
using System.Xml.Serialization;

public class EncodedBody
{
    [XmlElement("authCode")]
    public string AuthCode { get; set; }

    [XmlAttribute("authentication")]
    public string Authentication { get; set; }

    [XmlIgnore]
    public string EncodedData { get; set; }

    [XmlElement("encodedData")]
    public XmlCDataSection EncodedDataCData
    {
        get => new XmlDocument().CreateCDataSection(this.EncodedData);
        set => this.EncodedData = value.Value;
    }

    [XmlIgnore]
    public string EncodingParams { get; set; }

    [XmlElement("encodingParams")]
    public XmlCDataSection EncodingParamsCData
    {
        get => new XmlDocument().CreateCDataSection(this.EncodingParams);
        set => this.EncodingParams = value.Value;
    }
}
