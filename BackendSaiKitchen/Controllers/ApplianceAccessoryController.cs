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
        public object CreateUpdateApplianceAccessory(ApplianceAccessoryCustom accessory)
        {
            var _item = itemColorRepository.FindByCondition(x => x.Skucode == accessory.SKUCode && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Item)
                .Include(x => x.Color).FirstOrDefault();
            
            if (_item != null)
            {
                _item.Item.ApplianceAccessoryName = accessory.ApplianceAccessoryName;
                _item.Item.ApplianceAccesoryDescription = accessory.ApplianceAccesoryDescription;
                _item.Item.ApplianceAccessoryPrice = accessory.ApplianceAccessoryPrice;
                _item.Item.ApplianceAccessoryTypeId = accessory.ApplianceAccessoryTypeId;
                _item.Item.BrandId = accessory.BrandId;
                _item.Item.UnitOfMeasurementId = accessory.UnitOfMeasurementId;
                _item.Item.UpdatedBy = Constants.userId;
                _item.Item.UpdatedDate = Helper.Helper.GetDateTime();
                _item.ColorId = accessory.colorId;
                _item.ImageUrl = accessory.ApplianceAccessoryImgUrl;
                _item.Skucode = accessory.SKUCode;
                _item.UpdatedBy = Constants.userId;
                _item.UpdatedDate = Helper.Helper.GetDateTime();
                itemColorRepository.Update(_item);
                context.SaveChanges();
                response.data = _item;
            }
            else
            {
                ApplianceAccessory _accessory = new ApplianceAccessory();
                ItemColor _itemColor = new ItemColor();
                _accessory.ApplianceAccessoryName = accessory.ApplianceAccessoryName;
                _accessory.ApplianceAccesoryDescription = accessory.ApplianceAccesoryDescription;
                _accessory.ApplianceAccessoryPrice = accessory.ApplianceAccessoryPrice;
                _accessory.ApplianceAccessoryTypeId = accessory.ApplianceAccessoryTypeId;
                _accessory.BrandId = accessory.BrandId;
                _accessory.UnitOfMeasurementId = accessory.UnitOfMeasurementId;
                _accessory.CreatedBy = Constants.userId;
                _accessory.CreatedDate = Helper.Helper.GetDateTime();
                _accessory.IsActive = true;
                _accessory.IsDeleted = false;
                _itemColor.ColorId = accessory.colorId;
                _itemColor.ImageUrl = accessory.ApplianceAccessoryImgUrl;
                _itemColor.Skucode = accessory.SKUCode;
                _itemColor.CreatedBy = Constants.userId;
                _itemColor.CreatedDate = Helper.Helper.GetDateTime();
                _itemColor.IsActive = true;
                _item.IsDeleted = false;
                _itemColor.Item = _accessory;
                itemColorRepository.Create(_itemColor);
                context.SaveChanges();
                response.data = _itemColor;
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllApplianceAccessory()
        {
            var applianceAccessory = itemColorRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
               .Select(x => new
               {
                   ApplianceAccessoryId = x.Item.ApplianceAccessoryId,
                   itemColorId = x.ItemColorId,
                   SKUCode = x.Skucode,
                   colorId = x.ColorId,
                   colorName = x.Color.ColorName,
                   ImgUrl = x.ImageUrl,
                   ApplianceAccesoryDescription = x.Item.ApplianceAccesoryDescription,
                   ApplianceAccessoryName = x.Item.ApplianceAccessoryName,
                   ApplianceAccessoryPrice = x.Item.ApplianceAccessoryPrice,
                   ApplianceAccessoryTypeId = x.Item.ApplianceAccessoryTypeId,
                   ApplianceAccessoryTypeName = x.Item.ApplianceAccessoryType.ApplianceAccessoryTypeName,
                   BrandId = x.Item.BrandId,
                   BrandName = x.Item.Brand.BrandName,
                   UnitOfMeasurementId = x.Item.UnitOfMeasurementId,
                   UnitOfMeasurementName = x.Item.UnitOfMeasurement.UnitOfMeasurementName,
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
            var applianceAccessory = itemColorRepository.FindByCondition(x => x.ItemId == applianceAccessoryId && x.IsActive == true && x.IsDeleted == false)
                .Select(x => new 
                {
                    ApplianceAccessoryId = x.Item.ApplianceAccessoryId,
                    itemColorId = x.ItemColorId,
                    SKUCode = x.Skucode,
                    colorId = x.ColorId,
                    colorName = x.Color.ColorName,
                    ImgUrl = x.ImageUrl,
                    ApplianceAccesoryDescription = x.Item.ApplianceAccesoryDescription,
                    ApplianceAccessoryName = x.Item.ApplianceAccessoryName,
                    ApplianceAccessoryPrice = x.Item.ApplianceAccessoryPrice,
                    ApplianceAccessoryTypeId = x.Item.ApplianceAccessoryTypeId,
                    ApplianceAccessoryTypeName = x.Item.ApplianceAccessoryType.ApplianceAccessoryTypeName,
                    BrandId = x.Item.BrandId,
                    BrandName = x.Item.Brand.BrandName,
                    UnitOfMeasurementId = x.Item.UnitOfMeasurementId,
                    UnitOfMeasurementName = x.Item.UnitOfMeasurement.UnitOfMeasurementName,

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
            var applianceAccessory = itemColorRepository.FindByCondition(x => x.Item.BrandId == brandId && x.IsActive == true && x.IsDeleted == false)
                .Select(x => new
                {
                    ApplianceAccessoryId = x.Item.ApplianceAccessoryId,
                    itemColorId = x.ItemColorId,
                    SKUCode = x.Skucode,
                    colorId = x.ColorId,
                    colorName = x.Color.ColorName,
                    ImgUrl = x.ImageUrl,
                    ApplianceAccesoryDescription = x.Item.ApplianceAccesoryDescription,
                    ApplianceAccessoryName = x.Item.ApplianceAccessoryName,
                    ApplianceAccessoryPrice = x.Item.ApplianceAccessoryPrice,
                    ApplianceAccessoryTypeId = x.Item.ApplianceAccessoryTypeId,
                    ApplianceAccessoryTypeName = x.Item.ApplianceAccessoryType.ApplianceAccessoryTypeName,
                    BrandId = x.Item.BrandId,
                    BrandName = x.Item.Brand.BrandName,
                    UnitOfMeasurementId = x.Item.UnitOfMeasurementId,
                    UnitOfMeasurementName = x.Item.UnitOfMeasurement.UnitOfMeasurementName,
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
        //[HttpPost]
        //[Route("[action]")]
        //public object UpdateApplianceAccessoryById(ApplianceAccessoryCustom accessory)
        //{
        //    var _accessory = applianceAccessoryRepository.FindByCondition(x => x.ApplianceAccessoryId == accessory.ApplianceAccessoryId && x.IsActive == true && x.IsDeleted == false)
        //        .Include(x => x.ItemColors.Where(x => x.IsActive == true && x.IsDeleted == false))
        //        .ThenInclude(x => x.Color).FirstOrDefault();
        //    if (_accessory != null)
        //    {
        //        _accessory.ApplianceAccessoryName = accessory.ApplianceAccessoryName;
        //        _accessory.ApplianceAccesoryDescription = accessory.ApplianceAccesoryDescription;
        //        _accessory.ApplianceAccessoryPrice = accessory.ApplianceAccessoryPrice;
        //        _accessory.ApplianceAccessoryTypeId = accessory.ApplianceAccessoryTypeId;
        //        _accessory.BrandId = accessory.BrandId;
        //        _accessory.UnitOfMeasurementId = accessory.UnitOfMeasurementId;
        //        _accessory.UpdatedBy = Constants.userId;
        //        _accessory.UpdatedDate = Helper.Helper.GetDateTime();
        //        foreach (var color in _accessory.ItemColors.Where(x => x.ItemColorId == accessory.itemcolorId))
        //        {
        //            color.Skucode = accessory.SKUCode;
        //            color.ColorId = accessory.colorId;
        //            if (accessory.ApplianceAccessoryImgUrl != string.Empty && accessory.ApplianceAccessoryImgUrl != null)
        //            {
        //                color.ImageUrl = accessory.ApplianceAccessoryImgUrl;
        //            }
                    
        //        }



        //        applianceAccessoryRepository.Update(_accessory);
        //        context.SaveChanges();
        //        response.data = "Appliance And Accessory Updated Successfully ";
        //    }
        //    else
        //    {
        //        response.isError = true;
        //        response.errorMessage = "Appliance And Accessory Not Found";
        //    }
        //    return response;
        //}

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
