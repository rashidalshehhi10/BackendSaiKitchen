using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class MeasuementController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object AddMeasrmuent(Measurement measurement)
        {

            if (measurement.MeasurementId == 0)
            {
                
                Measurement oldMeasurement = measurementRepository.FindByCondition(x => x.FeesId == measurement.FeesId && x.InquiryWorkscopeId == measurement.InquiryWorkscopeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                if (oldMeasurement==null)
                {
                    measurementRepository.Create(measurement);
                    context.SaveChanges();
                    response.isError = false;
                    response.errorMessage = "Success";
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "Measurment already Exist";
                }

            }
            else
            {
                measurementRepository.Update(measurement);
                context.SaveChanges();
                response.isError = false;
                response.errorMessage = "Success";
            }

            return response;
        }

        
    }
}
