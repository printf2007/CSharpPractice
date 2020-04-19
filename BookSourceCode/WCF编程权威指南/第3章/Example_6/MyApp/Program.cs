using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.IO;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Product p = new Product
            {
                ID = Guid.NewGuid(),
                DescName = "产品 A",
                Size = 5.33f
            };
            DataContractSerializer sz = new DataContractSerializer(typeof(Product));
            Stream stream = new MemoryStream();
            sz.WriteObject(stream, p);

            stream.Position = 0L;
            XDocument doc = XDocument.Load(stream);
            Console.WriteLine(doc);

            Console.Read();
        }
    }

    [DataContract]
    public class Product
    {
        [DataMember(Order = 3)]
        public Guid ID { get; set; }
        [DataMember(Order = 8)]
        public string DescName { get; set; }
        [DataMember(Order = 6)]
        public float Size { get; set; }
    }
}
