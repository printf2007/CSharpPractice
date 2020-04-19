using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MyApp
{
    using 客户端;
    using 服务器;
    using 通用协定;

    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host=new ServiceHost(typeof(MyService)))
            {
                host.Open();

                /*--------------------- 客户端调用 ---------------------*/
                //CallbackHandler cbhandler = new CallbackHandler();
                //InstanceContext icontext = new InstanceContext(cbhandler);
                //DuplexChannelFactory<IService> fac = new DuplexChannelFactory<IService>(icontext, "test_ep");
                DuplexChannelFactory<IService> fac = new DuplexChannelFactory<IService>(new CallbackHandler(), "test_ep");
                IService channel = fac.CreateChannel();
                Console.WriteLine("即将调用服务。");
                channel.PostNumber(15);
                Console.WriteLine("服务调用结束。");

                Console.ReadKey();
                fac.Close();
            }
        }
    }
}

namespace 通用协定
{
    /// <summary>
    /// 服务协定
    /// </summary>
    [ServiceContract(Namespace = "http://demo.org", ConfigurationName = "isv", CallbackContract = typeof(ICallback))]
    public interface IService
    {
        [OperationContract(Action = "post_num", IsOneWay = true)]
        void PostNumber(int n);
    }

    /// <summary>
    /// 回调协定
    /// </summary>
    public interface ICallback
    {
        [OperationContract(Action = "report", IsOneWay = true)]
        void Report(double progress);
    }
}

namespace 服务器
{
    using 通用协定;

    [ServiceBehavior(ConfigurationName = "sv")]
    class MyService : IService
    {
        public void PostNumber(int n)
        {
            // 获取回调协定的通道对象
            ICallback cbchannel = OperationContext.Current.GetCallbackChannel<ICallback>();
            // 开始循环
            for (int x = 0; x <= n; x++)
            {
                // 延迟操作
                Task.Delay(500).Wait();
                // 计算处理进度
                double p = Convert.ToDouble(x) / Convert.ToDouble(n);
                // 通过回调报告处理进度
                cbchannel.Report(p);
            }
        }
    }
}

namespace 客户端
{
    using 通用协定;

    class CallbackHandler : ICallback
    {
        public void Report(double progress)
        {
            Console.Write("{0:P0} ", progress);
        }
    }
}
