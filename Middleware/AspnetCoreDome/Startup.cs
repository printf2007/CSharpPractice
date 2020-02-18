using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspnetCoreDome
{
    // 配置服务和管道的
    public class Startup
    {
        // 以依赖注入的方式将服务添加到服务容器，IoC容器
        // 依赖注入 ASP.NET Core 里面核心，因为它无处不在
        // IoC（控制反转）容器的两个作用：
        // 1、注册类型
        // 2、请求实例

        // 注册服务
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加对控制器和API相关的功能支持，不支持视图也不支持页面！
            services.AddControllers();

            //  添加对控制器、API、视图相关的功能支持,MVC的默认模板！
            services.AddControllersWithViews();

            // Web应用程序
            services.AddRazorPages();



        } 
        
        //配置管道里面的中间件
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
