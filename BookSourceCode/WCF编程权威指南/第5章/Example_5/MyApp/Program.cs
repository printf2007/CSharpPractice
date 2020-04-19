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
            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.Opened += (a1, a2) => Console.WriteLine("服务已运行。");
                host.Open();

                Console.ReadKey();
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void SayHello();
    }

    class DemoService : IDemo
    {
        public void SayHello()
        {
            Console.WriteLine("服务被调用。");
        }
    }
}
