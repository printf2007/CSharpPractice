using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServiceApp
{
    [ServiceContract(Namespace = "demo-sv", Name = "demo")]
    interface IDemo
    {
        [OperationContract(Name = "add", Action = "add-opt")]
        int Add(int x, int y);
    }

    class DemoService : IDemo
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务器";
            using(ServiceHost host= new ServiceHost(typeof(DemoService)))
            {
                host.Open();
                Console.WriteLine("WCF服务已启动。");

                var eps = host.Description.Endpoints;
                foreach (var ep in eps)
                {
                    string s = $"服务终结点地址：{ep.Address.Uri}";
                    Console.WriteLine(s);
                }
                Console.Read();
            }
        }
    }
}
