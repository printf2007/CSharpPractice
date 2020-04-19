using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MyApp
{
    using 服务器;
    using 客户端;

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(TestService), new Uri("http://localhost:3000"));
            host.Open();
            Console.WriteLine("服务已启动。");

            /********************* 客户端调用 **********************/
            IClient channel = ChannelFactory<IClient>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:3000"));
            // 获取来自服务器的数据
            StudentForCL retobj = channel.GetStudent();
            Console.WriteLine($"\n来自服务器的数据：\nName = {retobj.Name}");
            // 修改数据对象
            retobj.Name = "小刘";
            // 传回服务器
            channel.SetStudent(retobj);
            channel.Close();

            Console.Read();
            host.Close();
        }
    }
}

namespace 服务器
{
    [DataContract(Namespace = "sample-data")]
    public class StudentForSV
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "age")]
        public int Age { get; set; }
    }

    [ServiceContract(Name = "test", Namespace = "http://demo.org")]
    public interface IService
    {
        [OperationContract(Name = "getdata", Action = "get-data")]
        StudentForSV GetData();
        [OperationContract(Name = "setdata", Action = "set-data")]
        void SetData([MessageParameter(Name = "obj")]StudentForSV data);
    }

    class TestService : IService
    {
        public StudentForSV GetData()
        {
            StudentForSV stuobj = new StudentForSV();
            stuobj.Name = "小赵";
            stuobj.Age = 21;
            return stuobj;
        }

        public void SetData(StudentForSV data)
        {
            string s = $"\n客户端传回的数据：\nName = {data.Name}\nAge = {data.Age}";
            Console.WriteLine(s);
        }
    }
}

namespace 客户端
{
    [DataContract(Namespace = "sample-data")]
    public class StudentForCL : IExtensibleDataObject
    {
        public ExtensionDataObject ExtensionData { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    [ServiceContract(Name = "test", Namespace = "http://demo.org")]
    public interface IClient : IClientChannel
    {
        [OperationContract(Name = "getdata", Action = "get-data")]
        StudentForCL GetStudent();
        [OperationContract(Name = "setdata", Action = "set-data")]
        void SetStudent([MessageParameter(Name = "obj")] StudentForCL stuinst);
    }
}
