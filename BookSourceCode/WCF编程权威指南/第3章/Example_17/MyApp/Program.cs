using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace MyApp
{
    using Contracts;
    using ServiceInternal;

    class Program
    {
        static void Main(string[] args)
        {
            Uri svcuri = new Uri("http://localhost:650/addsv/");
            ServiceHost host = new ServiceHost(typeof(MyService), svcuri);
            host.Opened += (j, k) => Console.WriteLine("服务已经启动。");
            host.Open();

            // 客户端调用
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress epaddress = new EndpointAddress(svcuri);
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, epaddress);
            Console.WriteLine("即将调用 Add v1 方法");
            int r = channel.Add_v1(9, 12);
            Console.WriteLine($"调用结果：{r}\n");

            Console.WriteLine("即将调用 Add v2 方法");
            InputDataMessage input = new InputDataMessage();
            input.Number1 = 50;
            input.Number2 = 40;
            OutputDataMessage output = channel.Add_v2(input);
            Console.WriteLine($"调用结果：{output.Result}\n");
            ((IClientChannel)channel).Close();

            Console.ReadKey();
            host.Close();
        }
    }
}

namespace ServiceInternal
{
    using Contracts;

    class MyService : IDemo
    {
        public int Add_v1(int m, int n)
        {
            return m + n;
        }

        public OutputDataMessage Add_v2(InputDataMessage p)
        {
            OutputDataMessage output = new OutputDataMessage();
            output.Result = p.Number1 + p.Number2;
            return output;
        }
    }
}

namespace Contracts
{
    using Externs;

    [ExtContractBehavior]
    [ServiceContract(Name = "demo", Namespace = "demo-root")]
    public interface IDemo
    {
        [OperationContract(Name = "addv1", Action = "add-v1")]
        [return: MessageParameter(Name = "resultOutput")]
        int Add_v1([MessageParameter(Name = "num1")] int m,
                   [MessageParameter(Name = "num2")] int n);

        [OperationContract(Name = "addv2", Action = "add-v2")]
        OutputDataMessage Add_v2(InputDataMessage p);
    }

    [MessageContract(WrapperNamespace = "demo-data", IsWrapped = true, WrapperName = "Input_Nums")]
    public class InputDataMessage
    {
        [MessageBodyMember(Name = "x", Namespace = "demo-data-input")]
        public int Number1 { get; set; }
        [MessageBodyMember(Name = "y", Namespace = "demo-data-input")]
        public int Number2 { get; set; }
    }

    [MessageContract(IsWrapped = true, WrapperName = "Output_Res", WrapperNamespace = "demo-data")]
    public class OutputDataMessage
    {
        [MessageBodyMember(Name = "r", Namespace = "demo-data-output")]
        public int Result { get; set; }
    }
}

namespace Externs
{
    class MsgRecoder : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            string s = $"\n服务器回应的消息：\n{reply}";
            Console.WriteLine(s);
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            string s = $"\n客户端发送的消息：\n{request}";
            Console.WriteLine(s);
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    class ExtContractBehaviorAttribute : Attribute, IContractBehavior
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            MsgRecoder rd = new MsgRecoder();

            clientRuntime.ClientMessageInspectors.Add(rd);
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            return;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            return;
        }
    }
}
