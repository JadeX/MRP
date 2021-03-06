namespace MRP.Commands
{
    using System.Collections.Generic;
    using MRP.Xml.Datasets;

    public class EXPEO0 : Response
    {
        public List<MrpCategory> Categories { get; set; } = new List<MrpCategory>();

        public List<MrpProduct> Products { get; set; } = new List<MrpProduct>();

        public List<MrpReplacement> Replacements { get; set; } = new List<MrpReplacement>();
    }
}
