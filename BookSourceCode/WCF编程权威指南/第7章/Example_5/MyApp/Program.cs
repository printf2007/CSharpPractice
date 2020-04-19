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
            Console.Title = "统一错误处理";

            Uri address = new Uri("http://localhost:7022/demo");
            BasicHttpBinding binding = new BasicHttpBinding();

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IDemo), binding, address);
                MyEndpointBehavior bhv = new MyEndpointBehavior();
                ep.EndpointBehaviors.Add(bhv);

                host.Open();

                // 客户端调用
                IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, new EndpointAddress(address));
                // 调用第一个方法
                try
                {
                    channel.DeleteData(-3);
                }
                catch(FaultException fault)
                {
                    string errTxt = fault.Reason.GetMatchingTranslation().Text;
                    Console.WriteLine($"错误信息：{errTxt}。");
                }
                // 调用第二个方法
                try
                {
                    channel.UpdateAll();
                }
                catch(FaultException fault)
                {
                    string errTxt = fault.Reason.GetMatchingTranslation().Text;
                    Console.WriteLine($"错误信息：{errTxt}。");
                }

                Console.Read();
            }
        }
    }

    #region 服务协定与实现
    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void DeleteData(int dataID);
        [OperationContract]
        void UpdateAll();
    }

    class DemoService : IDemo
    {
        public void DeleteData(int dataID)
        {
            if (dataID < 0)
            {
                throw new ArgumentException("传入的参数不能小于0");
            }
        }

        public void UpdateAll()
        {
            throw new InvalidOperationException("无法连接数据库");
        }
    }
    #endregion

    #region 扩展
    class MyCustErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            // 直接返回true，表示由开发人员处理异常
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultException faultExc = new FaultException(error.Message);
            // 从FaultException产生Fault消息
            MessageFault msgFault = faultExc.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, "my-demo-error");
        }
    }

    class MyEndpointBehavior : IEndpointBehavior
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
            MyCustErrorHandler handler = new MyCustErrorHandler();
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(handler);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }
    }
    #endregion
}
