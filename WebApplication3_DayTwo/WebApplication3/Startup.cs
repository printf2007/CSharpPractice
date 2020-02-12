using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection; // DI 依赖注入框架（就在这里面）;
using Microsoft.Extensions.Hosting;
using WebApplication3.Extensions;
using WebApplication3.Services;

namespace WebApplication3
{
    // ASP.NET Core Web应用的启动类
    // 多环境概念，不用接口，而是用基于约定
    public class Startup 
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        // 配置服务，可选的
        // 以依赖注入的方式将服务(类)添加到服务（IOC）容器

        // 依赖注入、IoC
        // 这个概念非常重要，它是.NET Core的核心概念
        // 实现控制反转(IOC)的一种手段

        // IOC容器的作用
        // 1、注册类型，服务注册
        // 2、解析实例，A类，A类的实例

        // 假设我们有这么一个场景，你是一个四五岁的小孩子，你饿了，要吃东西
        // 你会怎么办？
        // 需要食物（抽象，是你依赖的），主动获取食物（被动），有风险的，这么做不符合常理！！
        // 1、叫外卖的、冲牛奶、去厨房找吃的、冰箱（主动，正转）
        // 2、叫妈妈的、叫父母（IOC容器的角色）的（注入）（被动，反转的是食物的控制权，绝对安全，而且保证适合你）
        public void ConfigureServices(IServiceCollection services)
        {
         // 上节课的源码，主机(IOC容器主机里)构建器里面，他就已经把容器创建好了， 
         // 整个应用都在主机里，整个应用都是可以用到这个容器的，获取已注册的任何类型的实例
         // 正是因为我们用到了依赖注入的手段，所以才产生控制反转这个结果
         // 工厂模式，你可以把IOC容器看做是工厂模式的升华

         //  类型的注册、实例（生命周期）的解析
         // IOC容器本身也是一个对象，这个对象里面存储的都是 已注册类型。
         // 但是，这个DI框架，功能不多，很简单，普通的需求是够用了。
         // 更多的一些注册类型的功能，更多的生命周期，需要第三方的IOC容器，支持第三方。
         // 你需要依赖管理的时候，你就可以用IOC。
         // ASP.NET Core 依赖注入是最基础，都得注入进来，你才可以用。
         // 实现单例模式，管理单例对象。
         // 主机创建完以后，容器里就已经为我们默认注册了一些服务。
         // 主机环境变量、整个应用配置，我们还可以自己注册服务

         // ASP.NET Core内置的服务组件
         // 添加对控制器和API相关功能的支持，但是不支持视图和页面（WEB API的模板默认使用的）
         services.AddControllers();
         // 3.x MVC 默认模板使用的
         services.AddControllersWithViews();
         // Web应用程序（Razor）
         services.AddRazorPages();

         // 这个MVC他是3.0之前的版本，2.X版本使用的
         services.AddMvc();

         // 他把生存期都已经设置好了，你不需要设置。
            // 但是，如果说你需要添加自定义服务类，你就需要选择一个生存期

            // 内置的，当然还有很多第三方的服务，支持.NET Core，提供一个服务扩展方法

            // 服务都是生存周期
            // 请求一个实例（是有生存周期，生存期）
            // 三种：
            // 瞬时，A类控制器依赖B类服务（瞬时的），也给实例出来，都是一个全新的实例，创建一个新的实例
            // 作用域，每一次有客户请求时，的实例只会实例化一次，在这个请求中，后面的操作还需要这个B类实例的，
            // 他不会在创建，而是把之前的创建再拿过来用哪个。
            // 单例：整个应用的生命周期，只要是向服务容器请求示例，第一次才会创建，之后都是使用这个。

            // 瞬时生存周期的自定义服务类
            // services.AddTransient<IMessageService, EmailService>();
            // services.AddScoped<IMessageService, EmailService>();
            // 后面生效，内置的不支持，属性注入，就用第三方的
            // 你同时只能用一个吧？思想的问题。
            // Email是一个服务，短信是一个服务，两个接口嘛！
            // 
            // services.AddSingleton<IMessageService, EmailService>();
            // services.AddSingleton<IMessageService, SmsService>();

            // 100多个服务，难道我要写100句吗？
            // 内置的你确实需要写100句，你需要第三方的IOC，可以批量注册 通过反射，你想怎么注册怎么注册
            // 

            // 你写了一个第三方的组件，这个组件需要依赖很多其它的服务，难道你要让开发人员自己来，而且还要
            // 自己选择生存期，也创建一个服务扩展方法，这个是约定
            // 可能还需要额外的配置，开发人员自己配置，提供配置方法
            // 如何按约定封装服务注册方法

            services.AddMessage();
            services.AddMessage(builder => builder.UseSms());

            // ASP.NET Core，咱们就按照这个框架的思路来，这个依赖注入也很方便，帮我们管理对象
            // 你可以不用自己写单例，IOC可以帮我们维护单例啊
            // 如果你觉得new方便，我建议你可以找助教老师，要一下我之前讲依赖注入的录播

            // 内置只能满足一般场景的需求，大部分的时候，都是需要第三方支持ASP.NET Core的IOC容器，
            // 用法都是一样的。
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 配置管道
        // 这个非常重要，他是配置管道的
        // Run以后，会调动一次，要配置。
        // 下节课详解管道和中间件，下节课，咱们会看到真正的ASP.NET Core的源码
        // 管道的实现，看完这部分源码，我在带着大家，去实现一个管道模型
        // 你就对管道和中间件彻底了解了！
        // 需要一节课的时间，时间已经到了尾声了，讲也讲不完
        // 管道是ASP.NET Core WEB应用的核心！！

        // 我把还原好的包，分享给大家，就不用爬墙了！


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
