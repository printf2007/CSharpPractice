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
            Console.Title = "动态终结点示例";

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.Open();

                // 客户端调用
                ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>("dyn_cl");
                IDemo channel = fac.CreateChannel();
                int resval = channel.Mult(9, 7);
                Console.WriteLine($"计算结果：{resval}");
                fac.Close();

                Console.Read();
            }
        }
    }


    [ServiceContract(ConfigurationName = "ical")]
    public interface IDemo
    {
        [OperationContract(Action = "mult")]
        int Mult(int x, int y);
    }

    [ServiceBehavior(ConfigurationName = "dmsv")]
    class DemoService : IDemo
    {
        public int Mult(int x, int y) => x * y;
    }
}
