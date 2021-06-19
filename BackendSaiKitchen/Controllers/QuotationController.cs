using BackendSaiKitchen.CustomModel;
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

        static List<File> files = new List<File>();

        public async Task<object> AddQuotation(CustomQuotation customQuotation)
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
                    if (Fileurl!=null)
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
                quotation.QuotationFile = files;
            
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
                quotation.ContractFile = files;
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


            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object AcceptQuotation(UpdateInquiryWorkscopeStatusModel updateMeasurementStatus)
        {
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateMeasurementStatus.InquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designAccepted;
                inquiryWorkscopeRepository.Update(inquiryWorkscope);
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
        public object DeclineQuotation(UpdateInquiryWorkscopeStatusModel updateMeasurementStatus)
        {
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateMeasurementStatus.InquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false).Include(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                inquiryWorkscope.Comments = updateMeasurementStatus.MeasurementComment;
                Helper.Helper.Each(inquiryWorkscope.Measurements, i => {
                    i.IsActive = false;
                    i.MeasurementComment = updateMeasurementStatus.MeasurementComment;
                });
                inquiryWorkscopeRepository.Update(inquiryWorkscope);
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
        public async Task<object> AddUpdateQuotationfiles(QuotationFile quotationFile)
        {
            files.Clear();
            if (quotationFile.base64img != null || quotationFile.base64img.Count > 0)
            {
                try
                {
                    var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == quotationFile.Ininquiryworkscopeid && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    foreach (var file in quotationFile.base64img)
                    {
                        string fileUrl = await Helper.Helper.UploadFileToBlob(file);
                        if (fileUrl != null)
                        {
                            files.Add(new File()
                            {
                                FileUrl = fileUrl,
                                FileName = fileUrl.Split('.')[0],
                                IsActive = true,
                                IsDeleted = false,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                CreatedDate = Helper.Helper.GetDateTime(),

                            });
                        }
                        else
                        {
                            response.isError = true;
                            response.errorMessage = Constants.wrongFileUpload;
                        }
                    }

                    Quotation quotation = new Quotation() { UpdatedBy = Constants.userId, QuotationFile = files };
                    quotation.IsActive = true;
                    quotation.Description = quotationFile.Comment;
                    quotation.IsDeleted = false;
                    inquiryworkscope.InquiryStatusId = (int)inquiryStatus.quotationWaitingForApproval;
                    inquiryworkscope.IsMeasurementDrawing = true;
                    inquiryworkscope.Comments = quotation.Description;
                    quotationRepositry.Create(quotation);
                    inquiryWorkscopeRepository.Update(inquiryworkscope);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                    response.isError = true;
                    response.errorMessage = Constants.QuotationFileMissing;
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = Constants.QuotationFileMissing;
            }
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddUpdateContractfiles(QuotationFile quotationFile)
        {
            files.Clear();
            if (quotationFile.base64img != null || quotationFile.base64img.Count > 0)
            {
                try
                {
                    var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == quotationFile.Ininquiryworkscopeid && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    foreach (var file in quotationFile.base64img)
                    {
                        string fileUrl = await Helper.Helper.UploadFileToBlob(file);
                        if (fileUrl != null)
                        {
                            files.Add(new File()
                            {
                                FileUrl = fileUrl,
                                FileName = fileUrl.Split('.')[0],
                                IsActive = true,
                                IsDeleted = false,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                CreatedDate = Helper.Helper.GetDateTime(),

                            });
                        }
                        else
                        {
                            response.isError = true;
                            response.errorMessage = Constants.wrongFileUpload;
                        }
                    }

                    Quotation quotation = new Quotation() { UpdatedBy = Constants.userId, ContractFile = files };
                    quotation.IsActive = true;
                    quotation.Description = quotationFile.Comment;
                    quotation.IsDeleted = false;
                    inquiryworkscope.InquiryStatusId = (int)inquiryStatus.quotationWaitingForApproval;
                    inquiryworkscope.IsMeasurementDrawing = true;
                    inquiryworkscope.Comments = quotation.Description;
                    quotationRepositry.Create(quotation);
                    inquiryWorkscopeRepository.Update(inquiryworkscope);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                    response.isError = true;
                    response.errorMessage = Constants.QuotationFileMissing;
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = Constants.QuotationFileMissing;
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllQuotations()
        {
            return quotationRepositry.FindByCondition(q => q.IsActive == true &&q.IsDeleted==false);
        }

        [HttpPost]
        [Route("[action]")]
        public object GetQuotationById(int inquiryId)
        {
            var quotation = quotationRepositry.FindByCondition(q => q.InquiryId == inquiryId && q.IsActive == true && q.IsDeleted == false).FirstOrDefault();
            if (quotation != null)
            {
                response.data = quotation;
            }
            else
            {
                response.isError = true;
                response.errorMessage = Constants.QuotationMissing;
            }
            return response;
        }
    }
}
