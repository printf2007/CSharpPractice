using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(DmService));
            // 创建绑定
            BasicHttpBinding binding = new BasicHttpBinding();
            ContractDescription cd = ContractDescription.GetContract(typeof(IDemo));
            // 创建地址头
            AddressHeader adheader = AddressHeader.CreateAddressHeader("other", "demo-data", 2000);
            // 将地址头与终结点地址关联
            EndpointAddress epaddr = new EndpointAddress(new Uri("http://localhost:8900/sv"), adheader);
            ServiceEndpoint ep = new ServiceEndpoint(cd, binding, epaddr);
            // 添加终结点
            host.AddServiceEndpoint(ep);
            host.Open();

            /******************* 客户端调用 ******************/
            // 客户端调用服务时，终结点地址必须包含与服务器一致的地址头
            EndpointAddress clepaddr = new EndpointAddress(new Uri("http://localhost:8900/sv"), AddressHeader.CreateAddressHeader("other", "demo-data", 2000));
            IDemo ch = ChannelFactory<IDemo>.CreateChannel(binding, clepaddr);
            ch.Call1();
            ch.Call2();

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void Call1();
        [OperationContract]
        void Call2();
    }

    class DmService : IDemo
    {
        public void Call1()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("消息头：");
            OperationContext opcontext = OperationContext.Current;
            var hds = opcontext.IncomingMessageHeaders;
            // 显示消息头
            int hdval = hds.GetHeader<int>("other", "demo-data");
            Console.WriteLine($"地址头内容：{hdval}");
            // 输出整条消息
            Console.WriteLine($"\nSOAP消息：\n{opcontext.RequestContext.RequestMessage}");
        }

        public void Call2()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("消息头：");
            OperationContext opcontext = OperationContext.Current;
            var hds = opcontext.IncomingMessageHeaders;
            int val = hds.GetHeader<int>("other", "demo-data");
            // 显示地址头
            Console.WriteLine($"地址头内容：{val}");
            // 显示整条消息
            Console.WriteLine($"\nSOAP消息：\n{opcontext.RequestContext.RequestMessage}");
        }
    }
}
