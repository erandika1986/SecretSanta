using SecretSanta.API.Helper;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Reflection;

namespace SecretSanta.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
        }

        public async Task<string> GetEmailTemplateContent()
        {

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

            var emailTemplatePath = @"EmailTemplate\secret-santa-email.html";

            var templatePath = Path.Combine(outPutDirectory, emailTemplatePath);

            var emailBodyText = await System.IO.File.ReadAllTextAsync(templatePath);

            return emailBodyText;
        }

        public async Task<bool> SendEmail(EmailMessage emailMessage)
        {
            try
            {
                var emailSetting = await GetEmailSettings();

                var email = new MailMessage();
                email.From = new MailAddress(emailSetting.FromEmail, "Secret Santa");

                foreach (var to in emailMessage.ToEmails.Select(toEmail => new MailAddress(toEmail)))
                {
                    email.To.Add(to);
                }

                email.Subject = emailMessage.Subject;
                email.Body = emailMessage.EmailBody;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.IsBodyHtml = true;

                var client = new SmtpClient(emailSetting.SMTPServer, int.Parse(emailSetting.SMTPPort));

                if (bool.Parse(emailSetting.SMTPEnableSsl))
                {
                    client.EnableSsl = bool.Parse(emailSetting.SMTPEnableSsl);
                }

                if (bool.Parse(emailSetting.SMTPUseDefaultCredentials))
                {
                    client.Credentials = new NetworkCredential(emailSetting.SMTPUsername, emailSetting.SMTPPassword);
                }

                await client.SendMailAsync(email);

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return false;
            }
        }

        private async Task<EmailSetting> GetEmailSettings()
        {
            var emailSetting = new EmailSetting();

            emailSetting.SMTPPort = _configuration.GetValue<string>("EmailSettings:SMTPPort");
            emailSetting.SMTPServer = _configuration.GetValue<string>("EmailSettings:SMTPServer");
            emailSetting.SMTPUsername = _configuration.GetValue<string>("EmailSettings:SMTPUsername");
            emailSetting.SMTPPassword = _configuration.GetValue<string>("EmailSettings:SMTPPassword");
            emailSetting.SMTPEnableSsl = _configuration.GetValue<string>("EmailSettings:SMTPEnableSsl");
            emailSetting.SMTPUseDefaultCredentials = _configuration.GetValue<string>("EmailSettings:SMTPUseDefaultCredentials");
            emailSetting.FromEmail = _configuration.GetValue<string>("EmailSettings:SMTPFromEmailAddress");

            return emailSetting;
        }
    }
}
