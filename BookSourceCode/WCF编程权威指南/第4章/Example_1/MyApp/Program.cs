using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Binding[] bindings =
            {
                new BasicHttpBinding(),
                new NetTcpBinding(),
                new WSHttpBinding()
            };

            foreach (Binding b in bindings)
            {
                Console.WriteLine($"绑定类型：{b.GetType().Name}");
                Console.WriteLine($"URI架构：{b.Scheme}，例如 {b.Scheme}://localhost:8080/service");
                Console.WriteLine("绑定对象中包含的绑定元素：");
                var bdElements = b.CreateBindingElements();
                foreach (var ele in bdElements)
                {
                    Console.WriteLine(ele.GetType().Name);
                }
                Console.WriteLine("--------------------------------------------");
            }

            Console.ReadKey();
        }
    }
}
