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
            Uri epaddr = new Uri("http://localhost:9100");
            WSHttpBinding binding = new WSHttpBinding();
            ServiceHost host = new ServiceHost(typeof(DemoService));
            host.AddServiceEndpoint(typeof(IDemo), binding, epaddr);
            host.Open();

            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, new EndpointAddress(epaddr));
            channel.Start();
            int r = channel.SQR(6);
            Console.WriteLine($"计算结果：{r}");
            channel.End();

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract(SessionMode = SessionMode.Required)]
    interface IDemo
    {
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Start();
        [OperationContract(IsInitiating = false, IsTerminating = false)]
        int SQR(int x);
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void End();
    }

    class DemoService : IDemo
    {
        public void Start()
        {
            Console.WriteLine("会话开始。");
        }

        public int SQR(int x)
        {
            return x * x;
        }

        public void End()
        {
            Console.WriteLine("会话结束。");
        }
    }
}
