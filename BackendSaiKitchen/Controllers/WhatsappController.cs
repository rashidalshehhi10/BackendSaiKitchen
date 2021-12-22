﻿using BackendSaiKitchen.Helper;
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
    public class WhatsappController : BaseController
    {
        [HttpGet]
        [Route("[action]")]
        public async Task DelayWhatsApp()
        {
            var Customers = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId && (x.ContactStatusId == (int)contactStatus.NeedToContact || x.ContactStatusId == (int)contactStatus.NeedToFollowUp)).ToList();
            int needToContactDelay = Customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToContact && Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.Helper.ConvertToDateTime(Helper.Helper.GetDateTime()).Date).Count();
            int needToFollowUpDelay = Customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.Helper.ConvertToDateTime(Helper.Helper.GetDateTime()).Date).Count();
            
            string report = "Daily Follow-Up Report" + Environment.NewLine + Environment.NewLine
                + "Need To Contact Delayed:- " + needToContactDelay + Environment.NewLine
                + "Need To Follow-Up Delayed:- " + needToFollowUpDelay + Environment.NewLine + Environment.NewLine + "Generated by SAI system";

            await Helper.Helper.SendWhatsappMessage("963930104705", "text", report);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task SendTodayEvents()
        {
            //var event1 = calendarEventRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Select(x => new
            //{
            //    x.CalendarEventDate,
            //    x.CalendarEventDescription,
            //    x.CalendarEventName,
            //    x.UserId,
            //    x.EventType.EventTypeName
            //}).ToList();
            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false &&
            x.UserRoles.Any(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId))
                .Include(x => x.CalendarEvents.Where(x => x.IsActive == true && x.IsDeleted == false)).ToList();
            foreach (var user in users)
            {
                foreach (var item in user.CalendarEvents.Where(x => Helper.Helper.ConvertToDateTime(x.CalendarEventDate).Date == Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date))
                {
                    string report = "Events Today" + Environment.NewLine + Environment.NewLine + item.CalendarEventName + " at " + item.CalendarEventDate + ":- " + item.CalendarEventDescription;
                    if ((bool)user.IsNotificationEnabled)
                    {
                        await Helper.Helper.SendWhatsappMessage(user.UserMobile, "text", report);
                    }
                }
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<object> MonthlyCustomerReport()
        {
            var wayOfContacts = wayOfContactRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            List<object> list = new List<object>();
            var lastmonth = Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).AddDays(-30);
            var customers = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId && x.Branch.IsActive == true && x.Branch.IsDeleted == false).Include(x => x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false)).ToList();
            foreach (var wayOfContact in wayOfContacts)
            {
                var customerss = customers.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.CreatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()) && x.WayofContactId == wayOfContact.WayOfContactId).Count();
                string way = wayOfContact.WayOfContactName + ":- " + customerss;
                if (customerss > 0)
                {
                    list.Add(way);
                }

            }
            var Added = customers.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.CreatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate())).Count();
            var WithInquiry = customers.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.CreatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()) && x.Inquiries.Any()).Count();
            var WithoutInquiry = customers.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.CreatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()) && x.Inquiries.Any() == false).Count();
            string report = "Monthly Customer Report (" + lastmonth.ToShortDateString() + " - " + Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).ToShortDateString() + ")" + Environment.NewLine + Environment.NewLine;
            report += "Customers Added:- " + Added + Environment.NewLine;
            foreach (var item in list)
            {
                report += "Customer From " + item.ToString() + Environment.NewLine;
            }
            report += "Customers With Inquiry:- " + WithInquiry + Environment.NewLine;
            report += "Customers Without Inquiry:- " + WithoutInquiry + Environment.NewLine + Environment.NewLine;
            report += "Generated by SAI system";

            response.data = report;

            await Helper.Helper.SendWhatsappMessage("963930104705", "text", report);

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task FollowUpMessage()
        {
            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false &&
            x.UserRoles.Any(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId))
                .Include(x => x.CustomerUsers.Where(x =>
                    x.IsActive == true && x.IsDeleted == false && x.CustomerNextMeetingDate != null &&
                    x.CustomerNextMeetingDate != "")).ToList();
            foreach (var user in users)
            {
                foreach (var customer in user.CustomerUsers.Where(x => Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date == Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date))
                {
                    string message = "You have to Follow-up with (" + customer.CustomerName + ") at " +
                                              customer.CustomerNextMeetingDate + " Contact: " + customer.CustomerContact
                                              + Environment.NewLine + Environment.NewLine + "Generated by SAI system";
                    if ((bool)user.IsNotificationEnabled)
                    {
                        await Helper.Helper.SendWhatsappMessage(user.UserMobile, "text", message);
                    }
                }
            }
            
        }

        [HttpPost]
        [Route("[action]")]
        public async Task DelayMessage()
        {
            var customers = customerRepository.FindByCondition(x =>
                    x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId && (x.ContactStatusId == (int)contactStatus.NeedToContact || x.ContactStatusId == (int)contactStatus.NeedToFollowUp)).Select(x => new
                    { x.CustomerId, x.CustomerName, x.CustomerContact, x.CustomerNextMeetingDate, x.CustomerAssignedTo }).ToList();
            foreach (var customer in customers.Where(x => Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date))
            {
                var sendto = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == customer.CustomerAssignedTo).Select(x => new 
                {
                    x.UserMobile,
                    x.IsNotificationEnabled,
                    x.UserName
                }).FirstOrDefault();
                string message = sendto.UserName+" have been Delayed Follow-up with (" + customer.CustomerName + ") at " +
                                          customer.CustomerNextMeetingDate + " Contact: " + customer.CustomerContact
                                          + Environment.NewLine + Environment.NewLine + "Generated by SAI system";
                await Helper.Helper.SendWhatsappMessage(sendto.UserMobile, "text", message);
            }
        }
    }
}
