using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(DemoService));

            // 准备绑定元素
            // 使用文本编码
            TextMessageEncodingBindingElement encoding = new TextMessageEncodingBindingElement();
            // 使用HTTP传输协议
            HttpTransportBindingElement transport = new HttpTransportBindingElement();
            // 创建自定义绑定
            CustomBinding binding = new CustomBinding(encoding, transport);
            // 添加终结点
            host.AddServiceEndpoint(typeof(IDemoService), binding, "http://localhost:900/demo");

            host.Open();

            // 客户端调用
            IDemoService channel = ChannelFactory<IDemoService>.CreateChannel(binding, new EndpointAddress("http://localhost:900/demo"));
            channel.SayHello("Tom");
            ((IClientChannel)channel).Close();

            Console.ReadKey();
            host.Close();
        }
    }


    [ServiceContract]
    public interface IDemoService
    {
        [OperationContract]
        void SayHello(string name);
    }

    internal class DemoService : IDemoService
    {
        public void SayHello(string name)
        {
            Console.WriteLine("Hello, {0}.", name);
        }
    }
}
