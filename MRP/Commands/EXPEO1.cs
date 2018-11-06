using System.Collections.Generic;
using MRP.Xml.Datasets;

namespace MRP.Commands
{
    public class EXPEO1 : Response
    {
        public List<MrpCategory> Categories { get; set; } = new List<MrpCategory>();

        public List<MrpProduct> Products { get; set; } = new List<MrpProduct>();

        public List<MrpReplacement> Replacements { get; set; } = new List<MrpReplacement>();

        public List<MrpWarehouse> Warehouses { get; set; } = new List<MrpWarehouse>();

        public List<MrpStock> Stocks { get; set; } = new List<MrpStock>();
    }
}
