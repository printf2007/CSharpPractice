using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CommonLib;
using System.Security.Cryptography.X509Certificates;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "示例服务器";

            ServiceHost host = new ServiceHost(typeof(DemoService));
            host.Opened += (e, f) =>
            {
                Console.WriteLine("服务已经启动。");
            };
            host.Open();
            OutputCertPKey();
            Console.Read();
            host.Close();
        }

        static void OutputCertPKey()
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            // 以只读方式打开证书容器
            store.Open(OpenFlags.ReadOnly);
            // 查找证书
            X509Certificate2 cer = null;
            foreach (var c in store.Certificates)
            {
                if (c.Subject.Contains("ZhServer"))
                {
                    cer = c;
                    break;
                }
            }
            if (cer == null) return;
            // 输出公钥
            string xmlKey = cer.PublicKey.Key.ToXmlString(false);
            // 进行编码
            string encodedKey = System.Net.WebUtility.HtmlEncode(xmlKey);
            Console.WriteLine($"\n证书公钥：\n{encodedKey}\n");
            store.Close();
            store.Dispose();
        }
    }

    [ServiceBehavior(ConfigurationName = "sv")]
    class DemoService : IDemo
    {
        public int Cac(int a, int b)
        {
            return a - b;
        }
    }
}
