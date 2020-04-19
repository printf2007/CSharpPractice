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
            Uri svaddr = new Uri("http://localhost:7730");

            ServiceHost host = new ServiceHost(typeof(DemoService), svaddr);
            host.Open();

            /*-------------------------------------------------------*/
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress epaddr = new EndpointAddress(svaddr);
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, epaddr);

            // 第一次调用
            using (OperationContextScope scope = new OperationContextScope((IContextChannel)channel))
            {
                // 插入自定义消息头
                OperationContext context = OperationContext.Current;
                MessageHeader hd = MessageHeader.CreateHeader("att_data", "sample-data", "e6acd75f6cab2cdef2d1");
                context.OutgoingMessageHeaders.Add(hd);

                channel.Run();
            }

            // 第二次调用
            channel.Run();

            ((IClientChannel)channel).Close();
            /*-------------------------------------------------------*/

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void Run();
    }

    class DemoService : IDemo
    {
        public void Run()
        {
            OperationContext opcontext = OperationContext.Current;
            // 输出SOAP消息头
            Console.WriteLine("\n");
            int c = opcontext.IncomingMessageHeaders.Count;
            for (int i = 0; i < c; i++)
            {
                MessageHeaderInfo info = opcontext.IncomingMessageHeaders[i];
                Console.WriteLine($"元素名：{info.Name}");
                Console.WriteLine($"命名空间：{info.Namespace}");
                Console.WriteLine($"内容：{opcontext.IncomingMessageHeaders.GetHeader<string>(i)}");
                Console.WriteLine("--------------------------------------------");
            }
        }
    }
}
