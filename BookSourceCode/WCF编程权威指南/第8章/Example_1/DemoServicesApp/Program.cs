using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace DemoServicesApp
{
    using Samples;

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "示例服务";

            using(ServiceHost host1 =new ServiceHost(typeof(DemoService1)), host2=new ServiceHost(typeof(DemoService2)))
            {
                host1.Opened += (a, b) => Console.WriteLine("示例服务1已启动。");
                host2.Opened += (c, d) => Console.WriteLine("示例服务2已启动。");

                host1.Open();
                host2.Open();

                Console.Read();
            }
        }
    }
}

namespace Samples
{
    [ServiceContract(Namespace = "sample-service", Name = "demo", ConfigurationName = "ical")]
    public interface IDemo
    {
        [OperationContract(Name = "calc", Action = "op-calc")]
        double Calc(double a, double b);
    }

    [ServiceBehavior(ConfigurationName = "sv1")]
    class DemoService1 : IDemo
    {
        public double Calc(double a, double b)
        {
            return a + b;
        }
    }

    [ServiceBehavior(ConfigurationName = "sv2")]
    class DemoService2 : IDemo
    {
        public double Calc(double a, double b)
        {
            return a * b;
        }
    }
}
