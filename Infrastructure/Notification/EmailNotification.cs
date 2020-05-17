using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Infrastructure.Notification
{
    public class EmailNotification : INotificationStrategy
    {
        public EmailConfigurations Configurations { get; }

        public EmailNotification(EmailConfigurations configurations)
        {
            Configurations = configurations;
        }

        public void SendMail(string subject, MailAddressCollection receivers, string body, Attachment attachment = null, MailAddressCollection ccEmails = null)
        {
            MailMessage mailMsg = new MailMessage();

            mailMsg.To.Clear();

            mailMsg.To.Add(receivers.ToString());

            if (ccEmails != null && ccEmails.Any())
                mailMsg.CC.Add(ccEmails.ToString());

            try
            {
                mailMsg.Subject = subject;
                mailMsg.Priority = MailPriority.Normal;
                mailMsg.IsBodyHtml = true;
                mailMsg.BodyEncoding = Encoding.GetEncoding("utf-8");
                mailMsg.Body = body;

                if (attachment != null)
                    mailMsg.Attachments.Add(attachment);

                MailAddress from = new MailAddress(Configurations.SenderEmail, Configurations.SenderDisplayName);
                mailMsg.From = from;

                SmtpClient SmtpMail = new SmtpClient()
                {
                    Credentials = new NetworkCredential(Configurations.SenderEmail, Configurations.SenderPassword),
                    Host = Configurations.ServerHostName,
                    Port = Configurations.ServerPortNumber,
                    EnableSsl = Configurations.EnableSSL,
                };

                SmtpMail.Send(mailMsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Notify(NotificationModel notificationModel)
        {
            //SendMail(notificationModel.Title, new MailAddressCollection
            //{
            //    notificationModel.Receiver
            //},  notificationModel.Body, null);
        }
    }
}
