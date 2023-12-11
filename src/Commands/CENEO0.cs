namespace MRP.Commands;

using System.Collections.Generic;
using MRP.Xml.Datasets;

public class CENEO0 : Response
{
    public List<MrpPrice> Prices { get; set; } = [];
}
