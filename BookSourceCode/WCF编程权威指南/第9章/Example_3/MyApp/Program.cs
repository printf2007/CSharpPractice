using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "示例程序";

            // 服务终结点地址
            Uri svepAddrUri = new Uri("http://localhost:4618/demo");
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.Message);
            // 客户不使用凭据
            binding.Security.Message.ClientCredentialType = MessageCredentialType.None;

            ServiceHost host = new ServiceHost(typeof(DemoService));
            // 添加终结点
            host.AddServiceEndpoint(typeof(IDemo), binding, svepAddrUri);
            // 从.pfx文件中加载证书
            X509Certificate2 cer = new X509Certificate2("testCert.pfx", "123456");
            // 设置服务器证书
            host.Credentials.ServiceCertificate.Certificate = cer;
            // 打开服务
            host.Opened += (x, y) =>
            {
                Console.WriteLine("服务已经启动。");
            };
            host.Open();

            // 以下代码模拟客户端调用
            // 直接将服务器证书的公钥作为终结点标识
            EndpointAddress epaddr = new EndpointAddress(svepAddrUri, EndpointIdentity.CreateRsaIdentity(cer.PublicKey.Key.ToXmlString(false)));
            ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>(binding, epaddr);
            // 取消对服务器证书的可信任验证
            fac.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            IDemo channel = fac.CreateChannel();
            int callres = channel.Add(58, 36);
            Console.WriteLine($"服务调用结果：{callres}");
            fac.Close();

            Console.Read();
            host.Close();
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        int Add(int a, int b);
    }

    class DemoService : IDemo
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
