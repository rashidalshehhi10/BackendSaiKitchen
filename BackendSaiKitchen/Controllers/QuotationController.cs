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
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationbyId(int inquiryId)
        {
           var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => y.InquiryStatusId == (int)inquiryStatus.quotationPending && y.IsActive == true && y.IsDeleted == false).Count()))
               .Include(x=>x.Customer).Include(x=>x.Building).Include(x=>x.InquiryWorkscopes.Where(y=>y.IsActive==true && y.IsDeleted==false)).ThenInclude(x=>x.Workscope)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry == null)
            {
                response.isError = true;
                response.errorMessage = "No Inquiry Found";
            }
            else
            {
                inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                response.data = inquiry;
            }
            return response;

        }
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationbyBranchId(int branchId)
        {

            var inquiries = inquiryRepository.FindByCondition(x => x.BranchId == branchId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => y.InquiryStatusId == (int)inquiryStatus.quotationPending && y.IsActive == true && y.IsDeleted == false).Count())).Include(x => x.Quotations.Where(y => y.IsDeleted == false)).Include(x => x.Building).Include(x => x.Customer).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Workscope).Select(x => new ViewInquiryDetail()
                {
                    InquiryId = x.InquiryId,
                   
                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                    WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).First(),
                    WorkScopeCount = x.InquiryWorkscopes.Count,
                    Status = x.InquiryWorkscopes.FirstOrDefault().InquiryStatusId,
                    BuildingAddress = x.Building.BuildingAddress,
                    BuildingCondition = x.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor,
                    BuildingReconstruction = (bool)x.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied = (bool)x.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate = Helper.Helper.GetDateFromString(x.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested,
                    CustomerId = x.CustomerId,
                    CustomerCode = "CS" + x.BranchId + "" + x.CustomerId,
                    CustomerName = x.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact,
                    BranchId = x.BranchId,
                    InquiryAddedBy = x.AddedByNavigation.UserName,
                    InquiryAddedById = x.AddedBy,
                    NoOfRevision = x.Quotations.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
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
                quotation.Description = customQuotation.Description;
                quotation.Discount = customQuotation.Discount;
                quotation.InquiryId = customQuotation.InquiryId;
                quotation.IsActive = true;
                quotation.IsDeleted = false;
                quotation.TotalAmount = customQuotation.TotalAmount;

                if (customQuotation.QuotationFiles.Count > 0)
                {
                    files.Clear();
                    foreach (var file in customQuotation.QuotationFiles)
                    {
                        var fileUrl = await Helper.Helper.UploadFile(file);
                        if (fileUrl != null)
                        {
                            files.Add(new File
                            {
                                FileUrl = fileUrl.Item1,
                                FileName = fileUrl.Item1.Split('.')[0],
                                FileContentType = fileUrl.Item2,
                                IsImage = true,
                                IsActive = true,
                                IsDeleted = false,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                CreatedDate = Helper.Helper.GetDateTime(),
                            });
                        }
                    }
                    quotation.Files = files;

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
            var inquiry = inquiryRepository.FindByCondition(i => i.IsActive == true && i.IsDeleted == false && i.InquiryId == inquiryId && i.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false)).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.quotationWaitingForApproval)).FirstOrDefault();
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
