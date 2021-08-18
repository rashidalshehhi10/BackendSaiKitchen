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
                        CustomerRegistered =x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
                        InquiryCompleted =x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId ==(int)inquiryStatus.inquiryCompleted).Count(),
                        InquiryIncomplete = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId != (int)inquiryStatus.inquiryCompleted).Count(),
                        Totalquotations = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationAccepted || x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForCustomerApproval)).Select(y => y.Quotations).Count(),
                        QuotationAccepted =x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationAccepted).Count(),
                        QuotationRejected =x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationRejected).Count(),
                        TotalJoborder = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderCreated).Count(),
                        TotalInquiries=x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                    }).FirstOrDefault();

                var inquiries = inquiryRepository.FindByCondition(x => x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false)
                    .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                    .ThenInclude(y => y.Workscope);
                    
                    dashborad.calendar = new List<Calendar>();
                foreach (var inquiry in inquiries)
                {
                    foreach (var inworkscope in inquiry.InquiryWorkscopes)
                    {
                        if (inworkscope.DesignAssignedTo == Constants.userId || inworkscope.MeasurementAssignedTo == Constants.userId)
                        {
                            dashborad.calendar.Add(new Calendar
                            {
                                InquiryId = (int)inworkscope.InquiryId,
                                InquiryWorkscopeId = inworkscope.InquiryWorkscopeId,
                                MeasurementScheduleDate = inworkscope.MeasurementAssignedTo == Constants.userId ? inworkscope.MeasurementScheduleDate : "",
                                DesignScheduledate = inworkscope.DesignAssignedTo == Constants.userId ? inworkscope.DesignScheduleDate : "",
                                WorkscopeName = inworkscope.Workscope.WorkScopeName,
                                InquiryworkscopeStatusId = (int)inworkscope.InquiryStatusId,
                                InquiryCode = inquiry.InquiryCode
                            });
                        }

                    }
                }
                
                response.data = dashborad;

            }
            else
            {
                response.isError = true;
                response.errorMessage = "User Not Found";
            }
            return response;
        }
    }
}
