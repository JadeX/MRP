namespace MRP.Commands
{
    using System.Collections.Generic;
    using MRP.Xml;

    public class RequestFilterOptions
    {
        public List<NameValueItem> FilterItems { get; private set; } = new List<NameValueItem>();

        public RequestFilterOptions Filter(string name, string value)
        {
            this.FilterItems.Add(new NameValueItem() { Name = name, Value = value });

            return this;
        }
    }
}
