using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace TestClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "测试客户端";

            // 路由服务器公开的三个终结点地址
            EndpointAddress rout_addr1 = new EndpointAddress("http://127.0.0.1:8007/rout");
            EndpointAddress rout_addr2 = new EndpointAddress("http://127.0.0.1:8008/rout");
            EndpointAddress rout_addr3 = new EndpointAddress("http://127.0.0.1:8009/rout");
            // 创建三个通道，分别将消息发送到三个路由终结点上
            ICalcService c1 = ChannelFactory<ICalcService>.CreateChannel(new BasicHttpBinding(), rout_addr1);
            ICalcService c2 = ChannelFactory<ICalcService>.CreateChannel(new BasicHttpBinding(), rout_addr2);
            ICalcService c3 = ChannelFactory<ICalcService>.CreateChannel(new BasicHttpBinding(), rout_addr3);

            double m = 6.0d, n = 8.0d, r = 0d;

            Console.WriteLine("消息将发往第一个路由地址。");
            r = c1.Calc(m, n);
            Console.WriteLine($"输入参数：{m}、{n}，计算结果：{r}。\n");

            Console.WriteLine("消息将发往第二个路由地址。");
            r = c2.Calc(m, n);
            Console.WriteLine($"输入参数：{m}、{n}，计算结果：{r}。\n");

            Console.WriteLine("消息将发往第三个路由地址。");
            r = c3.Calc(m, n);
            Console.WriteLine($"输入参数：{m}、{n}，计算结果：{r}。\n");

            Console.Read();
            c1.Close();
            c2.Close();
            c3.Close();
        }
    }

    [ServiceContract(Namespace = "sample-service", Name = "demo")]
    public interface ICalcService : IClientChannel
    {
        [OperationContract(Name = "calc", Action = "op-calc")]
        double Calc(double a, double b);
    }
}
