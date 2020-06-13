using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;

namespace MessageDemo
{
      class Program
      {
            static void Main ( string [ ] args )
            {
                  Message msg1 = Message.CreateMessage(MessageVersion.Soap11, "test-action", "你好，这是一条SOAP消息。");
                  MessageHeader hd1=MessageHeader.CreateHeader("ItemNumber","app_test",2000);
                  msg1.Headers.Add(hd1);
                  Console.WriteLine("第一条消息的文本：\n{0}\n" , msg1);
                  int hdval=msg1.Headers.GetHeader<int>("ItemNumber","app_test");
                  Console.WriteLine($"value is {hdval}");
                  Console.WriteLine($"Message body is {msg1.GetBody<string>()}");

                  Employee emp=new Employee{Name="Jacky",Age=10 };
                  Message msg=Message.CreateMessage(MessageVersion.Soap12,"test/opt",emp);
                  Console.WriteLine("This is a employee demo:{0}",msg);
                  Console.ReadLine( );
            }
      }

      public class Employee
      {
            public string Name { get; set; }
            public int Age { get; set; }

      }
}
