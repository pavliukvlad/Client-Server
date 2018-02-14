using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static int port = 8005;
        static string ipAdress = "127.0.0.1";

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAdress), port);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                    socket.Connect(ipEndPoint);
                    Console.WriteLine("Enter a message: ");
                    byte[] data = Encoding.Unicode.GetBytes(Console.ReadLine());

                    socket.Send(data);

                    data = new byte[256];
                    int bytes = 0;
                    StringBuilder sb = new StringBuilder();

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        sb.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);

                    Console.WriteLine("Response of server: " + sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
