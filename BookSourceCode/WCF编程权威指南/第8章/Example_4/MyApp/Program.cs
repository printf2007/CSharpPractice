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
            Console.Title = "示例服务";

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.Open();

                // 客户端调用
                DiscoveryClient discl = new DiscoveryClient(new UdpDiscoveryEndpoint());
                FindCriteria cri = new FindCriteria(typeof(IDemo));
                FindResponse response = discl.Find(cri);

                // 搜索结果
                Console.WriteLine("已发现的终结点：");
                foreach (EndpointDiscoveryMetadata epm in response.Endpoints)
                {
                    Console.WriteLine(epm.Address.Uri);
                }

                Console.Read();
            }
        }
    }

    [ServiceContract(ConfigurationName = "idemo")]
    public interface IDemo
    {
        [OperationContract]
        int Add(int x, int y);
    }

    [ServiceBehavior(ConfigurationName = "sv")]
    class DemoService : IDemo
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
