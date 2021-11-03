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
    public class BrandController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object CreateBrand(Brand brand)
        {
            Brand _brand = new Brand();
            if (brand != null)
            {
                _brand.BrandDescription = brand.BrandDescription;
                _brand.BrandName = brand.BrandName;
                _brand.CreatedBy = Constants.userId;
                _brand.CreatedDate = Helper.Helper.GetDateTime();
                _brand.IsActive = true;
                _brand.IsDeleted = false;
                brandRepository.Create(_brand);
                context.SaveChanges();
                response.data = "Brand Created Successfully";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Please enter All the Details";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetBrandById(int brandId)
        {
            var brand = brandRepository.FindByCondition(x => x.BrandId == brandId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (brand != null)
            {
                response.data = brand;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Brand Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateBrand(Brand brand)
        {
            var _brand = brandRepository.FindByCondition(x => x.BrandId == brand.BrandId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (_brand != null)
            {
                _brand.BrandName = brand.BrandName;
                _brand.BrandDescription = brand.BrandDescription;
                _brand.UpdatedBy = Constants.userId;
                _brand.UpdatedDate = Helper.Helper.GetDate();
                brandRepository.Update(_brand);
                context.SaveChanges();
                response.data = "Brand Updated Successfully";

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Brand Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteBrand(int brandId)
        {
            var brand = brandRepository.FindByCondition(x => x.BrandId == brandId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (brand != null)
            {
                brand.IsActive = false;
                brandRepository.Update(brand);
                context.SaveChanges();
                response.data = "Brand Deleted";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Brand Not Found";
            }
            return response;
        }
    }
}
