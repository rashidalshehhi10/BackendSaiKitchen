using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace BackendSaiKitchen.Controllers
{
    public class DashboardController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object ViewDashboard()
        {
            User user = userRepository
                .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId)
                .Include(x => x.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false)).ThenInclude(x => x.BranchRole)
                .FirstOrDefault();

            Dashborad dashborad;
            if (user != null)
            {
                dashborad = branchRepository.FindByCondition(x =>
                        x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false)
                    .Select(x => new Dashborad
                    {
                        CustomerContacted = x.Customers.Where(y =>
                            y.IsActive == true && y.IsDeleted == false && y.ContactStatusId == 1).Count(),
                        CustomerNeedtoContact = x.Customers.Where(y =>
                            y.IsActive == true && y.IsDeleted == false && y.ContactStatusId == 2).Count(),
                        CustomerRegistered = x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
                        InquiryCompleted = x.Inquiries.Where(y =>
                            y.IsActive == true && y.IsDeleted == false &&
                            y.InquiryStatusId == (int)inquiryStatus.jobOrderCompleted).Count(),
                        InquiryIncomplete = x.Inquiries.Where(y =>
                            y.IsActive == true && y.IsDeleted == false &&
                            y.InquiryStatusId != (int)inquiryStatus.jobOrderCompleted).Count(),
                        Totalquotations = x.Inquiries.Where(x =>
                            x.IsActive == true && x.IsDeleted == false &&
                            (x.InquiryStatusId == (int)inquiryStatus.quotationAccepted ||
                             x.InquiryStatusId == (int)inquiryStatus.quotationRejected ||
                             x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId ==
                             (int)inquiryStatus.quotationWaitingForCustomerApproval)).Count(),
                        QuotationAccepted = x.Inquiries.Where(x =>
                            x.IsActive == true && x.IsDeleted == false &&
                            x.InquiryStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                        QuotationRejected = x.Inquiries.Where(x =>
                            x.IsActive == true && x.IsDeleted == false &&
                            x.InquiryStatusId == (int)inquiryStatus.quotationRejected).Count(),
                        TotalJoborder = x.Inquiries.Where(x =>
                            x.IsActive == true && x.IsDeleted == false &&
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderCreated).Count(),
                        TotalInquiries = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count()
                    }).FirstOrDefault();

                // Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Inquiry, WorkScope> 
                var inquiries = inquiryRepository.FindByCondition(x =>
                    (x.BranchId == Constants.branchId || x.JobOrders.Any(y => y.IsActive == true && y.IsDeleted == false && y.FactoryId == Constants.branchId)) && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y =>
                    y.IsActive == true && y.IsDeleted == false && (y.MeasurementAssignedTo == Constants.userId ||
                                                                   y.DesignAssignedTo == Constants.userId)))
                .ThenInclude(y => y.Workscope).Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).Include(x => x.Customer);

                dashborad.calendar = new List<Calendar>();
                foreach (Inquiry inquiry in inquiries)
                {
                    if (inquiry.BranchId == Constants.branchId)
                    {
                        foreach (InquiryWorkscope inworkscope in inquiry.InquiryWorkscopes)
                        {
                            if (inworkscope.MeasurementAssignedTo == Constants.userId)
                            {
                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = inworkscope.InquiryWorkscopeId,
                                    Name = inworkscope.Workscope.WorkScopeName + " Measurement",
                                    Description = "You are assigned for " + inworkscope.Workscope.WorkScopeName +
                                                  " measurement of Inquiry Code: IN" + inquiry.BranchId + "" +
                                                  inquiry.CustomerId + "" + inquiry.InquiryId + " For Customer " + inquiry.Customer.CustomerName,
                                    Date = inworkscope.MeasurementScheduleDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.Measurement
                                });
                            }

                            if (inworkscope.DesignAssignedTo == Constants.userId)
                            {
                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = inworkscope.InquiryWorkscopeId,
                                    Name = inworkscope.Workscope.WorkScopeName + " Design",
                                    Description = "You are assigned for " + inworkscope.Workscope.WorkScopeName +
                                                  " Design of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                                  "" + inquiry.InquiryId + " For Customer " + inquiry.Customer.CustomerName,
                                    Date = inworkscope.DesignScheduleDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.Design
                                });
                            }
                        }
                    }


                    foreach (var job in inquiry.JobOrders)
                    {
                        if (user.UserRoles.FirstOrDefault().BranchRole.RoleTypeId == 1 && (job.FactoryId == Constants.branchId || inquiry.BranchId == Constants.branchId))
                        {
                            foreach (var jobdetail in job.JobOrderDetails)
                            {
                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Shop Drawing Completion Date",
                                    Description = "Shop Drawing Completion Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " For Customer " + inquiry.Customer.CustomerName +" at " + jobdetail.ShopDrawingCompletionDate,
                                    Date = jobdetail.ShopDrawingCompletionDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.JobOrder
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Production Completion Date",
                                    Description = "Production Completion Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " For Customer " + inquiry.Customer.CustomerName + " at " + jobdetail.ProductionCompletionDate,
                                    Date = jobdetail.ProductionCompletionDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.JobOrder
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Wooden Work Completion Date",
                                    Description = "Wooden Work Completion Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " For Customer " + inquiry.Customer.CustomerName + " at " + jobdetail.WoodenWorkCompletionDate,
                                    Date = jobdetail.WoodenWorkCompletionDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.JobOrder
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Material Delivery Final Date",
                                    Description = "Material Delivery Final Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " For Customer " + inquiry.Customer.CustomerName + " at " + jobdetail.MaterialDeliveryFinalDate,
                                    Date = jobdetail.MaterialDeliveryFinalDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.JobOrder
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Counter top Fixing Date",
                                    Description = "Counter top Fixing Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " For Customer " + inquiry.Customer.CustomerName + " at " + jobdetail.CountertopFixingDate,
                                    Date = jobdetail.CountertopFixingDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.JobOrder
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Installation Start Date",
                                    Description = "Installation Start Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " For Customer " + inquiry.Customer.CustomerName + " at " + jobdetail.InstallationStartDate,
                                    Date = jobdetail.InstallationStartDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.JobOrder
                                });
                            }
                        }
                    }
                    
                }

                var customers = customerRepository.FindByCondition(x =>
                    x.UserId == user.UserId && x.ContactStatusId == (int)contactStatus.NeedToContact &&
                    x.IsActive == true && x.IsDeleted == false && x.CustomerNextMeetingDate != null &&
                    x.CustomerNextMeetingDate != "").Select(x => new
                    { x.CustomerId, x.CustomerName, x.CustomerContact, x.CustomerNextMeetingDate });
                foreach (var customer in customers)
                {
                    dashborad.calendar.Add(new Calendar
                    {
                        Id = customer.CustomerId,
                        Name = "Meeting with " + customer.CustomerName,
                        Description = "You have to Follow-up with " + customer.CustomerName + " at " +
                                      customer.CustomerNextMeetingDate + " Contact: " + customer.CustomerContact,
                        Date = customer.CustomerNextMeetingDate,
                        OnClickURL = "",
                        EventTypeId = (int)eventType.Customer
                    });
                }

                calendarEventRepository
                    .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId)
                    .ToList().ForEach(x => dashborad.calendar.Add(new Calendar
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
                CalendarEvent calendar = calendarEventRepository.FindByCondition(x =>
                        x.IsActive == true && x.IsDeleted == false &&
                        x.CalendarEventId == calendarEvent.CalendarEventId)
                    .FirstOrDefault();
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
            CalendarEvent calendar = calendarEventRepository
                .FindByCondition(
                    x => x.CalendarEventId == calendarEventId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
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