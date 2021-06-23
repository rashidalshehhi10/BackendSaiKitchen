﻿using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class QuotationController : BaseController
    {
        public QuotationController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationbyId(int inquiryId)
        {
            response.data = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => y.InquiryStatusId == (int)inquiryStatus.quotationPending && y.IsActive == true && y.IsDeleted == false).Count()))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.FileDesigns.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false));
            return response;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationbyBranchId(int branchId)
        {
            response.data = inquiryRepository.FindByCondition(x => x.BranchId == branchId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => y.InquiryStatusId == (int)inquiryStatus.quotationPending && y.IsActive == true && y.IsDeleted == false).Count()))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.FileDesigns.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false));
            return response;
        }

        static List<File> files = new List<File>();

        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddQuotation(CustomQuotation customQuotation)
        {
            var quotationInquiry = inquiryRepository.FindByCondition(x => x.InquiryId == customQuotation.InquiryId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => y.InquiryStatusId == (int)inquiryStatus.quotationPending && y.IsActive == true && y.IsDeleted == false).Count())).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false));
            var inquiry = inquiryRepository.FindByCondition(i => i.IsActive == true && i.IsDeleted == false && i.InquiryId == customQuotation.InquiryId && i.InquiryWorkscopes.Count > 0).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.designAccepted)).FirstOrDefault();
            var _inquiry = inquiryRepository.FindByCondition(i => i.IsActive == true && i.IsDeleted == false && i.InquiryId == customQuotation.InquiryId && i.InquiryWorkscopes.Count > 0).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.designDelayed || y.InquiryStatusId == (int)inquiryStatus.designRejected || y.InquiryStatusId == (int)inquiryStatus.designPending)).FirstOrDefault();
            if (inquiry != null && _inquiry == null)
            {


                Quotation quotation = new Quotation();

                quotation.AdvancePayment = customQuotation.AdvancePayment;
                quotation.CreatedBy = customQuotation.CreatedBy;
                quotation.CreatedDate = customQuotation.CreatedDate;
                quotation.Description = customQuotation.Description;
                quotation.Discount = customQuotation.Discount;
                quotation.InquiryId = customQuotation.InquiryId;
                quotation.IsActive = customQuotation.IsActive;
                quotation.IsDeleted = customQuotation.IsDeleted;
                quotation.TotalAmount = customQuotation.TotalAmount;
                quotation.UpdatedBy = customQuotation.UpdatedBy;
                quotation.UpdatedDate = customQuotation.UpdatedDate;

                if (customQuotation.QuotationFiles.Count > 0 || customQuotation.ContractFiles.Count > 0)
                {
                    files.Clear();
                    foreach (var file in customQuotation.QuotationFiles)
                    {
                        string Fileurl = await Helper.Helper.UploadFileToBlob(file);
                        if (Fileurl != null)
                        {
                            files.Add(new File
                            {
                                FileUrl = Fileurl,
                                FileName = Fileurl.Split('.')[0],
                                IsActive = true,
                                IsDeleted = false,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                CreatedDate = Helper.Helper.GetDateTime(),
                            });
                        }
                    }
                    quotation.FileQuotations = files;

                    files.Clear();
                    foreach (var file in customQuotation.ContractFiles)
                    {
                        string Fileurl = await Helper.Helper.UploadFileToBlob(file);
                        if (Fileurl != null)
                        {
                            files.Add(new File
                            {
                                FileUrl = Fileurl,
                                FileName = Fileurl.Split('.')[0],
                                IsActive = true,
                                IsDeleted = false,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                CreatedDate = Helper.Helper.GetDateTime(),
                            });
                        }
                    }
                    quotation.FileContracts = files;
                    quotationRepositry.Create(quotation);
                    response.data = quotation;


                    List<int?> roletypeId = new List<int?>();

                    roletypeId.Add((int)roleType.Manager);


                    try
                    {
                        sendNotificationToHead(
                           " Added a New Quotation",
                         true,
                         Url.ActionLink("AcceptQuotation", "QuotationController", new { id = quotation.InquiryId }),
                         Url.ActionLink("DeclineQuotation", "QuotationController", new { id = quotation.InquiryId }),
                         roletypeId,
                         Constants.branchId,
                         (int)notificationCategory.Quotation);

                        await mailService.SendEmailAsync(new MailRequest() { ToEmail = inquiry.Customer.CustomerEmail, Subject = "Quotation Approve", Body = "Do You Approve on this quotation"/*,Attachments = quotation.FileQuotations*/ });
                    }
                    catch (Exception e)
                    {
                        Sentry.SentrySdk.CaptureMessage(e.Message);
                    }

                    context.SaveChanges();
                    return response;


                }
                else
                {
                    response.isError = true;
                    response.errorMessage = Constants.wrongFileUpload;
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Designs are not all accepted";
            }

            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object AcceptQuotation(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(i => i.IsActive == true && i.IsDeleted == false && i.InquiryId == inquiryId && i.InquiryWorkscopes.Any(y=>y.IsActive==true && y.IsDeleted==false)).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.quotationWaitingForApproval)).FirstOrDefault();
            if (inquiry != null)
            {

                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationAccepted;
                }

                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeclineQuotation(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(i => i.IsActive == true && i.IsDeleted == false && i.InquiryId == inquiryId && i.InquiryWorkscopes.Count > 0).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.designAccepted)).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                }

                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }
            return response;
        }

    }
}
