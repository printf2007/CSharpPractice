using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost host = new WebServiceHost(typeof(MyService)))
            {
                host.AddServiceEndpoint(typeof(IDemo), new WebHttpBinding(), "http://localhost:9090");
                host.Open();
                Console.WriteLine("服务已启动。");
                Console.Read();
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        DateTime GetTime();
    }

    class MyService : IDemo
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }
}
