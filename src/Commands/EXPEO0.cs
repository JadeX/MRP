namespace MRP.Commands;

using System.Collections.Generic;
using MRP.Xml.Datasets;

public class EXPEO0 : Response
{
    public List<MrpCategory> Categories { get; set; } = [];

    public List<MrpProduct> Products { get; set; } = [];

    public List<MrpReplacement> Replacements { get; set; } = [];
}
