using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.MemoryCache;
using Polly.CircuitBreaker;
using TimeSpan = System.TimeSpan;

namespace PollyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 一般有三步
            // 1. 定义故障（异常）条件,
            // 2. 指定策略，回退（降级）策略，替代操作，也叫降级操作
            // 3. 由策略来执行业务代码
//            Policy.Handle<HttpRequestException>(ex => ex.Message == "Hello")
//                .Or<ArgumentException>()
//                .Fallback(() => { Console.WriteLine("Polly Fallback!"); })
//                .Execute(() =>
//                {
//                    Console.WriteLine("DoSomething");
//                    throw new Exception("Error");
//                });
//
//
//            // 连续触发指定数量的故障后，就会把断路器打开，熔断状态
//           var policy = Policy.Handle<Exception>()
//                .CircuitBreaker(2,
//                    TimeSpan.FromSeconds(10),
//                    (exception, span) => {},
//                    () => {});
//
//           // 如果在故障采样的持续时间内，策略里的操作发生故障的比例超过故障阈值
//           // 断路器就会关闭，熔断状态，动态计算故障数，流量的参数
//           Policy.Handle<Exception>()
//               .AdvancedCircuitBreaker(
//                   0.5, // 故障数大于50%
//                   TimeSpan.FromSeconds(10), // 采样时间
//                   8, // 10秒内至少执行了8次操作
//                   TimeSpan.FromSeconds(30)
//               );
//
//           // 超时
//           // 控制业务方法运行的时间，如果达到指定达到指定的时间，还没完成，就视为异常！超时本身就已经是一种故障！！
//           Policy.Timeout(30, 
//               (context, span, arg3) => {}).Execute(() => { });
//
//           // 舱壁隔离
//           // 控制并发数量，来主动管理负载// 大于12个都被拒绝，只接受12个请求，同时处理12个，可以有10个排队
//           var policy1 = Policy.Bulkhead(12, 10);
//           policy1.Execute(() => {});
//
//           // 缓存
//           // 并不是实时更新的，也不频繁，缓存，咱们的服务大部分都是提供数据的，大部分的数据，并不需要实时性
//           var memoryCahe = new MemoryCache(new MemoryCacheOptions());
//           var memoryCaheProvider = new MemoryCacheProvider(memoryCahe);

//           var cachePoliy = Policy.Cache(memoryCaheProvider, new SlidingTtl(TimeSpan.FromSeconds(10)));
//           cachePoliy.Execute(() =>
//           {
//
//           });
//
//           Redis



//           // 断路器的状态
//           var state = policy.CircuitState;
//           // 手动开启断路器
//           policy.Isolate();
//           policy.Reset();
//                .Execute(() => { });
//                .Retry(5,(exception, i) => {})
//                .RetryForever(exception => {}) //一直重试，直到成功
//                .WaitAndRetry(5,
//                    i => TimeSpan.FromSeconds(Math.Pow(2,i)), 
//                    (exception, span) =>{} )// 每次重试之间，都会等待一段时间


            // 策略包装组合
            // 所有的策略，进行一种组合，灵活
            // 重试3次后，仍然失败，第四次时候就让他降级。

            // 降级策略
            var fallback = Policy.Handle<Exception>()
                .Fallback(() =>
                {
                    Console.WriteLine("Fallback");
                });

            // 重试策略
            var retry = Policy.Handle<Exception>()
                .Retry(3,
                    (exception, i, arg3) => { Console.WriteLine($"重试第{i}次"); });

            // 组合，包装，顺序！从右向左
            var policy = Policy.Wrap(fallback, retry);

            policy.Execute(() =>
            {
                Console.WriteLine("DoSome");
                throw new Exception("error");
            });
            

            // 熔断降级

            // 服务A，集群，两个节点
            // 调用节点1服务，如果调用失败或者超时，用轮训方式进行多次重试调用，重试多次以后仍然失败
            // 直接返回一个替代数据（降级）。之后时间，该服务被熔断，在熔断时间内，所有对该服务的调用都以替代数据返回
            // 熔断时间过后（半开启），尝试再次调用服务，如果成功，标记服务为正常；否则，再次进入熔断状态
            // 这回一个非常标准准的熔断降级策略。

            // 分析一下，用到了几种弹性策略？超时、重试、回退、断路器
        }
    }
}
