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
                host.Open();

                // 客户端调用
                ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>("ep_client");
                IDemo channel = fac.CreateChannel();
                channel.SayHello("Jack");
                fac.Close();

                Console.ReadKey();
            }
        }
    }

    [ServiceContract(ConfigurationName = "idemo")]
    public interface IDemo
    {
        [OperationContract]
        void SayHello(string name);
    }

    [ServiceBehavior(ConfigurationName = "sv")]
    class DemoService : IDemo
    {
        public void SayHello(string name)
        {
            Console.WriteLine("Hello, {0}", name);
        }
    }
}
