using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace TestClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "客户端";

            Uri servUri = new Uri("http://localhost:900");
            // 实例化绑定对象
            BasicHttpBinding binding = new BasicHttpBinding();
            // 创建通道工厂
            IChannelFactory<IRequestChannel> reqfac = null;
            if (binding.CanBuildChannelFactory<IRequestChannel>())
            {
                reqfac = binding.BuildChannelFactory<IRequestChannel>();
            }
            // 创建通道
            if (reqfac != null)
            {
                // 打开通道工厂
                reqfac.Open();
                EndpointAddress epaddr = new EndpointAddress(servUri);
                IRequestChannel channel = reqfac.CreateChannel(epaddr);
                // 必须先打开通道
                channel.Open();
                // 向服务器发送消息
                Message msg = Message.CreateMessage(binding.MessageVersion, "test-request", "你好，我是客户端。");
                Message replymsg = channel.Request(msg);
                // 输出服务器回应的消息
                string rmsg = replymsg.GetBody<string>();
                Console.WriteLine($"从服务器返回的消息：{rmsg}");
                // 关闭通道
                channel.Close();
            }

            Console.Read();
            // 关闭通道工厂
            reqfac.Close();
        }
    }
}
