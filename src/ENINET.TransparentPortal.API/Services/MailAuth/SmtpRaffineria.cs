using ENINET.TransparentPortal.API.Configuration;
using System.Net.Mail;

namespace ENINET.TransparentPortal.API.Services.MailAuth
{
    public class SmtpRaffineria : IMailAuth
    {
        private readonly IConfiguration _configuration;
        private MailServiceSettings _mailServiceSettings = new MailServiceSettings();
        private Exception _lastException = null;

        public SmtpRaffineria(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _configuration.Bind("MailService", _mailServiceSettings);
            _mailServiceSettings.SmtpPassword = System.Environment.GetEnvironmentVariable("MAIL_SERVICE_PASSWORD") ?? "";
            _mailServiceSettings.SmtpUser = System.Environment.GetEnvironmentVariable("MAIL_SERVICE_USER") ?? "";
        }

        public Exception Exception => _lastException;

        public bool SendMail(string to, Guid authorizationCode)
        {
            _lastException = null;
            var smtp = new SmtpClient(_mailServiceSettings.SmtpServer, _mailServiceSettings.SmtpPort)
            {
                //Credentials = new System.Net.NetworkCredential(_mailServiceSettings.SmtpUser, _mailServiceSettings.SmtpPassword),
                //EnableSsl = _mailServiceSettings.EnableSsl
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailServiceSettings.From),
                Subject = _mailServiceSettings.Subject,
                Body = string.Format(_mailServiceSettings.Object, authorizationCode),
                IsBodyHtml = true,
                To = { new MailAddress(to) }
            };
            try
            {
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                _lastException = ex;
                return false;
            }
        }
    }
}
