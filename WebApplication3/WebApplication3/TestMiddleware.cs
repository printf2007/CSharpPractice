using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace WebApplication3
{
    // 按照约定来创建
    // 具有类型为RequestDelegate的参数的公共构造函数
    // 名为Invoke或者InvokeAsync的公共方法，这个方法必须满足两个条件：返回Task；接受类型HttpContext的第一个参数
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // 中间件的功能代码就是写这里面的,支持方法注入
        // 中间件所依赖的类，才需要注册到容器里
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync($"Message:{httpContext.Request.Path}");
            // 你可以选择是否调用next
            // 如果这里不调用，那么这个中间件会触发短路，终端中间件
            await _next(httpContext);
        }
    }
}
