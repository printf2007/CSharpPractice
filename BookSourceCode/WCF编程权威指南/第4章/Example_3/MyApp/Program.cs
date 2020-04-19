using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 准备两个基址
            Uri[] baseAddrs =
            {
                new Uri("http://localhost:600/demo"),
                /* 使用 new Uri("http://localhost:1200/data") 会发生异常 */
                new Uri("net.tcp://localhost:800/demo")
            };

            // 创建ServiceHost实例
            ServiceHost host = new ServiceHost(typeof(DemoService), baseAddrs);
            // 打开服务
            host.Open();

            Console.WriteLine("服务正在运行。\n");

            // 输出终结点信息
            foreach (ServiceEndpoint ep in host.Description.Endpoints)
            {
                Console.WriteLine($"服务协定：{ep.Contract.ContractType.Name}");
                Console.WriteLine($"绑定：{ep.Binding.GetType().Name}");
                Console.WriteLine($"终结点地址：{ep.Address.Uri}");
                Console.WriteLine("----------------------------------------------");
            }

            Console.ReadKey();
            // 关闭服务
            host.Close();
        }
    }

    [ServiceContract]
    public interface IDemo1
    {
        [OperationContract]
        void TestMethod1();
    }

    [ServiceContract]
    public interface IDemo2
    {
        [OperationContract]
        void TestMethod2();
    }

    class DemoService : IDemo1, IDemo2
    {
        public void TestMethod1()
        {
            Console.WriteLine($"{nameof(TestMethod1)} 方法被调用。");
        }

        public void TestMethod2()
        {
            Console.WriteLine($"{nameof(TestMethod2)} 方法被调用。");
        }
    }
}
