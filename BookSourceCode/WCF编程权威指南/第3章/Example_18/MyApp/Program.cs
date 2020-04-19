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
            Uri addr = new Uri("http://localhost:600");
            using (ServiceHost h=new ServiceHost(typeof(MailSenderService), addr))
            {
                h.Open();

                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress epaddr = new EndpointAddress(addr);
                IMailSender channel = ChannelFactory<IMailSender>.CreateChannel(binding, epaddr);
                
                MailMessage mailmsg = new MailMessage();
                Attachment[] atts = new Attachment[]
                {
                    new Attachment { FileName = "track.mp3", FileSize = 3725812, FileType = "MP3" },
                    new Attachment {FileName = "world.jpg", FileSize = 137820, FileType = "JPEG" }
                };
                mailmsg.Attachments = atts;
                mailmsg.Subject = "Test Mail";
                mailmsg.Body = "some content";
                channel.PostMail(mailmsg);

                Console.ReadKey();
            }
        }
    }

    [MessageContract(WrapperName = "mail", WrapperNamespace = "mail-info")]
    public class MailMessage
    {
        [MessageBodyMember(Name = "subject", Namespace = "mail-info")]
        public string Subject { get; set; }
        [MessageBodyMember(Name = "body", Namespace = "mail-info")]
        public string Body { get; set; }
        [/*MessageHeader*/MessageHeaderArray(Name = "attachments", Namespace = "mail-info")]
        public Attachment[] Attachments { get; set; }
    }

    [DataContract(Namespace = "attachment-data")]
    public class Attachment
    {
        [DataMember]
        public string FileName;
        [DataMember]
        public long FileSize;
        [DataMember]
        public string FileType;
    }

    [ServiceContract(Namespace = "mail-sender-root")]
    public interface IMailSender
    {
        [OperationContract(Action = "post-mail")]
        void PostMail(MailMessage msg);
    }

    class MailSenderService : IMailSender
    {
        public void PostMail(MailMessage msg)
        {
            var reqmsg = OperationContext.Current.RequestContext.RequestMessage;
            Console.WriteLine("客户端提交的消息如下：\n{0}", reqmsg);
        }
    }
}
