using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace DemoServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务发现示例";

            using(ServiceHost host =new ServiceHost(typeof(DemoService)))
            {
                host.Open();

                DiscoveryClient client = new DiscoveryClient("ep_discovery");
                FindCriteria crit = new FindCriteria(typeof(IDemo));
                FindResponse resp = client.Find(crit);
                
                // 分析查找结果
                if(resp != null && resp.Endpoints.Count > 0)
                {
                    // 调用服务
                    EndpointDiscoveryMetadata epaddrMtd = resp.Endpoints[0];
                    IDemo channel = ChannelFactory<IDemo>.CreateChannel(new WSHttpBinding(), epaddrMtd.Address);
                    channel.TestCall();
                }

                Console.Read();
            }
        }
    }

    #region 协定与服务类
    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void TestCall();
    }

    class DemoService : IDemo
    {
        public void TestCall()
        {
            OperationContext context = OperationContext.Current;
            Console.WriteLine("被调用了。");
            Console.WriteLine($"当前终结点地址：\n{context.Channel.LocalAddress.Uri}");
        }
    }
    #endregion
}
