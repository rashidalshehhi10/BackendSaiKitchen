﻿//using MailKit.Net.Smtp;
//using MailKit.Security;
//using Microsoft.Extensions.Options;
//using MimeKit;
//using MimeKit.Text;

//namespace BackendSaiKitchen.Helper
//{
//    public class EmailService
//    {

//        private readonly AppSettings _appSettings;

//        public EmailService(IOptions<AppSettings> appSettings)
//        {
//            _appSettings = appSettings.Value;
//        }

//        public void Send(string from, string to, string subject, string html)
//        {
//            // create message
//            var email = new MimeMessage();
//            email.From.Add(MailboxAddress.Parse(from));
//            email.To.Add(MailboxAddress.Parse(to));
//            email.Subject = subject;
//            email.Body = new TextPart(TextFormat.Html) { Text = html };

//            // send email
//            using var smtp = new SmtpClient();
//            smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
//            smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
//            smtp.Send(email);
//            smtp.Disconnect(true);
//        }
//    }
//}

