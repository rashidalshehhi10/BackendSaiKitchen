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
    public class SizeController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object AddSize(CustomSize _size)
        {
            Size size = new Size();
            size.SizeName = _size.SizeName;
            size.SizeDescription = _size.SizeDescription;
            size.SizeHeight = _size.SizeHeight;
            size.SizeWidth = _size.SizeWidth;
            size.MaterialId = _size.MaterialId;
            sizeRepository.Create(size);
            context.SaveChanges();
            response.data = size;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateSize(CustomSize _size)
        {
            var size = sizeRepository.FindByCondition(x => x.SizeId == _size.SizeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (size != null)
            {
                size.SizeName = _size.SizeName;
                size.SizeDescription = _size.SizeDescription;
                size.SizeHeight = _size.SizeHeight;
                size.SizeWidth = _size.SizeWidth;
                size.MaterialId = _size.MaterialId;
                sizeRepository.Update(size);
                context.SaveChanges();
                response.data = size;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Size Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteSize(int SizeId)
        {
            var size = sizeRepository.FindByCondition(x => x.SizeId == SizeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (size != null)
            {
                size.IsActive = false;
                sizeRepository.Update(size);
                context.SaveChanges();
                response.data = size;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Size Not Found";
            }
            return response;
        }

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
