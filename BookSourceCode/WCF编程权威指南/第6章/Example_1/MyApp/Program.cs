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
            using(ServiceHost host=new ServiceHost(typeof(DemoService)))
            {
                host.Open();

                /*--------------- 客户端调用 ---------------*/
                ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>("dm_ep");
                IDemo channel = fac.CreateChannel();
                Console.WriteLine("开始调用服务。");
                channel.PostData(6000);
                Console.WriteLine("服务调用完成。");
                fac.Close();

                Console.ReadKey();
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract(IsOneWay = true)]
        void PostData(int number);
    }

    class DemoService : IDemo
    {
        public void PostData(int number)
        {
            Console.WriteLine("\n=========================");
            Console.WriteLine("服务器正在处理……");
            Task.Delay(3000).Wait();
            Console.WriteLine("客户端提交的数值：{0}", number);
            Console.WriteLine("=========================\n");
        }
    }
}
