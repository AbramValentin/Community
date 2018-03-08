using System.Threading.Tasks;

namespace Community.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string text);
    }
}
