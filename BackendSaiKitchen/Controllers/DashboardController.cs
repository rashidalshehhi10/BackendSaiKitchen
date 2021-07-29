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
            Dashborad dashborad = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false&& x.UserId == Constants.userId)
               .Select(x => new Dashborad
               {
                   CustomerRegistered = x.Customers.Where(y => y.IsActive == true && y.IsDeleted == false).Count(),
                   InquiryCompleted = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false && y.InquiryStatusId == (int)inquiryStatus.inquiryCompleted).Count(),
                   //Scheduledate =x.Inquiries.OrderBy(y => y.InquiryId).LastOrDefault(y => y.IsActive == true && y.IsDeleted ==false).InquiryWorkscopes.Select(z => z.DesignScheduleDate && z.MeasurementScheduleDate && z.InquiryWorkscopeId)//.LastOrDefault(x => x.IsActive ==true && x.IsDeleted==false).InquiryWorkscopes.Where(z => z.IsActive ==true && z.IsDeleted==false).OrderBy(z => z.DesignScheduleDate).SelectMany(z => z.MeasurementScheduleDate)
               }).FirstOrDefault();
            response.data = dashborad;
            return response;
        }
    }
}
