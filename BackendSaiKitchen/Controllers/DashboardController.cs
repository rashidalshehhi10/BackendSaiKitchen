using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackendSaiKitchen.Models;

namespace BackendSaiKitchen.Controllers
{
    public class DashboardController : BaseController
    {

        [HttpPost]
        [Route("[action]")]
        public object ViewDashboard()
        {
            var user = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId)
                .FirstOrDefault();

            Dashborad dashborad;
            if (user != null)
            {
                dashborad = branchRepository.FindByCondition(x => x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false)
                   .Select(x => new Dashborad
                   {
                       CustomerContacted = x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false && y.ContactStatusId == 1).Count(),
                       CustomerNeedtoContact = x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false && y.ContactStatusId == 2).Count(),
                       CustomerRegistered = x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
                       InquiryCompleted = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
                       InquiryIncomplete = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId != (int)inquiryStatus.inquiryCompleted).Count(),
                       Totalquotations = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationAccepted || x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)).Select(y => y.Quotations).Count(),
                       QuotationAccepted = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                       QuotationRejected = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationRejected).Count(),
                       TotalJoborder = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderCreated).Count(),
                       TotalInquiries = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                   }).FirstOrDefault();

                var inquiries = inquiryRepository.FindByCondition(x => x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false)
                    .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && (y.MeasurementAssignedTo == Constants.userId || y.DesignAssignedTo == Constants.userId)))
                    .ThenInclude(y => y.Workscope);

                dashborad.calendar = new List<Calendar>();
                foreach (var inquiry in inquiries)
                {
                    foreach (var inworkscope in inquiry.InquiryWorkscopes)
                    {
                        if (inworkscope.MeasurementAssignedTo == Constants.userId)
                        {
                            dashborad.calendar.Add(new Calendar
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

                            dashborad.calendar.Add(new Calendar
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

                    dashborad.calendar.Add(new Calendar
                    {
                        Id = customer.CustomerId,
                        Name = "Meeting with " + customer.CustomerName,
                        Description = "You have meeting with " + customer.CustomerName + " at " + customer.CustomerNextMeetingDate + " Contact: " + customer.CustomerContact,
                        Date = customer.CustomerNextMeetingDate,
                        OnClickURL = "",
                        EventTypeId = (int)eventType.Customer,
                    });

                }
                calendarEventRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId).ToList().ForEach(x => dashborad.calendar.Add(new Calendar
                {
                    Id = x.CalendarEventId,
                    Name = x.CalendarEventName,
                    Description = x.CalendarEventDescription,
                    Date = x.CalendarEventDate,
                    OnClickURL = x.CalendarEventOnClickUrl,
                    EventTypeId = (int)x.EventTypeId
                }));
                response.data = dashborad;

            }
            else
            {
                response.isError = true;
                response.errorMessage = "User Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object AddCalendarEvent(CalendarEvent calendarEvent)
        {
            calendarEvent.IsActive = true;
            calendarEvent.IsDeleted = false;
            //calendarEvent.EventTypeId = (int)eventType.Other;
            calendarEvent.UserId = Constants.userId;
            if (calendarEvent.CalendarEventId == 0)
            {
                calendarEventRepository.Create(calendarEvent);
            }
            else
            {
                var calendar = calendarEventRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.CalendarEventId == calendarEvent.CalendarEventId).FirstOrDefault();
                if (calendar != null)
                {
                    calendar.CalendarEventName = calendarEvent.CalendarEventName;
                    calendar.CalendarEventDescription = calendarEvent.CalendarEventDescription;
                    calendar.CalendarEventOnClickUrl = calendarEvent.CalendarEventOnClickUrl;
                    calendarEventRepository.Update(calendar);
                }

            }
            context.SaveChanges();
            response.data = calendarEvent;
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object DeleteCalendarEvent(int calendarEventId)
        {
            var calendar = calendarEventRepository.FindByCondition(x => x.CalendarEventId == calendarEventId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (calendar != null)
            {
                calendarEventRepository.Delete(calendar);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Event doesnt exist";
            }
            return response;
        }
    }
}
