using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspnetCoreDome
{
    // ��������
    // ��������
    public class Program
    {
        public static void Main(string[] args)
        {
            // ��������������������һЩ���ã��������ã���������������������
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // ����Ĭ��������
            // web�������ṩ��web���ܵ�

            // �����������á����ػ������������������в���
            // ������־���������IIS����
            Host.CreateDefaultBuilder(args)
                // ����Web����������һЩĬ������
                // ��������Kestrel����ΪWeb�����������������һЩ��ʼ������
                // �����ԣ���Kestrel�������ܵ�HTTP/2��������HTTPS
                // ��linux�±���windows���ܸ����ͣ�
                // �������������Լ�ȥ�飡
                // ����ǰ׺Ϊ"aspnetcore"�Ļ�������

                //�������ã�������á�����������
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // ��������־���,���÷�����һ����
                    webBuilder.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Debug));
                    // ����Kestrel
                    webBuilder.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 100 * 1024 * 1024);

                    // ���������Ӳ��������
                    webBuilder.UseUrls("http://*:6000;https://*:6001");

                    // ͬһ���������������ͬʱ���ڣ��������и����ȼ��ģ�����
                    // ���������ã������docker���𡣣� �� Ӧ������ �� Ӳ���� �� ��������
                    // ָ��webӦ�õ�������
                    webBuilder.UseStartup<Startup>();
                });
    }
}