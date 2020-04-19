using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "错误协定示例";

            Uri svaddress = new Uri("http://127.0.0.1:6822/test");
            BasicHttpBinding binding = new BasicHttpBinding();

            ServiceHost host = new ServiceHost(typeof(UserManagerService));
            host.AddServiceEndpoint(typeof(IUserManager), binding, svaddress);
            host.Open();

            /********************************************/
            ChannelFactory<IUserManager> fac = new ChannelFactory<IUserManager>(binding, new EndpointAddress(svaddress));
            fac.Endpoint.EndpointBehaviors.Add(new CustEndpointBehavior());

            IUserManager channel = fac.CreateChannel();
            try
            {
                channel.RegNew("ab", "123", "321", 50);
                Console.WriteLine("注册成功。");
            }
            catch(FaultException<ErrorDetail> fex)
            {
                FaultReason reason = fex.Reason;
                FaultReasonText text = reason.GetMatchingTranslation();
                Console.WriteLine("错误概要：{0}", text.Text);
                // 获取详细信息
                ErrorDetail det = fex.Detail;
                StringBuilder strbd = new StringBuilder();
                strbd.AppendLine(det.UserNameFaultInfo);
                strbd.AppendLine(det.PasswordFaultInfo);
                strbd.AppendLine(det.AgeFaultInfo);
                Console.WriteLine(strbd);
            }

            fac.Close();
            /********************************************/

            Console.Read();
            host.Close();
        }
    }

    #region 服务协定
    [ServiceContract(Namespace = "demo-usermgr")]
    public interface IUserManager
    {
        [OperationContract]
        [FaultContract(typeof(ErrorDetail), Namespace = "cust-faults", Action = "regFault")]
        void RegNew(string userName, string passWord, string passWord2, int age);
    }

    #endregion


    #region 服务类
    class UserManagerService : IUserManager
    {
        public void RegNew(string userName, string passWord, string passWord2, int age)
        {
            ErrorDetail detail = new ErrorDetail();
            if (userName.Length < 3)
                detail.UserNameFaultInfo = "用户名长度不能小于3。";
            if (passWord.Length < 6)
                detail.PasswordFaultInfo = "密码长度不能小于6。";
            if (passWord != passWord2)
            {
                detail.PasswordFaultInfo += "\n两次输入的密码不匹配。";
            }
            if (age < 0 || age > 120)
                detail.AgeFaultInfo = "年齡超出有效范围。";

            if(detail.UserNameFaultInfo != null || detail.PasswordFaultInfo != null || detail.AgeFaultInfo != null)
            {
                throw new FaultException<ErrorDetail>(detail, "输入的注册信息不符合要求。");
            }
        }
    }
    #endregion

    #region 数据协定
    [DataContract(Namespace = "cust-details")]
    public class ErrorDetail
    {
        [DataMember]
        public string UserNameFaultInfo { get; set; } = null;
        [DataMember]
        public string PasswordFaultInfo { get; set; } = null;
        [DataMember]
        public string AgeFaultInfo { get; set; } = null;
    }
    #endregion

    #region 扩展

    public class MessageLogger : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Console.WriteLine($"从服务器返回的消息：\n{reply}\n");
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }
    }

    public class CustEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            MessageLogger logger = new MessageLogger();
            clientRuntime.ClientMessageInspectors.Add(logger);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            return;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }
    }

    #endregion
}
