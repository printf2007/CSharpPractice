using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace AspnetCore03.Middleware
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseTest(this IApplicationBuilder app)
        {
            // 干一些其它事情
            return app.UseMiddleware<TestMiddleware>();
        }
    }
}
