namespace JadeX.MRP.Commands;

using System.Collections.Generic;
using MRP.Xml;

public class RequestOrdersOptions
{
    public List<NameValueItem> ParamItems { get; private set; } = [];
    public List<Order> OrderItems { get; private set; } = [];

    public RequestOrdersOptions Param(string name, string value)
    {
        this.ParamItems.Add(new NameValueItem() { Name = name, Value = value });

        return this;
    }

    public RequestOrdersOptions Order(Order order)
    {
        this.OrderItems.Add(order);

        return this;
    }
}
