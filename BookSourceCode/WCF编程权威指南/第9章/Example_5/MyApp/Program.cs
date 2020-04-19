using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Uri epuri = new Uri("http://localhost:3156");
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.Message);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

            using (ServiceHost host = new ServiceHost(typeof(DemoService)))
            {
                host.AddServiceEndpoint(typeof(IDemo), binding, epuri);
                // 设置服务器证书
                host.Credentials.ServiceCertificate.Certificate = LoadCertFromPfx();
                // 配置自定义用户和密码验证类
                host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new CustUsernamePasswordVal();

                host.Open();

                // 客户端调用
                EndpointAddress epaddr = new EndpointAddress(epuri, EndpointIdentity.CreateRsaIdentity(LoadCertFromPfx()));
                using (ChannelFactory<IDemo> fac = new ChannelFactory<IDemo>(binding, epaddr))
                {
                    fac.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                    // 设置用户名和密码
                    fac.Credentials.UserName.UserName = "Jack";
                    fac.Credentials.UserName.Password = "abc654";

                    IDemo cn = fac.CreateChannel();
                    double r = cn.Work(5.0d, 12.6d);
                    Console.WriteLine($"调用结果：{r}");
                }

                Console.Read();
            }
        }


        static X509Certificate2 LoadCertFromPfx()
        {
            X509Certificate2 cer = new X509Certificate2("testCert.pfx", "123");
            return cer;
        }

    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        double Work(double x, double y);
    }

    public class DemoService : IDemo
    {
        public double Work(double x, double y)
        {
            return x * y;
        }
    }


    internal class CustUsernamePasswordVal : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != "Jack" || password != "abc654")
            {
                throw new Exception("用户名或密码不正确");
            }
        }
    }
}
