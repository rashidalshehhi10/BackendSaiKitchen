using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackendSaiKitchen.Controllers
{
    public class ReportController : BaseController
    {
        public ReportController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetReportForCustomer(int CustomerId)
        {
            var customer = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.CustomerId == CustomerId)
                .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.InquiryWorkscopes.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(y => y.Workscope).FirstOrDefault();
            if (customer != null)
            {
                Report report = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.CustomerId == CustomerId)
                .Select(x => new Report
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    TotalInquiries = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                    Totalquotations = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Quotations.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
                    TotalJoborder = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
                    QuotationAccepted = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Quotations.Where(y => y.IsActive == true && y.IsDeleted == false && y.QuotationStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                    QuotationRejected = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Quotations.Where(y => y.IsActive == true && y.IsDeleted == false && y.QuotationStatusId == (int)inquiryStatus.quotationRejected).Count(),
                    QuotationPending = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Quotations.Where(y => y.IsActive == true && y.IsDeleted == false && y.QuotationStatusId == (int)inquiryStatus.quotationRejected).Count(),
                    Payments = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId != (int)paymentstatus.InstallmentCreated || y.PaymentStatusId != (int)paymentstatus.PaymentCreated)).Count(),
                    TotalAmount = (decimal)x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId != (int)paymentstatus.InstallmentCreated || y.PaymentStatusId != (int)paymentstatus.PaymentCreated) && y.PaymentModeId != null).Sum(y => y.PaymentAmount),
                    TotalUnpaid = (decimal)x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId == (int)paymentstatus.PaymentPending || y.PaymentStatusId == (int)paymentstatus.InstallmentPending || y.PaymentStatusId == (int)paymentstatus.InstallmentWaitingofApproval || y.PaymentStatusId == (int)paymentstatus.PaymentWaitingofApproval)).Sum(y => y.PaymentAmount),
                    TotalPaid = (decimal)x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                    CashPaid = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cash && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Count(),
                    TotalCash = (decimal)x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cash && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                    ChequePaid = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Count(),
                    TotalCheque = (decimal)x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                    OnlinePaid = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OnlinePayment && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Count(),
                    TotalOnline = (decimal)x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OnlinePayment && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                    BankPaid = x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OfflinePaybyCard && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Count(),
                    TotalBank = (decimal)x.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OfflinePaybyCard && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                }).FirstOrDefault();

                report.reviews = new List<Review>();
                foreach (var inquiry in customer.Inquiries)
                {
                    List<CustomerReview> CustomerReview = new List<CustomerReview>();
                    foreach (var inworkscope in inquiry.InquiryWorkscopes)
                    {
                        CustomerReview.Add(new CustomerReview
                        {
                            id = inworkscope.InquiryWorkscopeId,
                            feedBack = (int)inworkscope.FeedbackReaction,
                            statusId = (int)inworkscope.InquiryStatusId,
                            Name = inworkscope.Workscope.WorkScopeName,
                        });
                    }
                    report.reviews.Add(new Review
                    {
                        Code = inquiry.InquiryCode,
                        Date = inquiry.CreatedDate,
                        inquiryStatusId = (int)inquiry.InquiryStatusId,
                        customerReviews = CustomerReview,
                        CustomerName = inquiry.Customer.CustomerName
                    });
                }

                response.data = report;

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Customer Not Found";
            }

            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object GetReportForUser(ReqReport req)
        {
            var user = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == req.Id).Include(x => x.InquiryAddedByNavigations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Workscope).Include(x => x.InquiryAddedByNavigations.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(y => y.Customer)
                .Include(x => x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (user != null)
            {
                UserReport userReport = new UserReport();
                userReport.reviews = new List<Review>();
                userReport.CustomerCount = user.Customers.Where(x => DateTime.Parse(x.CreatedDate) >= DateTime.Parse(req.StartDate) && DateTime.Parse(x.CreatedDate) >= DateTime.Parse(req.StartDate)).Count();
                foreach (var inquiry in user.InquiryAddedByNavigations)
                {
                    if (DateTime.Parse(inquiry.InquiryStartDate) >= DateTime.Parse(req.StartDate) && DateTime.Parse(inquiry.InquiryStartDate) <= DateTime.Parse(req.EndDate))
                    {
                        List<CustomerReview> CustomerReview = new List<CustomerReview>();
                        foreach (var inworkscope in inquiry.InquiryWorkscopes)
                        {
                            CustomerReview.Add(new CustomerReview
                            {
                                id = inworkscope.InquiryWorkscopeId,
                                feedBack = (int)inworkscope.FeedbackReaction,
                                statusId = (int)inworkscope.InquiryStatusId,
                                Name = inworkscope.Workscope.WorkScopeName,
                            });
                        }
                        userReport.reviews.Add(new Review
                        {
                            Code = inquiry.InquiryCode,
                            CustomerName = inquiry.Customer.CustomerName,
                            Date = inquiry.InquiryStartDate,
                            inquiryStatusId = (int)inquiry.InquiryStatusId,
                            customerReviews = CustomerReview
                        });
                    }
                }
                response.data = userReport;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "User Not Found";
            }
            //response.data = report;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetReportForBranch(ReqReport req)
        {
            var _branch = branchRepository.FindByCondition(x => x.BranchId == req.Id && x.IsActive == true && x.IsDeleted == false).Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            if (_branch != null)
            {
                //var report = branchRepository.FindByCondition(x => x.BranchId == req.Id && x.IsActive == true && x.IsDeleted == false).Select(x => new {
                //    AmountRecived = x.Inquiries.FirstOrDefault(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId != (int)paymentstatus.InstallmentCreated || y.PaymentStatusId != (int)paymentstatus.PaymentCreated)).Sum(y => y.PaymentAmount),
                //    AmountPending = x.Inquiries.FirstOrDefault(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId == (int)paymentstatus.PaymentPending || y.PaymentStatusId == (int)paymentstatus.InstallmentPending || y.PaymentStatusId == (int)paymentstatus.InstallmentWaitingofApproval || y.PaymentStatusId == (int)paymentstatus.PaymentWaitingofApproval)).Sum(y => y.PaymentAmount),
                //    CompletedInquiries = x.Inquiries.Where(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)) && y.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
                //    InCompletedInquiries = x.Inquiries.Where(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)) && y.InquiryStatusId != (int)inquiryStatus.inquiryCompleted).Count(),
                //    QuotationAccepted = x.Inquiries.FirstOrDefault(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Quotations.Where(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                //    QuotationRejected = x.Inquiries.FirstOrDefault(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Quotations.Where(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.quotationRejected).Count(),
                //    QuotationPending = x.Inquiries.FirstOrDefault(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Quotations.Where(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.quotationPending).Count(),
                //    JobOrderCreated = x.Inquiries.FirstOrDefault(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)) && y.InquiryStatusId == (int)inquiryStatus.jobOrderCreated).JobOrders.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                //    JobOrderRejected = x.Inquiries.FirstOrDefault(y => y.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)) && y.InquiryStatusId == (int)inquiryStatus.jobOrderRejected).JobOrders.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                //});
                var branch = branchRepository.FindByCondition(x => x.BranchId == req.Id && x.IsActive == true && x.IsDeleted == false)
                    .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.Workscope)
                    .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.PaymentType)
                    .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.Building)
                    .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.Customer)
                    .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.User).FirstOrDefault();

                BranchReport report = new BranchReport();
                report.inquiryPendingDetails = new List<InquiryPendingDetails>();
                report.InquiryReceivedDetails = new List<InquiryReceivedDetails>();
                foreach (var inquiry in branch.Inquiries.Where(y => Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))
                {
                    report.inquiryPendingDetails.Add(new InquiryPendingDetails
                    {
                        Address = inquiry.Building.BuildingAddress,
                        CustomerName = inquiry.Customer.CustomerName,
                        Email = inquiry.Customer.CustomerEmail,
                        MobileNomber = inquiry.Customer.CustomerContact,
                        WorkscopeName = inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList(),
                        InquiryCode = inquiry.InquiryCode,
                        AmountPending =(decimal)inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentPending || x.PaymentStatusId == (int)paymentstatus.PaymentPending).Sum(x => x.PaymentAmount),
                       
                        reportPayments = inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentApproved || x.PaymentStatusId == (int)paymentstatus.PaymentApproved).Select(x => new reportPayment
                        {
                            Amount = (decimal)x.PaymentAmount,
                            //PaymentMethod = x.PaymentMethod,
                            //PaymentModeId = (int)x.PaymentModeId,
                            //PaymentType = x.PaymentType.PaymentTypeName,
                            Date = x.PaymentExpectedDate
                        }).ToList()
                    });

                    report.InquiryReceivedDetails.Add(new InquiryReceivedDetails
                    {
                        Address = inquiry.Building.BuildingAddress,
                        CustomerName = inquiry.Customer.CustomerName,
                        Email = inquiry.Customer.CustomerEmail,
                        MobileNomber = inquiry.Customer.CustomerContact,
                        WorkscopeName = inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList(),
                        InquiryCode = inquiry.InquiryCode,
                        AmountRecieved = (decimal)inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentApproved || x.PaymentStatusId == (int)paymentstatus.PaymentApproved).Sum(x => x.PaymentAmount),
                        reportPayments = inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentApproved || x.PaymentStatusId == (int)paymentstatus.PaymentApproved).Select(x => new reportPayment { Amount = (decimal)x.PaymentAmount,
                            PaymentMethod=x.PaymentMethod,
                        PaymentModeId = (int)x.PaymentModeId,
                        PaymentType = x.PaymentType.PaymentTypeName,
                        Date= x.PaymentExpectedDate}).ToList()
                    }) ;
                }

                report = new BranchReport()
                {
                    AmountPending = (decimal)branch.Inquiries.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId == (int)paymentstatus.PaymentPending || y.PaymentStatusId == (int)paymentstatus.InstallmentPending || y.PaymentStatusId == (int)paymentstatus.InstallmentWaitingofApproval || y.PaymentStatusId == (int)paymentstatus.PaymentWaitingofApproval)).Sum(y => y.PaymentAmount),
                    AmountRecived = (decimal)branch.Inquiries.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId != (int)paymentstatus.InstallmentCreated || y.PaymentStatusId != (int)paymentstatus.PaymentCreated)).Sum(y => y.PaymentAmount),
                    BankPaid = branch.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cash && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Count(),
                    CashPaid = branch.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Count(),
                    ChequePaid = branch.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Count(),
                    OnlinePaid = branch.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OnlinePayment && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Count(),
                    InquiriesCompleted = branch.Inquiries.Where(x => x.InquiryStatusId == (int)inquiryStatus.inquiryCompleted && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Count(),
                    InquiriesInComplete = branch.Inquiries.Where(x => x.InquiryStatusId != (int)inquiryStatus.inquiryCompleted && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Count(),
                    JobOrderCreated = branch.Inquiries.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)) && y.InquiryStatusId == (int)inquiryStatus.jobOrderCreated).JobOrders.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                    JobOrederRejected= branch.Inquiries.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)) && y.InquiryStatusId == (int)inquiryStatus.jobOrderRejected).JobOrders.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                    QuotationAccepted = branch.Inquiries.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Quotations.Where(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                    QuotationPending = branch.Inquiries.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Quotations.Where(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.quotationPending).Count(),
                    QuotationRejected = branch.Inquiries.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))).Quotations.Where(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.quotationRejected).Count(),
                    TotalBank = (decimal)branch.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OfflinePaybyCard && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                    TotalCash = (decimal)branch.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cash && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                    TotalCheque = (decimal)branch.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                    TotalOnline = (decimal)branch.Inquiries.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OnlinePayment && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(y => y.PaymentAmount),
                    
                };

                response.data = report;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Branch Not Found";
            }

            return response;
        }


    }
}
