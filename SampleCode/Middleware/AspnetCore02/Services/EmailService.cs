using System;

namespace AspnetCore02.Services
{
    public class EmailService : IMessageService
    {
        public string Send()
        {
            return "Email";
        }
    }
}