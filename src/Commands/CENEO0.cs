namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using JadeX.MRP.Xml.Datasets;

public class CENEO0 : Response
{
    public List<MrpPrice> Prices { get; set; } = [];
}
