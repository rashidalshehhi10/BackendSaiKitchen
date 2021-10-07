using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    public class FeesController : BaseController
    {

        [AuthFilter((int)permission.ManageFees, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetAllFees()
        {
            return FeesRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);

        }

        //[AuthFilter((int)permission.ManageFees, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public object GetFeesById(int feesId)
        {
            response.data = FeesRepository.FindByCondition(x => x.FeesId == feesId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (response.data == null)
            {
                response.isError = true;
                response.errorMessage = "Fees doesn't Exist";
            }
            return response;
        }

        [AuthFilter((int)permission.ManageFees, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public object AddFees(Fee fee)
        {
            if (fee.FeesId == 0)
            {
                FeesRepository.Create(fee);
                context.SaveChanges();
            }
            else
            {
                Fee oldFees = FeesRepository.FindByCondition(x => x.FeesId == fee.FeesId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                if (oldFees != null)
                {
                    oldFees.FeesAmount = fee.FeesAmount;
                    oldFees.FeesDescription = fee.FeesDescription;
                    oldFees.FeesName = fee.FeesName;
                    FeesRepository.Update(oldFees);
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

        [AuthFilter((int)permission.ManageFees, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object EditFees(Fee fee)
        {
            Fee oldFees = FeesRepository.FindByCondition(x => x.FeesId == fee.FeesId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldFees != null)
            {
                oldFees.FeesAmount = fee.FeesAmount;
                oldFees.FeesDescription = fee.FeesDescription;
                oldFees.FeesName = fee.FeesName;
                FeesRepository.Update(oldFees);
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

        [AuthFilter((int)permission.ManageFees, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public object DeleteFees(int feesId)
        {
            Fee oldFees = FeesRepository.FindByCondition(x => x.FeesId == feesId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldFees != null)
            {
                FeesRepository.Delete(oldFees);
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
