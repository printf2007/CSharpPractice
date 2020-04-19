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
                Console.WriteLine("服务已运行。\n");

                foreach (var ep in host.Description.Endpoints)
                {
                    Console.WriteLine($"终结点地址：{ep.Address.Uri}");
                    BasicHttpBinding binding = ep.Binding as BasicHttpBinding;
                    Console.WriteLine($"消息编码格式：{binding.MessageEncoding}");
                    Console.WriteLine($"接收消息的最大长度：{binding.MaxReceivedMessageSize}");
                    Console.WriteLine("===============================");
                }

                Console.ReadKey();
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        int Add(int m, int n);
    }

    class DemoService : IDemo
    {
        public int Add(int m, int n)
        {
            return m + n;
        }
    }
}
