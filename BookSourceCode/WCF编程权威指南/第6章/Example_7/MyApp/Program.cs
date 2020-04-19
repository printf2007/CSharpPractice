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
            Console.Title = "单线程服务示例";
            Uri address = new Uri("http://127.0.0.1:7122/demo");
            using (ServiceHost host=new ServiceHost(typeof(MyDemoService), address))
            {
                host.Open();

                // 客户端调用
                Action act = () =>
                {
                    IDemo channel = ChannelFactory<IDemo>.CreateChannel(new BasicHttpBinding(), new EndpointAddress(address));
                    channel.TestCall();
                    ((IClientChannel)channel).Close(); //关闭通道
                };
                Parallel.Invoke(act, act, act);

                Console.ReadKey();
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void TestCall();
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    class MyDemoService : IDemo
    {
        public void TestCall()
        {
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - 服务操作已被调用。");
            Task.Delay(5000).Wait();
            Console.WriteLine($"\n{DateTime.Now.ToLongTimeString()} - 服务操作即将完成。");
        }
    }
}
