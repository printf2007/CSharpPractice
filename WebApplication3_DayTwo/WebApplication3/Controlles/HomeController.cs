using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Services;

namespace WebApplication3.Controlles
{
    // MVC或者API的应用。
    // .NET Core会自动把你已注册的服务，给注入进来
    
    public class HomeController
    {
        private IMessageService _messageService;

        // 构造函数注入
        public HomeController(IMessageService messageService)
        {
            // 容器知道吗？必须选一个，
            _messageService = messageService;
        }
    }
}
