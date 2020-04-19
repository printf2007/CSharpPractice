using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CommonLib;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务器";
            using(ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.Open();

                Console.WriteLine("服务已经启动。");
                Console.Read();
            }
        }
    }


    #region 服务类
    class DemoService : IDemo
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
    #endregion
}
