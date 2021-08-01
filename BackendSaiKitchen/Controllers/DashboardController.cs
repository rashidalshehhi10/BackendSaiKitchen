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
            Dashborad dashborad = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId)
               .Select(x => new Dashborad
               {
                   CustomerRegistered = x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
                   InquiryCompleted = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
                   InquiryCode = x.Inquiries.OrderBy(y => y.InquiryId).Where(y => y.IsActive == true && y.IsDeleted == false).Select(y => y.InquiryCode).ToList(),
                   MeasurementScheduleDate = inquiryWorkscopeRepository.FindByCondition(y => x.Inquiries.Any(z => z.InquiryId == y.InquiryId && y.IsActive == true && y.IsDeleted == false)).ToList(),
                   //DesignScheduledate = x.Inquiries.OrderBy(y => y.InquiryId).FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.DesignScheduleDate).ToList(),
                   WorkscopeName = x.Inquiries.OrderBy(y => y.InquiryId).FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.Workscope.WorkScopeName).ToList()
               }).FirstOrDefault();
            try
            {
                dashborad.calendar = new List<Calendar>();
                for (int i = 0; i < dashborad.InquiryCode.Count; i++)
                {
                    dashborad.calendar.Add(new Calendar { DesignScheduledate = dashborad.MeasurementScheduleDate[i].DesignScheduleDate, MeasurementScheduleDate = dashborad.MeasurementScheduleDate[i].MeasurementScheduleDate, InquiryCode = dashborad.InquiryCode[i], WorkscopeName = dashborad.WorkscopeName[i] });
                }
            }
            catch (Exception ex)
            {

                Serilog.Log.Error(ex.Message);
            }
            response.data = dashborad;
            return response;
        }
    }
}
