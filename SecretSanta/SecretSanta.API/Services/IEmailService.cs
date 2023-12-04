using SecretSanta.API.Helper;

namespace SecretSanta.API.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailMessage emailMessage);
        Task<string> GetEmailTemplateContent();
    }
}
