using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Community.Services
{
    public class EmailServise : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var from = "valentinabramdeveloper@gmail.com";

            MailMessage message = new MailMessage(from, email, subject, body);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("valentinabramdeveloper@gmail.com", "valik123852");
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(message);
        }
    }
}
