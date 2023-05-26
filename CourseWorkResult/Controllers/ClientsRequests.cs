using CourseWorkResult.Json;
using CourseWorkResult.Models;
using CourseWorkResult.Validation;
using System.Collections.Generic;

namespace CourseWorkResult.Controllers
{
    class ClientsRequests
    {
        private static Client _client;

        public bool Connect() => _client.Connect();

        public ClientsRequests(string ipAddress, int port) => _client = new Client(ipAddress, port);

        public static string AddLaminate(Laminate laminate) => Client.TransmitMessage("AddLaminate", laminate);

        public static string RemoveLaminate(string laminateName) => Client.TransmitMessage("RemoveLaminate", laminateName);

        public static string UpdateLaminate(string laminateName, Laminate laminate) => Client.TransmitMessage("UpdateLaminate", (laminateName, laminate));

        public static Laminate GetLaminateByName(string laminateName)
        {
            string jsonLaminate = Client.TransmitMessage("GetLaminateByName", laminateName);
            return JsonOperations.Deserealize<Laminate>(jsonLaminate);
        }

        public static IEnumerable<Laminate> GetAllLaminates()
        {
            string jsonLaminates = Client.TransmitMessage("GetAllLaminates", new object());
            return JsonOperations.Deserealize<IEnumerable<Laminate>>(jsonLaminates);
        }

        public static CalculationResult Calculation(CalculationData calculationData)
        {
            string jsonCalculationResult = Client.TransmitMessage("Calculation", calculationData);
            return JsonOperations.Deserealize<CalculationResult>(jsonCalculationResult);
        }

        public static void EndConnection() => Client.TransmitMessage("End connection", new object());
    }
}
