using System.Xml.Linq;

namespace MRP.Commands
{
    public class Response : IResponse
    {
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public int ErrorCode { get; set; }

        public string ErrorClass { get; set; }

        public string ErrorMessage { get; set; }

        public XDocument Data { get; set; }
    }
}
