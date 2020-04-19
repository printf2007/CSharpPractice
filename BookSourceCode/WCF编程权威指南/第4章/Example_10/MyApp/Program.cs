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
            ServiceHost host = new ServiceHost(typeof(MyService));
            ContractDescription cd = ContractDescription.GetContract(typeof(IService));
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.None);
            EndpointAddress epaddr = new EndpointAddress("http://demo.org");
            ServiceEndpoint endPoint = new ServiceEndpoint(cd, binding, epaddr);
            endPoint.ListenUri = new Uri("http://localhost:8300");
            endPoint.ListenUriMode = ListenUriMode.Unique;
            host.AddServiceEndpoint(endPoint);

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
    public interface IService
    {
        [OperationContract]
        void TestCall();
    }

    class MyService : IService
    {
        public void TestCall()
        {
            string opt = OperationContext.Current.RequestContext.RequestMessage.Headers.Action;
            Console.WriteLine($"{opt} 操作被调用。");
        }
    }
}
