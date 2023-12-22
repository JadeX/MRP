namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using System.Xml.Serialization;

public class ADREO0 : Response
{
    public List<ADREO0Address>? Addresses { get; set; }
}

[XmlRoot("fields")]
public class ADREO0Address
{
    [XmlElement("ico")]
    public string? ICO { get; set; }

    [XmlElement("dic")]
    public string? DIC { get; set; }

    [XmlElement("ic_dph")]
    public string? ICDPH { get; set; }

    [XmlElement("id")]
    public string? Id { get; set; }

    [XmlElement("firma")]
    public string? Firma { get; set; }

    [XmlElement("meno")]
    public string? Meno { get; set; }

    [XmlElement("ulica")]
    public string? Ulica { get; set; }

    [XmlElement("mesto")]
    public string? Mesto { get; set; }

    [XmlElement("psc")]
    public string? PSC { get; set; }

    [XmlElement("kodstat")]
    public string? KodStat { get; set; }

    [XmlElement("telefon")]
    public string? Telefon { get; set; }

    [XmlElement("telefon1")]
    public string? Telefon1 { get; set; }

    [XmlElement("telefon2")]
    public string? Telefon2 { get; set; }

    [XmlElement("fax")]
    public string? Fax { get; set; }

    [XmlElement("email")]
    public string? Email { get; set; }

    [XmlElement("censkup")]
    public int CenSkup { get; set; }

    [XmlElement("stat")]
    public string? Stat { get; set; }
}
