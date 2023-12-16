namespace JadeX.MRP.Commands;

using System.Xml.Linq;

public interface IResponse
{
    XDocument? Data { get; set; }

    string? ErrorClass { get; set; }

    int ErrorCode { get; set; }

    string? ErrorMessage { get; set; }

    bool HasError { get; }
}
