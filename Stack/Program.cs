using NPoco.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            //     List<byte[]> aaa = new List<byte[]>();
            //     for (int i = 0; i < 128; i++)
            //     {
            //         byte[] a = new byte[1024 * 1024 * 1024];
            //         aaa.Add(a);
            //     }

            //     Console.WriteLine("Hello" );
            //     Console.ReadKey();

            Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);
            s.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 1024));

            var x = s.Accept();
            byte[] www = new byte[1024 * 1024 * 4];
            x.Receive(www);









            //for (int i = 0; i < 2; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        unsafe
            //        {
            //            var x = stackalloc int[1024 * 1024];
            //        }
            //        Task.Delay(10000).Wait();
            //    });

            //}
            //Console.WriteLine("Hello World");
        } 
        
      
    }
}
