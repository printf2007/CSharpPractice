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
            // 基址
            Uri baseAddress = new Uri("http://localhost:500");

            using(ServiceHost host=new ServiceHost(typeof(DemoService), baseAddress))
            {
                // 添加终结点
                BasicHttpBinding binding = new BasicHttpBinding();
                host.AddServiceEndpoint(typeof(IDemo), binding, "demo");

                // 打开服务
                host.Open();
                Console.WriteLine("WCF服务已启动。");

                Console.Read();
                host.Close(); //关闭服务
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        int Add(int a, int b);

        void Run();
    }

    internal class DemoService : IDemo
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public void Run()
        {
            // 由于此方法不会向客户端公开
            // 此处不添加任何实现代码
        }
    }
}
