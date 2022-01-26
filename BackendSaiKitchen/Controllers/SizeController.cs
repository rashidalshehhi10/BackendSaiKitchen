using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class SizeController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetAllSize()
        {
            var size = sizeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            response.data = size;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetSizeByMaterialId(int MaterialId)
        {
            var size = sizeRepository.FindByCondition(x => x.MaterialId == MaterialId && x.IsActive == true && x.IsDeleted == false).ToList();
            response.data = size;
            return response;
        }

    }
}
