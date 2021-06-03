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
                if (measurement.InquiryWorkscope.MeasurementAssignedTo!=null)
                {
                    sendNotificationToOneUser("wait here we have to send confirmation to head", false, null, null, (int)measurement.MeasurementApprovedByNavigation.UserId, (int)measurement.InquiryWorkscope.Inquiry.BranchId, (int)notificationCategory.Measurement);
                    List<int?> roletypeId = new List<int?>();
                    roletypeId.Add((int)roleType.Manager);
                    sendNotificationToHead(measurement.MeasurementApprovedByNavigation.UserName + "added new measurement", true, null, null, roletypeId, (int)measurement.InquiryWorkscope.Inquiry.BranchId, (int)notificationCategory.Other);
                    if ( notification.NotificationAcceptAction != null)
                    {
                        response.data = measurement;
                    }
                    else
                    {
                        measurement.InquiryWorkscope.InquiryStatusId = 2;
                        response.isError = true;
                        response.errorMessage = "Kindly upload measurement file";
                    }
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


    }
}
