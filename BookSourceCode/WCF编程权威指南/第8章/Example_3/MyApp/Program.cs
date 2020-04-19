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

                /***** 客户端调用 *****/
                DiscoveryClient disclient = new DiscoveryClient("dis_client");
                FindResponse response = disclient.Find(new FindCriteria(typeof(IDemo)));
                // 分别调用搜索到的终结点
                foreach(EndpointDiscoveryMetadata epmt in response.Endpoints)
                {
                    IDemo channel = ChannelFactory<IDemo>.CreateChannel(new BasicHttpBinding(), epmt.Address);
                    channel.Run();
                    ((IClientChannel)channel).Close();
                }

                Console.Read();
            }
        }
    }

    [ServiceContract(ConfigurationName = "idemo")]
    public interface IDemo
    {
        [OperationContract]
        void Run();
    }

    [ServiceBehavior(ConfigurationName = "demosv")]
    class DemoService : IDemo
    {
        public void Run()
        {
            OperationContext context = OperationContext.Current;
            EndpointAddress addr = context.Channel.LocalAddress;
            string msg = $"服务已调用，通道地址：\n{addr.Uri}\n";
            Console.WriteLine(msg);
        }
    }
}
