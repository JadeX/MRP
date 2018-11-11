using System.Collections.Generic;
using MRP.Xml.Datasets;

namespace MRP.Commands
{
    public class CENEO0 : Response
    {
        public List<MrpPrice> Prices { get; set; } = new List<MrpPrice>();
    }
}
