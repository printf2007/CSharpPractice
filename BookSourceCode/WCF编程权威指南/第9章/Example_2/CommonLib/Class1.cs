using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace CommonLib
{
    [ServiceContract(ConfigurationName = "itest")]
    public interface IDemo
    {
        [OperationContract]
        int Cac(int a, int b);
    }
}
