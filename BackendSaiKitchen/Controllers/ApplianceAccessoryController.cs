using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class ApplianceAccessoryController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object CreateApplianceAccessory(ApplianceAccessory accessory)
        {
            ApplianceAccessory _accessory = new ApplianceAccessory();
            if (accessory != null)
            {
                _accessory.ApplianceAccessoryName = accessory.ApplianceAccessoryName;
                //_accessory.Skucode = accessory.Skucode;
                _accessory.ApplianceAccesoryDescription = accessory.ApplianceAccesoryDescription;
                _accessory.ApplianceAccessoryPrice = accessory.ApplianceAccessoryPrice;
                _accessory.ApplianceAccessoryTypeId = accessory.ApplianceAccessoryTypeId;
                _accessory.BrandId = accessory.BrandId;
                _accessory.UnitOfMeasurementId = accessory.UnitOfMeasurementId;
                //_accessory.ApplianceAccessoryImgUrl = accessory.ApplianceAccessoryImgUrl;
                _accessory.CreatedBy = Constants.userId;
                _accessory.CreatedDate = Helper.Helper.GetDateTime();
                _accessory.IsActive = true;
                _accessory.IsDeleted = false;
                applianceAccessoryRepository.Create(_accessory);
                context.SaveChanges();
                response.data = "Appliance And Accessory Created ";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "there is no data";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllApplianceAccessory()
        {
            var applianceAccessory = applianceAccessoryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
               .Select(x => new
               {
                   ApplianceAccessoryId = x.ApplianceAccessoryId,
                   SKUCode = x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.ItemId).Select(x => x.Skucode).ToList(),
                   ColorName = x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.ItemId).Select(x => x.Color.ColorName).ToList(),
                   ColorId = x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.ItemId).Select(x => x.ColorId).ToList(),
                   ItemColorId = x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.ItemId).Select(x => x.ItemColorId).ToList(),
                   ApplianceAccesoryDescription = x.ApplianceAccesoryDescription,
                   ApplianceAccessoryImgUrl = x.ItemColors.FirstOrDefault().ImageUrl,
                   ApplianceAccessoryName = x.ApplianceAccessoryName,
                   ApplianceAccessoryPrice = x.ApplianceAccessoryPrice,
                   ApplianceAccessoryTypeId = x.ApplianceAccessoryTypeId,
                   ApplianceAccessoryTypeName = x.ApplianceAccessoryType.ApplianceAccessoryTypeName,
                   BrandId = x.BrandId,
                   BrandName = x.Brand.BrandName,
                   UnitOfMeasurementId = x.UnitOfMeasurementId,
                   UnitOfMeasurementName = x.UnitOfMeasurement.UnitOfMeasurementName,
               }).ToList();
                
            if (applianceAccessory != null)
            {
                response.data = applianceAccessory;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "There Is no Appliance and Accessory";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetApplianceAccessoryById(int applianceAccessoryId)
        {
            var applianceAccessory = applianceAccessoryRepository.FindByCondition(x => x.ApplianceAccessoryId == applianceAccessoryId && x.IsActive == true && x.IsDeleted == false)
                .Select(x => new 
                {
                    ApplianceAccessoryId = x.ApplianceAccessoryId,
                    SKUCode = x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.ItemId).Select(x => x.Skucode).ToList(),
                    ColorName = x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.ItemId).Select(x => x.Color.ColorName).ToList(),
                    ColorId = x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.ItemId).Select(x => x.ColorId).ToList(),
                    ItemColorId = x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.ItemId).Select(x => x.ItemColorId).ToList(),
                    ApplianceAccesoryDescription = x.ApplianceAccesoryDescription,
                    ApplianceAccessoryImgUrl = x.ItemColors.FirstOrDefault().ImageUrl,
                    ApplianceAccessoryName = x.ApplianceAccessoryName,
                    ApplianceAccessoryPrice = x.ApplianceAccessoryPrice,
                    ApplianceAccessoryTypeId = x.ApplianceAccessoryTypeId,
                    ApplianceAccessoryTypeName = x.ApplianceAccessoryType.ApplianceAccessoryTypeName,
                    BrandId = x.BrandId,
                    BrandName = x.Brand.BrandName,
                    UnitOfMeasurementId = x.UnitOfMeasurementId,
                    UnitOfMeasurementName = x.UnitOfMeasurement.UnitOfMeasurementName,

                }).FirstOrDefault();
            if (applianceAccessory != null)
            {
                response.data = applianceAccessory;

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Appliance And Accessory Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetItemByBrandId(AccessoryCustom custom)
        {
            var applianceAccessory = applianceAccessoryRepository.FindByCondition(x => x.BrandId == custom.brandId && x.ApplianceAccessoryTypeId == custom.TypeId && x.IsActive == true && x.IsDeleted == false)
                .Select(x => new
                {
                    ApplianceAccessoryId = x.ApplianceAccessoryId,
                    ApplianceAccessoryName = x.ApplianceAccessoryName,
                }).ToList();
            if (applianceAccessory != null)
            {
                response.data = applianceAccessory;

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Appliance And Accessory Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetApplianceAccessoryByBrandId(int brandId)
        {
            var applianceAccessory = applianceAccessoryRepository.FindByCondition(x => x.BrandId == brandId && x.IsActive == true && x.IsDeleted == false)
                .Select(x => new
                {
                    ApplianceAccessoryId = x.ApplianceAccessoryId,
                    SKUCode = x.ItemColors.FirstOrDefault().Skucode,
                    ColorName = x.ItemColors.FirstOrDefault().Color,
                    ColorId = x.ItemColors.FirstOrDefault().ColorId,
                    ItemColorId = x.ItemColors.FirstOrDefault().ItemColorId,
                    ApplianceAccesoryDescription = x.ApplianceAccesoryDescription,
                    ApplianceAccessoryImgUrl = x.ItemColors.FirstOrDefault().ImageUrl,
                    ApplianceAccessoryName = x.ApplianceAccessoryName,
                    ApplianceAccessoryPrice = x.ApplianceAccessoryPrice,
                    ApplianceAccessoryTypeId = x.ApplianceAccessoryTypeId,
                    ApplianceAccessoryTypeName = x.ApplianceAccessoryType.ApplianceAccessoryTypeName,
                    BrandId = x.BrandId,
                    BrandName = x.Brand.BrandName,
                    UnitOfMeasurementId = x.UnitOfMeasurementId,
                    UnitOfMeasurementName = x.UnitOfMeasurement.UnitOfMeasurementName,
                }).ToList();
            if (applianceAccessory != null)
            {
                response.data = applianceAccessory;

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Appliance And Accessory Not Found";
            }
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object UpdateApplianceAccessoryById(ApplianceAccessoryCustom accessory)
        {
            var _accessory = applianceAccessoryRepository.FindByCondition(x => x.ApplianceAccessoryId == accessory.ApplianceAccessoryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (_accessory != null)
            {
                _accessory.ApplianceAccessoryName = accessory.ApplianceAccessoryName;
                //_accessory.Skucode = accessory.SKUCode;
                _accessory.ApplianceAccesoryDescription = accessory.ApplianceAccesoryDescription;
                _accessory.ApplianceAccessoryPrice = accessory.ApplianceAccessoryPrice;
                _accessory.ApplianceAccessoryTypeId = accessory.ApplianceAccessoryTypeId;
                _accessory.BrandId = accessory.BrandId;
                _accessory.UnitOfMeasurementId = accessory.UnitOfMeasurementId;
                _accessory.UpdatedBy = Constants.userId;
                _accessory.UpdatedDate = Helper.Helper.GetDateTime();
                //foreach (var color in _accessory.ItemColors)
                //{
                //    color.Skucode = accessory.SKUCode;
                //    color.
                //}
                //if (accessory.ApplianceAccessoryImgUrl != string.Empty && accessory.ApplianceAccessoryImgUrl != null)
                //{
                //    _accessory.ApplianceAccessoryImgUrl = accessory.ApplianceAccessoryImgUrl;
                //}
                
               
                applianceAccessoryRepository.Update(_accessory);
                context.SaveChanges();
                response.data = "Appliance And Accessory Updated Successfully ";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Appliance And Accessory Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteApplianceAccessory(int applianceAccessoryId)
        {
            var accessory = applianceAccessoryRepository.FindByCondition(x => x.ApplianceAccessoryId == applianceAccessoryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (accessory != null)
            {
                accessory.IsActive = false;
                applianceAccessoryRepository.Update(accessory);
                response.data = "Appliance And Accessory Deleted";
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Appliance And Accessory Not Found";
            }
            return response;
        }
    }
}
