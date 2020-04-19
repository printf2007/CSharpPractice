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
            Console.Title = "可靠会话示例";

            ServiceHost host = new ServiceHost(typeof(DemoService));
            host.Open();

            ChannelFactory<IDemo> f = new ChannelFactory<IDemo>("cl");
            IDemo c = f.CreateChannel();
            c.Step1();
            c.Step2();
            c.Step3();
            f.Close();

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract(Namespace = "my-app", Name = "demo")]
    public interface IDemo
    {
        [OperationContract(Action = "step1")]
        void Step1();
        [OperationContract(Action = "step2")]
        void Step2();
        [OperationContract(Action = "step3")]
        void Step3();
    }

    class DemoService : IDemo
    {
        public void Step1()
        {
            Console.WriteLine("步骤1已执行。");
        }

        public void Step2()
        {
            Console.WriteLine("步骤2已执行。");
        }

        public void Step3()
        {
            Console.WriteLine("步骤3已执行。");
        }
    }
}
