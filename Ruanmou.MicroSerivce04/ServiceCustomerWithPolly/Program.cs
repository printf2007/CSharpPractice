﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ruanmou.ServiceDiscovery;
using Ruanmou.ServiceDiscovery.LoadBalancer;

namespace ServiceCustomerWithPolly
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ConsulServiceProvider(new Uri("http://127.0.0.1:8500"));
            var myServiceA = serviceProvider.CreateServiceBuilder(builder =>
            {
                builder.ServiceName = "MyServiceA";
                // 指定负载均衡器
                builder.LoadBalancer = TypeLoadBalancer.RoundRobin;
                // 指定Uri方案
                builder.UriScheme = Uri.UriSchemeHttp;
            });

            var httpClient  = new HttpClient();
            var policy = PolicyBuilder.CreatePolly();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"-------------第{i}次请求-------------");
                policy.Execute(() =>
                {
                    try
                    {
                        var uri = myServiceA.BuildAsync("/api/order").Result;
                        Console.WriteLine($"{DateTime.Now} - 正在调用：{uri}");
                        var content = httpClient.GetStringAsync(uri).Result;
                        Console.WriteLine($"调用结果：{content}");
                    }
                    catch (Exception e)
                    {
                        // 如果你要在策略里捕捉异常，如果这个异常还是你定义的故障，一定要把这异常再抛出来
                        Console.WriteLine($"调用异常：{e.GetType()}");
                        throw;
                    }
                    
                });
                Task.Delay(1000).Wait();
            }
        }
    }
}