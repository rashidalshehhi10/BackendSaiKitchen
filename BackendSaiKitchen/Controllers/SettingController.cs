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
    public class SettingController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetSetting()
        {
            var setting = settingRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (setting != null)
            {
                response.data = setting;

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Setting Not Found";
            }
            return response;

        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateSetting(Setting setting)
        {
            var Setting = settingRepository.FindByCondition(x => x.SettingId == setting.SettingId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();

            if (Setting != null)
            {
                Setting.SettingApprovalDelay = setting.SettingApprovalDelay;
                Setting.SettingAssigneeDelay = setting.SettingAssigneeDelay;
                Setting.SettingCustomerContactDelay = setting.SettingCustomerContactDelay;
                Setting.SettingDescription = setting.SettingDescription;
                Setting.SettingDesignDelay = setting.SettingDesignDelay;
                Setting.SettingMaintenanceAfterMonth = setting.SettingMaintenanceAfterMonth;
                Setting.SettingMeasurementDelay = setting.SettingMeasurementDelay;
                Setting.SettingNoActionDelayFromCustomer = setting.SettingNoActionDelayFromCustomer;
                Setting.SettingQuotationDelay = setting.SettingQuotationDelay;
                Setting.UpdatedBy = Constants.userId;
                Setting.UpdatedDate = Helper.Helper.GetDate();

                settingRepository.Update(Setting);
                response.data = "Setting Upadte Successfully";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Setting Not Found";
            }

            return response;
        }
    }
}
