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
            Stream stream = new MemoryStream();
            Car car = new Car();
            car.Speed = 96.3d;
            car.Color = 3224;
            car.Remarks = "示例文本";

            DataContractSerializer sz = new DataContractSerializer(typeof(Car));
            sz.WriteObject(stream, car);

            stream.Seek(0L, SeekOrigin.Begin);
            XDocument xdoc = XDocument.Load(stream);
            Console.WriteLine(xdoc);

            stream.Dispose();
            Console.Read();
        }
    }

    [DataContract(Namespace = "http://demo", Name = "car")]
    public class Car
    {
        [DataMember(Name = "speed")]
        public double Speed;
        [DataMember(Name = "color")]
        public int Color;
        [IgnoreDataMember]
        public string Remarks;
    }
}
