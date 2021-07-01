using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    public class FeesController : BaseController
    {


        [HttpPost]
        [Route("[action]")]
        public object GetAllFees()
        {
            return feesRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);

        }
        [HttpGet]
        [Route("[action]")]
        public object GetFeesById(int feesId)
        {
            response.data = feesRepository.FindByCondition(x => x.FeesId == feesId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (response.data == null)
            {
                response.isError = true;
                response.errorMessage = "Fees doesn't Exist";
            }
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object AddFees(Fee fee)
        {
            if (fee.FeesId == 0)
            {
                feesRepository.Create(fee);
                context.SaveChanges();
            }
            else
            {
                Fee oldFees = feesRepository.FindByCondition(x => x.FeesId == fee.FeesId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                if (oldFees != null)
                {
                    oldFees.FeesAmount = fee.FeesAmount;
                    oldFees.FeesDescription = fee.FeesDescription;
                    oldFees.FeesName = fee.FeesName;
                    feesRepository.Update(oldFees);
                    context.SaveChanges();
                    response.data = oldFees;
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "Fees doesn't exist";
                }
            }
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object EditFees(Fee fee)
        {
            Fee oldFees = feesRepository.FindByCondition(x => x.FeesId == fee.FeesId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldFees != null)
            {
                oldFees.FeesAmount = fee.FeesAmount;
                oldFees.FeesDescription = fee.FeesDescription;
                oldFees.FeesName = fee.FeesName;
                feesRepository.Update(oldFees);
                context.SaveChanges();
                response.data = oldFees;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Fees doesnt exist";
            }
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object DeleteFees(int feesId)
        {
            Fee oldFees = feesRepository.FindByCondition(x => x.FeesId == feesId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldFees != null)
            {
                feesRepository.Delete(oldFees);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Fees doesn't exist";
            }
            return response;
        }

    }
}
