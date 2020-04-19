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
            Uri baseAddr = new Uri("http://localhost:1100/base");
            ServiceHost host = new ServiceHost(typeof(TestService), baseAddr);
            // 添加终结点
            WSHttpBinding binding = new WSHttpBinding();
            host.AddServiceEndpoint(typeof(IService1), binding, "test1");
            host.AddServiceEndpoint(typeof(IService2), binding, "http://localhost:9800/test/demo");

            host.Open();

            foreach (ServiceEndpoint ep in host.Description.Endpoints)
            {
                Console.WriteLine($"终结点地址：{ep.Address.Uri}");
            }

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void TestA();
    }

    [ServiceContract]
    public interface IService2
    {
        [OperationContract]
        void TestB();
    }

    class TestService : IService1, IService2
    {
        public void TestA()
        {
            Console.WriteLine("操作 A 被调用。");
        }

        public void TestB()
        {
            Console.WriteLine("操作 B 被调用。");
        }
    }
}
