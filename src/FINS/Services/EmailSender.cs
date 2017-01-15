using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FINS.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //TODO: Implement Email Service
            Console.WriteLine($"Sending email to {email}");
            await Task.FromResult(0);
        }
    }

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
