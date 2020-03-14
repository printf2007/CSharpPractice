using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCore02.Services
{
    public class SmsService : IMessageService
    {
        public string Send()
        {
            return "Sms";
        }
    }
}
