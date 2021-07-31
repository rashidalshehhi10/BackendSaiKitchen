using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using Microsoft.AspNetCore.Mvc;
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
        public object ViewDashborad()
        {
            Dashborad dashborad = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserId == Constants.userId)
               .Select(x => new Dashborad
               {
                   CustomerRegistered = x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
                   InquiryCompleted = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
                   InquiryCode = x.Inquiries.OrderBy(y => y.InquiryId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.Inquiry.InquiryCode).ToList(),
                   MeasurementScheduleDate = x.Inquiries.OrderBy(y => y.InquiryId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.MeasurementScheduleDate).ToList(),
                   DesignScheduledate = x.Inquiries.OrderBy(y => y.InquiryId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.DesignScheduleDate).ToList(),
                   WorkscopeName = x.Inquiries.OrderBy(y => y.InquiryId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).InquiryWorkscopes.Select(z => z.Workscope.WorkScopeName).ToList()
               }).FirstOrDefault();
            try
            {
                dashborad.calendar = new List<Calendar>();
                for (int i = 0; i < dashborad.InquiryCode.Count; i++)
                {
                    dashborad.calendar.Add(new Calendar { DesignScheduledate = dashborad.DesignScheduledate[i], MeasurementScheduleDate = dashborad.MeasurementScheduleDate[i], InquiryCode = dashborad.InquiryCode[i], WorkscopeName = dashborad.WorkscopeName[i] });
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
