using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Services
{
    public class EmailService : IMessageService
    {
        public void Send()
        {
            Console.WriteLine("EMAIL");
        }
    }
}
