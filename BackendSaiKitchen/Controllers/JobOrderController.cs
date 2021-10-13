using BackendSaiKitchen.CustomModel;
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
    public class JobOrderController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object JobOrderFactoryApprove(CustomJobOrder order)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending))
                 .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                 .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                 .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                 .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                 .Include(x => x.Customer).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderInProgress;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderInProgress;
                });
                foreach (var joborder in inquiry.JobOrders)
                {
                    JobOrderDetail jobOrderDetail = new JobOrderDetail();
                    jobOrderDetail.MaterialRequestDate = order.materialRequestDate;
                    jobOrderDetail.MaterialDeliveryFinalDate = order.materialDeliveryFinalDate;
                    jobOrderDetail.ProductionCompletionDate = order.productionCompletionDate;
                    jobOrderDetail.ShopDrawingCompletionDate = order.shopDrawingCompletionDate;
                    jobOrderDetail.WoodenWorkCompletionDate = order.woodenWorkCompletionDate;
                    jobOrderDetail.CountertopFixingDate = order.counterTopFixingDate;
                    jobOrderDetail.JobOrderDetailDescription = order.Notes;
                    jobOrderDetail.CreatedBy = Constants.userId;
                    jobOrderDetail.CreatedDate = Helper.Helper.GetDateTime();
                    jobOrderDetail.InstallationStartDate = order.installationStartDate;
                    jobOrderDetail.IsActive = true;
                    jobOrderDetail.IsDeleted = false;
                    joborder.JobOrderDetails.Add(jobOrderDetail);
                }

                

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
        public object JobOrderFactoryReject(JobOrderFactory job)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == job.inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderFactoryRejected;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderFactoryRejected;
                });
                inquiry.InquiryComment = job.Reason;
                //Helper.Helper.Each(inquiry.JobOrders, x =>
                //{
                //    x.IsActive = false;
                //});
                response.data = "JobOrder Factory Rejected";
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetinquiryJobOrderFactoryDetailsById(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Workscope)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            // && (y.PaymentTypeId == (int)paymenttype.AdvancePayment || y.PaymentTypeId == (int)paymenttype.Installment))).FirstOrDefault();
            if (inquiry != null)
            {
                Inquirychecklist inquirychecklist = new Inquirychecklist()
                {
                    inquiry = inquiry,
                    fees = FeesRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
                };
                if (inquirychecklist == null)
                {
                    response.isError = true;
                    response.errorMessage = "No Inquiry Found";
                }
                else
                {
                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    response.data = inquirychecklist;
                }
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
        public object GetInquiryJobOrderFactoryByBranchId(int branchId)
        {
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending) && x.JobOrders.Any(y => y.IsActive == true && y.IsDeleted == false && y.FactoryId == branchId))
                .Select(x => new CheckListByBranch
                {
                    InquiryId = x.InquiryId,
                    QuotationNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationId,
                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                    WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).ToList(),
                    WorkScopeCount = x.InquiryWorkscopes.Count,
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
                    BranchId = x.BranchId,
                    InquiryAddedBy = x.ManagedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy,
                    NoOfRevision = x.Quotations.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                }).ToList();
            if (inquiries != null)
            {
                response.data = inquiries;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "there is no inquiries to Check";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetInquiryJobOrderByBranchId(int branchId)
        {
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesPending || x.InquiryStatusId == (int)inquiryStatus.contractInProgress || x.InquiryStatusId == (int)inquiryStatus.contractRejected)).Select(x => new CheckListByBranch
            {
                InquiryId = x.InquiryId,
                QuotationNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationId,
                InquiryDescription = x.InquiryDescription,
                InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).ToList(),
                WorkScopeCount = x.InquiryWorkscopes.Count,
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
                BranchId = x.BranchId,
                InquiryAddedBy = x.ManagedByNavigation.UserName,
                InquiryAddedById = x.ManagedBy,
                NoOfRevision = x.Quotations.Where(y => y.IsDeleted == false).Count(),
                InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                CommentAddedOn = x.InquiryCommentsAddedOn,
                DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.DesignAddedOn).FirstOrDefault(),
                MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                QuotationAddedOn = x.QuotationAddedOn
            }).ToList();
            if (inquiries != null)
            {
                response.data = inquiries;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "there is no inquiries to Check";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetinquiryJobOrderDetailsById(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayed || x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesDelayed || x.InquiryStatusId == (int)inquiryStatus.contractInProgress || x.InquiryStatusId == (int)inquiryStatus.contractRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Workscope)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            // && (y.PaymentTypeId == (int)paymenttype.AdvancePayment || y.PaymentTypeId == (int)paymenttype.Installment))).FirstOrDefault();
            if (inquiry != null)
            {
                Inquirychecklist inquirychecklist = new Inquirychecklist()
                {
                    inquiry = inquiry,
                    fees = FeesRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
                };
                if (inquirychecklist == null)
                {
                    response.isError = true;
                    response.errorMessage = "No Inquiry Found";
                }
                else
                {
                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    response.data = inquirychecklist;
                }
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
        public async Task<object> AddJobOrder(AddjobOrder order)
        {
            List<IFormFile> files = new List<IFormFile>();

            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesDelayed || x.InquiryStatusId == (int)inquiryStatus.contractInProgress || x.InquiryStatusId == (int)inquiryStatus.contractRejected))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted ==false))
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            
            JobOrder _jobOrder = new JobOrder();
            if (inquiry != null)
            {
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
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.contractWaitingForCustomerApproval;
                });
                foreach (var quotation in inquiry.Quotations)
                {
                    quotation.AdvancePayment = order.AdvancePayment;
                    quotation.IsInstallment = order.IsInstallment;
                    quotation.NoOfInstallment = order.NoOfInstallment;
                    quotation.AfterDelivery = order.AfterDelivery;

                    decimal percent = 0;
                    var amountwithoutAdvance = decimal.Parse(quotation.TotalAmount) - ((decimal.Parse(quotation.TotalAmount) / 100) * decimal.Parse(quotation.AdvancePayment));
                    quotation.Payments.Add(new Payment()
                    {
                        PaymentAmountinPercentage = decimal.Parse(order.AdvancePayment),
                        InquiryId = order.inquiryId,
                        PaymentName = "Advance Payment",
                        PaymentStatusId = (int)paymentstatus.PaymentCreated,
                        PaymentTypeId = (int)paymenttype.AdvancePayment,
                        PaymentDetail = "Advance Payment of " + order.inquiryId,
                        PaymentAmount = Decimal.Truncate((decimal.Parse(quotation.TotalAmount) / 100) * decimal.Parse(order.AdvancePayment) * 100),
                        PaymentExpectedDate = quotation.QuotationValidityDate,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedDate = Helper.Helper.GetDateTime(),
                        CreatedBy = Constants.userId,
                        UpdatedBy = Constants.userId,
                        UpdatedDate = Helper.Helper.GetDateTime(),

                    });
                    if (order.IsInstallment == true)
                    {

                        for (int i = 0; i < order.Payments.Count; i++)
                        {
                            var pay = order.Payments[i];
                            percent += (decimal)pay.PaymentAmountinPercentage;
                            if (order.Payments.Count - 1 == i)
                            {
                                pay.PaymentAmountinPercentage += ((100 - decimal.Parse(order.AdvancePayment)) - percent);
                            }

                            var paymentAmount = ((amountwithoutAdvance / 100) * pay.PaymentAmountinPercentage) * 100;
                            quotation.Payments.Add(new Payment()
                            {
                                PaymentAmountinPercentage = pay.PaymentAmountinPercentage,
                                InquiryId = order.inquiryId,
                                PaymentName = "Installment# " + (i + 1),
                                PaymentStatusId = (int)paymentstatus.InstallmentCreated,
                                PaymentTypeId = (int)paymenttype.Installment,
                                PaymentDetail = "Installment of " + order.inquiryId,
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
                            PaymentAmountinPercentage = decimal.Parse(order.BeforeInstallation),
                            InquiryId = order.inquiryId,
                            PaymentName = "Before Installation",
                            PaymentStatusId = (int)paymentstatus.PaymentCreated,
                            PaymentTypeId = (int)paymenttype.BeforeInstallation,
                            PaymentDetail = "Before Installation of " + order.inquiryId,
                            PaymentAmount = Decimal.Truncate(((decimal.Parse(quotation.TotalAmount) / 100) * decimal.Parse(order.BeforeInstallation)) * 100),
                            PaymentExpectedDate = "",
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = Helper.Helper.GetDateTime(),
                            CreatedBy = Constants.userId,
                            UpdatedBy = Constants.userId,
                            UpdatedDate = Helper.Helper.GetDateTime(),

                        });
                        order.AfterDelivery = (100 - (decimal.Parse(order.BeforeInstallation) + decimal.Parse(order.AdvancePayment))).ToString();
                        quotation.Payments.Add(new Payment()
                        {
                            PaymentAmountinPercentage = decimal.Parse(order.AfterDelivery),
                            InquiryId = order.inquiryId,
                            PaymentName = "After Delivery",
                            PaymentStatusId = (int)paymentstatus.PaymentCreated,
                            PaymentTypeId = (int)paymenttype.AfterDelivery,
                            PaymentDetail = "After Delivery of " + order.inquiryId,
                            PaymentAmount = Decimal.Truncate(((decimal.Parse(quotation.TotalAmount) / 100) * decimal.Parse(order.AfterDelivery)) * 100),
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

                    if (Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(_jobOrder.DetailedDesignFile)) != null)
                    {
                        files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(_jobOrder.DetailedDesignFile)));
                    }

                    if (Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(_jobOrder.MepdrawingFileUrl)) != null)
                    {
                        files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(_jobOrder.MepdrawingFileUrl)));
                    }
                    

                    foreach (var file in quotation.Files)
                    {
                        files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(file.FileUrl)));
                    }

                }
                inquiry.JobOrders.Add(_jobOrder);
                inquiryRepository.Update(inquiry);
                try
                {
                    await mailService.SendEmailAsync(new MailRequest { Subject = "Quotation Files", ToEmail = inquiry.Customer.CustomerEmail, Body = "Quotation File", Attachments = files });

                }
                catch (Exception ex)
                {
                    Sentry.SentrySdk.CaptureMessage(ex.Message);
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == approve.inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.contractWaitingForCustomerApproval)
               .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer)
                .FirstOrDefault();
            if (inquiry != null)
            {


                inquiry.InquiryStatusId = (int)inquiryStatus.contractApproved;
                inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                // inquiry.InquiryComment = updateQuotation.reason;

                foreach (var workscope in inquiry.InquiryWorkscopes)
                {
                    workscope.InquiryStatusId = (int)inquiryStatus.contractApproved;

                }
                foreach (var payment in inquiry.Payments)
                {
                    payment.PaymentModeId = approve.SelectedPaymentMode;
                    if (payment.PaymentModeId == (int)paymentMode.OnlinePayment)
                    {
                        payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                        payment.ClientSecret = approve.ClientSecret;
                        payment.PaymentMethod = approve.PaymentMethod;
                        payment.PaymentIntentToken = approve.PaymentIntentToken;
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

                    if (approve.Pdf != null && approve.Pdf.Count() >= 0)
                    {
                        var fileUrl = await Helper.Helper.UploadFile(approve.Pdf);
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
                }

                inquiryRepository.Update(inquiry);
                List<int?> roletypeId = new List<int?>();
                roletypeId.Add((int)roleType.Manager);
                try
                {
                    sendNotificationToHead("Contract Of inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Approved By Client", false,
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
        public object ClientRejectContract(ContractReject reject)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == reject.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.QuotationStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval && y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.PaymentTypeId != (int)paymenttype.Measurement && y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.contractRejected;
                //Helper.Helper.Each(inquiry.Quotations, x => x.QuotationStatusId = (int)inquiryStatus.contractRejected);
                foreach (var payment in inquiry.Payments)
                {
                    payment.IsActive = false;
                }

                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.contractRejected;
                }

                foreach (var quotation in inquiry.Quotations)
                {
                    quotation.IsActive = false;
                    quotation.Description = reject.Comment;
                    quotation.FeedBackReactionId = reject.FeedBackReactionId;
                }
                List<int?> roletypeId = new List<int?>();
                roletypeId.Add((int)roleType.Manager);
                try
                {
                    sendNotificationToHead("Contract Of inquiry Code: " + "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Rejected By Client Reason: " + reject.Comment, false,
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

    }
}
