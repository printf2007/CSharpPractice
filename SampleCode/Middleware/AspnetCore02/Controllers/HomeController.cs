using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCore02.Services;

namespace AspnetCore02.Controllers
{
    public class HomeController
    {
        public IMessageService MessageService { get; set; }
        public HomeController(IMessageService messageService)
        {
            MessageService = messageService;
        }
    }
}
