using BackendSaiKitchen.CustomModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Helper
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        //public static MailService mailService;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings != null ? mailSettings.Value : new MailSettings();
        }

        public MailService()
        {
            _mailSettings = new MailSettings();
            //mailService = this;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            MimeMessage email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            BodyBuilder builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (IFormFile file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
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
            using SmtpClient smtp = new SmtpClient();
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
                MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail)
                    .Replace("[link]", request.Link);
                MimeMessage email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                };
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = $"Welcome {request.UserName}";
                BodyBuilder builder = new BodyBuilder
                {
                    HtmlBody = MailText
                };
                email.Body = builder.ToMessageBody();
                using SmtpClient smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
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
                MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail)
                    .Replace("[link]", request.Link);
                MimeMessage email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                };
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = "Reset Password";
                BodyBuilder builder = new BodyBuilder
                {
                    HtmlBody = MailText
                };
                email.Body = builder.ToMessageBody();
                using SmtpClient smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }
        }


        public async Task SendInquiryEmailAsync(string toEmail, string inquiryCode, string measurementScheduleDate,
            string assignTo, string contactNumber, string buildingAddress)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\NewInquiryTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[inquirycode]", inquiryCode)
                    .Replace("[measurementscheduledate]", measurementScheduleDate).Replace("[assignto]", assignTo)
                    .Replace("[contactnumber]", contactNumber).Replace("[buildingaddress]", buildingAddress);
                MimeMessage email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                };
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "New Inquiry";
                BodyBuilder builder = new BodyBuilder
                {
                    HtmlBody = MailText
                };
                email.Body = builder.ToMessageBody();
                using SmtpClient smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }
        }

        public async Task SendDesignEmailAsync(string toEmail, string inquiryCode, string reviewDesignURL,
            string approveDesignURL, string rejectDesignURL)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\DesignReviewTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[ReviewDesignURL]", reviewDesignURL);
                //MailText = MailText.Replace("[ApproveDesignURL]", approveDesignURL).Replace("[RejectDesignURL]", rejectDesignURL).Replace("[ReviewDesignURL]", reviewDesignURL);
                MimeMessage email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                };
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Design Review of " + inquiryCode;
                BodyBuilder builder = new BodyBuilder
                {
                    HtmlBody = MailText
                };
                email.Body = builder.ToMessageBody();
                using SmtpClient smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }
        }

        public async Task SendQuotationEmailAsync(string toEmail, string inquiryCode, string reviewQuotation,
            string advancePaymentRate, string amount, string promo, string vAT, string totalAmount, string validityTill,
            string approveQuotationURL, string rejectQuotationURL)
        {
            try
            {
                var FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\QuotationApprovalTemplate.html";
                var str = new StreamReader(FilePath);
                var MailText = str.ReadToEnd();
                str.Close();
                //MailText = MailText.Replace("[ReviewQuotationURL]", reviewQuotation).Replace("[AdvancePaymentRate]", advancePaymentRate).Replace("[Amount]", amount).Replace("[Promo]", promo).Replace("[VAT]", vAT).Replace("[TotalAmount]", totalAmount).Replace("[ValidityTill]", validityTill).Replace("[ApproveQuotationURL]", approveQuotationURL).Replace("[RejectQuotationURL]", rejectQuotationURL);
                MailText = MailText.Replace("[ReviewQuotationURL]", reviewQuotation)
                    .Replace("[AdvancePaymentRate]", advancePaymentRate).Replace("[Amount]", amount)
                    .Replace("[Promo]", promo).Replace("[VAT]", vAT).Replace("[TotalAmount]", totalAmount)
                    .Replace("[ValidityTill]", validityTill);
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                };
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Quotation Approval of " + inquiryCode;
                var builder = new BodyBuilder
                {
                    HtmlBody = MailText
                };
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }
        }

        public async Task ApproveQuotationEmailAsync(string toEmail, string inquiryCode, string reviewQuotation,
            string advancePaymentRate, string amount, string promo, string vAT, string totalAmount, string validityTill,
            string approveQuotationURL, string rejectQuotationURL, List<IFormFile> Attachments)
        {
            try
            {
                var FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\QuotationApprovalTemplate.html";
                var str = new StreamReader(FilePath);
                var MailText = str.ReadToEnd();
                str.Close();
                //MailText = MailText.Replace("[ReviewQuotationURL]", reviewQuotation).Replace("[AdvancePaymentRate]", advancePaymentRate).Replace("[Amount]", amount).Replace("[Promo]", promo).Replace("[VAT]", vAT).Replace("[TotalAmount]", totalAmount).Replace("[ValidityTill]", validityTill).Replace("[ApproveQuotationURL]", approveQuotationURL).Replace("[RejectQuotationURL]", rejectQuotationURL);
                MailText = MailText.Replace("[ReviewQuotationURL]", reviewQuotation)
                    .Replace("[AdvancePaymentRate]", advancePaymentRate).Replace("[Amount]", amount)
                    .Replace("[Promo]", promo).Replace("[VAT]", vAT).Replace("[TotalAmount]", totalAmount)
                    .Replace("[ValidityTill]", validityTill);
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                };
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Quotation Approval of " + inquiryCode;
                var builder = new BodyBuilder
                {
                    HtmlBody = MailText
                };
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }
        }
    }
}