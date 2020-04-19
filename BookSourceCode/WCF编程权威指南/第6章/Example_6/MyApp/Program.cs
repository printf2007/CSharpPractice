using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "InstanceContextMode示例";

            using(ServiceHost host=new ServiceHost(typeof(DemoService)))
            {
                Uri epaddress = new Uri("http://127.0.0.1:6060/demo");
                WSHttpBinding binding = new WSHttpBinding();
                // 添加终结点
                host.AddServiceEndpoint(typeof(IDemo), binding, epaddress);
                // 打开服务主机
                host.Open();

                // 客户端调用
                IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, new EndpointAddress(epaddress));
                // 调用三次
                channel.Hello();
                channel.Hello();
                channel.Hello();
                // 关闭通道
                ((IClientChannel)channel).Close();

                Console.ReadKey();
            }
        }
    }

    [ServiceContract]
    interface IDemo
    {
        [OperationContract]
        void Hello();
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class DemoService : IDemo
    {
        public DemoService()
        {
            Console.WriteLine("服务正在实例化。");
        }

        public void Hello()
        {
            Console.WriteLine("你好，服务操作已调用。");
        }
    }
}
