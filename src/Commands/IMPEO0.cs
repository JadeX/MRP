namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using JadeX.MRP.Xml.Datasets;

public class IMPEO0 : Response
{
    public List<MrpOrderId>? OrderIds { get; set; }
}
