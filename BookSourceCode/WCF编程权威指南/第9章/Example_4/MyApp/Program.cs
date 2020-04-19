using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri svEndpointUri = new Uri("http://localhost:9711/test");
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.Message);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                // 添加终结点
                host.AddServiceEndpoint(typeof(IDemo), binding, svEndpointUri);
                // 设置服务器证书
                host.Credentials.ServiceCertificate.Certificate = LoadCertFromFile();
                // 设置用户名/密码的验证模型为Windows帐户集成
                host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Windows;
                host.Credentials.UserNameAuthentication.IncludeWindowsGroups = true;
                // 打开服务
                host.Open();

                // 客户端调用
                EndpointAddress epaddr = new EndpointAddress(svEndpointUri, EndpointIdentity.CreateRsaIdentity(LoadCertFromFile()));
                ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>(binding, epaddr);
                fac.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                // 设置用户名和密码
                fac.Credentials.UserName.UserName = "user-name";
                fac.Credentials.UserName.Password = "<password>";
                IDemo cnl = fac.CreateChannel();
                cnl.RunWork();
                fac.Close();

                Console.Read();
            }

            #region 内联函数
            X509Certificate2 LoadCertFromFile()
            {
                string pfxFile = "testCert.pfx";
                string pfxPwd = "123";
                X509Certificate2 cert = new X509Certificate2(pfxFile, pfxPwd);
                return cert;
            }
            #endregion

        }
    }

    #region 协定
    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void RunWork();
    }

    class DemoService : IDemo
    {
        public void RunWork()
        {
            Console.WriteLine("服务已调用。");
        }
    }
    #endregion
}
