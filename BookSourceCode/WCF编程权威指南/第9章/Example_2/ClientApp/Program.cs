using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CommonLib;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "示例客户端";

            ChannelFactory<IDemo> factory = new ChannelFactory<IDemo>("cl_ep");
            IDemo channel = factory.CreateChannel();
            int result = channel.Cac(7, 2);
            Console.WriteLine($"计算结果：{result}");

            Console.Read();
        }
    }
}
