using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Service));

            ContractDescription cd = ContractDescription.GetContract(typeof(IDemo));
            BasicHttpBinding binding = new BasicHttpBinding();
            // 准备地址头
            AddressHeader addrHeader1 = AddressHeader.CreateAddressHeader("filename", "urn:files", "test.docx");
            AddressHeader addrHeader2 = AddressHeader.CreateAddressHeader("filename", "urn:files", "face.png");
            EndpointAddress svepAddr = new EndpointAddress(new Uri("http://127.0.0.1:7000"), addrHeader1, addrHeader2);
            ServiceEndpoint svept = new ServiceEndpoint(cd, binding, svepAddr);
            host.AddServiceEndpoint(svept);

            host.Open();

            /************************* 客户端调用 ************************/
            // 地址头
            AddressHeader hd1 = AddressHeader.CreateAddressHeader("filename", "urn:files", "test.docx");
            AddressHeader hd2 = AddressHeader.CreateAddressHeader("filename", "urn:files", "face.png");
            // 终结点地址
            EndpointAddress epaddress = new EndpointAddress(new Uri("http://127.0.0.1:7000"), hd1, hd2);
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(new BasicHttpBinding(), epaddress);
            channel.Run(); //调用服务

            Console.ReadKey();
            host.Close();

        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void Run();
    }

    class Service : IDemo
    {
        public void Run()
        {
            var headers = OperationContext.Current.IncomingMessageHeaders;
            /*
             * 以下代码会出错
            string content = headers.GetHeader<string>("filename", "urn:files");
            Console.WriteLine($"地址头的值：{content}");
            */

            int hdcount = headers.Count; //总数
            List<string> vals = new List<string>();
            for (int i = 0; i < hdcount; i++)
            {
                MessageHeaderInfo info = headers[i];
                // 只提取需要的消息头
                if (info.Name == "filename" && info.Namespace == "urn:files")
                {
                    string temp = headers.GetHeader<string>(i);
                    vals.Add(temp);
                }
            }
            // 输出结果
            Console.WriteLine($"获取的地址头：{string.Join("、", vals.ToArray())}");
        }
    }

}
