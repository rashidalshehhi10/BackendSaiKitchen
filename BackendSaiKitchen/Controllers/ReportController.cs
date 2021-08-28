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
                    TotalJoborder = x.Inquiries.First(x => x.IsActive == true && x.IsDeleted == false).JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
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
            var user = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == req.Id).Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Workscope).Include(x => x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(y => y.Customer)
                .Include(x => x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (user != null)
            {
                UserReport userReport = new UserReport();
                userReport.reviews = new List<Review>();
                userReport.CustomerCount = user.Customers.Where(x => DateTime.Parse(x.CreatedDate) >= DateTime.Parse(req.StartDate) && DateTime.Parse(x.CreatedDate) >= DateTime.Parse(req.StartDate)).Count();
                foreach (var inquiry in user.Inquiries)
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
        public async Task<object> TestUpload(byte[] blob)
        {
            try
            {
                var Url = await Helper.Helper.PostFile(blob,"pdf");
                response.data = Url;
            }
            catch (Exception ex)
            {
                response.isError = true;
                response.errorMessage = ex.Message;

            }
            return response;
        }
    }
}
    