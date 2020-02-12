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
        // 有没有用过ASP.NET Core开发应用   回复1  自己玩过 2，没有 3

        // 知道 1  一知半解 2  不知道 3
        public static void Main(string[] args)
        {
            // 在我们配置完之前，是不会执行任何配置委托的（回调函数），我们所有的配置都被延后了。。
            // 以阻塞的方式运行主机，应用是一个控制台
            CreateHostBuilder(args).Build().Run();
        }

        // 默认主机构建器（配置都写好以后）
        // 主机负责应用的启动和生存期管理、配置服务器和请求处理管道
        // 默认设置日志记录、依赖关系的注入和配置
        // 主机是一个封装了应用资源的对象 .NET Core（.NET 核心API，功能性的组件都在Extensions）里的一个类（Host）！！
        // .NET Extensions 的源码（.NET Core 扩展包、日志、）
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // .NET Core 有两种主机，泛型（通用）主机
            // Web主机（他是通用主机的扩展，他提供额外WEB功能，支持HTTP，集成了Kestrel，内置了IIS集成）
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // 不建议用硬编码，不灵活，用配置文件或者命令行，官方文档去查他的配置项的键名
                    // 从前缀为"ASPNETCORE"的环境变量加载WEB主机配置
                    // 默认是将Kestrel设置为Web服务器并对其进行默认配置/支持IIS集成
                    // 自定义配置，关于WEB主机

                    // 组件配置，不属于主机，但是由主机调用（扩展类提供的配置方法）
                    
                    webBuilder.ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));

                    //指定ASP.NET Core应用启动类（以后会详细讲）
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseUrls("http://*:6000");

                });
        
        //不建议在这里使用硬编码
    }
}
