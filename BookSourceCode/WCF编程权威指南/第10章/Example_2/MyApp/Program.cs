using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "消息拦截器示例";

            Uri epuri = new Uri("http://localhost:1288");
            BasicHttpBinding binding = new BasicHttpBinding();

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.AddServiceEndpoint(typeof(IDemo), binding, epuri);
                host.Open();

                // 客户端调用
                EndpointAddress epaddr = new EndpointAddress(epuri);
                IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, epaddr);
                channel.CallTest();
                ((IClientChannel)channel).Close();

                Console.Read();
            }
        }
    }

    [ServiceContract]
    [CustContractBehavior]
    public interface IDemo
    {
        [OperationContract]
        void CallTest();
    }

    class DemoService : IDemo
    {
        public void CallTest()
        {
            Console.WriteLine("\n服务已调用。\n");
        }
    }


    public class ServerMessageLogger : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            string displayText = $"服务器收到以下请求消息：\n{request}\n";
            Console.WriteLine(displayText);
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            string displayText = $"服务器答复消息如下：\n{reply}\n";
            Console.WriteLine(displayText);
        }
    }

    public class ClientMessageLogger : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            string outputstr = $"客户端收到的服务器答复消息：\n{reply}\n";
            Console.WriteLine(outputstr);
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            string outputText = $"客户端即将发送的请求消息：\n{request}\n";
            Console.WriteLine(outputText);
            return null;
        }
    }


    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public class CustContractBehaviorAttribute : Attribute, IContractBehavior, IContractBehaviorAttribute
    {
        public Type TargetContract => typeof(IDemo);

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new ClientMessageLogger());
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.MessageInspectors.Add(new ServerMessageLogger());
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            return;
        }
    }
}
