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
            ServiceHost host = new ServiceHost(typeof(TestService));

            Uri listenAddress = new Uri("http://localhost:40000");
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.None);

            ContractDescription cd1 = ContractDescription.GetContract(typeof(IDemo1));
            EndpointAddress epaddr1 = new EndpointAddress("http://s1.owner");
            ServiceEndpoint ep1 = new ServiceEndpoint(cd1, binding, epaddr1);
            ep1.ListenUri = listenAddress;
            host.AddServiceEndpoint(ep1);

            ContractDescription cd2 = ContractDescription.GetContract(typeof(IDemo2));
            EndpointAddress epAddr2 = new EndpointAddress("http://s2.owner");
            ServiceEndpoint ep2 = new ServiceEndpoint(cd2, binding, epAddr2);
            ep2.ListenUri = listenAddress;
            host.AddServiceEndpoint(ep2);

            host.Open();

            foreach(ChannelDispatcher cndisp in host.ChannelDispatchers)
            {
                Console.WriteLine(cndisp.Listener.Uri);
            }

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract]
    public interface IDemo1
    {
        [OperationContract]
        void CallFirst();
    }

    [ServiceContract]
    public interface IDemo2
    {
        [OperationContract]
        void CallSecond();
    }

    class TestService : IDemo1, IDemo2
    {
        public void CallFirst()
        {
            Console.WriteLine($"{nameof(CallFirst)} 被调用。");
        }

        public void CallSecond()
        {
            Console.WriteLine($"{nameof(CallSecond)} 被调用。");
        }
    }
}
