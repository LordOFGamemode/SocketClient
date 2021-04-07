using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        private static Socket _clientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static void Main(string[] args)
        {
            Console.Title = "Client";
            LoopConnect();
            SendLoop();
            Console.ReadLine();
        }

        private static void SendLoop()
        {
            while (true)
            {
                Console.Write("enter request: ");
                string req = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(req);
                _clientSocket.Send(buffer);
                byte[] recievedbuffer = new byte[1024];
                int rec = _clientSocket.Receive(recievedbuffer);
                byte[] data = new byte[rec];
                Array.Copy(recievedbuffer, data, rec);
                Console.WriteLine("[Recieved] " + Encoding.ASCII.GetString(data));


            }
        }

        private static void LoopConnect()
        {
            int attampts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attampts++;
                    _clientSocket.Connect(IPAddress.Loopback, 100);
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Attempts: " + attampts);
                }
            }
            Console.Clear();
            Console.WriteLine("connected");

        }
    }
}
