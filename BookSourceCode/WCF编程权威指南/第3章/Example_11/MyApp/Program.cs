using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MyApp
{
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            Uri svaddress = new Uri("http://localhost:2000/news");

            using(ServiceHost host = new ServiceHost(typeof(MyService)))
            {
                // 添加终结点
                host.AddServiceEndpoint(typeof(IService), new BasicHttpBinding(), svaddress);
                // 打开服务
                host.Open();
                Console.WriteLine("服务已运行，请等待客户端调用。");

                // 客户端调用
                IService sv = ChannelFactory<IService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress(svaddress));
                News n = new News
                {
                    Title = "示例新闻",
                    Content = "学习编程是一个循序渐进的过程。",
                    PublishDate = new DateTime(2016, 8, 9)
                };
                sv.PostNews(n);
                ((IClientChannel)sv).Close();

                Console.Read();
            }
        }
    }

    class MyService : IService
    {
        public void PostNews(News obj)
        {
            string msg = $"新闻标题：{obj.Title}\n正文：{obj.Content}\n发布日期：{obj.PublishDate:d}";
            Console.WriteLine("发布成功，新闻概要如下：\n" + msg);
        }
    }
}


namespace Common
{
    [DataContract]
    public class News
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public DateTime PublishDate { get; set; }
    }

    [ServiceContract(Name = "news_sv", Namespace = "http://xnews.org")]
    public interface IService
    {
        [OperationContract(Name = "postNews", Action = "post_act", ReplyAction = "post_rpl")]
        void PostNews(News obj);
    }
}
