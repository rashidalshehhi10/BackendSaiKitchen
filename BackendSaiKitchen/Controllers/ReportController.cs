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
        public object Reporting(ReqReport report)
        {
            
            var user = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId)
                .FirstOrDefault();

            Report _report;
            if (user != null)
            {
                _report = branchRepository.FindByCondition(x => x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false)
                    .Select(x => new Report
                    {
                        Payments = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false /*&& (DateTime.Parse(x.CreatedDate) >= DateTime.Parse(report.StartDate) && DateTime.Parse(x.CreatedDate) <= DateTime.Parse(report.StartDate))*/).Count(),
                        QuotationAccepted= x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                        QuotationRejected = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationRejected).Count(),
                        TotalInquiries = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                        Totalquotations = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationAccepted || x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)).Select(y => y.Quotations).Count(),
                        TotalJoborder = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderCreated).Count()
                    }).FirstOrDefault();

                var inquiries = inquiryRepository.FindByCondition(x => x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false/* && (DateTime.Parse(x.CreatedDate) >= DateTime.Parse(report.StartDate) && DateTime.Parse(x.CreatedDate) <= DateTime.Parse(report.EndDate))*/)
                    .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && (y.MeasurementAssignedTo == Constants.userId || y.DesignAssignedTo == Constants.userId)))
                    .ThenInclude(y => y.Workscope);

                _report.calendar = new List<Calendar>();

                foreach (var inquiry in inquiries)
                {
                    foreach (var inworkscope in inquiry.InquiryWorkscopes)
                    {
                        if (inworkscope.MeasurementAssignedTo == Constants.userId)
                        {
                            _report.calendar.Add(new Calendar
                            {
                                Id = inworkscope.InquiryWorkscopeId,
                                Name = inworkscope.Workscope.WorkScopeName + " Measurement",
                                Description = "You are assigned for " + inworkscope.Workscope.WorkScopeName + " measurement of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId,
                                Date = inworkscope.MeasurementScheduleDate,
                                OnClickURL = "",
                                EventTypeId = (int)eventType.Measurement,
                            });
                        }
                        if (inworkscope.DesignAssignedTo == Constants.userId)
                        {

                            _report.calendar.Add(new Calendar
                            {
                                Id = inworkscope.InquiryWorkscopeId,
                                Name = inworkscope.Workscope.WorkScopeName + " Design",
                                Description = "You are assigned for " + inworkscope.Workscope.WorkScopeName + " Design of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId,
                                Date = inworkscope.DesignScheduleDate,
                                OnClickURL = "",
                                EventTypeId = (int)eventType.Design,
                            });
                        }

                    }
                }
                var customers = customerRepository.FindByCondition(x => x.UserId == user.UserId && x.ContactStatusId == (int)contactStatus.NeedToContact && x.IsActive == true && x.IsDeleted == false && x.CustomerNextMeetingDate != null && x.CustomerNextMeetingDate != "").Select(x => new { x.CustomerId, x.CustomerName, x.CustomerContact, x.CustomerNextMeetingDate });
                foreach (var customer in customers)
                {

                    _report.calendar.Add(new Calendar
                    {
                        Id = customer.CustomerId,
                        Name = "Meeting with " + customer.CustomerName,
                        Description = "You have to Follow-up with " + customer.CustomerName + " at " + customer.CustomerNextMeetingDate + " Contact: " + customer.CustomerContact,
                        Date = customer.CustomerNextMeetingDate,
                        OnClickURL = "",
                        EventTypeId = (int)eventType.Customer,
                    });

                }
                calendarEventRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId).ToList().ForEach(x => _report.calendar.Add(new Calendar
                {
                    Id = x.CalendarEventId,
                    Name = x.CalendarEventName,
                    Description = x.CalendarEventDescription,
                    Date = x.CalendarEventDate,
                    OnClickURL = x.CalendarEventOnClickUrl,
                    EventTypeId = (int)x.EventTypeId
                }));
                response.data = _report;
            }
            else
            {
                response.errorMessage = "User Not Found";
                response.isError = true;
            }
            return Response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> TestUpload(byte[] blob)
        {
            try
            {
               var Url = await Helper.Helper.PostFile(blob);
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
