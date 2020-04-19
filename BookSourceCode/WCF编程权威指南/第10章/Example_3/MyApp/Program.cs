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
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "消息格式化示例";

            Uri epuri = new Uri("http://localhost:6602/test");
            BasicHttpBinding binding = new BasicHttpBinding();

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.AddServiceEndpoint(typeof(IDemo), binding, epuri);
                host.Open();

                // 客户端调用
                EndpointAddress epaddr = new EndpointAddress(epuri);
                IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, epaddr);
                int result = channel.Add(10, 20);
                Console.WriteLine($"计算结果：{result}");
                ((IClientChannel)channel).Close();

                Console.Read();
            }
        }
    }

    #region 协定
    [ServiceContract(Namespace = "http://demo", Name = "demo")]
    [MyContractBehavior]
    public interface IDemo
    {
        [OperationContract(Action = "addopt", ReplyAction = "addreply")]
        int Add(int a, int b);
    }
    #endregion
    #region 服务类
    class DemoService : IDemo
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
    #endregion

    #region 扩展
    public class MyServerFormatter : IDispatchMessageFormatter
    {
        DispatchOperation opt = null;
        public MyServerFormatter(DispatchOperation op)
        {
            opt = op;
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            byte[] data = message.GetBody<byte[]>();
            int a = BitConverter.ToInt32(data, 0);
            int b = BitConverter.ToInt32(data, sizeof(int));
            parameters[0] = a;
            parameters[1] = b;
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            byte[] data = BitConverter.GetBytes((int)result);
            return Message.CreateMessage(messageVersion, opt.ReplyAction, data);
        }
    }

    public class MyClientFormatter : IClientMessageFormatter
    {
        ClientOperation opt = null;
        public MyClientFormatter(ClientOperation op)
        {
            opt = op;
        }

        public object DeserializeReply(Message message, object[] parameters)
        {
            byte[] data = message.GetBody<byte[]>();
            int res = BitConverter.ToInt32(data,0);
            return res;
        }

        public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
        {
            byte[] data = null;
            byte[] buffer1 = BitConverter.GetBytes((int)parameters[0]);
            byte[] buffer2 = BitConverter.GetBytes((int)parameters[1]);
            data = new byte[buffer1.Length + buffer2.Length];
            // 把两个字节数组的元素复制到一个数组中
            Array.Copy(buffer1, 0, data, 0, buffer1.Length);
            Array.Copy(buffer2, 0, data, buffer1.Length, buffer2.Length);
            return Message.CreateMessage(messageVersion, opt.Action, data);
        }
    }

    public class MyOperationBehavior : IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.Formatter = new MyClientFormatter(clientOperation);
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Formatter = new MyServerFormatter(dispatchOperation);
        }

        public void Validate(OperationDescription operationDescription)
        {
            return;
        }
    }

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class MyContractBehaviorAttribute : Attribute, IContractBehavior
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // 示例中只有一个操作协定
            // 故此处只取一个元素即可
            OperationDescription op = contractDescription.Operations[0];
            op.OperationBehaviors.Add(new MyOperationBehavior());
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            // 只取出一个元素即可
            OperationDescription op = contractDescription.Operations[0];
            op.OperationBehaviors.Add(new MyOperationBehavior());
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            return;
        }
    }
    #endregion
}
