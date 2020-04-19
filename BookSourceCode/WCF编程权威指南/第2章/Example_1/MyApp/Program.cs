using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Message msg1 = Message.CreateMessage(MessageVersion.Soap11, "test-action", "你好，这是一条SOAP消息。");
            Console.WriteLine("第一条消息的文本：\n{0}\n", msg1);

            Employee emp = new Employee
            {
                Name = "小刘",
                Age = 32,
                ID = "T-120005"
            };
            Message msg2 = Message.CreateMessage(MessageVersion.Soap12, "demo-opt", emp);
            Console.WriteLine("第二条消息的内容：\n{0}", msg2);

            Console.Read();
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string ID { get; set; }
    }
}
