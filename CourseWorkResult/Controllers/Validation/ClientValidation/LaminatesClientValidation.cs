using System.Linq;
using System.Net;

namespace CourseWorkResult.Controllers.Validation.ClientValidation
{
    class LaminatesClientValidation : ILaminatesClientValidation
    {
        public bool CheckIpAddress(string ipAddress) =>
            ipAddress != null && ipAddress.Count(c => c == '.') == 3 && 
            IPAddress.TryParse(ipAddress, out IPAddress address);

        public bool CheckPort(int port) => port > 0;
    }
}
