using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication3
{
    public class Program
    {
        // ��û���ù�ASP.NET Core����Ӧ��   �ظ�1  �Լ���� 2��û�� 3

        // ֪�� 1  һ֪��� 2  ��֪�� 3
        public static void Main(string[] args)
        {
            // ������������֮ǰ���ǲ���ִ���κ�����ί�еģ��ص����������������е����ö����Ӻ��ˡ���
            // �������ķ�ʽ����������Ӧ����һ������̨
            CreateHostBuilder(args).Build().Run();
        }

        // Ĭ�����������������ö�д���Ժ�
        // ��������Ӧ�õ������������ڹ������÷�������������ܵ�
        // Ĭ��������־��¼��������ϵ��ע�������
        // ������һ����װ��Ӧ����Դ�Ķ��� .NET Core��.NET ����API�������Ե��������Extensions�����һ���ࣨHost������
        // .NET Extensions ��Դ�루.NET Core ��չ������־����
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // .NET Core ���������������ͣ�ͨ�ã�����
            // Web����������ͨ����������չ�����ṩ����WEB���ܣ�֧��HTTP��������Kestrel��������IIS���ɣ�
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // ��������Ӳ���룬�����������ļ����������У��ٷ��ĵ�ȥ������������ļ���
                    // ��ǰ׺Ϊ"ASPNETCORE"�Ļ�����������WEB��������
                    // Ĭ���ǽ�Kestrel����ΪWeb���������������Ĭ������/֧��IIS����
                    // �Զ������ã�����WEB����

                    // ������ã��������������������������ã���չ���ṩ�����÷�����
                    
                    webBuilder.ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));

                    //ָ��ASP.NET CoreӦ�������ࣨ�Ժ����ϸ����
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseUrls("http://*:6000");

                });
        
        //������������ʹ��Ӳ����
    }
}
