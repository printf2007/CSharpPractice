using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection; // DI ����ע���ܣ����������棩;
using Microsoft.Extensions.Hosting;
using WebApplication3.Extensions;
using WebApplication3.Services;

namespace WebApplication3
{
    // ASP.NET Core WebӦ�õ�������
    // �໷��������ýӿڣ������û���Լ��
    public class Startup 
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        // ���÷��񣬿�ѡ��
        // ������ע��ķ�ʽ������(��)��ӵ�����IOC������

        // ����ע�롢IoC
        // �������ǳ���Ҫ������.NET Core�ĺ��ĸ���
        // ʵ�ֿ��Ʒ�ת(IOC)��һ���ֶ�

        // IOC����������
        // 1��ע�����ͣ�����ע��
        // 2������ʵ����A�࣬A���ʵ��

        // ������������ôһ������������һ���������С���ӣ�����ˣ�Ҫ�Զ���
        // �����ô�죿
        // ��Ҫʳ��������������ģ���������ȡʳ����������з��յģ���ô�������ϳ�����
        // 1���������ġ���ţ�̡�ȥ�����ҳԵġ����䣨��������ת��
        // 2��������ġ��и�ĸ��IOC�����Ľ�ɫ���ģ�ע�룩����������ת����ʳ��Ŀ���Ȩ�����԰�ȫ�����ұ�֤�ʺ��㣩
        public void ConfigureServices(IServiceCollection services)
        {
         // �Ͻڿε�Դ�룬����(IOC����������)���������棬�����Ѿ��������������ˣ� 
         // ����Ӧ�ö������������Ӧ�ö��ǿ����õ���������ģ���ȡ��ע����κ����͵�ʵ��
         // ������Ϊ�����õ�������ע����ֶΣ����ԲŲ������Ʒ�ת������
         // ����ģʽ������԰�IOC���������ǹ���ģʽ������

         //  ���͵�ע�ᡢʵ�����������ڣ��Ľ���
         // IOC��������Ҳ��һ�����������������洢�Ķ��� ��ע�����͡�
         // ���ǣ����DI��ܣ����ܲ��࣬�ܼ򵥣���ͨ�������ǹ����ˡ�
         // �����һЩע�����͵Ĺ��ܣ�������������ڣ���Ҫ��������IOC������֧�ֵ�������
         // ����Ҫ���������ʱ����Ϳ�����IOC��
         // ASP.NET Core ����ע���������������ע���������ſ����á�
         // ʵ�ֵ���ģʽ������������
         // �����������Ժ���������Ѿ�Ϊ����Ĭ��ע����һЩ����
         // ������������������Ӧ�����ã����ǻ������Լ�ע�����

         // ASP.NET Core���õķ������
         // ��ӶԿ�������API��ع��ܵ�֧�֣����ǲ�֧����ͼ��ҳ�棨WEB API��ģ��Ĭ��ʹ�õģ�
         services.AddControllers();
         // 3.x MVC Ĭ��ģ��ʹ�õ�
         services.AddControllersWithViews();
         // WebӦ�ó���Razor��
         services.AddRazorPages();

         // ���MVC����3.0֮ǰ�İ汾��2.X�汾ʹ�õ�
         services.AddMvc();

         // ���������ڶ��Ѿ����ú��ˣ��㲻��Ҫ���á�
            // ���ǣ����˵����Ҫ����Զ�������࣬�����Ҫѡ��һ��������

            // ���õģ���Ȼ���кܶ�������ķ���֧��.NET Core���ṩһ��������չ����

            // ��������������
            // ����һ��ʵ���������������ڣ������ڣ�
            // ���֣�
            // ˲ʱ��A�����������B�����˲ʱ�ģ���Ҳ��ʵ������������һ��ȫ�µ�ʵ��������һ���µ�ʵ��
            // ������ÿһ���пͻ�����ʱ����ʵ��ֻ��ʵ����һ�Σ�����������У�����Ĳ�������Ҫ���B��ʵ���ģ�
            // �������ڴ��������ǰ�֮ǰ�Ĵ������ù������ĸ���
            // ����������Ӧ�õ��������ڣ�ֻҪ���������������ʾ������һ�βŻᴴ����֮����ʹ�������

            // ˲ʱ�������ڵ��Զ��������
            // services.AddTransient<IMessageService, EmailService>();
            // services.AddScoped<IMessageService, EmailService>();
            // ������Ч�����õĲ�֧�֣�����ע�룬���õ�������
            // ��ͬʱֻ����һ���ɣ�˼������⡣
            // Email��һ�����񣬶�����һ�����������ӿ��
            // 
            // services.AddSingleton<IMessageService, EmailService>();
            // services.AddSingleton<IMessageService, SmsService>();

            // 100��������ѵ���Ҫд100����
            // ���õ���ȷʵ��Ҫд100�䣬����Ҫ��������IOC����������ע�� ͨ�����䣬������ôע����ôע��
            // 

            // ��д��һ�����������������������Ҫ�����ܶ������ķ����ѵ���Ҫ�ÿ�����Ա�Լ��������һ�Ҫ
            // �Լ�ѡ�������ڣ�Ҳ����һ��������չ�����������Լ��
            // ���ܻ���Ҫ��������ã�������Ա�Լ����ã��ṩ���÷���
            // ��ΰ�Լ����װ����ע�᷽��

            services.AddMessage();
            services.AddMessage(builder => builder.UseSms());

            // ASP.NET Core�����ǾͰ��������ܵ�˼·�����������ע��Ҳ�ܷ��㣬�����ǹ������
            // ����Բ����Լ�д������IOC���԰�����ά��������
            // ��������new���㣬�ҽ����������������ʦ��Ҫһ����֮ǰ������ע���¼��

            // ����ֻ������һ�㳡�������󣬴󲿷ֵ�ʱ�򣬶�����Ҫ������֧��ASP.NET Core��IOC������
            // �÷�����һ���ġ�
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ���ùܵ�
        // ����ǳ���Ҫ���������ùܵ���
        // Run�Ժ󣬻����һ�Σ�Ҫ���á�
        // �½ڿ����ܵ����м�����½ڿΣ����ǻῴ��������ASP.NET Core��Դ��
        // �ܵ���ʵ�֣������ⲿ��Դ�룬���ڴ��Ŵ�ң�ȥʵ��һ���ܵ�ģ��
        // ��ͶԹܵ����м�������˽��ˣ�
        // ��Ҫһ�ڿε�ʱ�䣬ʱ���Ѿ�����β���ˣ���Ҳ������
        // �ܵ���ASP.NET Core WEBӦ�õĺ��ģ���

        // �Ұѻ�ԭ�õİ����������ң��Ͳ�����ǽ�ˣ�


        // ����Run��ʱ��ִ�еġ�
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // �м����˳�򣬹ܵ�����м������˳��ģ��������Ǵ����˳��
            // ˳���Ǻ���Ҫ�ģ�

            // ����һ������ע���ܣ���������м����

            // ����м����������һЩ�����࣬��ͨ������ע��ע�������
            // app.UseCors();

            // �����һ���նˣ��ս�㣩�м��
            // app.Run(async context => { await context.Response.WriteAsync("Hello Run"); });
            // �м�����������࣬������ͨ����������ע��
            // ���ٷ��ĵ�

            if (env.IsDevelopment())
            {
                // ������Ա�쳣ҳ���м������������쳣�����������쳣�ڵ�ҳ������ʾ���������ҷǳ���ϸ
                // �����Ѻ�
                app.UseDeveloperExceptionPage();
            }
            
            // ������USE��ÿ��USE������_components�б����ί����ӽ�ȥ�����ɶҲû�и�

            // �й�ϵ��û�аɣ�����������Funcί�У����ڻ����������м��
            // ��ν���м������ʵ����һ��ί��
            app.Use(async (context, next) =>
            {
                // ������
                await context.Response.WriteAsync("Middleware 1 Begin");
                await next();
                // ��Ӧ
                // await context.Response.WriteAsync("Middleware 1 End");
            });

            // ��ν���м������ʵ����һ��ί��
            app.Use(async (context, next) =>
            {
                // ������
                await context.Response.WriteAsync("Middleware 1 Begin");
                await next();
                // ��Ӧ
                // await context.Response.WriteAsync("Middleware 1 End");
            });

            // ��д��һ���м�����㷢����ȥ�����˶������á�

            app.UseMiddleware<TestMiddleware>();
            app.UseTest();
            app.Run(async context => { await context.Response.WriteAsync("Hello Run"); });

            // ���дWEBӦ�ã�ʵ�ʾ�����д�м������ȫ�����м��������
            // ��д�Ŀ�������Ҳ���м�����һ���֡���
            // ����ASP.NET CoreӦ�ã������ڹܵ������еģ��ܵ���ŵĶ����м��

            // asp.net core 2.x��������һ��·���м������3.0��ʱ��
            // ·���м������������֡�
            // �ն�·���м��

            // ʶ��·�ɣ�ƥ��·�ɺ��ս��
            // app.UseRouting();


            // �ս���м��������·�ɺ��ս��֮���ӳ���ϵ��
            // �ս�㣬������ΪӦ�ó����ṩ���HTTP����Ĵ�������
            // MVCӦ�ã��ս��Ͷ�Ӧ�ſ������е�ĳ������
            // Web API
            // 
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context =>
            //     {
            //         await context.Response.WriteAsync("Hello World!");
            //     });
            // });
        }
    }
}
