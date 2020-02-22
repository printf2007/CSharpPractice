using System;
using Polly;

namespace ServiceCustomerWithPolly
{
    public static class PolicyBuilder
    {
        // 服务A消费提供者
        // 首先会调用其中一个服务节点，如果失败或者超时，则使用轮询的方式进行重试调用。重试多次仍然失败的话，则返回
        // 一个替代数据（服务降级）。之后一段时间，该服务被熔断，在熔断时间内，所有对该服务的调用都以替代数据返回。
        // 熔断时间过后，尝试再次调用服务A，如果成功，熔断器关闭。否则，再禁用该服务A一段时间
        public static ISyncPolicy CreatePolly()
        {
            // 超时1秒
            var timeoutPolicy = Policy.Timeout(1, (context, timespan, task) =>
            {
                Console.WriteLine("执行超时，抛出TimeoutRejectedException异常");
            });


            // 重试2次
            var retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetry(
                    2,
                    retryAttempt => TimeSpan.FromSeconds(2),
                    (exception, timespan, retryCount, context) =>
                    {
                        Console.WriteLine($"{DateTime.Now} - 重试 {retryCount} 次 - 抛出{exception.GetType()}");
                    });

            // 连续发生两次故障，就熔断3秒
            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreaker(
                    // 熔断前允许出现几次错误
                    2,
                    // 熔断时间
                    TimeSpan.FromSeconds(5),
                    // 熔断时触发 OPEN
                    onBreak: (ex, breakDelay) =>
                    {
                        Console.WriteLine($"{DateTime.Now} - 断路器：开启状态（熔断时触发）");
                    },
                    // 熔断恢复时触发 // CLOSE
                    onReset: () =>
                    {
                        Console.WriteLine($"{DateTime.Now} - 断路器：关闭状态（熔断恢复时触发）");
                    },
                    // 熔断时间到了之后触发，尝试放行少量（1次）的请求，
                    onHalfOpen: () =>
                    {
                        Console.WriteLine($"{DateTime.Now} - 断路器：半开启状态（熔断时间到了之后触发）");
                    }
                );

            // 回退策略，降级！
            var fallbackPolicy = Policy.Handle<Exception>()
                .Fallback(() =>
                {
                    // 是你们自己写的，比如我本来这个服务A访问服务B，服务B应该返回一个用户头像地址，
                    // 但是，由于服务B挂了，真实的投降肯定获取不到，返回一个通用头像
                    // 总不能因为，一个头像获取不了，我就给你报错吧。
                    // 服务降级的意义，是防止服务雪崩。数据本身没有意义，只是给我们一个时间，去处理问题。
                    // 微服务里面确实理论多，大家在讨论的大多是理论问题，不是代码实现问题。。

                    Console.WriteLine("这是一个Fallback");
                }, exception =>
                {
                    Console.WriteLine($"Fallback异常：{exception.GetType()}");
                });

            // 策略从右到左依次进行调用
            // 首先判断调用是否超时，如果超时就会触发异常，发生超时故障，然后就触发重试策略；
            // 如果重试两次中只要成功一次，就直接返回调用结果
            // 如果重试两次都失败，第三次再次失败，就会发生故障
            // 重试之后是断路器策略，所以这个故障会被断路器接收，当断路器收到两次故障，就会触发熔断，也就是说断路器开启
            // 断路器开启的3秒内，任何故障或者操作，都会通过断路器到达回退策略，触发降级操作
            // 3秒后，断路器进入到半开启状态，操作可以正常执行
            return Policy.Wrap(fallbackPolicy, circuitBreakerPolicy, retryPolicy, timeoutPolicy);
        }
    }
}
