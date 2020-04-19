using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                // 通过代码来添加Behavior
                //CustEndpointBehavior epbhv = new CustEndpointBehavior();
                //foreach (var endpoint in host.Description.Endpoints)
                //{
                //    endpoint.EndpointBehaviors.Add(epbhv);
                //}

                host.Open();

                /****************************************************/
                ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>("client_ep");

                // 通过代码来应用Behavior
                //fac.Endpoint.EndpointBehaviors.Add(new CustEndpointBehavior());

                IDemo cn = fac.CreateChannel();
                cn.Hello();
                fac.Close();

                Console.Read();
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void Hello();
    }

    class DemoService : IDemo
    {
        public void Hello()
        {
            Console.WriteLine("服务已调用。");
        }
    }


    #region 扩展
    public class CustEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // 无需处理，直接返回
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // 当该扩展在客户端上应用时会调用该方法
            // 开发人员可以在此处添加自定义处理代码
            // 以下为演示代码
            Console.WriteLine($"{GetType().Name}已在客户端上应用。");
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // 当该扩展在服务器上应用时，会调用该方法
            // 开发人员可以在此处添加自定义代码
            // 以下代码只做简单演示
            Console.WriteLine($"{GetType().Name}已在服务器上应用。");
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // 无需处理，直接返回
            return;
        }
    }

    public class CustEndpointBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(CustEndpointBehavior);

        protected override object CreateBehavior()
        {
            return new CustEndpointBehavior();
        }
    }
    #endregion
}
