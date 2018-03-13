using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Community.Services
{
    public class EmailServise : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailServise(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }
        
        public async Task SendEmailAsync(string email, string subject, string body)
        {
           
            var senderEmail = _configuration["EmailService:Email"];
            var senderCredentials = _configuration["EmailService:Password"];

            MailMessage message = new MailMessage(senderEmail, email, subject, body);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(senderEmail, senderCredentials);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(message);
        }
    }
}
