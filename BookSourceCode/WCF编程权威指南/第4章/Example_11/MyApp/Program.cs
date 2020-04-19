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
            ContractDescription cd = ContractDescription.GetContract(typeof(IDemo));
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            // 开启端口共享
            binding.PortSharingEnabled = true;
            EndpointAddress epaddress = new EndpointAddress("net.tcp://testhost");
            ServiceEndpoint epoint = new ServiceEndpoint(cd, binding, epaddress);
            epoint.ListenUri = new Uri("net.tcp://localhost:5000");
            epoint.ListenUriMode = ListenUriMode.Unique;
            host.AddServiceEndpoint(epoint);
            host.Open();

            foreach(ChannelDispatcher disp in host.ChannelDispatchers)
            {
                Console.WriteLine(disp.Listener.Uri);
            }

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void Call();
    }

    class DemoService : IDemo
    {
        public void Call()
        {
            Console.WriteLine("服务被调用。");
        }
    }
}
