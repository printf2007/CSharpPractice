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
    // ���÷���͹ܵ���
    public class Startup
    {
        // ������ע��ķ�ʽ��������ӵ�����������IoC����
        // ����ע�� ASP.NET Core ������ģ���Ϊ���޴�����
        // IoC�����Ʒ�ת���������������ã�
        // 1��ע������
        // 2������ʵ��

        // ע�����
        public void ConfigureServices(IServiceCollection services)
        {
            // ��ӶԿ�������API��صĹ���֧�֣���֧����ͼҲ��֧��ҳ�棡
            services.AddControllers();

            //  ��ӶԿ�������API����ͼ��صĹ���֧��,MVC��Ĭ��ģ�壡
            services.AddControllersWithViews();

            // WebӦ�ó���
            services.AddRazorPages();



        } 
        
        //���ùܵ�������м��
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
