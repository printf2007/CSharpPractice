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
            ServiceHost host = new ServiceHost(typeof(DemoService));
            ServiceEndpoint svEndpoint = new ServiceEndpoint(
                ContractDescription.GetContract(typeof(IDemo)),
                new WSHttpBinding(SecurityMode.None),
                new EndpointAddress("http://abc.org"));
            // 指定真实的地址
            svEndpoint.ListenUri = new Uri("http://127.0.0.1:7800");
            host.AddServiceEndpoint(svEndpoint);
            host.Open();

            //----------------------------------------------------------------
            // 逻辑地址
            EndpointAddress svaddress = new EndpointAddress("http://abc.org");
            // 物理地址
            Uri listenUri = new Uri("http://127.0.0.1:7800");
            // 绑定
            WSHttpBinding wsbinding = new WSHttpBinding(SecurityMode.None);
            // 创建调用通道
            IDemo cnl = ChannelFactory<IDemo>.CreateChannel(wsbinding, svaddress, listenUri);

            cnl.Test();

            ((IClientChannel)cnl).Close();

            /*
             * 以下是另一种方法
             * 
            ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>(wsbinding, svaddress);
            // 指定物理地址
            ClientViaBehavior via = new ClientViaBehavior(listenUri);
            fac.Endpoint.EndpointBehaviors.Add(via);
            // 创建通道
            IDemo cn = fac.CreateChannel();
            cn.Test();
            */
            //----------------------------------------------------------------

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void Test();
    }

    class DemoService : IDemo
    {
        public void Test()
        {
            // 输出 To 消息头的内容
            OperationContext context = OperationContext.Current;
            Uri to = context.IncomingMessageHeaders.To;
            Console.WriteLine($"To 消息头的内容：{to}");
        }
    }
}
