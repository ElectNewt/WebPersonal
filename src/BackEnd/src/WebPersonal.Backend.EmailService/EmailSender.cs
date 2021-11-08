using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebPersonal.Shared.ROP;

namespace WebPersonal.Backend.EmailService
{
    
    public interface IEmailSender
    {
        Task<Result<bool>> SendEmail(string to, string subject, string body);
    }
    
    public class EmailSender : IEmailSender
    {
        private readonly IOptionsMonitor<EmailConfiguration> _options;
        private EmailConfiguration EmailConfiguration => _options.CurrentValue;

        public EmailSender(IOptionsMonitor<EmailConfiguration> options)
        {
            _options = options;
        }

        public async Task<Result<bool>> SendEmail(string to, string subject, string body)
        {
            Console.WriteLine("this simulates the an email being Sent");
            Console.WriteLine($"Email configuration Server: {EmailConfiguration.SmtpServer}, From: {EmailConfiguration.From}");
            Console.WriteLine($"Email data To: {to}, subject: {subject}, body: {body}");
            return true;
        }
        
    }


    public class EmailConfiguration
    {
        public string SmtpServer { get; set; }
        public string From { get; set; }
    }
}

