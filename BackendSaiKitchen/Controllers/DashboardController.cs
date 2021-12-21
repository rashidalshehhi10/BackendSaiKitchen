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
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderCompleted).Count(),
                        QuotationRejected = x.Inquiries.Where(x =>
                            x.IsActive == true && x.IsDeleted == false &&
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress).Count(),
                        TotalJoborder = x.Inquiries.Where(x =>
                            x.IsActive == true && x.IsDeleted == false && (
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderApproved ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditPending ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditRejected ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderCompleted ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderCreated ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderCustomerRequestReschedule ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayed ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayRequested ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderDesignTeamDelay ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryDelayed ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesDelayed ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesPending ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderProcurementDelayed ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderReadyForInstallation ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderRejected ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleApproved ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested ||
                            x.InquiryStatusId == (int)inquiryStatus.jobOrderWaitingForApproval)).Count(),
                        ContactedWithInquiry = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == (int)contactStatus.Contacted && 
                        x.Inquiries.Any(y => y.IsActive == true && y.IsDeleted == false)).Count(),
                        ContactedWithoutinquiry = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == (int)contactStatus.Contacted &&
                        x.Inquiries.Any(y => y.IsActive == true && y.IsDeleted == false) == false).Count(),
                        NeedToFollowUp = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == (int)contactStatus.NeedToFollowUp).Count(),
                        NeedToFollowUpWithInquiry = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false)).Count(),
                        NeedToFollowUpWithoutInquiry = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false).Count(),
                        NotResponding = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == (int)contactStatus.NotResponing).Count(),
                        NotRespondingWithInquiry = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false)).Count(),
                        NotRespondingWithoutInquiry = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false && x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false).Count(),
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
                                                  inquiry.CustomerId + "" + inquiry.InquiryId + " (" + inquiry.Customer.CustomerName+")",
                                    Date = inworkscope.MeasurementScheduleDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.MeasurementAssign
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
                                                  "" + inquiry.InquiryId + " (" + inquiry.Customer.CustomerName + ")",
                                    Date = inworkscope.DesignScheduleDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.DesignAssign
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
                                              "" + inquiry.InquiryId + " (" + inquiry.Customer.CustomerName + ")" + " at " + jobdetail.ShopDrawingCompletionDate,
                                    Date = jobdetail.ShopDrawingCompletionDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.ShopDrawing
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Production Completion Date",
                                    Description = "Production Completion Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " (" + inquiry.Customer.CustomerName + ")" + " at " + jobdetail.ProductionCompletionDate,
                                    Date = jobdetail.ProductionCompletionDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.Production
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Wooden Work Completion Date",
                                    Description = "Wooden Work Completion Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " (" + inquiry.Customer.CustomerName + ")" + " at " + jobdetail.WoodenWorkCompletionDate,
                                    Date = jobdetail.WoodenWorkCompletionDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.WoodenWork
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Material Delivery Final Date",
                                    Description = "Material Delivery Final Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " (" + inquiry.Customer.CustomerName + ")" + " at " + jobdetail.MaterialDeliveryFinalDate,
                                    Date = jobdetail.MaterialDeliveryFinalDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.JobOrder
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Counter top Fixing Date",
                                    Description = "Counter top Fixing Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " (" + inquiry.Customer.CustomerName + ")" + " at " + jobdetail.CountertopFixingDate,
                                    Date = jobdetail.CountertopFixingDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.CounterTopFixing
                                });

                                dashborad.calendar.Add(new Calendar
                                {
                                    Id = job.JobOrderId,
                                    Name = "Installation Start Date",
                                    Description = "Installation Start Date of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId +
                                              "" + inquiry.InquiryId + " (" + inquiry.Customer.CustomerName + ")" + " at " + jobdetail.InstallationStartDate,
                                    Date = jobdetail.InstallationStartDate,
                                    OnClickURL = "",
                                    EventTypeId = (int)eventType.Installation
                                });
                            }
                        }
                    }
                    
                }

                var customers = customerRepository.FindByCondition(x =>
                    x.UserId == user.UserId &&
                    x.IsActive == true && x.IsDeleted == false && x.CustomerNextMeetingDate != null &&
                    x.CustomerNextMeetingDate != "").Select(x => new
                    { x.CustomerId, x.CustomerName, x.CustomerContact, x.CustomerNextMeetingDate });
                foreach (var customer in customers)
                {
                    dashborad.calendar.Add(new Calendar
                    {
                        Id = customer.CustomerId,
                        Name = "Meeting with " + customer.CustomerName,
                        Description = "You have to Follow-up with (" + customer.CustomerName + ") at " +
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
        public async Task FollowUpMessage()
        {
            var customers = customerRepository.FindByCondition(x =>
                    x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId && x.CustomerNextMeetingDate != null &&
                    x.CustomerNextMeetingDate != "").Select(x => new
                    { x.CustomerId, x.CustomerName, x.CustomerContact, x.CustomerNextMeetingDate ,x.CustomerAssignedTo }).ToList();
            foreach (var customer in customers.Where(x => Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date == Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date))
            {
                var sendto = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == customer.CustomerAssignedTo).Select(x => x.UserMobile).FirstOrDefault();
                string message = "You have to Follow-up with (" + customer.CustomerName + ") at " +
                                          customer.CustomerNextMeetingDate + " Contact: " + customer.CustomerContact
                                          + Environment.NewLine + Environment.NewLine + "Generated by SAI system";
                await Helper.Helper.SendWhatsappMessage(sendto, "text", message);
            }
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