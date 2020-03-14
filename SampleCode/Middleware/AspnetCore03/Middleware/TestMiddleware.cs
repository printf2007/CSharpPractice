using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AspnetCore03.Middleware
{
    public class TestMiddleware
    {

        private readonly RequestDelegate _next;
        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // 执行中间件里逻辑的方法
        public async Task InvokeAsync(HttpContext httpContext)
        {
            // 这里有些逻辑
            await _next(httpContext);
            // 之后
        }
    }
}
