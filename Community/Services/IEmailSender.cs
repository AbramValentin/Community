using System.Threading.Tasks;

namespace Community.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string text);
    }
}
