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
            // 服务终结点地址
            Uri epaddrUri = new Uri("http://localhost:1356/demo");
            // 通信所使用的绑定
            Binding binding = new WSHttpBinding();
            // 初始化服务主机
            ServiceHost host = new ServiceHost(typeof(DemoService));
            // 添加终结点
            host.AddServiceEndpoint(typeof(IDemo), binding, epaddrUri);
            // 运行服务
            host.Open();

            // 客户端调用
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, new EndpointAddress(epaddrUri));
            channel.Add1(10);
            channel.Add2(20);
            // 获取最终结果
            Console.WriteLine($"\n最后的计算结果：{channel.GetResult()}");
            // 关闭通道
            ((IClientChannel)channel).Close();

            Console.ReadKey();
            // 关闭服务
            host.Close();
        }
    }

    [ServiceContract(SessionMode = SessionMode.Allowed)]
    interface IDemo
    {
        [OperationContract(Action = "add-1", IsOneWay = true)]
        void Add1(int m);
        [OperationContract(Action = "add-2", IsOneWay = true)]
        void Add2(int n);
        [OperationContract(Action = "get-result")]
        int GetResult();
    }

    class DemoService : IDemo
    {
        int myValue;

        public DemoService()
        {
            // 初始化
            myValue = 0;
        }

        public void Add1(int m)
        {
            PrintSessionID();
            myValue += m;
        }

        public void Add2(int n)
        {
            PrintSessionID();
            myValue += n;
        }

        public int GetResult()
        {
            PrintSessionID();
            return myValue;
        }

        /// <summary>
        /// 打印会话ID
        /// </summary>
        private void PrintSessionID()
        {
            var context = OperationContext.Current;
            string msg = $"{context.IncomingMessageHeaders.Action,-10}操作被调用，会话ID为：{context.SessionId}";
            Console.WriteLine(msg);
        }
    }
}
