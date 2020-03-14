using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCore02.Middleware;
using AspnetCore02.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspnetCore02
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // ˲ʱ��
            services.AddTransient<IMessageService, EmailService>();
            // ������
            services.AddScoped<IMessageService, EmailService>();
            // ����
            services.AddSingleton<IMessageService, SmsService>();
            // ע����ͬ�ķ��񣬺���ĻḲ��ǰ���
//            services.AddSingleton<IMessageService, SmsService>();
            services.AddMessage(builder => builder.UseSms());

        }

        // �����м�����м����ɹܵ�
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageService messageService)
        {
            messageService.Send();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {

                    await context.Response.WriteAsync(messageService.Send());
                });
            });
        }
    }
}
