using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class ApplianceAccessoryTypeController : BaseController
    {
        [HttpGet]
        [Route("[action]")]
        public object GetAllApplianceAccessoryType()
        {
            response.data = applianceAccessoryTypeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }
    }
}
