using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "示例客户端";

            AnnouncementService annsvc = new AnnouncementService();
            // 处理上线/下线提醒事件
            annsvc.OnlineAnnouncementReceived += OnlineRec;
            annsvc.OfflineAnnouncementReceived += OfflineRec;
            // 创建ServiceHost对象，在客户端侦听服务上线/下线通知
            using (ServiceHost host = new ServiceHost(annsvc))
            {
                // 添加终结点
                host.AddServiceEndpoint(new UdpAnnouncementEndpoint());
                host.Open();
                Console.Read();
            }
        }

        private static void OfflineRec(object sender, AnnouncementEventArgs e)
        {
            Console.WriteLine($"\n服务已下线，终结点地址为：{e.EndpointDiscoveryMetadata.Address.Uri}");
        }

        private static void OnlineRec(object sender, AnnouncementEventArgs e)
        {
            Console.WriteLine($"\n服务上线，其终结点地址为：{e.EndpointDiscoveryMetadata.Address.Uri}");
            // 调用服务
            IService svcl = ChannelFactory<IService>.CreateChannel(new BasicHttpBinding(), e.EndpointDiscoveryMetadata.Address);
            svcl.Hello();
            svcl.Close();
        }
    }

    [ServiceContract(Namespace = "http://demos", Name = "test_sv", ConfigurationName = "ct")]
    public interface IService : IClientChannel
    {
        [OperationContract(Name = "sayHello", Action = "hello")]
        void Hello();
    }
}
