using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Message msg = Message.CreateMessage(MessageVersion.Soap11, "my-msg", "Sample Content");
            // 创建消息头
            MessageHeader hd = MessageHeader.CreateHeader("ItemNumber", "app-test", 2000);
            // 将标头添加到消息头集合中
            msg.Headers.Add(hd);

            // 输出消息的XML文本
            Console.WriteLine(msg);

            Console.WriteLine();

            // 获取消息头内容
            int hdval = msg.Headers.GetHeader<int>("ItemNumber", "app-test");
            // 输出标头的内容
            Console.WriteLine($"消息头的值：{hdval}");
            // 输出消息正文的内容
            Console.WriteLine($"消息正文内容：{msg.GetBody<string>()}");

            Console.Read();
        }
    }
}
