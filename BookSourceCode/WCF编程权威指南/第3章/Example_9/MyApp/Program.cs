using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream stream = new MemoryStream();
            Colors c = Colors.B;
            DataContractSerializer sz = new DataContractSerializer(typeof(Colors));
            sz.WriteObject(stream, c);

            stream.Seek(0L, SeekOrigin.Begin);
            XDocument xdoc = XDocument.Load(stream);
            Console.WriteLine("序列化生成的XML文档：\n{0}", xdoc);

            stream.Position = 0L;
            Colors c2 = (Colors)sz.ReadObject(stream);
            Console.WriteLine("\n反序列化后得到的值：{0}", c2);

            Console.Read();
        }
    }

    [DataContract(Namespace = "http://color", Name = "color")]
    enum Colors
    {
        [EnumMember(Value = "red")]
        R,
        [EnumMember(Value = "green")]
        G,
        [EnumMember(Value = "blue")]
        B
    }
}
