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
            Console.Title = "示例服务";

            Uri address = new Uri("http://127.0.0.1:5000");
            using(ServiceHost host=new ServiceHost(typeof(DemoSv), address))
            {
                host.Open();

                // 客户端调用
                Action act = () =>
                {
                    using(ChannelFactory<IDemo> fac= new ChannelFactory<IDemo>(new BasicHttpBinding(), new EndpointAddress(address)))
                    {
                        IDemo cn = fac.CreateChannel();
                        cn.Run();
                    }
                };
                Task[] tasks =
                {
                    new Task(act),
                    new Task(act),
                    new Task(act),
                    new Task(act),
                    new Task(act)
                };
                foreach (Task t in tasks)
                {
                    t.Start();
                }
                Task.WaitAll(tasks);

                Console.ReadKey();
            }
        }
    }

    #region 服务2的协定
    [ServiceContract(Namespace = "samples", Name = "sv2")]
    interface IService2
    {
        [OperationContract(Name = "getNum", Action = "get-num")]
        int GetNumber();
    }
    #endregion

    [ServiceContract]
    interface IDemo
    {
        [OperationContract]
        void Run();
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    class DemoSv : IDemo
    {
        public void Run()
        {
            Console.WriteLine("即将调用【服务 2】");
            // 调用另一个服务
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://127.0.0.1:6000");
            IService2 cn2 = ChannelFactory<IService2>.CreateChannel(binding, address);
            int r = cn2.GetNumber();
            (cn2 as IClientChannel).Close(); //关闭通道
            Console.WriteLine($"【服务 2】调用完成，获得结果：{r}");
        }
    }
}
