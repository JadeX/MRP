namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using JadeX.MRP.Xml.Datasets;

public class EXPEO0 : Response
{
    public List<MrpCategory> Categories { get; set; } = [];

    public List<MrpProduct> Products { get; set; } = [];

    public List<MrpReplacement> Replacements { get; set; } = [];
}
