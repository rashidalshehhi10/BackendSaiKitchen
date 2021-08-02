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
                .FirstOrDefault();

            Dashborad dashborad = new Dashborad()
            {
                CustomerRegistered = user.Customers.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                InquiryCompleted =user.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count()
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
