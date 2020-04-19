using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;
using System.Xml;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>
            {
                ["name"] = "小赵",
                ["age"] = "28",
                ["city"] = "广州"
            };
            CustomBodyWriter bw = new CustomBodyWriter(dic);
            Message msg = Message.CreateMessage(MessageVersion.Soap11, "test-data", bw);

            Console.WriteLine($"将消息转为字符串：\n{msg}");

            Console.Read();
        }
    }

    class CustomBodyWriter : BodyWriter
    {
        IDictionary<string, string> m_data;
        public CustomBodyWriter(IDictionary<string, string> data) : base(true)
        {
            m_data = data;
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            // 写入包装数据的元素
            writer.WriteStartElement("student_info");
            // 把字典中的key和value都写入XML元素
            foreach(string k in m_data.Keys)
            {
                writer.WriteElementString(k, m_data[k]);
            }
            // 写入包装元素的结束标记
            writer.WriteEndElement();
            // 将内容写入基础流
            writer.Flush();
        }
    }
}
