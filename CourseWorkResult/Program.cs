using CourseWorkResult.Controllers;
using CourseWorkResult.Views;
using System;
using System.Windows.Forms;

namespace CourseWorkResult
{
    static class Program
    {
        private static readonly string ipAddress = "127.0.0.1";
        private static readonly int port = 80;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ClientsRequests clientsRequests = new ClientsRequests(ipAddress, port);

            if (clientsRequests.Connect())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LaminateTable());
                return;
            }

            MessageBox.Show("Сервер недоступен! Попробуйте подключиться позже.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
