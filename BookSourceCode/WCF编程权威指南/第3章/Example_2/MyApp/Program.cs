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
            ContractDescription cd = ContractDescription.GetContract(typeof(ITest));
            foreach (OperationDescription op in cd.Operations)
            {
                Console.WriteLine($"操作名称：{op.Name}");
                Console.WriteLine("----------------------------------");
                foreach (MessageDescription msg in op.Messages)
                {
                    Console.WriteLine($"Action = {msg.Action}");
                }
            }

            Console.Read();
        }
    }

    [ServiceContract]
    interface ITest
    {
        [OperationContract]
        void Run();
    }
}
