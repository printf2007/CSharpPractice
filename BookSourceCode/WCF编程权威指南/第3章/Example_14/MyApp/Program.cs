using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Serialization;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Service));
            BasicHttpBinding binding = new BasicHttpBinding();
            host.AddServiceEndpoint(typeof(IDemo), binding, "http://localhost:800");
            host.Open();
            Console.WriteLine("服务已经启动。");

            // 客户端调用
            EndpointAddress epaddr = new EndpointAddress("http://localhost:800");
            IDemo channel = ChannelFactory<IDemo>.CreateChannel(binding, epaddr);
            AudioTrack trackobj = new AudioTrack();
            trackobj.TrackNo = 1;
            trackobj.Title = "test song";
            trackobj.Artist = "Tom";
            trackobj.Album = "test album";
            channel.PostMusic(trackobj);

            Console.ReadKey();
            host.Close();
        }
    }


    public class AudioTrack
    {
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "artist")]
        public string Artist { get; set; }
        [XmlAttribute(AttributeName = "no")]
        public int TrackNo { get; set; }
        [XmlAttribute(AttributeName = "album")]
        public string Album { get; set; }
    }

    [ServiceContract, XmlSerializerFormat]
    interface IDemo
    {
        [OperationContract]
        void PostMusic(AudioTrack track);
    }

    class Service : IDemo
    {
        public void PostMusic(AudioTrack track)
        {
            string s = $"曲目编号：{track.TrackNo}\n标题：{track.Title}\n艺术家：{track.Artist}\n所属专辑：{track.Album}";
            Console.WriteLine(s);
        }
    }
}

