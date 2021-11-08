using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using Sentry;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constants = BackendSaiKitchen.Helper.Constants;
using File = BackendSaiKitchen.Models.File;
using Log = Serilog.Log;

namespace BackendSaiKitchen.Controllers
{
    public class QuotationController : BaseController
    {
        private static readonly List<File> files = new();
        private static readonly List<IFormFile> formFile = new();

        public QuotationController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }

        //[AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiryForQuotationbyId(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId &&
                    x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                    && x.IsDeleted == false &&
                    x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x
                        .InquiryWorkscopes.Where(y =>
                            (y.InquiryStatusId == (int)inquiryStatus.quotationPending ||
                             y.InquiryStatusId == (int)inquiryStatus.quotationRejected ||
                             y.InquiryStatusId == (int)inquiryStatus.quotationDelayed ||
                             x.InquiryStatusId == (int)inquiryStatus.quotationRevisionRequested) &&
                            y.IsActive == true && y.IsDeleted == false).Count())
                .Include(x => x.Promo)
                .Include(x => x.Payments.Where(y =>
                    y.PaymentTypeId == (int)paymenttype.Measurement &&
                    y.PaymentStatusId == (int)paymentstatus.PaymentApproved && y.IsActive == true &&
                    y.IsDeleted == false))
                .Include(x => x.Customer).Include(x => x.Building)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Workscope)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();
            if (inquiry != null)
            {
                GetfeesForQuotation getfees = new GetfeesForQuotation
                {
                    inquiry = inquiry,
                    fees = FeesRepository
                        .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
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
            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                    x.BranchId == branchId &&
                    x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                    && x.IsDeleted == false &&
                    x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == x
                        .InquiryWorkscopes.Where(y =>
                            y.InquiryStatusId == (int)inquiryStatus.quotationSchedulePending && y.IsActive == true &&
                            y.IsDeleted == false).Count())
                .Include(x => x.Quotations.Where(y => y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Workscope)
                .Select(x => new ViewInquiryDetail
                {
                    InquiryId = x.InquiryId,

                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                    //WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).First(),
                    //WorkScopeCount = x.InquiryWorkscopes.Count,
                    Status = x.InquiryStatusId,
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
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName).ToList(),
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
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
            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                    x.BranchId == branchId &&
                    (x.InquiryStatusId == (int)inquiryStatus.quotationPending ||
                     x.InquiryStatusId == (int)inquiryStatus.quotationRejected ||
                     x.InquiryStatusId == (int)inquiryStatus.quotationDelayed ||
                     x.InquiryStatusId == (int)inquiryStatus.quotationRevisionRequested) &&
                    x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                    && x.IsDeleted == false &&
                    x.InquiryWorkscopes.Count(y => y.IsActive == true && y.IsDeleted == false) == x
                        .InquiryWorkscopes.Count(y => (y.InquiryStatusId == (int)inquiryStatus.quotationPending ||
                                                       y.InquiryStatusId == (int)inquiryStatus.quotationRejected ||
                                                       x.InquiryStatusId == (int)inquiryStatus.quotationDelayed ||
                                                       x.InquiryStatusId == (int)inquiryStatus.quotationRevisionRequested) &&
                                                      y.IsActive == true && y.IsDeleted == false))
                .Select(x => new ViewInquiryDetail
                {
                    InquiryId = x.InquiryId,

                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                    //WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).First(),
                    //WorkScopeCount = x.InquiryWorkscopes.Count,
                    Status = x.InquiryStatusId,
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
                    NoOfRevision = x.Quotations.Count(y => y.IsDeleted == false),
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    QuotationScheduleDate = x.QuotationScheduleDate,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName).ToList(),
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiryForApprovalQuotationbyBranchId(int branchId)
        {
            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                    x.BranchId == branchId && x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForApproval &&
                    x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                    && x.IsDeleted == false &&
                    x.InquiryWorkscopes.Count(y => y.IsActive == true && y.IsDeleted == false) == x
                        .InquiryWorkscopes.Count(y => y.InquiryStatusId == (int)inquiryStatus.quotationWaitingForApproval && y.IsActive == true &&
                                                      y.IsDeleted == false))
                .Select(x => new ViewInquiryDetail
                {
                    InquiryId = x.InquiryId,

                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                    //WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).First(),
                    //WorkScopeCount = x.InquiryWorkscopes.Count,
                    Status = x.InquiryStatusId,
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
                    NoOfRevision = x.Quotations.Count(y => y.IsDeleted == false),
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    QuotationScheduleDate = x.QuotationScheduleDate,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName).ToList(),
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

       // [AuthFilter((int)permission.ManageQuotation, (int)permissionLevel.Create)]
        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddQuotation(CustomQuotation customQuotation)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == customQuotation.InquiryId && x.IsActive == true
                    && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationPending ||
                                                       x.InquiryStatusId == (int)inquiryStatus.quotationRejected ||
                                                       x.InquiryStatusId == (int)inquiryStatus.quotationRevisionRequested ||
                                                       x.InquiryStatusId == (int)inquiryStatus.quotationDelayed
                                                       ))
                .Include(x => x.Customer).Include(x => x.Building)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Workscope)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                Helper.Helper.Each(inquiry.Quotations, x => x.IsActive = false);
                if (customQuotation.PromoCode != null && customQuotation.PromoCode != "")
                {
                    var promo = promoRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.PromoCode == customQuotation.PromoCode).FirstOrDefault();
                    inquiry.Promo = promo;
                    inquiry.PromoId = promo.PromoId;
                    inquiry.PromoDiscount = promo.PromoDiscount;
                }
                Quotation quotation = new Quotation
                {
                    AdvancePayment = customQuotation.AdvancePayment,
                    BeforeInstallation = customQuotation.BeforeInstallation,
                    AfterDelivery = customQuotation.AfterDelivery,
                    //quotation.IsInstallment = customQuotation.IsInstallment;
                    //quotation.NoOfInstallment = customQuotation.NoOfInstallment;
                    Description = customQuotation.Description,
                    Discount = customQuotation.Discount,
                    InquiryId = customQuotation.InquiryId,
                    QuotationValidityDate = customQuotation.QuotationValidityDate,
                    Vat = customQuotation.Vat,
                    ProposalReferenceNumber = customQuotation.ProposalReferenceNumber,
                    IsActive = true,
                    IsDeleted = false,
                    Amount = customQuotation.Amount,
                    TotalAmount = customQuotation.TotalAmount,
                    QuotationStatusId = (int)inquiryStatus.quotationWaitingForApproval,
                    CalculationSheetFile = customQuotation.CalculationSheetFile,
                    IsPaid = customQuotation.IsPaid,
                    CreatedDate = Helper.Helper.GetDateTime(),
                    CreatedBy = Constants.userId,
                    UpdatedBy = Constants.userId,
                    UpdatedDate = Helper.Helper.GetDateTime(),
                    QuotationAddedBy = Constants.userId,
                    QuotationAddedDate = Helper.Helper.GetDateTime()
                };


                if (customQuotation.QuotationFiles.Count > 0)
                {
                    files.Clear();
                    formFile.Clear();
                    foreach (string fileUrl in customQuotation.QuotationFiles)
                    {
                        //var fileUrl = await Helper.Helper.UploadFile(file);
                        if (fileUrl != null)
                        {
                            files.Add(new File
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
                                CreatedDate = Helper.Helper.GetDateTime()
                            });
                        }
                    }
                    //formFile.Add(Helper.Helper.ConvertBytestoIFormFile(file));
                    quotation.Files = files;
                    foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                    {
                        inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationWaitingForApproval;
                    }

                    inquiry.InquiryStatusId = (int)inquiryStatus.quotationWaitingForApproval;
                    inquiry.QuotationAssignTo = Constants.userId;
                    //decimal percent = 0;
                    //var amountwithoutAdvance = decimal.Parse(customQuotation.TotalAmount) - ((decimal.Parse(customQuotation.TotalAmount) / 100) * decimal.Parse(customQuotation.AdvancePayment));
                    //quotation.Payments.Add(new Payment()
                    //{
                    //    PaymentAmountinPercentage = decimal.Parse(customQuotation.AdvancePayment),
                    //    InquiryId = customQuotation.InquiryId,
                    //    PaymentName = "Advance Payment",
                    //    PaymentStatusId = (int)paymentstatus.PaymentCreated,
                    //    PaymentTypeId = (int)paymenttype.AdvancePayment,
                    //    PaymentDetail = "Advance Payment of " + customQuotation.InquiryId,
                    //    PaymentAmount = Decimal.Truncate((decimal.Parse(customQuotation.TotalAmount) / 100) * decimal.Parse(customQuotation.AdvancePayment) * 100),
                    //    PaymentExpectedDate = customQuotation.QuotationValidityDate,
                    //    IsActive = true,
                    //    IsDeleted = false,
                    //    CreatedDate = Helper.Helper.GetDateTime(),
                    //    CreatedBy = Constants.userId,
                    //    UpdatedBy = Constants.userId,
                    //    UpdatedDate = Helper.Helper.GetDateTime(),

                    //});
                    //if (customQuotation.IsInstallment == true)
                    //{

                    //    for (int i = 0; i < customQuotation.Payments.Count; i++)
                    //    {
                    //        var pay = customQuotation.Payments[i];
                    //        percent += (decimal)pay.PaymentAmountinPercentage;
                    //        if (customQuotation.Payments.Count - 1 == i)
                    //        {
                    //            pay.PaymentAmountinPercentage += ((100 - decimal.Parse(customQuotation.AdvancePayment)) - percent);
                    //        }

                    //        var paymentAmount = ((amountwithoutAdvance / 100) * pay.PaymentAmountinPercentage) * 100;
                    //        quotation.Payments.Add(new Payment()
                    //        {
                    //            PaymentAmountinPercentage = pay.PaymentAmountinPercentage,
                    //            InquiryId = customQuotation.InquiryId,
                    //            PaymentName = "Installment# " + (i + 1),
                    //            PaymentStatusId = (int)paymentstatus.InstallmentCreated,
                    //            PaymentTypeId = (int)paymenttype.Installment,
                    //            PaymentDetail = "Installment of " + customQuotation.InquiryId,
                    //            PaymentAmount = Decimal.Truncate((decimal)paymentAmount),
                    //            PaymentExpectedDate = pay.PaymentExpectedDate,
                    //            IsActive = true,
                    //            IsDeleted = false,
                    //            CreatedDate = Helper.Helper.GetDateTime(),
                    //            CreatedBy = Constants.userId,
                    //            UpdatedBy = Constants.userId,
                    //            UpdatedDate = Helper.Helper.GetDateTime(),

                    //        });
                    //        //Helper.Helper.AddPayment(paymentAmount);

                    //    }
                    //}
                    //else
                    //{
                    //    quotation.Payments.Add(new Payment()
                    //    {
                    //        PaymentAmountinPercentage = decimal.Parse(customQuotation.BeforeInstallation),
                    //        InquiryId = customQuotation.InquiryId,
                    //        PaymentName = "Before Installation",
                    //        PaymentStatusId = (int)paymentstatus.PaymentCreated,
                    //        PaymentTypeId = (int)paymenttype.BeforeInstallation,
                    //        PaymentDetail = "Before Installation of " + customQuotation.InquiryId,
                    //        PaymentAmount = Decimal.Truncate(((decimal.Parse(customQuotation.TotalAmount) / 100) * decimal.Parse(customQuotation.BeforeInstallation)) * 100),
                    //        PaymentExpectedDate = "",
                    //        IsActive = true,
                    //        IsDeleted = false,
                    //        CreatedDate = Helper.Helper.GetDateTime(),
                    //        CreatedBy = Constants.userId,
                    //        UpdatedBy = Constants.userId,
                    //        UpdatedDate = Helper.Helper.GetDateTime(),

                    //    });
                    //    customQuotation.AfterDelivery = (100 - (decimal.Parse(customQuotation.BeforeInstallation) + decimal.Parse(customQuotation.AdvancePayment))).ToString();
                    //    quotation.Payments.Add(new Payment()
                    //    {
                    //        PaymentAmountinPercentage = decimal.Parse(customQuotation.AfterDelivery),
                    //        InquiryId = customQuotation.InquiryId,
                    //        PaymentName = "After Delivery",
                    //        PaymentStatusId = (int)paymentstatus.PaymentCreated,
                    //        PaymentTypeId = (int)paymenttype.AfterDelivery,
                    //        PaymentDetail = "After Delivery of " + customQuotation.InquiryId,
                    //        PaymentAmount = Decimal.Truncate(((decimal.Parse(customQuotation.TotalAmount) / 100) * decimal.Parse(customQuotation.AfterDelivery)) * 100),
                    //        PaymentExpectedDate = "",
                    //        IsActive = true,
                    //        IsDeleted = false,
                    //        CreatedDate = Helper.Helper.GetDateTime(),
                    //        CreatedBy = Constants.userId,
                    //        UpdatedBy = Constants.userId,
                    //        UpdatedDate = Helper.Helper.GetDateTime(),

                    //    });
                    //}
                    //foreach (var payment in quotation.Payments)
                    //{
                    //    if (payment.PaymentAmount == 0)
                    //    {
                    //        payment.IsActive = false;
                    //    }
                    //}

                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    inquiry.QuotationAddedOn = Helper.Helper.GetDateTime();
                    inquiry.Quotations.Add(quotation);
                    inquiryRepository.Update(inquiry);
                    response.data = null;

                    List<int?> roletypeId = new List<int?>
                    {
                        (int)roleType.Manager
                    };

                    sendNotificationToHead(
                        content: "New Quotation Added Of Inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                 "" + inquiry.InquiryId, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " +
                        inquiry.Customer.CustomerName, null,
                        //true,
                        //Url.ActionLink("AcceptQuotation", "QuotationController", new { id = quotation.InquiryId }),
                        //Url.ActionLink("DeclineQuotation", "QuotationController", new { id = quotation.InquiryId }),
                        roletypeId,
                        Constants.branchId,
                        (int)notificationCategory.Quotation);


                    


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
            Inquiry inquiry = inquiryRepository.FindByCondition(i =>
                    i.IsActive == true && i.IsDeleted == false && i.InquiryId == inquiryId &&
                    i.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y =>
                    y.IsActive == true && y.IsDeleted == false &&
                    y.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
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
        [HttpPost]
        [Route("[action]")]
        public object DeclineQuotation(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(i =>
                    i.IsActive == true && i.IsDeleted == false && i.InquiryId == inquiryId &&
                    i.InquiryWorkscopes.Count > 0).Include(x => x.InquiryWorkscopes.Where(y =>
                    y.IsActive == true && y.IsDeleted == false &&
                    y.InquiryStatusId == (int)inquiryStatus.designAccepted)).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
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
        public object ViewQuotationDetailsById(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Quotations.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Promo).FirstOrDefault();
            if (inquiry != null)
            {
                response.data = inquiry;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Quotation Not Found;";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> HeadAcceptQuotation(CustomQuotation _quotation)
        {
            Quotation quotation = quotationRepository.FindByCondition(x =>
                    x.IsActive == true && x.IsDeleted == false && x.QuotationId == _quotation.QuotationId)
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Inquiry)
                .ThenInclude(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Inquiry).ThenInclude(x => x.Customer)
                .FirstOrDefault();
            if (quotation != null)
            {
                quotation.QuotationCode = "QTN" + quotation.Inquiry.BranchId + "" + quotation.Inquiry.CustomerId + "" +
                                          quotation.InquiryId + "" + quotation.QuotationId;

                foreach (InquiryWorkscope inquiryWorkscope in quotation.Inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationWaitingForCustomerApproval;
                }

                quotation.Inquiry.InquiryStatusId = (int)inquiryStatus.quotationWaitingForCustomerApproval;

                if (_quotation.IsEdit)
                {
                    quotation.Amount = _quotation.Amount;
                    quotation.CalculationSheetFile = _quotation.CalculationSheetFile;
                    quotation.ProposalReferenceNumber = _quotation.ProposalReferenceNumber;
                    quotation.Description = _quotation.Description;
                    quotation.TotalAmount = _quotation.TotalAmount;
                    quotation.QuotationStatusId = (int)inquiryStatus.quotationWaitingForCustomerApproval;
                    quotation.AdvancePayment = _quotation.AdvancePayment;
                    quotation.AfterDelivery = _quotation.AfterDelivery;
                    quotation.BeforeInstallation = _quotation.BeforeInstallation;

                    if (_quotation.QuotationFiles != null && _quotation.QuotationFiles.Count > 0)
                    {
                        files.Clear();
                        Helper.Helper.Each(quotation.Files, x => x.IsActive = false);
                        foreach (string file in _quotation.QuotationFiles)
                        {
                            if (file != null)
                            {
                                files.Add(new File
                                {
                                    FileUrl = file,
                                    FileName = file.Split('.')[0],
                                    FileContentType = file.Split('.').Length > 1 ? file.Split('.')[1] : "mp4",
                                    IsImage = file.Split('.').Length > 1,
                                    IsActive = true,
                                    IsDeleted = false,
                                    UpdatedBy = Constants.userId,
                                    UpdatedDate = Helper.Helper.GetDateTime(),
                                    CreatedBy = Constants.userId,
                                    CreatedDate = Helper.Helper.GetDateTime()
                                });
                            }
                        }

                        quotation.Files = files;
                    }
                }
                try
                {
                    await mailService.SendQuotationEmailAsync(quotation.Inquiry.Customer.CustomerEmail, quotation.Inquiry.InquiryCode,
                        Constants.CRMBaseUrl + "/invoice.html?inquiryId=" + quotation.InquiryId, quotation.AdvancePayment,
                        quotation.Amount, quotation.Discount, quotation.Vat, quotation.TotalAmount,
                        quotation.QuotationValidityDate,
                        Constants.ServerBaseURL + "/api/Quotation/AcceptQuotation?inquiryId=" + quotation.InquiryId,
                        Constants.ServerBaseURL + "/api/Quotation/DeclineQuotation?inquiryId=" + quotation.InquiryId);
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureMessage(ex.Message);
                }
                quotationRepository.Update(quotation);
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
        [HttpPost]
        [Route("[action]")]
        public object HeadDeclineQuotation(AddComment comment)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(i =>
                    i.IsActive == true && i.IsDeleted == false && i.InquiryId == comment.inquiryId &&
                    i.InquiryWorkscopes.Count > 0).Include(x => x.InquiryWorkscopes.Where(y =>
                    y.IsActive == true && y.IsDeleted == false )).FirstOrDefault();
            if (inquiry != null)
            {
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.quotationRevisionRequested);

                inquiry.InquiryStatusId = (int)inquiryStatus.quotationRevisionRequested;
                inquiry.InquiryComment = comment.comment;
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
        public async Task<object> ViewContractForCustomer(int inquiryId)
        {
            List<int> q = inquiryWorkscopeRepository
                .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == inquiryId)
                .OrderBy(x => x.WorkscopeId).GroupBy(x => x.WorkscopeId).Select(x => x.Count()).ToList();
            List<TermsAndCondition> terms = termsAndConditionsRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .ToList();
            ViewQuotation viewQuotation = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    x.InquiryStatusId == (int)inquiryStatus.contractWaitingForCustomerApproval)
                .Select(x => new ViewQuotation
                {
                    InvoiceNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations
                        .OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false)
                        .QuotationId,
                    CreatedDate = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).CreatedDate,
                    ValidDate = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationValidityDate,
                    //SerialNo =
                    Description = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Description,
                    Discount = x.IsMeasurementPromo == false ? x.PromoDiscount : "0",
                    MeasurementFee = x.Payments.FirstOrDefault(y =>
                        y.PaymentTypeId == (int)paymenttype.Measurement &&
                        y.PaymentStatusId == (int)paymentstatus.PaymentApproved && y.IsActive == true &&
                        y.IsDeleted == false).PaymentAmount.ToString(),
                    Amount = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Amount,
                    Vat = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Vat,
                    IsInstallment = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).IsInstallment,
                    //MeasurementFees = x.Payments.OrderBy(y => y.PaymentId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Fees.FeesAmount,
                    AdvancePayment = x.Payments
                        .FirstOrDefault(y =>
                            y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true &&
                            y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    BeforeInstallation = x.Payments
                        .FirstOrDefault(y =>
                            y.PaymentTypeId == (int)paymenttype.BeforeInstallation && y.IsActive == true &&
                            y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    AfterDelivery = x.Payments
                        .FirstOrDefault(y =>
                            y.PaymentTypeId == (int)paymenttype.AfterDelivery && y.IsActive == true &&
                            y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    installments = x.Payments.Where(y =>
                            y.PaymentTypeId == (int)paymenttype.Installment && y.IsActive == true &&
                            y.IsDeleted == false)
                        .ToList(),
                    CustomerName = x.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp,
                    QuotationScheduleDate = x.QuotationScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress,
                    BranchAddress = x.Branch.BranchAddress,
                    BranchContact = x.Branch.BranchContact,
                    ProposalReferenceNumber = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).ProposalReferenceNumber,
                    TermsAndConditionsDetail = terms,
                    Files = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Files.Select(x => x.FileUrl)
                        .ToList(),
                    Quantity = q, //x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.WorkscopeId).GroupBy(g => g.WorkscopeId).Select(g => g.Count()).ToList(),
                    inquiryWorkScopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .OrderBy(x => x.WorkscopeId).Select(x => x.Workscope.WorkScopeName).ToList(),
                    TotalAmount = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).TotalAmount
                }).FirstOrDefault();

            if (viewQuotation != null)
            {
                Inquiry inquiry = inquiryRepository
                    .FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false)
                    .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
                foreach (InquiryWorkscope inws in inquiry.InquiryWorkscopes)
                {
                    foreach (Design design in inws.Designs)
                    {
                        foreach (File file in design.Files)
                        {
                            if (file != null)
                            {
                                viewQuotation.Files.Add(file.FileUrl);
                            }
                        }
                    }
                }

                foreach (JobOrder job in inquiry.JobOrders)
                {
                    if (job.MepdrawingFileUrl != null && job.MepdrawingFileUrl != "")
                    {
                        viewQuotation.Files.Add(job.MepdrawingFileUrl);
                    }
                }

                try
                {
                    viewQuotation.invoiceDetails = new List<InvoiceDetail>();
                    for (int i = 0; i < viewQuotation.inquiryWorkScopeNames.Count; i++)
                    {
                        viewQuotation.invoiceDetails.Add(new InvoiceDetail
                        {
                            inquiryWorkScopeNames = viewQuotation.inquiryWorkScopeNames[i],
                            Quantity = viewQuotation.Quantity[i]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }

                viewQuotation.TermsAndConditionsDetail.RemoveAll(x =>
                    x.IsInstallmentTerms != viewQuotation.IsInstallment);

                if (viewQuotation.IsInstallment == true)
                {
                    for (int i = 0; i < viewQuotation.installments.Count - 1; i++)
                    {
                        viewQuotation.TermsAndConditionsDetail.Add(new TermsAndCondition
                        {
                            TermsAndConditionsDetail =
                                viewQuotation.TermsAndConditionsDetail[1].TermsAndConditionsDetail,
                            IsInstallmentTerms = true
                        });
                    }
                }

                int j = -1;
                List<TermsAndCondition> deletedTermsAndConditions = new List<TermsAndCondition>();
                viewQuotation.TermsAndConditionsDetail.ForEach(x =>
                {
                    if (x.IsInstallmentTerms == viewQuotation.IsInstallment)
                    {
                        if (viewQuotation.IsInstallment == false)
                        {
                            if (x.TermsAndConditionsDetail.Contains("[AdvancePayment]") &&
                                (viewQuotation.AdvancePayment == null || viewQuotation.AdvancePayment == "0"))
                            {
                                //viewQuotation.TermsAndConditionsDetail.Remove(x);
                                deletedTermsAndConditions.Add(x);
                            }
                            else if (x.TermsAndConditionsDetail.Contains("[BeforeInstallation]") &&
                                     (viewQuotation.BeforeInstallation == null ||
                                      viewQuotation.BeforeInstallation == "0"))
                            {
                                //viewQuotation.TermsAndConditionsDetail.Remove(x);
                                deletedTermsAndConditions.Add(x);
                            }
                            else if (x.TermsAndConditionsDetail.Contains("[AfterDelivery]") &&
                                     (viewQuotation.AfterDelivery == null || viewQuotation.AfterDelivery == "0"))
                            {
                                //viewQuotation.TermsAndConditionsDetail.Remove(x);
                                deletedTermsAndConditions.Add(x);
                            }

                            x.TermsAndConditionsDetail = x.TermsAndConditionsDetail
                                .Replace("[AdvancePayment]", viewQuotation.AdvancePayment + "%")
                                .Replace("[BeforeInstallation]", viewQuotation.BeforeInstallation + "%")
                                .Replace("[AfterDelivery]", viewQuotation.AfterDelivery + "%");
                        }
                        else
                        {
                            if (j == -1)
                            {
                                x.TermsAndConditionsDetail = x.TermsAndConditionsDetail.Replace("[AdvancePayment]",
                                    viewQuotation.AdvancePayment + "%");
                            }
                            else
                            {
                                x.TermsAndConditionsDetail = x.TermsAndConditionsDetail
                                    .Replace("[noofInstallment]", j + 1 + "")
                                    .Replace("[installmentPercentage]",
                                        viewQuotation.installments[j].PaymentAmountinPercentage + "%")
                                    .Replace("[installmentDate]",
                                        viewQuotation.installments[j].PaymentExpectedDate + "");
                            }

                            j++;
                        }
                    }
                });
                deletedTermsAndConditions.ForEach(x => viewQuotation.TermsAndConditionsDetail.Remove(x));


                response.data = viewQuotation;
            }
            else
            {
                response.errorMessage = "Contract Has Been Approved or Rejected Before";
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
            List<int> q = inquiryWorkscopeRepository
                .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == inquiryId)
                .OrderBy(x => x.WorkscopeId).GroupBy(x => x.WorkscopeId).Select(x => x.Count()).ToList();
            List<TermsAndCondition> terms = termsAndConditionsRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .ToList();
            ViewQuotation viewQuotation = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false 
                    //&&
                    //x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval
                    )
                .Select(x => new ViewQuotation
                {
                    InvoiceNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations
                        .OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false)
                        .QuotationId,
                    CreatedDate = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).CreatedDate,
                    ValidDate = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationValidityDate,
                    //SerialNo =
                    Description = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Description,
                    Discount = x.IsMeasurementPromo == false ? x.PromoDiscount : "0",
                    MeasurementFee = x.Payments.FirstOrDefault(y =>
                        y.PaymentTypeId == (int)paymenttype.Measurement &&
                        y.PaymentStatusId == (int)paymentstatus.PaymentApproved && y.IsActive == true &&
                        y.IsDeleted == false).PaymentAmount.ToString(),
                    Amount = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Amount,
                    Vat = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Vat,
                    IsInstallment = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).IsInstallment,
                    //MeasurementFees = x.Payments.OrderBy(y => y.PaymentId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Fees.FeesAmount,
                    AdvancePayment = x.Payments
                        .FirstOrDefault(y =>
                            y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true &&
                            y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    BeforeInstallation = x.Payments
                        .FirstOrDefault(y =>
                            y.PaymentTypeId == (int)paymenttype.BeforeInstallation && y.IsActive == true &&
                            y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    AfterDelivery = x.Payments
                        .FirstOrDefault(y =>
                            y.PaymentTypeId == (int)paymenttype.AfterDelivery && y.IsActive == true &&
                            y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                    installments = x.Payments.Where(y =>
                            y.PaymentTypeId == (int)paymenttype.Installment && y.IsActive == true &&
                            y.IsDeleted == false)
                        .ToList(),
                    CustomerName = x.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp,
                    QuotationScheduleDate = x.QuotationScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress,
                    BranchAddress = x.Branch.BranchAddress,
                    BranchContact = x.Branch.BranchContact,
                    ProposalReferenceNumber = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).ProposalReferenceNumber,
                    TermsAndConditionsDetail = terms,
                    Files = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Files.Select(x => x.FileUrl)
                        .ToList(),
                    Quantity = q, //x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.WorkscopeId).GroupBy(g => g.WorkscopeId).Select(g => g.Count()).ToList(),
                    inquiryWorkScopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .OrderBy(x => x.WorkscopeId).Select(x => x.Workscope.WorkScopeName).ToList(),
                    TotalAmount = x.Quotations.OrderBy(y => y.QuotationId)
                        .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).TotalAmount
                }).FirstOrDefault();
            if (viewQuotation != null)
            {
                try
                {
                    viewQuotation.invoiceDetails = new List<InvoiceDetail>();
                    for (int i = 0; i < viewQuotation.inquiryWorkScopeNames.Count; i++)
                    {
                        viewQuotation.invoiceDetails.Add(new InvoiceDetail
                        {
                            inquiryWorkScopeNames = viewQuotation.inquiryWorkScopeNames[i],
                            Quantity = viewQuotation.Quantity[i]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }

                viewQuotation.TermsAndConditionsDetail.RemoveAll(x =>
                    x.IsInstallmentTerms != viewQuotation.IsInstallment);

                if (viewQuotation.IsInstallment == true)
                {
                    for (int i = 0; i < viewQuotation.installments.Count - 1; i++)
                    {
                        viewQuotation.TermsAndConditionsDetail.Add(new TermsAndCondition
                        {
                            TermsAndConditionsDetail =
                                viewQuotation.TermsAndConditionsDetail[1].TermsAndConditionsDetail,
                            IsInstallmentTerms = true
                        });
                    }
                }

                int j = -1;
                List<TermsAndCondition> deletedTermsAndConditions = new List<TermsAndCondition>();
                viewQuotation.TermsAndConditionsDetail.ForEach(x =>
                {
                    if (x.IsInstallmentTerms == viewQuotation.IsInstallment)
                    {
                        if (viewQuotation.IsInstallment == false)
                        {
                            if (x.TermsAndConditionsDetail.Contains("[AdvancePayment]") &&
                                (viewQuotation.AdvancePayment == null || viewQuotation.AdvancePayment == "0"))
                            {
                                //viewQuotation.TermsAndConditionsDetail.Remove(x);
                                deletedTermsAndConditions.Add(x);
                            }
                            else if (x.TermsAndConditionsDetail.Contains("[BeforeInstallation]") &&
                                     (viewQuotation.BeforeInstallation == null ||
                                      viewQuotation.BeforeInstallation == "0"))
                            {
                                //viewQuotation.TermsAndConditionsDetail.Remove(x);
                                deletedTermsAndConditions.Add(x);
                            }
                            else if (x.TermsAndConditionsDetail.Contains("[AfterDelivery]") &&
                                     (viewQuotation.AfterDelivery == null || viewQuotation.AfterDelivery == "0"))
                            {
                                //viewQuotation.TermsAndConditionsDetail.Remove(x);
                                deletedTermsAndConditions.Add(x);
                            }

                            x.TermsAndConditionsDetail = x.TermsAndConditionsDetail
                                .Replace("[AdvancePayment]", viewQuotation.AdvancePayment + "%")
                                .Replace("[BeforeInstallation]", viewQuotation.BeforeInstallation + "%")
                                .Replace("[AfterDelivery]", viewQuotation.AfterDelivery + "%");
                        }
                        else
                        {
                            if (j == -1)
                            {
                                x.TermsAndConditionsDetail = x.TermsAndConditionsDetail.Replace("[AdvancePayment]",
                                    viewQuotation.AdvancePayment + "%");
                            }
                            else
                            {
                                x.TermsAndConditionsDetail = x.TermsAndConditionsDetail
                                    .Replace("[noofInstallment]", j + 1 + "")
                                    .Replace("[installmentPercentage]",
                                        viewQuotation.installments[j].PaymentAmountinPercentage + "%")
                                    .Replace("[installmentDate]",
                                        viewQuotation.installments[j].PaymentExpectedDate + "");
                            }

                            j++;
                        }
                    }
                });
                deletedTermsAndConditions.ForEach(x => viewQuotation.TermsAndConditionsDetail.Remove(x));


                response.data = viewQuotation;
            }
            else
            {
                response.errorMessage = "Quotation Has Been Approved or Rejected Before";
                response.isError = true;
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> ClientApproveQuotationAsync(UpdateQuotationStatus updateQuotation)
        {
            List<IFormFile> files = new List<IFormFile>();
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == updateQuotation.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)
                .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y =>
                    y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer)
                .FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.contractInProgress;
                inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                // inquiry.InquiryComment = updateQuotation.reason;

                foreach (InquiryWorkscope workscope in inquiry.InquiryWorkscopes)
                {
                    workscope.InquiryStatusId = (int)inquiryStatus.contractInProgress;
                }

                foreach (Quotation quotation in inquiry.Quotations)
                {
                    if (updateQuotation.Pdf != null && updateQuotation.Pdf.Count() >= 0)
                    {
                        Tuple<string, string> fileUrl = await Helper.Helper.UploadFile(updateQuotation.Pdf);
                        quotation.Files.Add(new File
                        {
                            FileUrl = fileUrl.Item1,
                            FileName = fileUrl.Item1.Split('.')[0],
                            FileContentType = fileUrl.Item2,
                            IsImage = false,
                            IsActive = true,
                            IsDeleted = false
                        });
                    }

                    foreach (File file in quotation.Files)
                    {
                        files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(file.FileUrl)));
                    }
                }

                inquiryRepository.Update(inquiry);
                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };
                try
                {
                    await sendNotificationToHead(
                        content: "Quotation Of inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" +
                                 inquiry.InquiryId + " Approved By Client", false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " +
                        inquiry.Customer.CustomerName, null, roletypeId, inquiry.BranchId,
                        (int)notificationCategory.Quotation);
                    await mailService.SendEmailAsync(new MailRequest
                    {
                        Subject = "Quotation Files",
                        ToEmail = inquiry.Customer.CustomerEmail,
                        Body = "Quotation File",
                        Attachments = files
                    });
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureMessage(ex.Message);
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
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == updateQuotation.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y =>
                    y.QuotationStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval &&
                    y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y =>
                    y.PaymentTypeId != (int)paymenttype.Measurement && y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                Helper.Helper.Each(inquiry.Quotations, x => x.QuotationStatusId = (int)inquiryStatus.quotationRejected);

                foreach (Payment payment in inquiry.Payments)
                {
                    payment.IsActive = false;
                }
                //payment.PaymentStatusId = (int)paymentstatus.PaymentRejected;

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                }

                foreach (Quotation quotation in inquiry.Quotations)
                {
                    quotation.IsActive = false;
                    quotation.Description = updateQuotation.reason;
                    quotation.FeedBackReactionId = updateQuotation.FeedBackReactionId;
                }

                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };
                try
                {
                    sendNotificationToHead(
                        content: "Quotation Of inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" +
                                 inquiry.InquiryId + " Rejected By Client Reason: " + updateQuotation.reason, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " +
                        inquiry.Customer.CustomerName, null, roletypeId, inquiry.BranchId,
                        (int)notificationCategory.Quotation);
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureMessage(ex.Message);
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
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Customer)
                .Include(x => x.Quotations.Where(y =>
                    y.QuotationStatusId == (int)inquiryStatus.contractWaitingForCustomerApproval &&
                    y.IsActive == true && y.IsDeleted == false)).ThenInclude(x =>
                    x.Payments.Where(y =>
                        y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true &&
                        y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                decimal d = 0;
                long amount = 0;
                //foreach (var quotaion in inquiry.Quotations)
                //{
                d = decimal.Parse(inquiry.Quotations.LastOrDefault()?.AdvancePayment) *
                    (decimal.Parse(inquiry.Quotations.LastOrDefault()?.TotalAmount) / 100) * 100;
                amount += Convert.ToInt64(d);
                //}
                PaymentIntentService paymentIntents = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "aed",
                    ReceiptEmail = inquiry.Customer.CustomerEmail
                });
                inquiry.Quotations.LastOrDefault().Payments
                        .LastOrDefault(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment).PaymentStatusId =
                    (int)paymentstatus.PaymentPending;
                inquiry.Quotations.LastOrDefault().Payments
                        .LastOrDefault(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment).PaymentModeId =
                    (int)paymentMode.OnlinePayment;
                inquiry.Quotations.LastOrDefault().Payments
                        .LastOrDefault(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment).ClientSecret =
                    paymentIntent.ClientSecret;
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
        public object stripeByPaymentId(int PaymentId)
        {
            var payment = paymentRepository.FindByCondition(x => x.PaymentId == PaymentId && x.IsActive == true && x.IsDeleted == false && (x.PaymentStatusId != (int)paymentstatus.InstallmentApproved || x.PaymentStatusId != (int)paymentstatus.PaymentApproved) && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false)
                .Include(x => x.Inquiry).ThenInclude(x => x.Customer)
            .FirstOrDefault();

            if (payment != null)
            {
                PaymentIntentService paymentIntents = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
                {
                    Amount =(long?)payment.PaymentAmount,
                    Currency = "aed",
                    ReceiptEmail = payment.Inquiry.Customer.CustomerEmail,
                });
                payment.PaymentStatusId =
                    (int)paymentstatus.PaymentPending;
                payment.PaymentModeId =
                    (int)paymentMode.OnlinePayment;
               payment.ClientSecret =
                    paymentIntent.ClientSecret;
                response.data = paymentIntent.ClientSecret;
                paymentRepository.Update(payment);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Payment Not Found";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object QuotationSchedule(quotationScheduleUpdate scheduleUpdate)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == scheduleUpdate.inquiryId &&
                    x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false) && x.IsActive == true
                    && x.IsDeleted == false &&
                    x.InquiryWorkscopes.Count(y => y.IsActive == true && y.IsDeleted == false) == x
                        .InquiryWorkscopes.Count(y => (y.InquiryStatusId == (int)inquiryStatus.quotationSchedulePending ||
                                                       y.InquiryStatusId == (int)inquiryStatus.quotationPending ||
                                                       y.InquiryStatusId == (int)inquiryStatus.quotationRevisionRequested ||
                                                       y.InquiryStatusId == (int)inquiryStatus.quotationDelayed ||
                                                       y.InquiryStatusId == (int)inquiryStatus.quotationRejected) && y.IsActive == true &&
                                                      y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                //.Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && x.IsDeleted == false))
                .FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.quotationPending;
                inquiry.QuotationScheduleDate = scheduleUpdate.date;
                // inquiry.QuotationAssignTo = scheduleUpdate.userId;
                foreach (InquiryWorkscope inworkscope in inquiry.InquiryWorkscopes)
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

        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddContract(AddjobOrder order)
        {
            List<IFormFile> files = new List<IFormFile>();

            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesPending ||
                     x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesDelayed ||
                     x.InquiryStatusId == (int)inquiryStatus.contractInProgress ||
                     x.InquiryStatusId == (int)inquiryStatus.contractRejected))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x =>
                    x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer).FirstOrDefault();

            JobOrder _jobOrder = new JobOrder();
            if (inquiry != null)
            {
                Helper.Helper.Each(inquiry.JobOrders, x => x.IsActive = false);
                inquiry.InquiryStatusId = (int)inquiryStatus.contractWaitingForCustomerApproval;
                //_jobOrder.JobOrderRequestedDeadline = order.JobOrderRequestedDeadline;
                //_jobOrder.JobOrderRequestedComments = order.JobOrderRequestedComments;
                _jobOrder.DataSheetApplianceFileUrl = order.DataSheetApplianceFileUrl;
                _jobOrder.IsAppliancesProvidedByClient = order.IsAppliancesProvidedByClient;
                _jobOrder.DetailedDesignFile = order.DetailedDesignFile;
                _jobOrder.MaterialSheetFileUrl = order.MaterialSheetFileUrl;
                _jobOrder.MepdrawingFileUrl = order.MepdrawingFileUrl;
                // _jobOrder.Comments = order.Comments;
                _jobOrder.IsActive = true;
                _jobOrder.IsDeleted = false;
                _jobOrder.CreatedBy = Constants.userId;
                _jobOrder.CreatedDate = Helper.Helper.GetDateTime();

                foreach (InquiryWorkscope inWorkscope in inquiry.InquiryWorkscopes)
                {
                    inWorkscope.InquiryStatusId = (int)inquiryStatus.contractWaitingForCustomerApproval;
                    foreach (Design design in inWorkscope.Designs)
                    {
                        foreach (File file in design.Files)
                        {
                            if (file.FileUrl != null && file.FileUrl != "")
                            {
                                files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(file.FileUrl)));
                            }

                        }
                    }
                }

                int quotationId = 0;
                foreach (Quotation quotation in inquiry.Quotations)
                {
                    quotationId = quotation.QuotationId;
                    quotation.IsPaid = quotation.IsPaid == null ? true : quotation.IsPaid;
                    quotation.AdvancePayment = (bool)quotation.IsPaid ? order.AdvancePayment : "0";
                    quotation.IsInstallment = order.IsInstallment;
                    quotation.NoOfInstallment = order.NoOfInstallment;
                    quotation.AfterDelivery = order.AfterDelivery;
                    quotation.BeforeInstallation = order.BeforeInstallation;
                    quotation.QuotationStatusId = (int)inquiryStatus.contractWaitingForCustomerApproval;
                    if ((bool)quotation.IsPaid)
                    {
                        decimal percent = 0;
                        decimal amountwithoutAdvance = decimal.Parse(quotation.TotalAmount) -
                                                   decimal.Parse(quotation.TotalAmount) / 100 *
                                                   decimal.Parse(quotation.AdvancePayment);
                        quotation.Payments.Add(new Payment
                        {
                            PaymentAmountinPercentage = decimal.Parse(order.AdvancePayment),
                            InquiryId = order.inquiryId,
                            PaymentName = "Advance Payment",
                            PaymentStatusId = (int)paymentstatus.PaymentCreated,
                            PaymentTypeId = (int)paymenttype.AdvancePayment,
                            PaymentDetail = "Advance Payment of " + order.inquiryId,
                            PaymentAmount = decimal.Truncate(decimal.Parse(quotation.TotalAmount) / 100 *
                                                             decimal.Parse(order.AdvancePayment) * 100),
                            PaymentExpectedDate = quotation.QuotationValidityDate,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = Helper.Helper.GetDateTime(),
                            CreatedBy = Constants.userId,
                            UpdatedBy = Constants.userId,
                            UpdatedDate = Helper.Helper.GetDateTime()
                        });
                        if (order.IsInstallment == true)
                        {
                            for (int i = 0; i < order.Payments.Count; i++)
                            {
                                Payment pay = order.Payments[i];
                                percent += (decimal)pay.PaymentAmountinPercentage;
                                if (order.Payments.Count - 1 == i)
                                {
                                    pay.PaymentAmountinPercentage += 100 - decimal.Parse(order.AdvancePayment) - percent;
                                }

                                decimal? paymentAmount = amountwithoutAdvance / 100 * pay.PaymentAmountinPercentage * 100;
                                quotation.Payments.Add(new Payment
                                {
                                    PaymentAmountinPercentage = pay.PaymentAmountinPercentage,
                                    InquiryId = order.inquiryId,
                                    PaymentName = "Installment# " + (i + 1),
                                    PaymentStatusId = (int)paymentstatus.InstallmentCreated,
                                    PaymentTypeId = (int)paymenttype.Installment,
                                    PaymentDetail = "Installment of " + order.inquiryId,
                                    PaymentAmount = decimal.Truncate((decimal)paymentAmount),
                                    PaymentExpectedDate = pay.PaymentExpectedDate,
                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedDate = Helper.Helper.GetDateTime(),
                                    CreatedBy = Constants.userId,
                                    UpdatedBy = Constants.userId,
                                    UpdatedDate = Helper.Helper.GetDateTime()
                                });
                                //Helper.Helper.AddPayment(paymentAmount);
                            }
                        }
                        else
                        {
                            quotation.Payments.Add(new Payment
                            {
                                PaymentAmountinPercentage = decimal.Parse(order.BeforeInstallation),
                                InquiryId = order.inquiryId,
                                PaymentName = "Before Installation",
                                PaymentStatusId = (int)paymentstatus.PaymentCreated,
                                PaymentTypeId = (int)paymenttype.BeforeInstallation,
                                PaymentDetail = "Before Installation of " + order.inquiryId,
                                PaymentAmount = decimal.Truncate(decimal.Parse(quotation.TotalAmount) / 100 *
                                                                 decimal.Parse(order.BeforeInstallation) * 100),
                                PaymentExpectedDate = "",
                                IsActive = true,
                                IsDeleted = false,
                                CreatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime()
                            });
                            order.AfterDelivery =
                                (100 - (decimal.Parse(order.BeforeInstallation) + decimal.Parse(order.AdvancePayment)))
                                .ToString();
                            quotation.Payments.Add(new Payment
                            {
                                PaymentAmountinPercentage = decimal.Parse(order.AfterDelivery),
                                InquiryId = order.inquiryId,
                                PaymentName = "After Delivery",
                                PaymentStatusId = (int)paymentstatus.PaymentCreated,
                                PaymentTypeId = (int)paymenttype.AfterDelivery,
                                PaymentDetail = "After Delivery of " + order.inquiryId,
                                PaymentAmount = decimal.Truncate(decimal.Parse(quotation.TotalAmount) / 100 *
                                                                 decimal.Parse(order.AfterDelivery) * 100),
                                PaymentExpectedDate = "",
                                IsActive = true,
                                IsDeleted = false,
                                CreatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime()
                            });
                        }

                        foreach (Payment payment in quotation.Payments)
                        {
                            if (payment.PaymentAmount == 0)
                            {
                                payment.IsActive = false;
                            }
                        }
                    }
                    

                    if (_jobOrder.MepdrawingFileUrl != null && _jobOrder.MepdrawingFileUrl != "")
                    {
                        files.Add(Helper.Helper.ConvertBytestoIFormFile(
                                                await Helper.Helper.GetFile(_jobOrder.MepdrawingFileUrl)));
                    }
                    

                    //foreach (var file in quotation.Files)
                    //{
                    //    files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(file.FileUrl)));
                    //}
                }

                inquiry.JobOrders.Add(_jobOrder);
                inquiryRepository.Update(inquiry);
                try
                {
                    await mailService.SendQuotationEmailAsync(inquiry.Customer.CustomerEmail, inquiry.InquiryCode,
                        Constants.CRMBaseUrl + "/payinvoice.html?inquiryId=" + inquiry.InquiryId,
                        inquiry.Quotations.FirstOrDefault(x => x.QuotationId == quotationId).AdvancePayment,
                        inquiry.Quotations.FirstOrDefault(x => x.QuotationId == quotationId).Amount,
                        inquiry.Quotations.FirstOrDefault(x => x.QuotationId == quotationId).Discount,
                        inquiry.Quotations.FirstOrDefault(x => x.QuotationId == quotationId).Vat,
                        inquiry.Quotations.FirstOrDefault(x => x.QuotationId == quotationId).TotalAmount,
                        inquiry.Quotations.FirstOrDefault(x => x.QuotationId == quotationId).QuotationValidityDate,
                        Constants.ServerBaseURL + "/api/Quotation/AcceptQuotation?inquiryId=" + inquiry.InquiryId,
                        Constants.ServerBaseURL + "/api/Quotation/DeclineQuotation?inquiryId=" + inquiry.InquiryId);
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureMessage(ex.Message);
                }

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
        public async Task<object> ClientApproveContract(ContrcatApprove approve)
        {
            List<IFormFile> files = new List<IFormFile>();
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == approve.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    x.InquiryStatusId == (int)inquiryStatus.contractWaitingForCustomerApproval)
                .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == true))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y =>
                    y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();
            if (inquiry != null)
            {
                bool ispaid = false;
                foreach (Quotation quotation in inquiry.Quotations)
                {
                    ispaid =(bool) quotation.IsPaid;
                    quotation.QuotationStatusId = ispaid ? (int)inquiryStatus.contractApproved : (int)inquiryStatus.checklistPending;
                    if (approve.Pdf != null && approve.Pdf.Length != 0)
                    {
                        Tuple<string, string> fileUrl = await Helper.Helper.UploadFile(approve.Pdf);
                        quotation.Files.Add(new File
                        {
                            FileUrl = fileUrl.Item1,
                            FileName = fileUrl.Item1.Split('.')[0],
                            FileContentType = fileUrl.Item2,
                            IsImage = false,
                            IsActive = true,
                            IsDeleted = false
                        });
                    }

                    foreach (File file in quotation.Files)
                    {
                        if (file != null)
                        {
                            files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(file.FileUrl)));

                        }
                    }
                }
                inquiry.InquiryStatusId = ispaid ? (int)inquiryStatus.contractApproved :(int)inquiryStatus.checklistPending ;
                inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                // inquiry.InquiryComment = updateQuotation.reason;

                foreach (InquiryWorkscope workscope in inquiry.InquiryWorkscopes)
                {
                    workscope.InquiryStatusId = ispaid ? (int)inquiryStatus.contractApproved : (int)inquiryStatus.checklistPending;
                    foreach (Design design in workscope.Designs)
                    {
                        foreach (File file in design.Files)
                        {
                            if (file != null)
                            {
                                files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(file.FileUrl)));
                            }
                        }
                    }
                }
                if (ispaid)
                {
                    foreach (Payment payment in inquiry.Payments)
                    {
                        payment.PaymentModeId = approve.SelectedPaymentMode;
                        if (payment.PaymentModeId == (int)paymentMode.OnlinePayment)
                        {
                            payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                            payment.ClientSecret = approve.ClientSecret;
                            payment.PaymentMethod = approve.PaymentMethod;
                            payment.PaymentIntentToken = approve.PaymentIntentToken;
                            payment.InvoiceCode = "INV" + inquiry.BranchId + "" + inquiry.CustomerId + "" +
                                                  inquiry.InquiryId + "" + inquiry.Quotations.FirstOrDefault().QuotationId +
                                                  "" + payment.PaymentId;
                            inquiry.InquiryStatusId = (int)inquiryStatus.checklistPending;
                            Helper.Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.checklistPending);
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
                }
                


                foreach (JobOrder joborder in inquiry.JobOrders)
                {
                    if (joborder.MepdrawingFileUrl != null && joborder.MepdrawingFileUrl != "")
                    {

                        files.Add(Helper.Helper.ConvertBytestoIFormFile(
                            await Helper.Helper.GetFile(joborder.MepdrawingFileUrl)));
                    }
                }

                inquiryRepository.Update(inquiry);
                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };
                try
                {
                    sendNotificationToHead(
                        content: "Contract Of inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" +
                                 inquiry.InquiryId + " Approved By Client", false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " +
                        inquiry.Customer.CustomerName, null, roletypeId, inquiry.BranchId,
                        (int)notificationCategory.Quotation);
                    await mailService.SendEmailAsync(new MailRequest
                    {
                        Subject = "Contract Files",
                        ToEmail = inquiry.Customer.CustomerEmail,
                        Body = "Contract Files",
                        Attachments = files
                    });
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureMessage(ex.Message);
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
        public object ClientRejectContract(ContractReject reject)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == reject.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y =>
                    y.QuotationStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval &&
                    y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y =>
                    y.PaymentTypeId != (int)paymenttype.Measurement && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.contractRejected;
                //Helper.Helper.Each(inquiry.Quotations, x => x.QuotationStatusId = (int)inquiryStatus.contractRejected);
                foreach (Payment payment in inquiry.Payments)
                {
                    payment.IsActive = false;
                }

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.contractRejected;
                }

                foreach (Quotation quotation in inquiry.Quotations)
                {
                    quotation.IsActive = false;
                    quotation.Description = reject.Comment;
                    quotation.FeedBackReactionId = reject.FeedBackReactionId;
                }

                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };
                try
                {
                    sendNotificationToHead(
                        content: "Contract Of inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" +
                                 inquiry.InquiryId + " Rejected By Client Reason: " + reject.Comment, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " +
                        inquiry.Customer.CustomerName, null, roletypeId, inquiry.BranchId,
                        (int)notificationCategory.Quotation);
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureMessage(ex.Message);
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