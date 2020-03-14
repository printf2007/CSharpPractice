using System;
using System.Net;
using System.Net.Sockets;

namespace SocketDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            s.Bind(IPEndPoint.Parse("0.0.0.0:8080"));
            s.Listen(1000);
            Console.WriteLine("Hello World!");
        }
    }
}
