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

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(MyService));

            string uri = "http://localhost:550/act";
            BasicHttpBinding binding = new BasicHttpBinding();

            host.AddServiceEndpoint(typeof(IService), binding, uri);

            host.Open();

            IComputer channel = ChannelFactory<IComputer>.CreateChannel(binding, new EndpointAddress(uri));
            int result = channel.Run(10);
            Console.WriteLine("计算结果：{0}", result);
            ((IClientChannel)channel).Close();

            Console.Read();
            host.Close();
        }
    }
}

namespace 客户端
{
    [ServiceContract(Namespace = "my-sample", Name = "num_sv")]
    public interface IComputer
    {
        [OperationContract(Name = "NumWork", Action = "work-req", ReplyAction = "work-resp")]
        [return: MessageParameter(Name = "outValue")]
        int Run([MessageParameter(Name = "num")]int k);
    }
}

namespace 服务器
{
    [ServiceContract(Namespace = "my-sample", Name = "num_sv")]
    public interface IService
    {
        [OperationContract(Name = "NumWork", Action = "work-req", ReplyAction = "work-resp")]
        [return: MessageParameter(Name = "outValue")]
        int Work([MessageParameter(Name = "num")]int x);
    }

    class MyService : IService
    {
        public int Work(int x)
        {
            return x * x;
        }
    }
}
