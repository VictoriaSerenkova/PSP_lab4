using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
        static int port = 8040;
        static TcpListener server;
        static IPAddress ip;

        static void Main(string[] args)
        {
            try
            {
                ip = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(ip, port);
                server.Start();

                Console.WriteLine("Сервер запущен. Ожидание подключений ...");

                while (true)
                {
                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Клиент подключен");
                    Server clientobject = new Server(client);

                    Thread clientThread = new Thread(new ThreadStart(clientobject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (server != null)
                    server.Stop();
                Console.ReadKey();
            }

            Console.ReadKey();
        }
    }
}
