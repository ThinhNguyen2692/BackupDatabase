using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bus_backUpData.Models;

namespace Bus_backUpData.Func
{
    public static class LibraryEmail
    {
        public static void SendMail(string subject, string Body, string EmailTo, string email, string PassEmail)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email, PassEmail),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = Body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(new MailAddress(EmailTo));

            smtpClient.Send(mailMessage);
        }
    }
}
