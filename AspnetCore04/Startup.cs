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


        // Ĭ�ϣ�û���ҵ���Ӧ�Ļ���������ʱ�򣬲Ż������
        public void ConfigureServices(IServiceCollection services)
        {

        }

        // ����������ִ��һ�Σ����м����ӵ��ܵ���
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> _logger)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // ASP.NET Core 3.x ���ǳɶԳ��֣�3.0�����ӵ������м��
            // ASP.NET Core 2.x ��û��������������ġ�

            // ����ƥ��·�����ս�㣨�˵㣩�ģ�������·����Ϣ��д��HttpContext��������һ���м��
            // �˵����һ���ն��м����ĩβ�ģ�����ֹ��һ����ÿһ��Action����������һ���ն��м��
            // ˭��ѡ�񣬰������������·�ɣ�����·����Ϣ��ѡ��һ���˵㣬˭��ѡ��˭��ִ��
            app.UseRouting();

            // ��ȡ·����Ϣ������·������(·��ģ��)����ȡ��Ӧ�м��
            app.Use(async (context, next) =>
            {
                var ep = context.GetEndpoint();
                var rv = context.Request.RouteValues;
                await ep.RequestDelegate(context);

            });

            // ����ִ�У��������ã�
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}");
;           });
        }
    }
}
