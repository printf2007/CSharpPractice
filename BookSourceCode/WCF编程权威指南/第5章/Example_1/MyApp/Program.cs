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
            using(ServiceHost host1=new ServiceHost(typeof(DemoService1)), host2=new ServiceHost(typeof(DemoService2)))
            {
                // 分别打开两个服务主机
                host1.Open();
                host2.Open();
                // ……
                Console.Read();
            }
        }
    }


    #region 服务协定
    [ServiceContract]
    public interface IDemoService1
    {
        [OperationContract]
        void SayHello();
    }

    [ServiceContract]
    public interface IDemoService2
    {
        [OperationContract]
        void SayGoodbye();
    }
    #endregion

    #region 服务类
    class DemoService1 : IDemoService1
    {
        public void SayHello()
        {
            // ……
        }
    }

    class DemoService2 : IDemoService2
    {
        public void SayGoodbye()
        {
            // ……
        }
    }
    #endregion

}
