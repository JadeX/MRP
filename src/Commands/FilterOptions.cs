namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using MRP.Xml;

public class FilterOptions
{
    public List<NameValueItem> FilterItems { get; private set; } = [];

    public FilterOptions Filter(string name, string value)
    {
        this.FilterItems.Add(new NameValueItem() { Name = name, Value = value });
        return this;
    }
}
