namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using JadeX.MRP.Xml.Datasets;

public class EXPEO1 : Response
{
    public List<MrpCategory>? Categories { get; set; }

    public List<MrpProduct>? Products { get; set; }

    public List<MrpReplacement>? Replacements { get; set; }

    public List<MrpStock>? Stocks { get; set; }

    public List<MrpWarehouse>? Warehouses { get; set; }
}
