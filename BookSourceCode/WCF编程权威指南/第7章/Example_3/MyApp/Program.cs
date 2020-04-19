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
            Uri svaddr = new Uri("http://localhost:6312");
            BasicHttpBinding binding = new BasicHttpBinding();

            ServiceHost host = new ServiceHost(typeof(DemoService));
            host.AddServiceEndpoint(typeof(IDemo), binding, svaddr);
            host.Open();

            EndpointAddress epaddr = new EndpointAddress(svaddr);
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, epaddr);
            try
            {
                int res = channel.Div(6, 0);
                Console.WriteLine("计算结果：{0}", res);
            }
            catch(FaultException fex)
            {
                FaultReason r = fex.Reason;
                FaultReasonText text = r.GetMatchingTranslation();
                Console.WriteLine($"错误信息：{text.Text}");
            }

            Console.Read();
        }
    }


    #region 服务协定与实现
    [ServiceContract]
    [MessageLogBehavior]
    public interface IDemo
    {
        [OperationContract]
        int Div(int x, int y);
    }

    class DemoService : IDemo
    {
        public int Div(int x, int y)
        {
            if (y == 0)
            {
                FaultReason reason = new FaultReason("除数不能为0");
                FaultCode code = new FaultCode("NotZero", "my-cust-err");
                throw new FaultException(reason, code);
            }

            return (x / y);
        }
    }

    #endregion

    #region 扩展
    class MessageLogger : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Console.WriteLine($"\n包装的错误消息：\n{reply}\n");
        }
    }

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    class MessageLogBehaviorAttribute : Attribute, IContractBehavior
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            return;
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.MessageInspectors.Add(new MessageLogger());
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            return;
        }
    }
    #endregion
}
