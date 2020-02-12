using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApplication3.Services;

namespace WebApplication3.Extensions
{
    public static class MessageServiceExtensions
    {
        public static void AddMessage(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMessageService, SmsService>();
        }

        public static void AddMessage(this IServiceCollection serviceCollection, Action<MessageServiceBuilder> options)
        {
            // 创建构建器对象
            var builder = new MessageServiceBuilder(serviceCollection);
            // 调用委托，对象传进来
            options(builder);
        }
    }
}
