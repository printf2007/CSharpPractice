using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Linq;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream stream = new MemoryStream();
            Client.ProductInfo p1 = new Client.ProductInfo
            {
                ID = 1000069,
                ProdName = "测试产品"
            };
            DataContractSerializer sz = new DataContractSerializer(typeof(Client.ProductInfo));
            sz.WriteObject(stream, p1);

            // 输出XML
            stream.Position = 0L;
            XDocument doc = XDocument.Load(stream);
            Console.WriteLine($"序列化后生成的XML文档：\n{doc}");

            Console.WriteLine();

            // 反序列化
            // 注意，运行下面代码会发生异常
            stream.Position = 0L;
            DataContractSerializer dsz = new DataContractSerializer(typeof(Server.Product));
            Server.Product p2 = (Server.Product)dsz.ReadObject(stream);

            stream.Dispose();
            Console.Read();
        }
    }
}

namespace Server
{
    [DataContract(Name = "product", Namespace = "sample-data")]
    class Product
    {
        [DataMember(Name = "id")]
        public int PID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "color", IsRequired = true)]
        public int Color { get; set; }
    }
}

namespace Client
{
    [DataContract(Name = "product", Namespace = "sample-data")]
    class ProductInfo
    {
        [DataMember(Name = "id")]
        public int ID { get; set;}
        [DataMember(Name = "name")]
        public string ProdName { get; set;}
    }
}
