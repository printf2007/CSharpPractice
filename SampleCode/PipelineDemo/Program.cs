using System;
using System.Threading.Tasks;

namespace PipelineDemo
{
    public delegate Task RequestDelegate(HttpContext context);

    class Program
    {
        // ASP.NET Core就是使用这种方式开发的。
        // 不是.NET Core
        // 我觉得很好啊
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
            // 主机创建以后运行的
            var firstMiddleware = app.Build();

            // 当请求进来的时候，就会执行第一个中间件
            // 主机给的
            firstMiddleware(new HttpContext());

            // 责任链模式，这里我觉得很容易就搞明白了！理解很吃力，责任链模式不了解
            // 源码和录播视频，都可以找班主任获取。
            // 下节课就不会这么难了啊，会把ASP.NET Core里面其它一些零碎的基础知识整合一下。
            // 应用配置、路由、静态文件
            // 责任链模式应用很广的，凡是流程化的应用， 都可以这么做，我自己已经仿造ASP.NET Core写了一个流程框架。
        }
    }
}
