namespace JadeX.MRP.Commands;

using System.Xml.Linq;

public class Response : IResponse
{
    public XDocument? Data { get; set; }
    public string? ErrorClass { get; set; }
    public int ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public bool HasError => !string.IsNullOrEmpty(this.ErrorMessage);
}
