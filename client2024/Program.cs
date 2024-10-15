// See https://aka.ms/new-console-template for more information


using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApplication_1_2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Створюємо сокет для клієнта
            Socket clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            // Задаємо серверну адресу та порт
            IPAddress serverIpAddress = IPAddress.Parse("127.0.0.1");
            int serverPort = 6116;
            IPEndPoint serverEndPoint = new IPEndPoint(serverIpAddress, serverPort);

            try
            {
                // Підключаємося до сервера
                clientSocket.Connect(serverEndPoint);
                Console.WriteLine($"Підключено до сервера {serverEndPoint}");

                // Відправляємо дані серверу
                string message = "Hello, Server!";
                byte[] dataToSend = Encoding.ASCII.GetBytes(message);
                clientSocket.Send(dataToSend);

                // Отримуємо відповідь від сервера
                byte[] buffer = new byte[1024];
                int bytesRead = clientSocket.Receive(buffer);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Від сервера отримано відповідь: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            finally
            {
                Console.ReadLine();
                // Закриваємо з'єднання з сервером
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }

            
        }
    }
}