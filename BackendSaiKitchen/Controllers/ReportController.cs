using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var _branch = branchRepository.FindByCondition(x => x.BranchId == req.Id && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();

            if (_branch != null)
            {
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
                    .ThenInclude(x => x.User)
                    .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(x => x.BranchRole).FirstOrDefault();

                BranchReport report = new BranchReport();

                if (branch.Inquiries.Count != 0)
                {
                    report = new BranchReport()
                    {
                        AmountPending = branch?.Inquiries?.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId == (int)paymentstatus.PaymentPending || y.PaymentStatusId == (int)paymentstatus.InstallmentPending || y.PaymentStatusId == (int)paymentstatus.InstallmentWaitingofApproval || y.PaymentStatusId == (int)paymentstatus.PaymentWaitingofApproval))?.Sum(y => (decimal?)y.PaymentAmount),
                        AmountReceived = branch?.Inquiries?.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Sum(y => (decimal?)y.PaymentAmount),
                        BankPaid = branch?.Inquiries?.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cash && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Count(),
                        CashPaid = branch?.Inquiries?.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Count(),
                        ChequePaid = branch?.Inquiries?.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Count(),
                        OnlinePaid = branch?.Inquiries?.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OnlinePayment && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Count(),
                        InquiriesCompleted = branch?.Inquiries?.Where(x => x.InquiryStatusId == (int)inquiryStatus.inquiryCompleted && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Count(),
                        InquiriesInComplete = branch?.Inquiries?.Where(x => x.InquiryStatusId != (int)inquiryStatus.inquiryCompleted && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Count(),
                        JobOrderCreated = branch?.Inquiries?.Where(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)) && x.InquiryStatusId == (int)inquiryStatus.jobOrderCreated)?.Count(),//.JobOrders.Where(z => z.IsActive == true && z.IsDeleted == false)?.Count(),
                        JobOrederRejected = branch?.Inquiries?.Where(x => x.IsActive == true && x.IsDeleted == false && (Helper.Helper.ConvertToDateTime(x.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(x.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)) && x.InquiryStatusId == (int)inquiryStatus.jobOrderRejected)?.Count(),//.JobOrders.Where(z => z.IsActive == true && z.IsDeleted == false)?.Count(),
                        QuotationAccepted = branch?.Inquiries?.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Quotations?.Where(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.quotationAccepted)?.Count(),
                        QuotationPending = branch?.Inquiries?.Where(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate) && (y.InquiryStatusId == (int)inquiryStatus.quotationPending || y.InquiryStatusId == (int)inquiryStatus.quotationSchedulePending)))?.Count(),
                        QuotationRejected = branch?.Inquiries?.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false && (Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))?.Quotations?.Where(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.quotationRejected)?.Count(),
                        TotalBank = (decimal)branch?.Inquiries?.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false)?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OfflinePaybyCard && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Sum(y => y.PaymentAmount),
                        TotalCash = (decimal)branch?.Inquiries?.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false)?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cash && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Sum(y => y.PaymentAmount),
                        TotalCheque = (decimal)branch?.Inquiries?.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false)?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Sum(y => y.PaymentAmount),
                        TotalOnline = (decimal)branch?.Inquiries?.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false)?.Payments?.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OnlinePayment && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved))?.Sum(y => y.PaymentAmount),
                        CustomerSatisfy = (double?)Math.Round((decimal)branch?.Inquiries?.FirstOrDefault(y => Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))?.InquiryWorkscopes?.Select(x => (x.FeedbackReaction == null ? 1 : (x.FeedbackReaction == 0 ? 1 : x.FeedbackReaction))).Average() * 100 / 7) // branch?.Inquiries?.FirstOrDefault(y => Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate))?.InquiryWorkscopes ?.Select(x => x.FeedbackReaction).Count()*100
                    };
                }
                report.inquiryPendingDetails = new List<InquiryPendingDetails>();
                report.InquiryReceivedDetails = new List<InquiryReceivedDetails>();
                report.topFivePaidCustomers = new List<TopFivePaidCustomer>();
                report.TopFiveNewCustomers = new List<TopFiveNewCustomers>();
                report.employees = new List<Employee>();
                report.customerContactSources = new List<CustomerContactSource>();
                report.receivedPaymentModes = new List<ReceivedPaymentMode>();
                report.CustomerSatisfaction = new List<MonthlyReview>();
                report.MonthlyAmountReceived = new List<MonthlyReview>();
                foreach (var inquiry in branch.Inquiries.Where(y => Helper.Helper.ConvertToDateTime(y.CreatedDate) >= Helper.Helper.ConvertToDateTime(req.StartDate) && Helper.Helper.ConvertToDateTime(y.CreatedDate) <= Helper.Helper.ConvertToDateTime(req.EndDate)))
                {
                    report.inquiryPendingDetails.Add(new InquiryPendingDetails
                    {
                        Address = inquiry.Building.BuildingAddress,
                        CustomerName = inquiry.Customer.CustomerName,
                        Email = inquiry.Customer.CustomerEmail,
                        MobileNomber = inquiry.Customer.CustomerContact,
                        WorkscopeName = inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList(),
                        InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId,
                        AmountPending = (decimal)inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentPending || x.PaymentStatusId == (int)paymentstatus.PaymentPending).Sum(x => x.PaymentAmount),

                        reportPayments = inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentPending || x.PaymentStatusId == (int)paymentstatus.PaymentPending).Select(x => new reportPayment
                        {
                            Amount = (decimal)x.PaymentAmount,
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
                        InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId,
                        AmountRecieved = (decimal)inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentApproved || x.PaymentStatusId == (int)paymentstatus.PaymentApproved).Sum(x => x.PaymentAmount),
                        reportPayments = inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentApproved || x.PaymentStatusId == (int)paymentstatus.PaymentApproved).Select(x => new reportPayment
                        {
                            Amount = (decimal)x.PaymentAmount,
                            PaymentMethod = x.PaymentMethod,
                            PaymentModeId = (int)x.PaymentModeId,
                            PaymentType = x.PaymentType.PaymentTypeName,
                            Date = x.PaymentExpectedDate
                        }).ToList()
                    });
                }

                var inquiries = branch.Inquiries.Where(y => Helper.Helper.ConvertToDateTime(y.CreatedDate).Month >= Helper.Helper.ConvertToDateTime(req.StartDate).Month && Helper.Helper.ConvertToDateTime(y.CreatedDate).Month <= Helper.Helper.ConvertToDateTime(req.EndDate).Month);

                for (int i = Helper.Helper.ConvertToDateTime(req.StartDate).Month; i <= Helper.Helper.ConvertToDateTime(req.EndDate).Month; i++)
                {
                    var inworscopes = inquiries.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Month == i).Select(x => x.InquiryWorkscopes).ToList();
                    var payments = inquiries.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Month == i).Select(x => x.Payments).ToList();
                    double? FeedBackAverage = 0;
                    decimal? PaymentAverage = 0;

                    string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3);


                    inworscopes.ForEach(x => FeedBackAverage = x.Select(y => (y.FeedbackReaction == null ? 1 : (y.FeedbackReaction == 0 ? 1 : y.FeedbackReaction))).Average());
                    payments.ForEach(x => PaymentAverage = x.Select(y => y.PaymentAmount == null ? 0 : y.PaymentAmount).Average());
                    report.CustomerSatisfaction.Add(new MonthlyReview
                    {
                        Avarege = (decimal?)FeedBackAverage,
                        Month = month
                    });

                    report.MonthlyAmountReceived.Add(new MonthlyReview
                    {
                        Avarege = PaymentAverage,
                        Month = month
                    });
                }



                int x = 0;
                foreach (var Customer in branch.Customers.OrderBy(x => Helper.Helper.ConvertToDateTime(x.CreatedDate)))
                {
                    report.TopFiveNewCustomers.Add(new TopFiveNewCustomers
                    {
                        Name = Customer.CustomerName,
                        CreatedDate = Customer.CreatedDate,
                        CustomerContact = Customer.CustomerContact,
                    });

                    decimal amount = 0;
                    foreach (var inquiry in Customer.Inquiries)
                    {
                        foreach (var payment in inquiry.Payments.Where(x => x.PaymentStatusId == (int)paymentstatus.InstallmentApproved || x.PaymentStatusId == (int)paymentstatus.PaymentApproved))
                        {
                            amount += (decimal)payment.PaymentAmount;
                        }
                    }
                    report.topFivePaidCustomers.Add(new TopFivePaidCustomer
                    {
                        Name = Customer.CustomerName,
                        AmountRecieved = amount,
                        CustomerContact = Customer.CustomerContact,
                    });
                    if (x >= 4)
                        break;
                    x++;
                }

                foreach (var wayofcontact in wayOfContactRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false))
                {
                    double value = 0;
                    if (branch.Customers.Count() != 0)
                        value = ((double)branch.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.WayofContactId == wayofcontact.WayOfContactId).Count() / (double)branch.Customers.Count()) * 100;



                    report.customerContactSources.Add(new CustomerContactSource
                    {
                        ContactMode = wayofcontact.WayOfContactName,
                        Percentage = Convert.ToInt32(Math.Round((decimal)value))
                    });
                }

                for (int i = 1; i < 5; i++)
                {
                    string mode = Enum.GetName(typeof(paymentMode), i);
                    decimal value = 0;
                    if (report.AmountReceived != 0 && report.AmountReceived != null)
                        value = (decimal)(paymentRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && (x.PaymentStatusId == (int)paymentstatus.PaymentApproved || x.PaymentStatusId == (int)paymentstatus.InstallmentApproved)).Sum(x => x.PaymentAmount) / report.AmountReceived) * 100;


                    report.receivedPaymentModes.Add(new ReceivedPaymentMode
                    {
                        PaymentMode = mode,
                        Percentage = Convert.ToInt32(Math.Round((decimal)value, 0))
                    });
                }



                foreach (var userrole in branch.UserRoles)
                {
                    if (userrole.User != null && userrole.User.IsActive == true && userrole.User.IsDeleted == false)
                    {
                        report.employees.Add(new Employee
                        {
                            Contact = userrole.User.UserMobile,
                            Email = userrole.User.UserEmail,
                            Name = userrole.User.UserName,
                            Position = userrole.BranchRole.BranchRoleName
                        });
                    }

                }

                report.topFivePaidCustomers.OrderBy(x => x.AmountRecieved);
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
