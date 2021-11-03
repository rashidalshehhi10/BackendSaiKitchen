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
    public class UnitOfMeasurementController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object CreateUnitOfMeasurement(UnitOfMeasurement unit)
        {
            UnitOfMeasurement _unit = new UnitOfMeasurement();
            if (unit != null)
            {
                _unit.UnitOfMeasurementName = unit.UnitOfMeasurementName;
                _unit.UnitOfMeasurementDescription = unit.UnitOfMeasurementDescription;
                _unit.CreatedBy = Constants.userId;
                _unit.CreatedDate = Helper.Helper.GetDateTime();
                _unit.IsActive = true;
                _unit.IsDeleted = false;
                unitOfMeasurementRepository.Create(_unit);
                context.SaveChanges();
                response.data = "Unit Of Measurement Created";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Not Created";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateUnitOfMeasurement(UnitOfMeasurement unit)
        {
            var _unit = unitOfMeasurementRepository.FindByCondition(x => x.UnitOfMeasurementId = unit.UnitOfMeasurementId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (_unit != null)
            {
                _unit.UnitOfMeasurementName = unit.UnitOfMeasurementName;
                _unit.UnitOfMeasurementDescription = unit.UnitOfMeasurementDescription;
                _unit.UpdatedBy = Constants.userId;
                _unit.UpdatedDate = Helper.Helper.GetDateTime();
                unitOfMeasurementRepository.Update(_unit);
                context.SaveChanges();
                response.data = "Unit Of Measurement Updated";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Unit Of Measurement Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllUnitOfMeasurement()
        {
            var units = unitOfMeasurementRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            response.data = units;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetUnitOfMeasurementById(int unitOfMeasurementId)
        {
            var unit = unitOfMeasurementRepository.FindByCondition(x => x.UnitOfMeasurementId == unitOfMeasurementId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (unit != null)
            {
                response.data = unit;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Unit Of Measurement Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteUnitOfMeasurement(int unitOfMeasurementId)
        {
            var unit = unitOfMeasurementRepository.FindByCondition(x => x.UnitOfMeasurementId == unitOfMeasurementId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (unit != null)
            {
                unit.IsActive = false;
                unitOfMeasurementRepository.Update(unit);
                response.data = "Unit Of Measurement Deleted";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Unit Of Measurement Not Found";
            }
            return response;
        }
    }
}
