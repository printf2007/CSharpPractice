using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace DemoSerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "示例服务器";

            Task.Delay(2000).Wait();

            using(ServiceHost host = new ServiceHost(typeof(DemoSvc)))
            {
                host.Opened += (x, y) => Console.WriteLine("服务已经启动。");
                host.Open();
                Console.Read();
            }
        }
    }

    [ServiceContract(Namespace = "http://demos", Name = "test_sv", ConfigurationName = "ct")]
    public interface IDemo
    {
        [OperationContract(Name = "sayHello", Action = "hello")]
        void SayHello();
    }

    [ServiceBehavior(ConfigurationName = "sv")]
    class DemoSvc : IDemo
    {
        public void SayHello()
        {
            Console.WriteLine("你好，服务已调用。");
        }
    }
}
