using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCore02.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetCore02.Middleware
{
    // 注册的自己的服务
    public static class MessageServiceExtensions
    {
        public static void AddMessage(this IServiceCollection serviceCollection, Action<MessageServiceBuilder> configure)
        {
//            serviceCollection.AddSingleton<IMessageService, EmailService>();
            var builder = new MessageServiceBuilder(serviceCollection);
            configure(builder);
        }

    }
}
