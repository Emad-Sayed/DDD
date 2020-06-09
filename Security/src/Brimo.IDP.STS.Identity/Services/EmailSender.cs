using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Brimo.IDP.STS.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly IConfiguration _configuration;

        public EmailSender(ILogger<EmailSender> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var MailServer = _configuration["EmailSettings:MailServer"];
            var Password = _configuration["EmailSettings:Password"];
            var Sender = _configuration["EmailSettings:Sender"];
            var MailPort = int.Parse(_configuration["EmailSettings:MailPort"]);
            var SenderName = _configuration["EmailSettings:SenderName"];
            var IsEnableSSL = bool.Parse(_configuration["EmailSettings:IsEnableSSL"]);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(SenderName);
            mail.To.Add(email);


            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = htmlMessage;

            using (SmtpClient smtpServer = new SmtpClient())
            {
                smtpServer.UseDefaultCredentials = true;
                smtpServer.Credentials = new NetworkCredential(Sender, Password);
                smtpServer.Host = MailServer;
                smtpServer.Port = MailPort;
                smtpServer.EnableSsl = IsEnableSSL;
                await smtpServer.SendMailAsync(mail);
            }
            _logger.LogInformation($"Email: {email}, subject: {subject}, message: {htmlMessage}");
        }
    }
}






