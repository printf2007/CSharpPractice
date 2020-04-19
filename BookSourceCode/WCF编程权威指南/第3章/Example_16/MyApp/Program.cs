using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri svaddress = new Uri("http://localhost:7133");
            ServiceHost host = new ServiceHost(typeof(DemoService), svaddress);
            host.Open();
            Console.WriteLine("服务已启动。\n");

            // 客户端调用
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress epaddress = new EndpointAddress(svaddress);
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, epaddress);
            V1.InfoItem item1 = new V1.InfoItem
            {
                Name = "Jack",
                Age = 22,
                Gender = 1
            };
            channel.Post_V1(item1);
            V2.InfoItem item2 = new V2.InfoItem
            {
                Name = "Lucy",
                Age = 20,
                Gender = 0
            };
            channel.Post_V2(item2);

            ((IClientChannel)channel).Close();

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract(Namespace = "service-ct-root", Name = "DemoService")]
    public interface IDemo
    {
        [OperationContract(Name = "postV1", Action = "post-v1")]
        void Post_V1(V1.InfoItem pItem);
        [OperationContract(Name = "postV2", Action = "post-v2")]
        void Post_V2(V2.InfoItem pItem);
    }

    internal class DemoService : IDemo
    {
        public void Post_V1(V1.InfoItem pItem)
        {
            Message msg = OperationContext.Current.RequestContext.RequestMessage;
            // 获取当前调用的Action头内容
            string curAction = msg.Headers.Action;
            Console.Write("\n\n");
            Console.WriteLine("操作 {0} 被调用。", curAction);
            Console.WriteLine($"传递的信息：Name = {pItem.Name}，Age = {pItem.Age}，Gender = {pItem.Gender}");
            // 输出客户端请求的SOAP消息
            Console.WriteLine($"生成SOAP消息：\n{msg}");
        }

        public void Post_V2(V2.InfoItem pItem)
        {
            Message msg = OperationContext.Current.RequestContext.RequestMessage;
            string action = msg.Headers.Action;
            Console.Write("\n\n");
            Console.WriteLine("操作 {0} 被调用。", action);
            Console.WriteLine($"收到的信息：Name = {pItem.Name}，Age = {pItem.Age}，Gender = {pItem.Gender}");
            Console.WriteLine($"生成的SOAP消息：\n{msg}");
        }
    }
}

namespace V1
{
    [MessageContract(IsWrapped = true, WrapperNamespace = "demo-msgc-root", WrapperName = "MyMessage")]
    public class InfoItem
    {
        [MessageBodyMember(Namespace = "demo-data-item", Name = "stu_name")]
        public string Name { get; set; }
        [MessageBodyMember(Namespace = "demo-data-item", Name = "stu_age")]
        public int Age { get; set; }
        [MessageBodyMember(Namespace = "demo-data-item", Name = "stu_gender")]
        public byte Gender { get; set; }
    }
}

namespace V2
{
    [MessageContract(IsWrapped = false, WrapperNamespace = "demo-msgc-root", WrapperName = "MyMessage")]
    public class InfoItem
    {
        [MessageBodyMember(Namespace = "demo-data-item", Name = "stu_name")]
        public string Name { get; set; }
        [MessageBodyMember(Namespace = "demo-data-item", Name = "stu_age")]
        public int Age { get; set; }
        [MessageBodyMember(Namespace = "demo-data-item", Name = "stu_gender")]
        public byte Gender { get; set; }
    }
}
