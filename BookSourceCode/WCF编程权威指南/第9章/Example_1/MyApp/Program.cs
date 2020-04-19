using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "各绑定的默认安全模式";

            BasicHttpBinding binding1 = new BasicHttpBinding();
            Console.WriteLine($"{binding1.GetType().Name}：{binding1.Security.Mode}");

            WSHttpBinding binding2 = new WSHttpBinding();
            Console.WriteLine($"{binding2.GetType().Name}：{binding2.Security.Mode}");

            NetTcpBinding binding3 = new NetTcpBinding();
            Console.WriteLine($"{binding3.GetType().Name}：{binding3.Security.Mode}");

            BasicHttpsBinding binding4 = new BasicHttpsBinding();
            Console.WriteLine($"{binding4.GetType().Name}：{binding4.Security.Mode}");

            Console.Read();
        }

    }
}
