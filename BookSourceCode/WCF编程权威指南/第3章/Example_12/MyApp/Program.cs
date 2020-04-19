using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(MyService)))
            {
                host.AddServiceEndpoint(typeof(IDemo), new BasicHttpBinding(), "http://localhost:700");
                host.Open();
                Console.WriteLine("服务已启动。");

                IDemo c = ChannelFactory<IDemo>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:700"));
                Random rand = new Random();
                List<TestData> items = new List<TestData>();
                for (int i = 0; i < 50; i++)
                {
                    TestData dt = new TestData();
                    dt.ItemA = rand.NextDouble();
                    dt.ItemB = rand.NextDouble();
                    dt.ItemC = rand.NextDouble();
                    items.Add(dt);
                }
                c.SetData(items.ToArray());
                ((IClientChannel)c).Close();

                Console.Read();
            }
        }
    }

    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        void SetData(TestData[] items);
    }

    [DataContract]
    public class TestData
    {
        [DataMember]
        public double ItemA;
        [DataMember]
        public double ItemB;
        [DataMember]
        public double ItemC;
    }

    //[ServiceBehavior(MaxItemsInObjectGraph = 300)]
    class MyService : IDemo
    {
        public void SetData(TestData[] items)
        {
            string msg = $"已收到，数组长度为：{items.Length}";
            Console.WriteLine(msg);
        }
    }
}
