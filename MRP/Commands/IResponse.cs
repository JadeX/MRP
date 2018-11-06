using System.Xml.Linq;

namespace MRP.Commands
{
    public interface IResponse
    {
        bool HasError { get; }

        int ErrorCode { get; set; }

        string ErrorClass { get; set; }

        string ErrorMessage { get; set; }

        XDocument Data { get; set; }
    }
}
