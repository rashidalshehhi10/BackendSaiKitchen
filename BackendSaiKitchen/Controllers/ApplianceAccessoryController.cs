using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class ApplianceAccessoryController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object CreateApplianceAccessory(ApplianceAccessory accessory)
        {
            ApplianceAccessory _accessory = new ApplianceAccessory();
            if (accessory != null)
            {
                _accessory.ApplianceAccessoryName = accessory.ApplianceAccessoryName;
                _accessory.ApplianceAccesoryDescription = accessory.ApplianceAccesoryDescription;
                _accessory.ApplianceAccessoryPrice = accessory.ApplianceAccessoryPrice;
                _accessory.ApplianceAccessoryTypeId = accessory.ApplianceAccessoryTypeId;
                _accessory.BrandId = accessory.BrandId;
                _accessory.UnitOfMeasurementId = accessory.UnitOfMeasurementId;
                _accessory.CreatedBy = Constants.userId;
                _accessory.CreatedDate = Helper.Helper.GetDateTime();
                _accessory.IsActive = true;
                _accessory.IsDeleted = false;
                applianceAccessoryRepository.Create(_accessory);
                context.SaveChanges();
                response.data = "Appliance And Accessory Created ";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "there is no data";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllApplianceAccessory()
        {
            var applianceAccessory = applianceAccessoryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            if (applianceAccessory != null)
            {
                response.data = applianceAccessory;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "There Is no Appliance and Accessory";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetApplianceAccessoryById(int applianceAccessoryId)
        {
            var applianceAccessory = applianceAccessoryRepository.FindByCondition(x => x.ApplianceAccessoryId == applianceAccessoryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (applianceAccessory != null)
            {
                response.data = applianceAccessory;

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Appliance And Accessory Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateApplianceAccessoryById(ApplianceAccessory accessory)
        {
            var _accessory = applianceAccessoryRepository.FindByCondition(x => x.ApplianceAccessoryId == accessory.ApplianceAccessoryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (accessory != null)
            {
                _accessory.ApplianceAccessoryName = accessory.ApplianceAccessoryName;
                _accessory.ApplianceAccesoryDescription = accessory.ApplianceAccesoryDescription;
                _accessory.ApplianceAccessoryPrice = accessory.ApplianceAccessoryPrice;
                _accessory.ApplianceAccessoryTypeId = accessory.ApplianceAccessoryTypeId;
                _accessory.BrandId = accessory.BrandId;
                _accessory.UnitOfMeasurementId = accessory.UnitOfMeasurementId;
                _accessory.UpdatedBy = Constants.userId;
                _accessory.UpdatedDate = Helper.Helper.GetDateTime();
                applianceAccessoryRepository.Update(_accessory);
                context.SaveChanges();
                response.data = "Appliance And Accessory Updated Successfully ";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Appliance And Accessory Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteApplianceAccessory(int applianceAccessoryId)
        {
            var accessory = applianceAccessoryRepository.FindByCondition(x => x.ApplianceAccessoryId == applianceAccessoryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (accessory != null)
            {
                accessory.IsActive = false;
                applianceAccessoryRepository.Update(accessory);
                response.data = "Appliance And Accessory Deleted";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Appliance And Accessory Not Found";
            }
            return response;
        }
    }
}
