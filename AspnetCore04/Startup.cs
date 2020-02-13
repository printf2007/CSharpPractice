using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCore04.SettingModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspnetCore04
{

    public class Startup
    {


        // 默认，没有找到对应的环境方法的时候，才会找这个
        public void ConfigureServices(IServiceCollection services)
        {

        }

        // 运行主机，执行一次，把中间件添加到管道里
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> _logger)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // ASP.NET Core 3.x 里是成对出现，3.0新增加的两个中间件
            // ASP.NET Core 2.x 是没有这两个玩意儿的。

            // 负责匹配路由与终结点（端点）的，解析出路由信息，写进HttpContext，传给下一个中间件
            // 端点就是一个终端中间件（末尾的），不止有一个，每一个Action，都可以是一个终端中间件
            // 谁来选择，把请求解析成了路由，根据路由信息来选择一个端点，谁来选择？谁来执行
            app.UseRouting();

            // 获取路由信息，包括路由数据(路由模板)、获取对应中间件
            app.Use(async (context, next) =>
            {
                var ep = context.GetEndpoint();
                var rv = context.Request.RouteValues;
                await ep.RequestDelegate(context);

            });

            // 负责执行，负责配置，
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}");
;           });
        }
    }
}
