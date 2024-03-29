namespace JadeX.MRP.Xml;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using JadeX.MRP.Commands;
using static JadeX.MRP.Commands.OrderOptions;

public class Data
{
    [XmlElement("filter")]
    public Filter? Filter { get; set; }

    [XmlElement("objednavka")]
    public List<IMPEO0Order>? Orders { get; set; }

    [XmlElement("params")]
    public IMPEO0Params? Params { get; set; }
}

public class Filter
{
    [XmlElement("fltvalue")]
    public List<NameValueItem>? Items { get; set; }
}

[XmlRoot("mrpRequest")]
public class MrpRequest
{
    [XmlElement("data")]
    public Data? Data { get; set; }

    [XmlElement("request")]
    public Request? Request { get; set; }
}

public class NameValueItem
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlText]
    public string? Value { get; set; }
}

public class Request
{
    [XmlAttribute("command")]
    public MrpCommands Command { get; set; }

    [XmlAttribute("requestId")]
    public string RequestId { get; set; } = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
}
