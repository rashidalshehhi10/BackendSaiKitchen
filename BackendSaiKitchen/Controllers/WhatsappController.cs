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
            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Include(x => x.CustomerUsers.Where(x => x.IsActive == true && x.IsDeleted == false &&
            (x.ContactStatusId == (int)contactStatus.NeedToContact || x.ContactStatusId == (int)contactStatus.NeedToFollowUp))).Include(x => x.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
            .ThenInclude(x => x.BranchRole).ThenInclude(x => x.RoleHeads.Where(x => x.IsActive == true && x.IsDeleted == false)).ToList();
            
            foreach (var user in users)
            {
                int needToContactDelay = user.CustomerUsers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToContact &&
            Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date
            < Helper.Helper.ConvertToDateTime(Helper.Helper.GetDateTime()).Date).Count();
                int needToFollowUpDelay = user.CustomerUsers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp &&
                Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date
                < Helper.Helper.ConvertToDateTime(Helper.Helper.GetDateTime()).Date).Count();

                string report = "Daily Follow-Up Delay Report" + Environment.NewLine + Environment.NewLine 
                    + user.UserName + ":-" + Environment.NewLine
                    + "Need To Contact Delayed:- " + needToContactDelay + Environment.NewLine
                    + "Need To Follow-Up Delayed:- " + needToFollowUpDelay + Environment.NewLine + Environment.NewLine + "Generated by SAI system";

                if (needToContactDelay > 0 || needToFollowUpDelay > 0)
                {
                    foreach (var role in user.UserRoles)
                    {
                        foreach (var head in role.BranchRole.RoleHeads)
                        {
                            var userheads = userRoleRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchRoleId == head.HeadRoleId && x.BranchId == role.BranchId  && x.User.IsActive == true && x.User.IsDeleted == false)
                                .Select(x => new
                                {
                                    x.UserId,
                                    x.User.UserName,
                                    x.User.UserMobile,
                                    x.User.IsNotificationEnabled
                                }).ToList();
                            foreach (var userhead in userheads)
                            {
                                try
                                {
                                    if (userhead.IsNotificationEnabled != null && (bool)userhead.IsNotificationEnabled && userhead.UserMobile != null)
                                    {

                                        await Helper.Helper.SendWhatsappMessage(userhead.UserMobile, "text", report);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Sentry.SentrySdk.CaptureMessage(e.Message);
                                }

                            }
                        }
                    }
                    if (user.IsNotificationEnabled != null && (bool)user.IsNotificationEnabled )
                    {
                        await Helper.Helper.SendWhatsappMessage(user.UserMobile, "text", report);
                    }
                }
                
            }
            
        }

        [HttpGet]
        [Route("[action]")]
        public async Task SendTodayEvents()
        {

            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false )
                .Include(x => x.CalendarEvents.Where(x => x.IsActive == true && x.IsDeleted == false)).ToList();
            foreach (var user in users)
            {
                foreach (var item in user.CalendarEvents.Where(x => Helper.Helper.ConvertToDateTime(x.CalendarEventDate).Date == Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date))
                {
                    string report = "Events Today" + Environment.NewLine + Environment.NewLine + item.CalendarEventName + " at " + item.CalendarEventDate + ":- " + item.CalendarEventDescription;
                    if (user.IsNotificationEnabled != null && (bool)user.IsNotificationEnabled)
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
            var lastmonth = Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).AddDays(-30).Date;
            var customers = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.Branch.IsActive == true && x.Branch.IsDeleted == false).Include(x => x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false)).ToList();
            var inquiry = inquiryRepository.FindByCondition(x => x.IsDeleted == false).Include(x => x.Quotations.Where(x => x.IsActive == true && x.IsDeleted == false)).ToList();
            var quotation = quotationRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.QuotationStatusId == (int)inquiryStatus.contractApproved).ToList();
            foreach (var wayOfContact in wayOfContacts)
            {
                var customerss = customers.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.CreatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()) && x.WayofContactId == wayOfContact.WayOfContactId).Count();
                string way = wayOfContact.WayOfContactName + ":- " + customerss;
                if (customerss > 0)
                {
                    list.Add(way);
                }

            }
            var Added = customers.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.CreatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date).Count();
            var WithInquiry = customers.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.CreatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date && x.Inquiries.Any()).Count();
            var WithoutInquiry = customers.Where(x => Helper.Helper.ConvertToDateTime(x.CreatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.CreatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date && x.Inquiries.Any() == false).Count();
            var escalatedInquiry = inquiry.Where(x => x.IsActive == false && Helper.Helper.ConvertToDateTime(x.EscalationRequestedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.EscalationRequestedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date).Count();
            var newinquiries = inquiry.Where(x => x.IsActive == true && Helper.Helper.ConvertToDateTime(x.InquiryStartDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.InquiryStartDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date).Count();
            var inquiriesinprogress = inquiry.Where(x => x.IsActive == true && (x.Quotations.Any(x => x.QuotationStatusId != (int)inquiryStatus.contractApproved) || x.Quotations.Any() == false)).Count();
            var CompletedInquiries = inquiry.Where(x => x.IsActive == true && x.InquiryEndDate != null && Helper.Helper.ConvertToDateTime(x.InquiryEndDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.InquiryEndDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date).Count();
            var successfulsales = quotation.Where(x => Helper.Helper.ConvertToDateTime(x.UpdatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.UpdatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date).Count();
            var temp = quotation.Where(x => Helper.Helper.ConvertToDateTime(x.UpdatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.UpdatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date).ToList();
            var total = quotation.Where(x => Helper.Helper.ConvertToDateTime(x.UpdatedDate).Date >= lastmonth && Helper.Helper.ConvertToDateTime(x.UpdatedDate).Date <= Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date).Sum(x => double.Parse(x.TotalAmount));
            string report = "Monthly Report (" + lastmonth.ToShortDateString() + " - " + Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).ToShortDateString() + ")" + Environment.NewLine + Environment.NewLine;
            report += "Customers Added:- " + Added + Environment.NewLine;
            foreach (var item in list)
            {
                report += "Customer From " + item.ToString() + Environment.NewLine;
            }
            report += "Customers With Inquiry:- " + WithInquiry + Environment.NewLine;
            report += "Customers Without Inquiry:- " + WithoutInquiry + Environment.NewLine + Environment.NewLine;
            report += "Sales:" + Environment.NewLine +
                "Successful Sales:- " + successfulsales + Environment.NewLine +
                "Total Amount of Sales:- " + total + Environment.NewLine +
                "Completed Job Order:- " + CompletedInquiries + Environment.NewLine +
                "New Inquiries:- " + newinquiries + Environment.NewLine +
                "On-Going Inquiries:- " + inquiriesinprogress + Environment.NewLine +
                "Escalated Inquiries:- " + escalatedInquiry + Environment.NewLine + Environment.NewLine;
            report += "Generated by SAI system";

            response.data = report;

            await Helper.Helper.SendWhatsappMessage("963930104705", "text", report);
           // await Helper.Helper.SendWhatsappMessage("971545552471", "text", report);

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task FollowUpMessage()
        {
            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.CustomerUsers.Where(x =>
                    x.IsActive == true && x.IsDeleted == false && x.CustomerNextMeetingDate != null &&
                    x.CustomerNextMeetingDate != "")).ToList();
            foreach (var user in users)
            {
                foreach (var customer in user.CustomerUsers.Where(x => x.UserId == user.UserId && Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date == Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date))
                {
                    string message = "You have to Follow-up with (" + customer.CustomerName + ") at " +
                                              customer.CustomerNextMeetingDate + " Contact: " + customer.CustomerContact 
                                              +Environment.NewLine + Environment.NewLine + "Notes:" +customer.CustomerNotes
                                              + Environment.NewLine + Environment.NewLine + "Generated by SAI system";
                    if (user.IsNotificationEnabled != null && (bool)user.IsNotificationEnabled)
                    {
                        await Helper.Helper.SendWhatsappMessage(user.UserMobile, "text", message);
                    }
                }
            }
            
        }

        [HttpPost]
        [Route("[action]")]
        public object Test()
        {
            var numbers = userRoleRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchRoleId == 23)
                                .Select(x => new 
                                { 
                                    x.UserId,
                                    x.User.UserMobile,
                                    x.User.IsNotificationEnabled
                                }).ToList();
            response.data = numbers;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task DelayMessage()
        {
            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.CustomerUsers.Where(x =>
                    x.IsActive == true && x.IsDeleted == false && x.CustomerNextMeetingDate != null &&
                    x.CustomerNextMeetingDate != "")).Include(x => x.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                    .ThenInclude(x => x.BranchRole).ThenInclude(x => x.RoleHeads.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ToList();
            foreach (var user in users)
            {
                foreach (var customer in user.CustomerUsers.Where(x => Helper.Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date <
                Helper.Helper.ConvertToDateTime(Helper.Helper.GetDate()).Date))
                {
                    
                    string message = user.UserName + " have been Delayed Follow-up with (" + customer.CustomerName + ") at " +
                                              customer.CustomerNextMeetingDate + " Contact: " + customer.CustomerContact
                                              + Environment.NewLine + Environment.NewLine + "Generated by SAI system";
                    await Helper.Helper.SendWhatsappMessage(user.UserMobile, "text", message);
                    foreach (var role in user.UserRoles)
                    {
                        foreach (var head in role.BranchRole.RoleHeads)
                        {
                            var userheads = userRoleRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchRoleId == head.HeadRoleId)
                                .Select(x => new 
                                { 
                                    x.User.UserMobile,
                                    x.User.IsNotificationEnabled
                                }).ToList();
                            foreach (var userhead in userheads)
                            {
                                if ((bool)userhead.IsNotificationEnabled && userhead.IsNotificationEnabled != null && userhead.UserMobile != null)
                                {

                                    await Helper.Helper.SendWhatsappMessage(userhead.UserMobile, "text", message);
                                }
                            }
                        }
                    }
                }
            }
            
        }
    }
}
