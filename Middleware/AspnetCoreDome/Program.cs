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
    // 配置主机
    // 启动流程
    public class Program
    {
        public static void Main(string[] args)
        {
            // 创建主机生成器（都是一些配置，主机配置），创建主机，运行主机
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // 创建默认生成器
            // web主机是提供了web功能的

            // 加载主机配置、加载环境变量、加载命令行参数
            // 配置日志组件、启用IIS集成
            Host.CreateDefaultBuilder(args)
                // 配置Web主机，带了一些默认配置
                // ！！！将Kestrel设置为Web服务器，并对其进行一些初始化配置
                // 不可以！！Kestrel！高性能的HTTP/2服务器，HTTPS
                // 在linux下比在windows性能更生猛！
                // 反向代理的作用自己去查！
                // 加载前缀为"aspnetcore"的环境变量

                //主机配置：组件配置、主机配置项
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // 第三方日志组件,配置方法是一样的
                    webBuilder.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Debug));
                    // 配置Kestrel
                    webBuilder.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 100 * 1024 * 1024);

                    // 主机配置项，硬编码配置
                    webBuilder.UseUrls("http://*:6000;https://*:6001");

                    // 同一个配置项，四种配置同时存在，他们是有个优先级的！！！
                    // 命令行配置（灵活。结合docker部署。） 》 应用配置 》 硬编码 》 环境变量
                    // 指定web应用的启动类
                    webBuilder.UseStartup<Startup>();
                });
    }
}