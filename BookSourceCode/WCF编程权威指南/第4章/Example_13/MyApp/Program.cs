using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(DemoService));
            ServiceEndpoint ep = new ServiceEndpoint(ContractDescription.GetContract(typeof(IDemo)), new WSHttpBinding(SecurityMode.None), new EndpointAddress("http://demo"));
            ep.ListenUri = new Uri("http://localhost:7000");
            // 插入自定义Behavior
            ep.EndpointBehaviors.Add(new CustEndpointBehavior());
            host.AddServiceEndpoint(ep);
            host.Open();

            /***************************************************************/
            // 客户端调用
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.None);
            // 创建地址头
            AddressHeader adheader = AddressHeader.CreateAddressHeader("data", "sample-test", "Bob");
            EndpointAddress epaddr = new EndpointAddress(new Uri("http://demo"), adheader);
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, epaddr, new Uri("http://localhost:7000"));
            // 调用服务
            channel.SayHello();
            ((IClientChannel)channel).Close();

            Console.ReadKey();
            host.Close();
        }
    }

    class CustAddressFilter : MessageFilter
    {
        private EndpointAddress _epaddress;

        public CustAddressFilter(EndpointAddress addr)
        {
            _epaddress = addr;
        }

        public override bool Match(Message message)
        {
            // 先检测逻辑地址
            Uri inputAddress = message.Headers.To;
            if (_epaddress.Uri.Equals(inputAddress))
            {
                // 检测地址头
                int index = message.Headers.FindHeader("data", "sample-test");
                if (index > -1)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Match(MessageBuffer buffer)
        {
            return Match(buffer.CreateMessage());
        }
    }

    class CustEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            return;
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            CustAddressFilter myfilter = new CustAddressFilter(endpoint.Address);
            endpointDispatcher.AddressFilter = myfilter;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }
    }

    #region 服务协定与服务类
    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void SayHello();
    }

    class DemoService : IDemo
    {
        public void SayHello()
        {
            Console.WriteLine("服务操作已调用。");
        }
    }
    #endregion
}
