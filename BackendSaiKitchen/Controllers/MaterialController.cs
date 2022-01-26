using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class MaterialController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetAllMaterial()
        {
            var Materials = materialRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            response.data = Materials;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetMaterialByWorkscopeId(int WorkscopeId)
        {
            var Materials = materialRepository.FindByCondition(x => x.WorkscopeId == WorkscopeId && x.IsActive == true && x.IsDeleted == false).ToList();
            response.data = Materials;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetMaterailById(int MaterailId)
        {
            var Materail = materialRepository.FindByCondition(x => x.MaterialId == MaterailId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (Materail != null)
            {
                response.data = Materail;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Materail Not Found";
            }
            return response;
        }
    }
}
