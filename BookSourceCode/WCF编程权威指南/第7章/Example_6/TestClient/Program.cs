using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CommonLib;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "测试客户端";

            ChannelFactory<IDemo> factory = new ChannelFactory<IDemo>("cl_ep");
            IDemo cnl = factory.CreateChannel();
            int result = cnl.Add(2, 7);
            Console.WriteLine($"计算结果：{result}");

            factory.Close();
            Console.Read();
        }
    }
}
