using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LokesNewsletterFunction.Services
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _senderName;
        private readonly string _senderEmail;

        public EmailService(string smtpHost, int smtpPort, string smtpUser, string smtpPass, string senderName, string senderEmail)
        {
            _smtpHost = smtpHost ?? throw new ArgumentNullException(nameof(smtpHost));
            _smtpPort = smtpPort;
            _smtpUser = smtpUser ?? throw new ArgumentNullException(nameof(smtpUser));
            _smtpPass = smtpPass ?? throw new ArgumentNullException(nameof(smtpPass));
            _senderName = senderName ?? throw new ArgumentNullException(nameof(senderName));
            _senderEmail = senderEmail ?? throw new ArgumentNullException(nameof(senderEmail));
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentNullException(nameof(to));

            using var message = new MailMessage();
            message.From = new MailAddress(_senderEmail, _senderName);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}
