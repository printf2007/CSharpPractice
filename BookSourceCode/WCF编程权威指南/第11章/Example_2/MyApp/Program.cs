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
            using(WebServiceHost host = new WebServiceHost(typeof(TestService)))
            {
                host.AddServiceEndpoint(typeof(ITest), new WebHttpBinding(), "http://localhost:323");
                host.Open();
                Console.WriteLine("服务已经启动。");
                Console.Read();
            }
        }
    }

    [ServiceContract]
    public interface ITest
    {
        [OperationContract]
        [WebGet(UriTemplate = "/add?a={n1}&b={n2}")]
        int Add(int n1, int n2);
    }

    class TestService : ITest
    {
        public int Add(int n1, int n2)
        {
            return n1 + n2;
        }
    }
}
