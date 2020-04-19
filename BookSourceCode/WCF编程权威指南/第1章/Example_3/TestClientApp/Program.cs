using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace TestClientApp
{
    [ServiceContract(Namespace = "demo-sv", Name = "demo")]
    interface ITestService/* : IClientChannel*/
    {
        [OperationContract(Name = "add", Action = "add-opt")]
        int Add(int x, int y);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "客户端";

            // 服务终结点URI
            Uri svepUri = new Uri("http://localhost:900/service");
            // 创建终结点地址包装对象
            EndpointAddress epaddr = new EndpointAddress(svepUri);
            // 创建用于与服务通信的绑定
            BasicHttpBinding binding = new BasicHttpBinding();
            // 引用服务协定
            ITestService svcontractInstance = ChannelFactory<ITestService>.CreateChannel(binding, epaddr);
            // 调用WCF服务
            int res = svcontractInstance.Add(3, 17);
            Console.WriteLine("调用结果：{0}", res);

            // 关闭通道
            ((IClientChannel)svcontractInstance).Close();

            Console.Read();

        }
    }
}
