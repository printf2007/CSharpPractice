using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(MyService), new Uri("http://localhost:800"));
            // 打开服务
            host.Open();
            Console.WriteLine("服务已启动。");

            EndpointAddress epaddress = new EndpointAddress("http://localhost:800");
            BasicHttpBinding binding = new BasicHttpBinding();
            IService svobj = ChannelFactory<IService>.CreateChannel(binding, epaddress);
            ProductItem p = new ProductItem
            {
                PID = 100001,
                Name = "test product",
                Color = "black",
                PostTime = DateTime.Now
            };
            svobj.NewProduct(p);

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void NewProduct(ProductItem item);
    }

    class MyService : IService
    {
        public void NewProduct(ProductItem item)
        {
            string str = $"{nameof(ProductItem.PostTime)} = {item.PostTime:D}";
            str += $"\n{nameof(ProductItem.PID)} = {item.PID}";
            str += $"\n{nameof(ProductItem.Name)} = {item.Name}";
            str += $"\n{nameof(ProductItem.Color)} = {item.Color}";
            Console.WriteLine("收到来自客户端的数据：\n{0}", str);
        }
    }

    [MessageContract]
    public class ProductItem
    {
        [MessageBodyMember]
        public string Name { get; set; }
        [MessageBodyMember]
        public string Color { get; set; }
        [MessageBodyMember]
        public int PID { get; set; }
        [MessageHeader]
        public DateTime PostTime { get; set; }
    }
}
