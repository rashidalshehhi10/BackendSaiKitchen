﻿using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class NewsletterController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetAllNewsletter()
        {
            var newsletterTypes = newsletterRepository.FindByCondition(x => x.IsDeleted == false).Select(x => new
            {
                newsletterId = x.NewsletterId,
                newsletterHeading = x.NewsletterHeading,
                newsletterBody = x.NewsletterBody,
                newsletterAttachmentUrl = x.NewsletterAttachmentUrl,
                newsletterTypeId = x.NewsletterTypeId,
                newsletterTypeName = x.NewsletterType.NewsletterTypeName,
                newsletterSendingDate = x.NewsletterSendingDate,
                newsletterFrequencyId = x.NewsletterFrequencyId,
                newsletterFrequencyName = x.NewsletterFrequency.NewsletterFrequencyName,
                Isactive = x.IsActive
            });
            response.data = newsletterTypes;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> SendEmail(SendEmail email)
        {
            List<IFormFile> files = new List<IFormFile>();
            var newsletter = newsletterRepository.FindByCondition(x => x.IsDeleted == false && x.NewsletterId == email.NewsletterId).FirstOrDefault();
            if (newsletter != null)
            {
                if (email.Emailto != string.Empty && email.Emailto != null)
                {
                    if (newsletter.NewsletterAttachmentUrl != string.Empty && newsletter.NewsletterAttachmentUrl != null)
                    {
                        files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(newsletter.NewsletterAttachmentUrl)));
                    }
                    try
                    {
                        await mailService.SendEmailAsync(new MailRequest
                        {
                            Subject = newsletter.NewsletterHeading,
                            ToEmail = email.Emailto,
                            Body = newsletter.NewsletterBody,
                            Attachments = files
                        });
                    }
                    catch (Exception e)
                    {
                        Sentry.SentrySdk.CaptureMessage(e.Message);
                    }
                    response.data = newsletter;
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "enter the email ";
                }
                
            }
            else
            {
                response.isError = true;
                response.errorMessage = "newsletter not found";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetNewsletterById(int newsletterId)
        {
            var newsletter = newsletterRepository.FindByCondition(x => x.IsDeleted == false && x.NewsletterId == newsletterId).Select(x => new
            {
                newsletterId = x.NewsletterId,
                newsletterHeading = x.NewsletterHeading,
                newsletterBody = x.NewsletterBody,
                newsletterAttachmentUrl = x.NewsletterAttachmentUrl,
                newsletterTypeId = x.NewsletterTypeId,
                newsletterTypeName = x.NewsletterType.NewsletterTypeName,
                newsletterSendingDate = x.NewsletterSendingDate,
                newsletterFrequencyId = x.NewsletterFrequencyId,
                newsletterFrequencyName = x.NewsletterFrequency.NewsletterFrequencyName,
                Isactive = x.IsActive
            }).FirstOrDefault();
            if (newsletter != null)
            {
                response.data = newsletter;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "News letter Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object CreateNewsletter(NewsletterCustom newsletter)
        {
            Newsletter _new = new Newsletter();
            if (newsletter != null)
            {
                _new.NewsletterAttachmentUrl = newsletter.NewsletterAttachmentUrl;
                _new.NewsletterBody = newsletter.NewsletterBody;
                _new.NewsletterFrequencyId = newsletter.NewsletterFrequencyId;
                _new.NewsletterHeading = newsletter.NewsletterHeading;
                _new.NewsletterSendingDate = newsletter.NewsletterSendingDate;
                _new.NewsletterTypeId = newsletter.NewsletterTypeId;
                _new.AddedBy = newsletter.AddedBy;
                _new.IsActive = newsletter.isactive;
                _new.IsDeleted = false;
                _new.CreatedBy = Constants.userId;
                _new.CreatedDate = Helper.Helper.GetDateTime();
                newsletterRepository.Create(_new);
                response.data = _new;
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Please enter the data correctly";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateNewsletter(NewsletterCustom newsletter)
        {
            Newsletter _new = newsletterRepository.FindByCondition(x => x.NewsletterId == newsletter.NewsletterId && x.IsDeleted == false).FirstOrDefault();
            if (_new != null)
            {
                _new.NewsletterAttachmentUrl = newsletter.NewsletterAttachmentUrl;
                _new.NewsletterBody = newsletter.NewsletterBody;
                _new.NewsletterFrequencyId = newsletter.NewsletterFrequencyId;
                _new.NewsletterHeading = newsletter.NewsletterHeading;
                _new.NewsletterSendingDate = newsletter.NewsletterSendingDate;
                _new.NewsletterTypeId = newsletter.NewsletterTypeId;
                _new.AddedBy = newsletter.AddedBy;
                _new.IsActive = newsletter.isactive;
                _new.UpdatedBy = Constants.userId;
                _new.CreatedDate = Helper.Helper.GetDateTime();
                newsletterRepository.Update(_new);
                response.data = _new;
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "new Letter Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteNewsletter(int NewsletterId)
        {
            Newsletter _new = newsletterRepository.FindByCondition(x => x.NewsletterId == NewsletterId && x.IsDeleted == false).FirstOrDefault();
            if (_new != null)
            {
                _new.IsActive = false;
                _new.IsDeleted = true;
                _new.UpdatedBy = Constants.userId;
                _new.CreatedDate = Helper.Helper.GetDateTime();
                newsletterRepository.Update(_new);
                response.data = _new;
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "new Letter Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object IsactiveNewsletter(Isactive active)
        {
            Newsletter _new = newsletterRepository.FindByCondition(x => x.NewsletterId == active.NewsletterId && x.IsDeleted == false).FirstOrDefault();
            if (_new != null)
            {
                _new.IsActive = active.isactive;
                _new.UpdatedBy = Constants.userId;
                _new.CreatedDate = Helper.Helper.GetDateTime();
                newsletterRepository.Update(_new);
                response.data = _new;
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "new Letter Not Found";
            }
            return response;
        }
    }
}
