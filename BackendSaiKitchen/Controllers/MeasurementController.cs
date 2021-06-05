using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
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
            
            if (measurement.Files.Count > 0)
            {
                Guid obj = Guid.NewGuid();
                using (var stream = new MemoryStream(measurement.Files.FirstOrDefault().FileImage))
                {
                    FileStream file = new FileStream(@"Assets/Images/" + obj.ToString(), FileMode.Create, FileAccess.Write);
                    stream.WriteTo(file);
                    file.Close();
                    stream.Close();
                }

                measurementRepository.Create(measurement);

                //TODO:
                // fileRepository(new File(){ measurementId = measurement.measurementId }

                if (measurement.InquiryWorkscope.MeasurementAssignedTo != null)
                {
                    sendNotificationToOneUser("wait here we have to send confirmation to head", false, null, null, (int)measurement.MeasurementApprovedByNavigation.UserId, (int)measurement.InquiryWorkscope.Inquiry.BranchId, (int)notificationCategory.Measurement);
                    
                    List<int?> roletypeId = new List<int?>();
                    
                    roletypeId.Add((int)roleType.Manager);
                    
                    sendNotificationToHead(
                        measurement.MeasurementTakenBy + " added new measurement",
                        true,
                        Url.ActionLink("Accept", "MeasuementController", new { id = measurement.MeasurementId }),
                        Url.ActionLink("Decline", "MeasuementController", new { id = measurement.MeasurementId }),
                        roletypeId,
                        (int)measurement.InquiryWorkscope.Inquiry.BranchId,
                        (int)notificationCategory.Measurement);
       
                    response.data = measurement;
                    return response;
                }
                
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Kindly upload measurement file";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Accept(int id)
        {
            var measurmenet = measurementRepository.FindByCondition(m => m.MeasurementId == id).FirstOrDefault();
           // measurmenet.InquiryWorkscope.DesignAssignedTo
            //measurmenet.InquiryWorkscope.DesignScheduleDate
            measurementRepository.Update(measurmenet);
            context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Decline(int id)
        {

            var measurement = measurementRepository.FindByCondition(m => m.MeasurementId == id).FirstOrDefault();
            measurement.InquiryWorkscope.InquiryStatusId = 7;
            measurementRepository.Update(measurement);
            context.SaveChanges();
           
            return Ok();
        }

    }
}
