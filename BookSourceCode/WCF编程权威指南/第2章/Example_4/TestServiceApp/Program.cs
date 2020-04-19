using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace TestServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务器";

            // 服务器侦听连接的地址
            Uri listUri = new Uri("http://localhost:900");
            // 实例化绑定对象
            BasicHttpBinding binding = new BasicHttpBinding();
            // 创建通道侦听器
            IChannelListener<IReplyChannel> listener = null;
            if (binding.CanBuildChannelListener<IReplyChannel>())
            {
                listener = binding.BuildChannelListener<IReplyChannel>(listUri);

                // 必须调用Open方法
                listener.Open();
                while (true)
                {
                    IReplyChannel reply = listener.AcceptChannel();
                    // 打开通道
                    reply.Open();
                    // 接收客户端的消息
                    RequestContext context = reply.ReceiveRequest();
                    // 获取客户端发送的内容
                    string msg = context.RequestMessage.GetBody<string>();
                    Console.WriteLine($"来自客户端的消息：{msg}");
                    // 回复消息
                    Message replymsg = Message.CreateMessage(binding.MessageVersion, "test-reply", "已收到消息。");
                    context.Reply(replymsg);
                    // 关闭通道
                    reply.Close();

                    Console.WriteLine("请按任意键继续，按Esc键退出。");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                        break;
                }
                // 关闭侦听器
                listener.Close();
            }
        }
    }
}
