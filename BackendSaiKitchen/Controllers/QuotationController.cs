using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using Stripe;
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

        //[AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationbyId(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                 && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => (y.InquiryStatusId == (int)inquiryStatus.quotationPending || y.InquiryStatusId == (int)inquiryStatus.quotationRejected || y.InquiryStatusId == (int)inquiryStatus.quotationDelayed) && y.IsActive == true && y.IsDeleted == false).Count()))
                .Include(x => x.Promo)
                .Include(x => x.Payments.Where(y => y.PaymentTypeId == (int)paymenttype.Measurement && y.PaymentStatusId == (int)paymentstatus.PaymentApproved && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer).Include(x => x.Building).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Workscope)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();
            if (inquiry != null)
            {
                GetfeesForQuotation getfees = new GetfeesForQuotation()
                {
                    inquiry = inquiry,
                    fees = feesRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
                };
                if (getfees == null)
                {
                    response.isError = true;
                    response.errorMessage = "Inquiry doesnt exist";
                }
                else
                {
                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    response.data = getfees;
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry doesnt exist";
            }
            return response;
        }

        [AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationAssignmentbyBranchId(int branchId)
        {

            var inquiries = inquiryRepository.FindByCondition(x => x.BranchId == branchId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => (y.InquiryStatusId == (int)inquiryStatus.quotationSchedulePending) && y.IsActive == true && y.IsDeleted == false).Count()))
                .Include(x => x.Quotations.Where(y => y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Workscope)
                .Select(x => new ViewInquiryDetail()
                {
                    InquiryId = x.InquiryId,

                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                    //WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).First(),
                    //WorkScopeCount = x.InquiryWorkscopes.Count,
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
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp,
                    BranchId = x.BranchId,
                    InquiryAddedBy = x.ManagedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy,
                    NoOfRevision = x.Quotations.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList(),
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }
        // [AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationbyBranchId(int branchId)
        {

            var inquiries = inquiryRepository.FindByCondition(x => x.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.quotationPending || x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed) && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                  && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => (y.InquiryStatusId == (int)inquiryStatus.quotationPending || y.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed) && y.IsActive == true && y.IsDeleted == false).Count()))
                  .Select(x => new ViewInquiryDetail()
                  {
                      InquiryId = x.InquiryId,

                      InquiryDescription = x.InquiryDescription,
                      InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                      //WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).First(),
                      //WorkScopeCount = x.InquiryWorkscopes.Count,
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
                      CustomerWhatsapp = x.Customer.CustomerWhatsapp,
                      BranchId = x.BranchId,
                      InquiryAddedBy = x.ManagedByNavigation.UserName,
                      InquiryAddedById = x.ManagedBy,
                      NoOfRevision = x.Quotations.Where(y => y.IsDeleted == false).Count(),
                      InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                      QuotationScheduleDate = x.QuotationScheduleDate,
                      WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList(),
                      CommentAddedOn = x.InquiryCommentsAddedOn,
                      DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.DesignAddedOn).FirstOrDefault(),
                      MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                      QuotationAddedOn = x.QuotationAddedOn
                  }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

        static List<Models.File> files = new List<Models.File>();
        static List<IFormFile> formFile = new List<IFormFile>();

        [AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Create)]
        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddQuotation(CustomQuotation customQuotation)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == customQuotation.InquiryId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                 && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => (y.InquiryStatusId == (int)inquiryStatus.quotationPending || y.InquiryStatusId == (int)inquiryStatus.quotationRejected) && y.IsActive == true && y.IsDeleted == false).Count()))
                .Include(x => x.Customer).Include(x => x.Building).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Workscope)
                 .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                 .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                Quotation quotation = new Quotation();
                quotation.AdvancePayment = customQuotation.AdvancePayment;
                quotation.BeforeInstallation = customQuotation.BeforeInstallation;
                quotation.AfterDelivery = customQuotation.AfterDelivery;
                quotation.Description = customQuotation.Description;
                quotation.Discount = customQuotation.Discount;
                quotation.InquiryId = customQuotation.InquiryId;
                quotation.QuotationValidityDate = customQuotation.QuotationValidityDate;
                quotation.Vat = customQuotation.Vat;
                quotation.ProposalReferenceNumber = customQuotation.ProposalReferenceNumber;
                quotation.IsActive = true;
                quotation.IsDeleted = false;
                quotation.Amount = customQuotation.Amount;
                quotation.TotalAmount = customQuotation.TotalAmount;
                quotation.IsInstallment = customQuotation.IsInstallment;
                quotation.NoOfInstallment = customQuotation.NoOfInstallment;
                quotation.QuotationStatusId = (int)inquiryStatus.quotationWaitingForCustomerApproval;
                quotation.CalculationSheetFile = customQuotation.CalculationSheetFile;
                quotation.CreatedDate = Helper.Helper.GetDateTime();
                quotation.CreatedBy = Constants.userId;
                quotation.UpdatedBy = Constants.userId;
                quotation.UpdatedDate = Helper.Helper.GetDateTime();
                quotation.QuotationAddedBy = Constants.userId;
                quotation.QuotationAddedDate = Helper.Helper.GetDateTime();


                if (customQuotation.QuotationFiles.Count > 0)
                {
                    files.Clear();
                    formFile.Clear();
                    foreach (var fileUrl in customQuotation.QuotationFiles)
                    {
                        //var fileUrl = await Helper.Helper.UploadFile(file);
                        if (fileUrl != null)
                        {
                            files.Add(new Models.File
                            {
                                FileUrl = fileUrl,
                                FileName = fileUrl.Split('.')[0],
                                FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                IsImage = fileUrl.Split('.').Length > 1,
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
                    inquiry.QuotationAssignTo = Constants.userId;
                    decimal percent = 0;
                    var amountwithoutAdvance = decimal.Parse(customQuotation.TotalAmount) - ((decimal.Parse(customQuotation.TotalAmount) / 100) * decimal.Parse(customQuotation.AdvancePayment));
                    quotation.Payments.Add(new Payment()
                    {
                        PaymentAmountinPercentage = decimal.Parse(customQuotation.AdvancePayment),
                        InquiryId = customQuotation.InquiryId,
                        PaymentName = "Advance Payment",
                        PaymentStatusId = (int)paymentstatus.PaymentCreated,
                        PaymentTypeId = (int)paymenttype.AdvancePayment,
                        PaymentDetail = "Advance Payment of " + customQuotation.InquiryId,
                        PaymentAmount = Decimal.Truncate((decimal.Parse(customQuotation.TotalAmount) / 100) * decimal.Parse(customQuotation.AdvancePayment) * 100),
                        PaymentExpectedDate = customQuotation.QuotationValidityDate,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedDate = Helper.Helper.GetDateTime(),
                        CreatedBy = Constants.userId,
                        UpdatedBy = Constants.userId,
                        UpdatedDate = Helper.Helper.GetDateTime(),

                    });
                    if (customQuotation.IsInstallment == true)
                    {

                        for (int i = 0; i < customQuotation.Payments.Count; i++)
                        {
                            var pay = customQuotation.Payments[i];
                            percent += (decimal)pay.PaymentAmountinPercentage;
                            if (customQuotation.Payments.Count - 1 == i)
                            {
                                pay.PaymentAmountinPercentage += ((100 - decimal.Parse(customQuotation.AdvancePayment)) - percent);
                            }

                            var paymentAmount = ((amountwithoutAdvance / 100) * pay.PaymentAmountinPercentage) * 100;
                            quotation.Payments.Add(new Payment()
                            {
                                PaymentAmountinPercentage = pay.PaymentAmountinPercentage,
                                InquiryId = customQuotation.InquiryId,
                                PaymentName = "Installment# " + (i + 1),
                                PaymentStatusId = (int)paymentstatus.InstallmentCreated,
                                PaymentTypeId = (int)paymenttype.Installment,
                                PaymentDetail = "Installment of " + customQuotation.InquiryId,
                                PaymentAmount = Decimal.Truncate((decimal)paymentAmount),
                                PaymentExpectedDate = pay.PaymentExpectedDate,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),

                            });
                            //Helper.Helper.AddPayment(paymentAmount);

                        }
                    }
                    else
                    {
                        quotation.Payments.Add(new Payment()
                        {
                            PaymentAmountinPercentage = decimal.Parse(customQuotation.BeforeInstallation),
                            InquiryId = customQuotation.InquiryId,
                            PaymentName = "Before Installation",
                            PaymentStatusId = (int)paymentstatus.PaymentCreated,
                            PaymentTypeId = (int)paymenttype.BeforeInstallation,
                            PaymentDetail = "Before Installation of " + customQuotation.InquiryId,
                            PaymentAmount = Decimal.Truncate(((decimal.Parse(customQuotation.TotalAmount) / 100) * decimal.Parse(customQuotation.BeforeInstallation)) * 100),
                            PaymentExpectedDate = "",
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = Helper.Helper.GetDateTime(),
                            CreatedBy = Constants.userId,
                            UpdatedBy = Constants.userId,
                            UpdatedDate = Helper.Helper.GetDateTime(),

                        });
                        customQuotation.AfterDelivery = (100 - (decimal.Parse(customQuotation.BeforeInstallation) + decimal.Parse(customQuotation.AdvancePayment))).ToString();
                        quotation.Payments.Add(new Payment()
                        {
                            PaymentAmountinPercentage = decimal.Parse(customQuotation.AfterDelivery),
                            InquiryId = customQuotation.InquiryId,
                            PaymentName = "After Delivery",
                            PaymentStatusId = (int)paymentstatus.PaymentCreated,
                            PaymentTypeId = (int)paymenttype.AfterDelivery,
                            PaymentDetail = "After Delivery of " + customQuotation.InquiryId,
                            PaymentAmount = Decimal.Truncate(((decimal.Parse(customQuotation.TotalAmount) / 100) * decimal.Parse(customQuotation.AfterDelivery)) * 100),
                            PaymentExpectedDate = "",
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = Helper.Helper.GetDateTime(),
                            CreatedBy = Constants.userId,
                            UpdatedBy = Constants.userId,
                            UpdatedDate = Helper.Helper.GetDateTime(),

                        });
                    }
                    foreach (var payment in quotation.Payments)
                    {
                        if (payment.PaymentAmount == 0)
                        {
                            payment.IsActive = false;
                        }
                    }

                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    inquiry.QuotationAddedOn = Helper.Helper.GetDateTime();
                    inquiry.Quotations.Add(quotation);
                    inquiryRepository.Update(inquiry);
                    response.data = null;

                    List<int?> roletypeId = new List<int?>();
                    roletypeId.Add((int)roleType.Manager);

                    sendNotificationToHead(
                       "New Quotation Added Of Inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId, false,
                       " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null,
                     //true,
                     //Url.ActionLink("AcceptQuotation", "QuotationController", new { id = quotation.InquiryId }),
                     //Url.ActionLink("DeclineQuotation", "QuotationController", new { id = quotation.InquiryId }),
                     roletypeId,
                     Constants.branchId,
                     (int)notificationCategory.Quotation);



                    await mailService.SendQuotationEmailAsync(inquiry.Customer.CustomerEmail, inquiry.InquiryCode, Constants.CRMBaseUrl + "/invoice.html?inquiryId=" + inquiry.InquiryId, quotation.AdvancePayment, quotation.Amount, quotation.Discount, quotation.Vat, quotation.TotalAmount, quotation.QuotationValidityDate, Constants.ServerBaseURL + "/api/Quotation/AcceptQuotation?inquiryId=" + inquiry.InquiryId, Constants.ServerBaseURL + "/api/Quotation/DeclineQuotation?inquiryId=" + inquiry.InquiryId);


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
                response.errorMessage = "Inquiry doesnt exist";
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
            List<TermsAndCondition> terms = termsAndConditionsRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            ViewQuotation viewQuotation = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)
                .Select(x => new ViewQuotation
                {
                    InvoiceNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationId,
                    CreatedDate = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).CreatedDate,
                    ValidDate = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationValidityDate,
                    //SerialNo =
                    Description = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Description,
                    Discount = x.IsMeasurementPromo == false ? x.PromoDiscount : "0",
                    MeasurementFee = x.Payments.FirstOrDefault(y => y.PaymentTypeId == (int)paymenttype.Measurement && y.PaymentStatusId == (int)paymentstatus.PaymentApproved && y.IsActive == true && y.IsDeleted == false).PaymentAmount.ToString(),
                    Amount = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Amount,
                    Vat = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Vat,
                    IsInstallment = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).IsInstallment,
                    //MeasurementFees = x.Payments.OrderBy(y => y.PaymentId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Fees.FeesAmount,
                    AdvancePayment = x.Payments.FirstOrDefault(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true && y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    BeforeInstallation = x.Payments.FirstOrDefault(y => y.PaymentTypeId == (int)paymenttype.BeforeInstallation && y.IsActive == true && y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    AfterDelivery = x.Payments.FirstOrDefault(y => y.PaymentTypeId == (int)paymenttype.AfterDelivery && y.IsActive == true && y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    installments = x.Payments.Where(y => y.PaymentTypeId == (int)paymenttype.Installment && y.IsActive == true && y.IsDeleted == false).ToList(),
                    CustomerName = x.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp,
                    QuotationScheduleDate = x.QuotationScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress,
                    BranchAddress = x.Branch.BranchAddress,
                    BranchContact = x.Branch.BranchContact,
                    ProposalReferenceNumber = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).ProposalReferenceNumber,
                    TermsAndConditionsDetail = terms,
                    Files = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Files,
                    Quantity = q,//x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.WorkscopeId).GroupBy(g => g.WorkscopeId).Select(g => g.Count()).ToList(),
                    inquiryWorkScopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.WorkscopeId).Select(x => x.Workscope.WorkScopeName).ToList(),
                    TotalAmount = x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).TotalAmount

                }).FirstOrDefault();
            if (viewQuotation != null)
            {
                try
                {
                    viewQuotation.invoiceDetails = new List<InvoiceDetail>();
                    for (int i = 0; i < viewQuotation.inquiryWorkScopeNames.Count; i++)
                    {
                        viewQuotation.invoiceDetails.Add(new InvoiceDetail() { inquiryWorkScopeNames = viewQuotation.inquiryWorkScopeNames[i], Quantity = viewQuotation.Quantity[i] });
                    }

                }
                catch (Exception ex)
                {

                    Serilog.Log.Error(ex.Message);
                }

                viewQuotation.TermsAndConditionsDetail.RemoveAll(x => x.IsInstallmentTerms != viewQuotation.IsInstallment);

                if (viewQuotation.IsInstallment == true)
                {
                    for (int i = 0; i < viewQuotation.installments.Count - 1; i++)
                    {
                        viewQuotation.TermsAndConditionsDetail.Add(new TermsAndCondition() { TermsAndConditionsDetail = viewQuotation.TermsAndConditionsDetail[1].TermsAndConditionsDetail, IsInstallmentTerms = true });
                    }
                }
                int j = -1;
                viewQuotation.TermsAndConditionsDetail.ForEach((x) =>
                {
                    if (x.IsInstallmentTerms == viewQuotation.IsInstallment)
                    {
                        if (viewQuotation.IsInstallment == false)
                        {
                            x.TermsAndConditionsDetail = x.TermsAndConditionsDetail.Replace("[AdvancePayment]", viewQuotation.AdvancePayment + "%").Replace("[BeforeInstallation]", viewQuotation.BeforeInstallation + "%").Replace("[AfterDelivery]", viewQuotation.AfterDelivery + "%");
                        }
                        else
                        {
                            if (j == -1)
                            {
                                x.TermsAndConditionsDetail = x.TermsAndConditionsDetail.Replace("[AdvancePayment]", viewQuotation.AdvancePayment + "%");
                            }
                            else
                            {
                                x.TermsAndConditionsDetail = x.TermsAndConditionsDetail.Replace("[noofInstallment]", (j + 1) + "").Replace("[installmentPercentage]", viewQuotation.installments[j].PaymentAmountinPercentage + "%").Replace("[installmentDate]", viewQuotation.installments[j].PaymentExpectedDate + "");
                            }

                            j++;
                        }
                    }
                    else
                    {
                        //viewQuotation.TermsAndConditionsDetail.Remove(x);
                    }
                });

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
            }
            else
            {
                response.errorMessage = "Inquiry doesnt exist";
                response.isError = true;

            }
            return response;

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> ClientApproveQuotationAsync(UpdateQuotationStatus updateQuotation)
        {
            List<IFormFile> files = new List<IFormFile>();
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == updateQuotation.inquiryId && x.IsActive == true && x.IsDeleted == false)
               .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.QuotationStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval && y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer)
                .FirstOrDefault();
            if (inquiry != null)
            {

                int status = updateQuotation.PaymentMethod.ToString() != null && updateQuotation.PaymentMethod.ToString() != "" ? (int)inquiryStatus.jobOrderFilesPending : (int)inquiryStatus.quotationAccepted;
                inquiry.InquiryStatusId = status;
                inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;

                //inquiry.Quotations.FirstOrDefault().QuotationStatusId = (int)inquiryStatus.quotationAccepted;

                foreach (var workscope in inquiry.InquiryWorkscopes)
                {
                    workscope.InquiryStatusId = status;
                }
                foreach (var payment in inquiry.Payments)
                {
                    payment.PaymentModeId = updateQuotation.SelectedPaymentMode;
                    if (payment.PaymentModeId == (int)paymentMode.OnlinePayment)
                    {
                        payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                        payment.ClientSecret = updateQuotation.ClientSecret;
                        payment.PaymentMethod = updateQuotation.PaymentMethod;
                        payment.PaymentIntentToken = updateQuotation.PaymentIntentToken;
                        payment.InvoiceCode = "INV" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + "" + inquiry.Quotations.FirstOrDefault().QuotationId + "" + payment.PaymentId;

                    }
                    else
                    {
                        payment.PaymentStatusId = (int)paymentstatus.PaymentPending;
                    }
                    if (payment.PaymentAmount == 0)
                    {
                        payment.IsActive = false;
                    }
                }

                foreach (var quotation in inquiry.Quotations)
                {
                    quotation.FeedBackReactionId = updateQuotation.FeedBackReactionId;
                    quotation.Description = updateQuotation.reason;
                    quotation.QuotationCode = "QTN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + "" + quotation.QuotationId;
                    quotation.QuotationStatusId = (int)inquiryStatus.quotationAccepted;

                    if (updateQuotation.Pdf != null && updateQuotation.Pdf.Count() >= 0)
                    {
                        var fileUrl = await Helper.Helper.UploadFile(updateQuotation.Pdf);
                        quotation.Files.Add(new Models.File
                        {
                            FileUrl = fileUrl.Item1,
                            FileName = fileUrl.Item1.Split('.')[0],
                            FileContentType = fileUrl.Item2,
                            IsImage = false,
                            IsActive = true,
                            IsDeleted = false,
                        });
                    }
                    foreach (var file in quotation.Files)
                    {
                        files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(file.FileUrl)));
                    }

                }
                inquiryRepository.Update(inquiry);
                try
                {
                    await mailService.SendEmailAsync(new MailRequest { Subject = "Quotation Files", ToEmail = inquiry.Customer.CustomerEmail, Body = "Quotation File", Attachments = files });

                }
                catch (Exception ex)
                {
                    Sentry.SentrySdk.CaptureMessage(ex.Message);
                }
                List<int?> roletypeId = new List<int?>();
                roletypeId.Add((int)roleType.Manager);
                try
                {
                    sendNotificationToHead("Quotation Of inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Approved By Client", false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roletypeId, inquiry.BranchId, (int)notificationCategory.Quotation);
                }
                catch (Exception ex)
                {

                    Sentry.SentrySdk.CaptureMessage(ex.Message);
                }
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
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.QuotationStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.PaymentTypeId != (int)paymenttype.Measurement && y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                inquiry.Quotations.FirstOrDefault().QuotationStatusId = (int)inquiryStatus.quotationRejected;
                foreach (var payment in inquiry.Payments)
                {
                    payment.IsActive = false;
                    //payment.PaymentStatusId = (int)paymentstatus.PaymentRejected;
                }

                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                }

                foreach (var quotation in inquiry.Quotations)
                {
                    quotation.IsActive = false;
                    quotation.Description = updateQuotation.reason;
                    quotation.FeedBackReactionId = updateQuotation.FeedBackReactionId;
                }
                List<int?> roletypeId = new List<int?>();
                roletypeId.Add((int)roleType.Manager);
                try
                {
                    sendNotificationToHead("Quotation Of inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Rejected By Client Reason: " + updateQuotation.reason, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roletypeId, inquiry.BranchId, (int)notificationCategory.Quotation);
                }
                catch (Exception ex)
                {

                    Sentry.SentrySdk.CaptureMessage(ex.Message);
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



        [HttpPost]
        [Route("[action]")]
        public object stripe(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false).Include(x => x.Customer).Include(x => x.Quotations.Where(y => y.QuotationStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval && y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Payments.Where(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                decimal d = 0;
                long amount = 0;
                //foreach (var quotaion in inquiry.Quotations)
                //{
                d = (decimal.Parse(inquiry.Quotations.LastOrDefault().AdvancePayment) * (decimal.Parse(inquiry.Quotations.LastOrDefault().TotalAmount) / 100)) * 100;
                amount += Convert.ToInt64(d);
                //}
                var paymentIntents = new PaymentIntentService();
                var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "aed",
                    ReceiptEmail = inquiry.Customer.CustomerEmail

                });
                inquiry.Quotations.LastOrDefault().Payments.LastOrDefault(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment).PaymentStatusId = (int)paymentstatus.PaymentPending;
                inquiry.Quotations.LastOrDefault().Payments.LastOrDefault(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment).PaymentModeId = (int)paymentMode.OnlinePayment;
                inquiry.Quotations.LastOrDefault().Payments.LastOrDefault(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment).ClientSecret = paymentIntent.ClientSecret;
                response.data = paymentIntent.ClientSecret;
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry Not Found";
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object QuotationSchedule(quotationScheduleUpdate scheduleUpdate)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == scheduleUpdate.inquiryId && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                 && x.IsDeleted == false && (x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x.InquiryWorkscopes.Where(y => (y.InquiryStatusId == (int)inquiryStatus.quotationSchedulePending) && y.IsActive == true && y.IsDeleted == false).Count()))
               .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                //.Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && x.IsDeleted == false))
                .FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.quotationPending;
                inquiry.QuotationScheduleDate = scheduleUpdate.date;
                // inquiry.QuotationAssignTo = scheduleUpdate.userId;
                foreach (var inworkscope in inquiry.InquiryWorkscopes)
                {
                    inworkscope.InquiryStatusId = (int)inquiryStatus.quotationPending;
                }

                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry Not Found";
                response.isError = true;
            }
            return response;
        }
    }
}
