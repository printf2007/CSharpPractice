using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(CalculatorService));
            // 添加第一个终结点
            host.AddServiceEndpoint(typeof(ICalculator1), new WSHttpBinding(), "http://localhost:1700/cal");
            // 添加第二个终结点
            ContractDescription cd = ContractDescription.GetContract(typeof(ICalculator2));
            EndpointAddress addr = new EndpointAddress("http://localhost:9000/cal");
            BasicHttpBinding binding = new BasicHttpBinding();
            ServiceEndpoint endpoint = new ServiceEndpoint(cd, binding, addr);
            host.AddServiceEndpoint(endpoint);

            host.Open();
            Console.WriteLine("服务已启动。\n\n");

            foreach (ServiceEndpoint ep in host.Description.Endpoints)
            {
                string msg = $"协定：{ep.Contract.ContractType.Name}\n绑定：{ep.Binding.GetType().Name}\n地址：{ep.Address.Uri}";
                Console.WriteLine(msg);
                Console.WriteLine("------------------------------------------");
            }

            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceContract]
    public interface ICalculator1
    {
        [OperationContract]
        double Add(double x, double y);
    }

    [ServiceContract]
    public interface ICalculator2
    {
        [OperationContract]
        double Sub(double x, double y);
    }

    class CalculatorService : ICalculator1, ICalculator2
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Sub(double x, double y)
        {
            return x - y;
        }
    }
}
