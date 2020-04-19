using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Service2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务 2";

            using (ServiceHost host = new ServiceHost(typeof(Service)))
            {
                host.AddServiceEndpoint(typeof(IService2), new BasicHttpBinding(), "http://127.0.0.1:6000");

                host.Open();

                Console.ReadKey();
            }
        }
    }

    [ServiceContract(Namespace = "samples",
        Name = "sv2")]
    interface IService2
    {
        [OperationContract(Name = "getNum", Action = "get-num")]
        int GetNumber();
    }

    class Service : IService2
    {
        static Random rand = new Random();
        public int GetNumber()
        {
            int n;
            lock (rand)
            {
                n = rand.Next(0, 10000);
            }
            Console.WriteLine($"服务产生的数字：{n}");
            return n;
        }
    }
}
