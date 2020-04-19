using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Routing;

namespace RoutingServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "路由服务";

            using(ServiceHost host=new ServiceHost(typeof(RoutingService)))
            {
                host.Opened += (a, b) => Console.WriteLine("路由服务已经启动。");
                host.Open();

                Console.Read();
            }
        }
    }
}
