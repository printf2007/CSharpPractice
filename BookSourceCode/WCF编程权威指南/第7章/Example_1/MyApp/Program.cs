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
            Console.Title = "异常示例";
            Uri address = new Uri("http://127.0.0.1:7711");
            BasicHttpBinding binding = new BasicHttpBinding();
            ServiceHost host = new ServiceHost(typeof(DemoService));
            // 添加终结点
            //host.AddServiceEndpoint(typeof(IDemo), binding, address);
            // 打开服务主机并开始监听请求
            host.Open();

            // 客户端调用
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, new EndpointAddress(address));
            try
            {
                int r = channel.Add(-2, 7);
                Console.WriteLine($"计算结果：{r}");
            }
            catch(FaultException fex)
            {
                Console.WriteLine($"错误信息：\n{fex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"其他异常信息：\n{ex.Message}");
            }
            (channel as IClientChannel).Close();

            Console.ReadKey();
            // 关闭服务主机
            host.Close();
        }
    }

    #region 服务协定
    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        int Add(int a, int b);
    }
    #endregion

    #region 服务实现类
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    class DemoService : IDemo
    {
        public int Add(int a, int b)
        {
            if (a < 0 || b < 0)
            {
                throw new ArgumentException("参数不能小于0");
            }
            return a + b;
        }
    }
    #endregion
}
