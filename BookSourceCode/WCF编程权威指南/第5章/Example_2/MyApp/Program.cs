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
            using(ServiceHost host=new ServiceHost(typeof(DemoCalculatorService)))
            {
                host.Open();
                // ……
                Console.Read();
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        int Add(int a, int b);
    }

    [ServiceBehavior(ConfigurationName = "calsv")]
    class DemoCalculatorService : IDemo
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
