namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using System.Xml.Serialization;

public class EXPEO1 : EXPEO0
{
    public List<EXPEO1Stock>? Stocks { get; set; }
    public List<EXPEO1Warehouse>? Warehouses { get; set; }
}

[XmlRoot("fields")]
public class EXPEO1Stock
{
    [XmlElement("cena")]
    public float Cena { get; set; }

    [XmlElement("cena1")]
    public float Cena1 { get; set; }

    [XmlElement("cena1sdph")]
    public float Cena1SDPH { get; set; }

    [XmlElement("cena2")]
    public float Cena2 { get; set; }

    [XmlElement("cena2sdph")]
    public float Cena2SDPH { get; set; }

    [XmlElement("cena3")]
    public float Cena3 { get; set; }

    [XmlElement("cena3sdph")]
    public float Cena3SDPH { get; set; }

    [XmlElement("cena4")]
    public float Cena4 { get; set; }

    [XmlElement("cena4sdph")]
    public float Cena4SDPH { get; set; }

    [XmlElement("cena5")]
    public float Cena5 { get; set; }

    [XmlElement("cena5sdph")]
    public float Cena5SDPH { get; set; }

    [XmlElement("cenasdph")]
    public float CenaSDPH { get; set; }

    [XmlElement("cisloskl")]
    public int CisloSkl { get; set; }

    [XmlElement("cislokar")]
    public float Kod { get; set; }

    [XmlElement("mena")]
    public string? Mena { get; set; }

    [XmlElement("pocetmj")]
    public float PocetMJ { get; set; }

    [XmlElement("pocobjmj")]
    public float PocObjMJ { get; set; }

    [XmlElement("pocrezmj")]
    public float PocRezMJ { get; set; }

    [XmlElement("pozice")]
    public string? Pozice { get; set; }
}

[XmlRoot("fields")]
public class EXPEO1Warehouse
{
    [XmlElement("cisloskl")]
    public int CisloSkl { get; set; }

    [XmlElement("nazevskl")]
    public string? NazevSkl { get; set; }
}
