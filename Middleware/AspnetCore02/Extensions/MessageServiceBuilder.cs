using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCore02.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetCore02.Middleware
{
    public class MessageServiceBuilder
    {
        public IServiceCollection ServiceCollection { get; set; }

        public MessageServiceBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public void UseEmail()
        {
            ServiceCollection.AddSingleton<IMessageService, EmailService>();
        }

        public void UseSms()
        {
            ServiceCollection.AddSingleton<IMessageService, SmsService>();
        }
    }
}
