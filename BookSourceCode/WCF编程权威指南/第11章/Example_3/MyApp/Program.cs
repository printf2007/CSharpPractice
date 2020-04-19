using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Syndication;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost host = new WebServiceHost(typeof(DemoService)))
            {
                host.AddServiceEndpoint(typeof(IDemo), new WebHttpBinding(), "http://localhost:700");
                host.Opened += (x, y) => Console.WriteLine("服务已经启动。");
                host.Open();
                Console.Read();
            }
        }
    }

    [ServiceContract]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    public interface IDemo
    {
        [OperationContract]
        [WebGet(UriTemplate = "/news?f={format}")]
        SyndicationFeedFormatter GetNews(string format);
    }

    class DemoService : IDemo
    {
        public SyndicationFeedFormatter GetNews(string format)
        {
            // 生成订阅文档
            SyndicationFeed feed = new SyndicationFeed();
            // 文档标题
            feed.Title = new TextSyndicationContent("WCF日报");
            // 文档简述
            feed.Description = new TextSyndicationContent("发布最新的WCF学习资源。");
            // 文档发布者
            feed.Authors.Add(new SyndicationPerson("zhou"));
            // 分类
            feed.Categories.Add(new SyndicationCategory("程序员新闻"));

            // 示例新闻记录
            SyndicationItem item1 = new SyndicationItem();
            item1.Title = new TextSyndicationContent("示例新闻-1");
            item1.Summary = new TextSyndicationContent("概要-001");
            item1.Authors.Add(new SyndicationPerson("Li"));
            item1.Content = SyndicationContent.CreatePlaintextContent("新闻1的测试正文。");
            item1.Links.Add(new SyndicationLink(new Uri("http://test/01")));

            SyndicationItem item2 = new SyndicationItem();
            item2.Title = new TextSyndicationContent("示例新闻-2");
            item2.Summary = new TextSyndicationContent("概要-002");
            item2.Links.Add(new SyndicationLink(new Uri("http://test/02")));
            item2.Authors.Add(new SyndicationPerson("Wu"));
            item2.Content = SyndicationContent.CreatePlaintextContent("新闻2的测试内容。");

            List<SyndicationItem> subitems = new List<SyndicationItem>();
            subitems.Add(item1);
            subitems.Add(item2);

            // 将订阅项与订阅文档关联
            feed.Items = subitems;

            // 判断要返回的格式
            string f = format.ToLower();
            if (f == "rss")
                return new Rss20FeedFormatter(feed);
            else
                return new Atom10FeedFormatter(feed);
        }
    }
}
