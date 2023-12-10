namespace MRP.Xml.Datasets
{
    using System.Xml.Serialization;

    [XmlRoot("fields")]
    public class MrpProduct
    {
        [XmlElement("baleni")]
        public float Baleni { get; set; }

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

        [XmlElement("ciskat")]
        public int Ciskat { get; set; }

        [XmlElement("cislo")]
        public float Cislo { get; set; }

        [XmlElement("hmotnost")]
        public float Hmotnost { get; set; }

        [XmlElement("jednotka")]
        public string Jednotka { get; set; }

        [XmlElement("kod")]
        public string Kod { get; set; }

        [XmlElement("kod1")]
        public string Kod1 { get; set; }

        [XmlElement("malobr")]
        public string MalObr { get; set; }

        [XmlElement("malobraz")]
        public string MalObraz { get; set; }

        [XmlElement("malpopis")]
        public string MalPopis { get; set; }

        [XmlElement("mena")]
        public string Mena { get; set; }

        [XmlElement("nazev")]
        public string Nazev { get; set; }

        [XmlElement("nazev2")]
        public string Nazev2 { get; set; }

        [XmlElement("pocetmj")]
        public float PocetMJ { get; set; }

        [XmlElement("pocobjmj")]
        public float PocObMJ { get; set; }

        [XmlElement("pocrezmj")]
        public float PocRezMJ { get; set; }

        [XmlElement("pozice")]
        public string Pozice { get; set; }

        [XmlElement("poznamka")]
        public string Poznamka { get; set; }

        [XmlElement("sazbadph")]
        public float SazbaDPH { get; set; }

        [XmlElement("skupina")]
        public string Skupina { get; set; }

        [XmlElement("skupnazev")]
        public string SkupNazev { get; set; }

        [XmlElement("usrfld1")]
        public string UsrFld1 { get; set; }

        [XmlElement("usrfld2")]
        public string UsrFld2 { get; set; }

        [XmlElement("usrfld3")]
        public string UsrFld3 { get; set; }

        [XmlElement("usrfld4")]
        public string UsrFld4 { get; set; }

        [XmlElement("usrfld5")]
        public string UsrFld5 { get; set; }

        [XmlElement("velobr")]
        public string VelObr { get; set; }

        [XmlElement("velobraz")]
        public string VelObraz { get; set; }

        [XmlElement("velpopis")]
        public string VelPopis { get; set; }
    }
}
