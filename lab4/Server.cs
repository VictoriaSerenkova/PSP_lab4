using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab4
{
    class Server
    {
        public TcpClient client;
        static NetworkStream stream = null;

        public Server(TcpClient tcpClient)
        {
            client = tcpClient;
        }

        public void Process()
        {
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64];
                while (true)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    var read_string = builder.ToString();
                    Console.WriteLine(read_string);


                    if (ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), new Teylar(read_string)))
                    {
                        Console.WriteLine(":главный поток начал работу");
                        Thread.Sleep(1000);
                        Console.WriteLine( "главный поток вышел");
                    }
                    else
                    {
                        Console.WriteLine("поток не создан");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
                Console.ReadKey();
            }
        }

        static void ThreadProc(Object stateInfo)
        {
            Teylar ti = (Teylar)stateInfo;
            var answer = Encoding.Unicode.GetBytes(ti.Start());
            stream.Write(answer, 0, answer.Length);
        }
    }
}
