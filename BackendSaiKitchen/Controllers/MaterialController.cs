using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Models;
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
        public object AddMaterial(CustomMaterial _material)
        {
            Material material = new Material();
            material.MaterialName = _material.MaterialName;
            material.MaterialDescription = _material.MaterialDescription;
            material.WorkscopeId = _material.WorkscopeId;
            materialRepository.Create(material);
            context.SaveChanges();
            response.data = material;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateMaterial(CustomMaterial _material)
        {
            var material = materialRepository.FindByCondition(x => x.MaterialId == _material.MaterialId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (material != null)
            {
                material.MaterialName = _material.MaterialName;
                material.MaterialDescription = _material.MaterialDescription;
                material.WorkscopeId = _material.WorkscopeId;
                materialRepository.Update(material);
                context.SaveChanges();
                response.data = material;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Material Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteMaterial(int MaterialId)
        {
            var material = materialRepository.FindByCondition(x => x.MaterialId == MaterialId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (material != null)
            {
                material.IsActive = false;
                materialRepository.Update(material);
                context.SaveChanges();
                response.data = material;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Material Not Found";
            }
            return response;
        }

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
