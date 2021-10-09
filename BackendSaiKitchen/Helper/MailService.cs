﻿using BackendSaiKitchen.CustomModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Helper
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings != null ? mailSettings.Value : new MailSettings();
        }
        public MailService()
        {

            _mailSettings = new MailSettings();
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task SendWelcomeEmailAsync(PasswordRequest request)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\WelcomeTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail).Replace("[link]", request.Link);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = $"Welcome {request.UserName}";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
        }
        public async Task SendForgotEmailAsync(PasswordRequest request)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\ForgotPasswordTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail).Replace("[link]", request.Link);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = "Reset Password";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {

                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
        }


        public async Task SendInquiryEmailAsync(String toEmail, String inquiryCode, String measurementScheduleDate, String assignTo, String contactNumber, String buildingAddress)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\NewInquiryTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[inquirycode]", inquiryCode).Replace("[measurementscheduledate]", measurementScheduleDate).Replace("[assignto]", assignTo).Replace("[contactnumber]", contactNumber).Replace("[buildingaddress]", buildingAddress);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "New Inquiry";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {

                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
        }

        public async Task SendDesignEmailAsync(String toEmail, String inquiryCode, String reviewDesignURL, String approveDesignURL, String rejectDesignURL)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\DesignReviewTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[ReviewDesignURL]", reviewDesignURL);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Design Review of " + inquiryCode;
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {

                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
        }

        public async Task SendQuotationEmailAsync(String toEmail, String inquiryCode, String reviewQuotation, String advancePaymentRate, String amount, String promo, String vAT, String totalAmount, String validityTill, String approveQuotationURL, String rejectQuotationURL)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\QuotationApprovalTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[ReviewQuotationURL]", reviewQuotation).Replace("[AdvancePaymentRate]", advancePaymentRate).Replace("[Amount]", amount).Replace("[Promo]", promo).Replace("[VAT]", vAT).Replace("[TotalAmount]", totalAmount).Replace("[ValidityTill]", validityTill);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Quotation Approval of " + inquiryCode;
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {

                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
        }
        public async Task ApproveQuotationEmailAsync(String toEmail, String inquiryCode, String reviewQuotation, String advancePaymentRate, String amount, String promo, String vAT, String totalAmount, String validityTill, String approveQuotationURL, String rejectQuotationURL, List<IFormFile> Attachments)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\QuotationApprovalTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[ReviewQuotationURL]", reviewQuotation).Replace("[AdvancePaymentRate]", advancePaymentRate).Replace("[Amount]", amount).Replace("[Promo]", promo).Replace("[VAT]", vAT).Replace("[TotalAmount]", totalAmount).Replace("[ValidityTill]", validityTill);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Quotation Approval of " + inquiryCode;
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {

                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
        }


    }
}
