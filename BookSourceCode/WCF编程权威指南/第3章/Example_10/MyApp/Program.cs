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
            Student stu = new Student();
            stu.Name = "小明";
            stu.Age = 22;
            AddressInfo addr = new AddressInfo();
            addr.Province = "山东";
            addr.City = "济南";
            addr.ZipCode = "250000";
            stu.Address = addr;

            Stream stream = new MemoryStream();

            // 添加已知类型的方法一
            //List<Type> types = new List<Type>();
            //types.Add(typeof(AddressInfo));
            //DataContractSerializer sz = new DataContractSerializer(typeof(Student), types);

            // 添加已知类型的方法二
            DataContractSerializerSettings setting = new DataContractSerializerSettings();
            setting.KnownTypes = new Type[] { typeof(AddressInfo) };
            DataContractSerializer sz = new DataContractSerializer(typeof(Student), setting);

            sz.WriteObject(stream, stu);

            stream.Position = 0L;
            XDocument doc = XDocument.Load(stream);
            Console.WriteLine($"序列化生成的XML文档：\n{doc}");

            Console.WriteLine("\n\n");
            stream.Position = 0L;
            Student stu2 = (Student)sz.ReadObject(stream);
            Console.WriteLine($"姓名：{stu2.Name}");
            Console.WriteLine($"年龄：{stu2.Age}");
            Console.WriteLine("地址信息：\n---------------------");
            AddressInfo addrinfo = stu2.Address as AddressInfo;
            Console.WriteLine($"{addrinfo.Province}省{addrinfo.City}市，邮编：{addrinfo.ZipCode}");

            stream.Dispose();
            Console.Read();
        }
    }

    // 添加已知类型的方法三
    [DataContract]
    [KnownType(typeof(AddressInfo))]
    public class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public object Address { get; set; }
    }

    [DataContract]
    public class AddressInfo
    {
        [DataMember]
        public string Province { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
    }

}
