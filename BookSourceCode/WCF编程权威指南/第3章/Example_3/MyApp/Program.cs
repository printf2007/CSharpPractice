using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MyApp
{
    using 服务器;
    using 客户端;

    class Program
    {
        static void Main(string[] args)
        {
            Uri svuri = new Uri("http://localhost:3000");
            ServiceHost host = new ServiceHost(typeof(MyService), svuri);
            host.Open();

            // 调用服务
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress epaddr = new EndpointAddress(svuri);
            IService channel = ChannelFactory<IService>.CreateChannel(binding, epaddr);
            int res = channel.Add(6, 10);
            Console.WriteLine("计算结果：{0}", res);
            ((IClientChannel)channel).Close();

            Console.Read();
            host.Close();
        }
    }
}

namespace 服务器
{
    [ServiceContract(Namespace = "http://someone.net", Name = "demo")]
    class MyService
    {
        [OperationContract(Name = "add", Action = "add", ReplyAction = "addResponse")]
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}

namespace 客户端
{
    [ServiceContract(Namespace = "http://someone.net", Name = "demo")]
    public interface IService
    {
        [OperationContract(Name = "add", Action = "add", ReplyAction = "addResponse")]
        int Add(int a, int b);
    }
}
