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

        // �����м����
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // �м����ʵ����ί�У���
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

            // �м�������п��ܻ������ܶ����
            

            // �ն��м�����������һ��������ܵ����棬û���ն��м������������
            // �ղ��㿴�Ĵ�����Ҳ���м������û�������ն��м�
//            app.Run(async context =>
//            {
//                await context.Response.WriteAsync("Hello Run \r\n");
//            });


            if (env.IsDevelopment())
            {
                // ������Ա�쳣ҳ���м��
                app.UseDeveloperExceptionPage();
            }

            // ���ܱȽ϶���м䣬�ṩ���ã���ʼ���ȵ�
            app.UseMiddleware<TestMiddleware>();


            app.UseTest();

            // �ս��·���м����ƥ��·�ɺ��ս�㣡
            // MVC�еĿ��������Action �Ϳ���Ϊ��һ���ս��
            app.UseRouting();

            // �ս���м��������·�ɺ��ս��֮��Ĺ�ϵ��
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
