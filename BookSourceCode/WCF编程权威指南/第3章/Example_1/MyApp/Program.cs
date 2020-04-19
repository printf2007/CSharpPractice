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
            ContractDescription ctdesc = ContractDescription.GetContract(typeof(IDemo));
            string s = $"服务协定名称：{ctdesc.Name}\n";
            s += $"服务协定的命名空间：{ctdesc.Namespace}";
            Console.WriteLine(s);

            ContractDescription cd = ContractDescription.GetContract(typeof(ITest));
            Console.WriteLine($"服务协定名称：{cd.Name}\n服务协定命名空间：{cd.Namespace}");

            Console.Read();
        }
    }

    [ServiceContract]
    interface IDemo
    {
        [OperationContract]
        int Add(int x, int y);
    }

    [ServiceContract(Namespace = "http://test.com", Name = "MyTask")]
    interface ITest
    {
        [OperationContract]
        void StartTask();
    }
}
