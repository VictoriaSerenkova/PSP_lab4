using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientLab4
{
    class Program
    {
        static string x { get; set; }

        static void Main(string[] args)
        {
            TcpClient client = null;
            int port = 8040;
            string ip = "127.0.0.1";

            try
            {
                Console.Write("Enter x:");
                x = Console.ReadLine();

                client = new TcpClient(ip, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] data = Encoding.Unicode.GetBytes(x);
                    stream.Write(data, 0, data.Length);
                    data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string answ = builder.ToString();

                    Console.WriteLine("Ln({0}) = {1}", x, answ);

                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
                Console.ReadKey();
            }
            finally
            {
                client.Close();
            }
        }

    }
}
