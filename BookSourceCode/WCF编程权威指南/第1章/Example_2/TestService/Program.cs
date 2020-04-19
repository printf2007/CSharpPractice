using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            // 服务基址
            Uri baseUri = new Uri("http://localhost:500");
            using (ServiceHost host=new ServiceHost(typeof(MyService), baseUri))
            {
                // 公开服务的元数据
                ServiceMetadataBehavior metadata = null;
                host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (metadata == null)
                {
                    metadata = new ServiceMetadataBehavior();
                }
                metadata.HttpGetEnabled = true; //必须
                host.Description.Behaviors.Add(metadata);

                // 打开服务
                host.Open();
                Console.WriteLine("服务已运行。");

                Console.Read();
            }
        }
    }

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        double Sqr(double i);
    }

    class MyService : IService
    {
        public double Sqr(double i)
        {
            return i * i;
        }
    }
}
