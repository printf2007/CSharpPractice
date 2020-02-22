using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.MemoryCache;
using Polly.CircuitBreaker;
using Polly.Timeout;

namespace PollyDemo
{
    class Program
    {
        static void Main(string[] args)
        { 
           // 三步
           // 1. 定义故障,发生了什么样的异常叫故障
           // 2. 指定策略，当故障发生，我们要以什么策略来处理
           // 3. 执行策略，业务方法被包装为委托，由策略器来执行这个委托
           Policy.Handle<Exception>()
               .Fallback(() =>
               {
                   Console.WriteLine("Polly FallBack!");
               })
               .Execute(() =>
               {
                   //业务方法
                   Console.WriteLine("DoSomething");
                   throw new Exception("Hello");
                   
               });

           // 带条件的单个异常 故障
           Policy.Handle<HttpRequestException>(ex => ex.Message == "Error")
               .Or<ArgumentException>(ex=>ex.Message == "Error");

           // 重试一次
           Policy.Handle<Exception>().Retry();

           Policy.Handle<Exception>().Retry(5);

           Policy.Handle<Exception>().Retry(3, (ex, count) =>
           {
               // 记录异常信息，日志
           });

           Policy.Handle<Exception>().RetryForever();

           // 等待并重试
           Policy.Handle<Exception>().WaitAndRetry(new[]
           {
               TimeSpan.FromSeconds(1),
               TimeSpan.FromSeconds(2),
               TimeSpan.FromSeconds(3)
           });
           Policy.Handle<Exception>().WaitAndRetry(5, 
               i => TimeSpan.FromSeconds(Math.Pow(2, i)));
           // Policy.Handle<Exception>().WaitAndRetryForever();
           // 如果说，所有的重试都失败，真正的把异常报出来。

           // 断路器
           // 在连续触发了指定数量的异常后开启断路器（状态变成open），进入熔断状态
           // 并在指定的时间的内保持熔断状态
           var breaker = Policy.Handle<Exception>().CircuitBreaker(
               2, 
               TimeSpan.FromMinutes(1));

           Policy.Handle<Exception>().AdvancedCircuitBreaker(
               0.5, // 失败操作数大于50%
               TimeSpan.FromSeconds(10), // 10秒的采样时间
               8,   // 10秒内至少执行8次操作
               TimeSpan.FromSeconds(30)); // 熔断30秒

           // 超时本身就是一种故障
           // Policy.Timeout(30)

           // 舱壁隔离
           Policy.Bulkhead(
               12, 
               context => { });

           Policy.Bulkhead(12, 10);



           
        }


    }
}
