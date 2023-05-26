using CourseWorkResult.Controllers.Validation.ClientValidation;
using CourseWorkResult.Json;
using CourseWorkResult.Models;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CourseWorkResult.Validation
{
    class Client
    {
        private static TcpClient _client;
        private static string _ipAddress;
        private static int _port;

        public Client(string ipAddress, int port)
        {
            LaminatesServerValidation laminatesServerValidation = new LaminatesServerValidation();
            Console.ForegroundColor = ConsoleColor.Red;

            if (!laminatesServerValidation.CheckIpAddress(ipAddress))
            {
                Console.WriteLine("Произошла ошибка! Неккоректный IPv4-адрес!");
                ipAddress = "127.0.0.1";
                Console.WriteLine($"Текущий ip-адрес: {ipAddress}");
            }

            if (!laminatesServerValidation.CheckPort(port))
            {
                Console.WriteLine("Произошла ошибка! Неккоректный порт!");
                port = 80;
                Console.WriteLine($"Текущий порт: {port}");
            }

            _client = new TcpClient();
            _ipAddress = ipAddress;
            _port = port;
        }

        public bool Connect()
        {
            try
            {
                _client.Connect(_ipAddress, _port);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static void SendMessage(string message)
        {
            if (!_client.Connected)
            {
                return;
            }

            var stream = _client.GetStream();
            var buffer = Encoding.UTF8.GetBytes(message);

            try
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (IOException)
            {
                MessageBox.Show("Внезапное отключение от сервера!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private static string ReceiveMessage()
        {
            if (!_client.Connected)
            {
                return string.Empty;
            }

            var stream = _client.GetStream();
            var buffer = new byte[160000];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        public static string TransmitMessage<T>(string operationName, T item)
        {
            string jsonData = JsonOperations.Serilalize(item);
            Request request = new Request(operationName, jsonData);
            string jsonRequest = JsonOperations.Serilalize(request);
            SendMessage(jsonRequest);
            return ReceiveMessage();
        }
    }
}
