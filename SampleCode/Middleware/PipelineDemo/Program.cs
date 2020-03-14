using System;
using System.Threading.Tasks;

namespace PipelineDemo
{
    public delegate Task RequestDelegate(HttpContext context);

    class Program
    {
        static void Main(string[] args)
        {
            var app = new ApplicationBuilder();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("中间件1号 Begin");
                await next();
                Console.WriteLine("中间件1号 End");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("中间件2号 Begin");
                await next();
                Console.WriteLine("中间件2号 End");
            });

            // 这时候管道已经形成，执行第一个中间件，就会依次调用下一个
            var firstMiddleware = app.Build();

            firstMiddleware(new HttpContext());
        }
    }
}
