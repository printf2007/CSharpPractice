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
            // 将序列化的内容写入内存流
            MemoryStream stream = new MemoryStream();
            DataContractSerializer dsz = new DataContractSerializer(typeof(Student));
            Student stuobj = new Student
            {
                Name = "小张",
                Age = 23,
                Birthday = new DateTime(1988, 7, 12)
            };
            // 序列化
            dsz.WriteObject(stream, stuobj);

            stream.Position = 0L;
            XDocument xmldoc = XDocument.Load(stream);
            Console.WriteLine($"\n序列化后的XML文档：\n{xmldoc}");

            // 反序列化
            stream.Position = 0L;
            Student scobj = (Student)dsz.ReadObject(stream);
            Console.WriteLine($"\n反序列化后：\nName = {scobj.Name}\nAge = {scobj.Age}\nBirthday = {scobj.Birthday:d}");

            stream.Dispose();
            Console.Read();
        }
    }

    [DataContract(Namespace = "http://demo", Name = "student")]
    class Student
    {
        [DataMember(Name = "name")]
        internal string Name { get; set; }
        [DataMember(Name = "age")]
        internal int Age { get; set; }
        [DataMember(Name = "birthday")]
        internal DateTime Birthday { get; set; }
    }
}
