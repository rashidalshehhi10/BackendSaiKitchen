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
            //Dashborad dashborad = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId)
            //   .Select(x => new Dashborad
            //   {
            //       CustomerRegistered = x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
            //       InquiryCompleted = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
            //       InquiryCode = x.Inquiries.OrderBy(y => y.InquiryId).Where(y => y.IsActive == true && y.IsDeleted == false).Select(y => y.InquiryCode).ToList(),
            //       MeasurementScheduleDate = x.Inquiries.OrderBy(y => y.InquiryId).FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.MeasurementScheduleDate).ToList(),
            //       DesignScheduledate = x.Inquiries.OrderBy(y => y.InquiryId).FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.DesignScheduleDate).ToList(),
            //       WorkscopeName = x.Inquiries.OrderBy(y => y.InquiryId).FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.Workscope.WorkScopeName).ToList()
            //   }).FirstOrDefault();
            //try
            //{
            //    dashborad.calendar = new List<Calendar>();
            //    for (int i = 0; i < dashborad.InquiryCode.Count; i++)
            //    {
            //        dashborad.calendar.Add(new Calendar { DesignScheduledate = dashborad.DesignScheduledate[i], MeasurementScheduleDate = dashborad.MeasurementScheduleDate[i], InquiryCode = dashborad.InquiryCode[i], WorkscopeName = dashborad.WorkscopeName[i] });
            //    }
            //}
            //catch (Exception ex)
            //{

            //    Serilog.Log.Error(ex.Message);
            //}



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
                .FirstOrDefault();

            Dashborad dashborad = new Dashborad()
            {
                CustomerRegistered = user.Customers.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                InquiryCompleted =user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
                InquiryIncomplete = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId != (int)inquiryStatus.inquiryCompleted).Count(),
                TotalInquiries = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                CustomerContacted=user.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == 1).Count(),
                CustomerNeedtoContact= user.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == 2).Count(),
                Totalquotations = user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationAccepted|| x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)).Select(y => y.Quotations).Count(),
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
                        InquiryId= inquiry.InquiryId,
                        InquiryWorkscopeId =inworkscope.InquiryWorkscopeId,
                        InquiryCode = inquiry.InquiryCode,
                        MeasurementScheduleDate =inworkscope.MeasurementScheduleDate,
                        DesignScheduledate=inworkscope.DesignScheduleDate,
                        WorkscopeName=inworkscope.Workscope.WorkScopeName,
                        InquiryworkscopeStatusId=(int)inworkscope.InquiryStatusId
                    });
                }
            }
            response.data = dashborad;
            return response;
        }
    }
}
