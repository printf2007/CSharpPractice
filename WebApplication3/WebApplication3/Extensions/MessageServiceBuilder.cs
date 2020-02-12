using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApplication3.Services;

namespace WebApplication3.Extensions
{
    // 消息服务构建器,提供配置
    // 你可以多看看.NET Core 的源码， 里面到处都是这种东西！！
    // 创建主机，这种东西很多
    public class MessageServiceBuilder
    {
        public IServiceCollection Services { get; set; }

        public MessageServiceBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public void UseEmail()
        {
            Services.AddSingleton<IMessageService, EmailService>();
        }

        public void UseSms()
        {
            Services.AddSingleton<IMessageService, SmsService>();
        }
    }
}
