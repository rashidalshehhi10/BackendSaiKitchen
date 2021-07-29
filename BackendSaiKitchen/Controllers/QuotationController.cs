using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
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

        [AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationbyId(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                 && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => y.InquiryStatusId == (int)inquiryStatus.quotationPending && y.IsActive == true && y.IsDeleted == false).Count()))
                .Include(x => x.Promo)
                .Include(x => x.Payments.Where(y => y.PaymentTypeId == (int)paymenttype.Measurement && y.PaymentStatusId == (int)paymentstatus.PaymentApproved && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer).Include(x => x.Building).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Workscope)
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

        [AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Read)]
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
        static List<IFormFile> formFile = new List<IFormFile>();

        //[AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddQuotation(CustomQuotation customQuotation)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == customQuotation.InquiryId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                 && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => y.InquiryStatusId == (int)inquiryStatus.quotationPending && y.IsActive == true && y.IsDeleted == false).Count()))
                .Include(x => x.Customer).Include(x => x.Building).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Workscope)
                 .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                 .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                Quotation quotation = new Quotation();
                quotation.AdvancePayment = customQuotation.AdvancePayment;
                quotation.Description = customQuotation.Description;
                quotation.Discount = customQuotation.Discount;
                quotation.InquiryId = customQuotation.InquiryId;
                quotation.QuotationValidityDate = customQuotation.QuotationValidityDate;
                quotation.Vat = customQuotation.Vat;
                quotation.IsActive = true;
                quotation.IsDeleted = false;
                quotation.Amount = customQuotation.Amount;
                quotation.TotalAmount = customQuotation.TotalAmount;
                quotation.IsInstallment = customQuotation.IsInstallment;
                quotation.NoOfInstallment = customQuotation.NoOfInstallment;
                quotation.CreatedDate = Helper.Helper.GetDateTime();
                quotation.CreatedBy = Constants.userId;
                quotation.UpdatedBy = Constants.userId;
                quotation.UpdatedDate = Helper.Helper.GetDateTime();


                if (customQuotation.QuotationFiles.Count > 0)
                {
                    files.Clear();
                    formFile.Clear();
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
                        //formFile.Add(Helper.Helper.ConvertBytestoIFormFile(file));
                    }
                    quotation.Files = files;
                    foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                    {
                        inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationWaitingForCustomerApproval;
                    }
                    inquiry.InquiryStatusId = (int)inquiryStatus.quotationWaitingForCustomerApproval;
                    inquiry.Quotations.Add(quotation);
                    decimal percent = 0;
                    if (customQuotation.IsInstallment == true)
                    {

                        for (int i = 0; i < customQuotation.Payments.Count; i++)
                        {
                            var pay = customQuotation.Payments[i];
                            percent += (decimal)pay.PaymentAmountinPercentage;
                            if (customQuotation.Payments.Count - 1 == i)
                            {
                                pay.PaymentAmountinPercentage += (100 - percent);
                            }
                            var paymentAmount = decimal.Parse(customQuotation.TotalAmount) * pay.PaymentAmountinPercentage / 100;
                            inquiry.Payments.Add(new Payment()
                            {
                                PaymentAmountinPercentage = pay.PaymentAmountinPercentage,
                                InquiryId = customQuotation.InquiryId,
                                PaymentName = pay.PaymentName,
                                PaymentStatusId = (int)paymentstatus.InstallmentCreated,
                                PaymentTypeId = (int)paymenttype.Installment,
                                PaymentDetail = pay.PaymentDetail,
                                PaymentAmount = paymentAmount,
                                PaymentExpectedDate = pay.PaymentExpectedDate,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),

                            });
                            Helper.Helper.AddPayment(paymentAmount);

                        }
                    }
                    else
                    {
                        foreach (var pay in customQuotation.Payments)
                        {
                            inquiry.Payments.Add(new Payment()
                            {
                                InquiryId = customQuotation.InquiryId,
                                PaymentName = pay.PaymentName,
                                PaymentStatusId = (int)paymentstatus.PaymentCreated,
                                PaymentTypeId = (int)paymenttype.AdvancePayment,
                                PaymentDetail = pay.PaymentDetail,
                                PaymentAmount = pay.PaymentAmount,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),

                            });
                            Helper.Helper.AddPayment(pay.PaymentAmount);
                        }
                    }

                    inquiryRepository.Update(inquiry);
                    response.data = null;

                    List<int?> roletypeId = new List<int?>();
                    roletypeId.Add((int)roleType.Manager);

                    sendNotificationToHead(
                       " Added a New Quotation", false, null, null,
                     //true,
                     //Url.ActionLink("AcceptQuotation", "QuotationController", new { id = quotation.InquiryId }),
                     //Url.ActionLink("DeclineQuotation", "QuotationController", new { id = quotation.InquiryId }),
                     roletypeId,
                     Constants.branchId,
                     (int)notificationCategory.Quotation);


                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    await mailService.SendQuotationEmailAsync(inquiry.Customer.CustomerEmail, inquiry.InquiryCode, quotation.AdvancePayment, quotation.Amount, quotation.Discount, quotation.Vat, quotation.TotalAmount, quotation.QuotationValidityDate, Constants.ServerBaseURL + "/api/Quotation/AcceptQuotation?inquiryId=" + inquiry.InquiryId, Constants.ServerBaseURL + "/api/Quotation/DeclineQuotation?inquiryId=" + inquiry.InquiryId);


                    context.SaveChanges();
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = Constants.QuotationFileMissing;
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Designs are not all accepted";
            }
            return response;
        }

        //[AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Update)]
        [HttpGet]
        [Route("[action]")]
        public object AcceptQuotation(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(i => i.IsActive == true && i.IsDeleted == false && i.InquiryId == inquiryId && i.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)).FirstOrDefault();
            if (inquiry != null)
            {

                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationAccepted;
                }


                inquiry.InquiryStatusId = (int)inquiryStatus.quotationAccepted;

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

        [AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Update)]
        [HttpGet]
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
                inquiry.InquiryStatusId = (int)inquiryStatus.quotationRejected;

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
        public async Task<object> ViewQuotationForCustomer(int inquiryId)
        {
            //inquiryWorkscopeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList().ForEach(c => {
            //    int a = c.InquiryWorkscopeId;
            //});
            List<int> q = inquiryWorkscopeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == inquiryId).OrderBy(x => x.WorkscopeId).GroupBy(x => x.WorkscopeId).Select(x => x.Count()).ToList();

            ViewQuotation viewQuotation = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)
                .Select(x => new ViewQuotation
                {
                    InvoiceNo = "INV" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true & y.IsDeleted == false).QuotationId,
                    CreatedDate = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).CreatedDate,
                    ValidDate = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationValidityDate,
                    //SerialNo =
                    Description = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Description,
                    Discount = x.PromoDiscount,
                    Amount = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Amount,
                    Vat = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Vat,
                    MeasurementFees = x.Payments.OrderBy(y => y.PaymentId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Fees.FeesAmount,
                    CustomerName = x.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact,
                    BuildingAddress = x.Building.BuildingAddress,
                    Files = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Files,
                    Quantity = q,//x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.WorkscopeId).GroupBy(g => g.WorkscopeId).Select(g => g.Count()).ToList(),
                    inquiryWorkScopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.WorkscopeId).Select(x => x.Workscope.WorkScopeName).ToList(),
                    TotalAmount = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).TotalAmount

                }).FirstOrDefault();  



         //var i=   (from xx in context.InquiryWorkscopes
         //    group xx.InquiryWorkscopeId by xx into g
         //    let count = g.Count()
         //    orderby count descending
         //    select new
         //    {
         //        Count = count,
         //    });

          //var   V = viewQuotation.invoiceDetails.Add(new InvoiceDetail()
          //  {
          //      Quantity = count.ToString(),
          //      inquiryWorkScopeNames = g.Key.Workscope.WorkScopeName,
          //  }),
            //var v = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false).Select(y => y.InquiryWorkscopes.Where(z => z.IsActive == true && z.IsDeleted == false).GroupBy(z => z.WorkscopeId));
           
            //.Select(z=>z.Key

        //    ));
        //viewQuotation.invoiceDetails.Add(inquiryRepository.FindByCondition(x=>x.InquiryWorkscopes.Where))
        //Quantity = new List<object>().AddRange(x.InquiryWorkscopes.GroupBy(y => y.WorkscopeId).Count()),
        //// inquiryWorkScopeNames = x.InquiryWorkscopes.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).Workscope.WorkScopeName.ToList()
        response.data = viewQuotation;
            return response;

        }

        [HttpPost]
        [Route("[action]")]
        public object ClientApproveQuotation(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.quotationAccepted;
                foreach (var payment in inquiry.Payments)
                {
                    payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                }

                inquiryRepository.Update(inquiry);
                response.data = inquiry;
                context.SaveChanges();
            }
            else
            {
                response.isError = false;
                response.errorMessage = "inquiry does not exist";
            }
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object ClientRejectQuotation(UpdateQuotationStatus updateQuotation)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == updateQuotation.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                foreach (var payment in inquiry.Payments)
                {
                    payment.IsActive = false;
                    // payment.PaymentStatusId =(int)paymentstatus.PaymentRejected;
                }

                foreach (var quotation in inquiry.Quotations)
                {
                    quotation.IsActive = false;
                    quotation.Description = updateQuotation.reason;
                }
                inquiryRepository.Update(inquiry);
                response.data = inquiry;
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry Doesn't Exist";
            }

            return response;
        }
    }
}
