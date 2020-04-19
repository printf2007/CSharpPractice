using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Globalization;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "自定义错误信息";

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.Open();

                ChannelFactory<IDemo> fact = new ChannelFactory<IDemo>("ep_client");
                IDemo channel = fact.CreateChannel();
                try
                {
                    string res = channel.SayHello("B");
                    Console.WriteLine($"调用结果：{res}");
                }
                catch(FaultException fex)
                {
                    if (fex.Reason != null)
                    {
                        FaultReason reason = fex.Reason;
                        // 获取错误信息
                        FaultReasonText txt = reason.GetMatchingTranslation();
                        Console.WriteLine($"错误：{txt.Text}");
                    }
                }
                fact.Close();

                Console.ReadKey();
            }
        }
    }

    #region 服务协定

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        string SayHello(string name);
    }

    #endregion

    #region 服务实现类
    class DemoService : IDemo
    {
        public string SayHello(string name)
        {
            if (name.Length < 2)
            {
                // 错误描述文本
                FaultReasonText faulttext = new FaultReasonText("名字长度不能小于2");
                FaultReason reason = new FaultReason(faulttext);
                // 抛出异常
                throw new FaultException(reason);
            }
            return $"你好，{name}。";
        }
    }
    #endregion
}
