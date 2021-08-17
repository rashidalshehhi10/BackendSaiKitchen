using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BackendSaiKitchen.Controllers
{
    public class DashboardController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object ViewDashborad()
        {
            var user = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId)
                .Include(x => x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Workscope)
                .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Payments.Where(z => z.IsActive == true && z.IsDeleted == false))
                .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.JobOrders.Where(z => z.IsActive == true && z.IsDeleted == false))
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.BranchRole)
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Branch)
                .FirstOrDefault();
            Dashborad dashborad = new Dashborad();
            var roletypeid = user.UserRoles.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).BranchRole.RoleTypeId;
            var branchid = user.UserRoles.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false).BranchId;
            if (user != null)
            {


                if (roletypeid == (int)roleType.Manager)
                {
                    var branch = branchRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchid)
                        .Include(x => x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .Include(x => x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false))
                        .ThenInclude(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .ThenInclude(x => x.Workscope)
                        .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .ThenInclude(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .ThenInclude(y => y.Payments.Where(z => z.IsActive == true && z.IsDeleted == false))
                        .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .ThenInclude(y => y.JobOrders.Where(z => z.IsActive == true && z.IsDeleted == false))
                        .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .ThenInclude(y => y.BranchRole)
                        .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .ThenInclude(x => x.Branch).FirstOrDefault();
                    dashborad = new Dashborad()
                    {
                        CustomerRegistered = branch.Customers.Count(),
                        InquiryCompleted = branch.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
                        InquiryIncomplete = branch.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId != (int)inquiryStatus.inquiryCompleted).Count(),
                        TotalInquiries = branch.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                        CustomerContacted = branch.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == 1).Count(),
                        CustomerNeedtoContact = branch.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == 2).Count(),
                        Totalquotations = branch.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationAccepted || x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)).Select(y => y.Quotations).Count(),
                        QuotationAccepted = branch.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                        QuotationRejected = branch.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationRejected).Count(),
                        TotalJoborder = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderCreated).Count()

                    };
                    dashborad.calendar = new List<Calendar>();
                    foreach (var inquiry in user.Inquiries)
                    {
                        foreach (var inworkscope in inquiry.InquiryWorkscopes)
                        {
                            dashborad.calendar.Add(new Calendar
                            {
                                InquiryId = inquiry.InquiryId,
                                InquiryWorkscopeId = inworkscope.InquiryWorkscopeId,
                                InquiryCode = inquiry.InquiryCode,
                                MeasurementScheduleDate = inworkscope.MeasurementScheduleDate,
                                DesignScheduledate = inworkscope.DesignScheduleDate,
                                WorkscopeName = inworkscope.Workscope.WorkScopeName,
                                InquiryworkscopeStatusId = (int)inworkscope.InquiryStatusId
                            });
                        }
                    }
                }
                else
                {
                    dashborad = new Dashborad()
                    {
                        CustomerRegistered = user.Customers.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                        InquiryCompleted = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
                        InquiryIncomplete = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId != (int)inquiryStatus.inquiryCompleted).Count(),
                        TotalInquiries = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                        CustomerContacted = user.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == 1).Count(),
                        CustomerNeedtoContact = user.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == 2).Count(),
                        Totalquotations = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationAccepted || x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)).Select(y => y.Quotations).Count(),
                        QuotationAccepted = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                        QuotationRejected = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationRejected).Count(),
                        //Invoicegenerated = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false ).Select(y => y.Quotations.Where(z => z.IsActive == true && z.IsDeleted == false).Select(f => f.Files)).Count(),
                        //TotalCashPayment = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Select(y => y.Quotations.Where(z => z.IsActive == true && z.IsDeleted == false).Select(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cash && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)))).Count(),
                        //TotalChequePayment = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Select(y => y.Quotations.Where(z => z.IsActive == true && z.IsDeleted == false).Select(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.Cheque && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)))).Count(),
                        //TotalOnlinePayment = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Select(y => y.Quotations.Where(z => z.IsActive == true && z.IsDeleted == false).Select(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.PaymentModeId == (int)paymentMode.OnlinePayment && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)))).Count(),
                        //InvoicePaid = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Select(y => y.Quotations.Where(z => z.IsActive == true && z.IsDeleted == false).Select(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved||y.PaymentStatusId ==(int)paymentstatus.InstallmentApproved)))).Count(),
                        TotalJoborder = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderCreated).Count()

                    };
                    dashborad.calendar = new List<Calendar>();
                    foreach (var inquiry in user.Inquiries)
                    {
                        foreach (var inworkscope in inquiry.InquiryWorkscopes)
                        {
                            dashborad.calendar.Add(new Calendar
                            {
                                InquiryId = inquiry.InquiryId,
                                InquiryWorkscopeId = inworkscope.InquiryWorkscopeId,
                                InquiryCode = inquiry.InquiryCode,
                                MeasurementScheduleDate = inworkscope.MeasurementScheduleDate,
                                DesignScheduledate = inworkscope.DesignScheduleDate,
                                WorkscopeName = inworkscope.Workscope.WorkScopeName,
                                InquiryworkscopeStatusId = (int)inworkscope.InquiryStatusId
                            });
                        }
                    }
                }

            }
            else
            {
                response.isError = true;
                response.errorMessage = "User Not Found";
            }
            response.data = dashborad;
            return response;
        }
    }
}
