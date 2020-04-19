using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Security.Permissions;
using System.Security.Cryptography.X509Certificates;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "授权与角色示例";

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                // 设置服务器证书
                host.Credentials.ServiceCertificate.Certificate = LoadCerFromFile();
                host.Open();

                // 客户端调用
                EndpointAddress epaddr = new EndpointAddress(new Uri("http://localhost:8611/dm"), EndpointIdentity.CreateRsaIdentity(LoadCerFromFile()));
                WSHttpBinding binding = new WSHttpBinding("wsbd");
                using (ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>(binding, epaddr))
                {
                    fac.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                    // 设置用户名与密码
                    fac.Credentials.UserName.UserName = "Tim";
                    fac.Credentials.UserName.Password = "456";

                    IDemo cn = fac.CreateChannel();
                    try
                    {
                        cn.SayHello();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Console.Read();
            }
        }

        static X509Certificate2 LoadCerFromFile()
        {
            X509Certificate2 cert = new X509Certificate2("testCert.pfx", "123");
            return cert;
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void SayHello();
    }

    class DemoService : IDemo
    {
        [PrincipalPermission(SecurityAction.Demand, Role = Roles.Admin)]
        public void SayHello()
        {
            Console.WriteLine("服务已调用。");
        }
    }
}
