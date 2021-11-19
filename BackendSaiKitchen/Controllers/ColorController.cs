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
    public class ColorController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetAllColor()
        {
            var colors = colorRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Select(x => new
            {
                ColorId = x.ColorId,
                ColorName = x.ColorName,
                ColorCode = x.ColorCode,
                ColorDescription = x.ColorDescription,
            }).ToList();
            response.data = colors;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetColorById(int colorId)
        {
            var color = colorRepository.FindByCondition(x => x.ColorId == colorId && x.IsActive == true && x.IsDeleted == false).Select(x => new
            {
                ColorId = x.ColorId,
                ColorName = x.ColorName,
                ColorCode = x.ColorCode,
                ColorDescription = x.ColorDescription,
            }).FirstOrDefault();
            if (color != null)
            {
                response.data = color;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Color Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetColorByCode(string colorCode)
        {
            var color = colorRepository.FindByCondition(x => x.ColorCode == colorCode && x.IsActive == true && x.IsDeleted == false).Select(x => new
            {
                ColorId = x.ColorId,
                ColorName = x.ColorName,
                ColorCode = x.ColorCode,
                ColorDescription = x.ColorDescription,
            }).FirstOrDefault();
            if (color != null)
            {
                response.data = color;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Color Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object CreateColor(Color color)
        {
            if (color != null)
            {
                Color _color = new Color();
                _color.IsActive = true;
                _color.IsDeleted = false;
                _color.CreatedBy = Constants.userId;
                _color.CreatedDate = Helper.Helper.GetDateTime();
                _color.ColorName = color.ColorName;
                _color.ColorCode = color.ColorCode;
                _color.ColorDescription = color.ColorDescription;
                colorRepository.Create(_color);
                context.SaveChanges();
                response.data = _color;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Please Enter Data Correctly";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateColor(Color color)
        {
            var _color = colorRepository.FindByCondition(x => x.ColorId == color.ColorId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (_color != null)
            {
                
                _color.IsActive = true;
                _color.IsDeleted = false;
                _color.UpdatedBy = Constants.userId;
                _color.UpdatedDate = Helper.Helper.GetDateTime();
                _color.ColorName = color.ColorName;
                _color.ColorCode = color.ColorCode;
                _color.ColorDescription = color.ColorDescription;
                colorRepository.Update(_color);
                context.SaveChanges();
                response.data = _color;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Color Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteColor(int colorId)
        {
            var _color = colorRepository.FindByCondition(x => x.ColorId == colorId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (_color != null)
            {

                _color.IsActive = false;
                _color.UpdatedBy = Constants.userId;
                _color.UpdatedDate = Helper.Helper.GetDateTime();
                colorRepository.Update(_color);
                context.SaveChanges();
                response.data = _color;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Color Not Found";
            }
            return response;
        }
    }
}
