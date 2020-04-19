using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "查找范围示例";

            using(ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.Open();

                // 客户端调用
                // 查找服务
                DiscoveryClient discl = new DiscoveryClient(new UdpDiscoveryEndpoint());
                FindCriteria findcri = new FindCriteria(typeof(IDemo));
                // 指定要查找的范围
                findcri.Scopes.Add(new Uri("http://samples/g1"));
                FindResponse resp = discl.Find(findcri);
                // 输出查找结果
                Console.WriteLine("查找结果：");
                foreach (EndpointDiscoveryMetadata epmt in resp.Endpoints)
                {
                    Console.WriteLine(epmt.Address.Uri);
                }

                Console.Read();
            }
        }
    }

    [ServiceContract(ConfigurationName = "democt")]
    public interface IDemo
    {
        [OperationContract]
        void SayHello();
    }

    [ServiceBehavior(ConfigurationName = "sv")]
    class DemoService : IDemo
    {
        public void SayHello()
        {
            Console.WriteLine("你好，服务已调用。");
        }
    }
}
