using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCore03.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspnetCore03
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // 配置中间件！
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 中间件其实就是委托！！
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 1 Begin \r\n");
                await next();
                await context.Response.WriteAsync("Middleware 1 End \r\n");
            });
//
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 2 Begin \r\n");
                await next();
                await context.Response.WriteAsync("Middleware 2 End \r\n");
            });

            // 中间件里面有可能会依赖很多服务
            

            // 终端中间件，放在最后一个，如果管道里面，没有终端中间件，发生错误！
            // 刚才你看的错误，他也是中间件，你没有设置终端中间
//            app.Run(async context =>
//            {
//                await context.Response.WriteAsync("Hello Run \r\n");
//            });


            if (env.IsDevelopment())
            {
                // 开发人员异常页面中间件
                app.UseDeveloperExceptionPage();
            }

            // 功能比较多的中间，提供设置，初始化等等
            app.UseMiddleware<TestMiddleware>();


            app.UseTest();

            // 终结点路由中间件，匹配路由和终结点！
            // MVC中的控制器里的Action 就可以为是一个终结点
            app.UseRouting();

            // 终结点中间件，配置路由和终结点之间的关系的
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
