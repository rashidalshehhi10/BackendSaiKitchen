using BackendSaiKitchen.CustomModel;
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

    public class NewsletterTypeController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetAllNewsletterType()
        {
            var newsletterTypes = newsletterTypeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            response.data = newsletterTypes;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetNewsletterTypeById(int newsletterTypeId)
        {
            var newsletterType = newsletterTypeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.NewsletterTypeId == newsletterTypeId);
            if (newsletterType != null)
            {
                response.data = newsletterType;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "News letter Type Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllNewsletterFrequency()
        {
            var Frequency = newsletterFrequencyRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            response.data = Frequency;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object CreateNewsletterType(NewsletterCustom type)
        {
            NewsletterType _type = new NewsletterType();
            if (type != null)
            {
                _type.NewsletterTypeName = type.NewsletterTypeName;
                _type.NewsletterTypeDescription = type.NewsletterTypeDescription;
                _type.IsActive = true;
                _type.IsDeleted = false;
                _type.CreatedBy = Constants.userId;
                _type.CreatedDate = Helper.Helper.GetDateTime();
                newsletterTypeRepository.Create(_type);
                response.data = _type;
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Please ente the data correctly";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateNewsletterType(NewsletterCustom type)
        {
            NewsletterType _type = newsletterTypeRepository.FindByCondition(x => x.NewsletterTypeId == type.NewsletterTypeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (_type != null)
            {
                _type.NewsletterTypeName = type.NewsletterTypeName;
                _type.NewsletterTypeDescription = type.NewsletterTypeDescription;
                _type.IsActive = true;
                _type.IsDeleted = false;
                _type.UpdatedBy = Constants.userId;
                _type.UpdatedDate = Helper.Helper.GetDateTime();
                newsletterTypeRepository.Update(_type);
                response.data = _type;
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "News letter Type Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteNewsletterType(int NewsletterTypeId)
        {
            NewsletterType _type = newsletterTypeRepository.FindByCondition(x => x.NewsletterTypeId == NewsletterTypeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (_type != null)
            {
                _type.IsActive = false;
                _type.UpdatedBy = Constants.userId;
                _type.UpdatedDate = Helper.Helper.GetDateTime();
                newsletterTypeRepository.Update(_type);
                response.data = _type;
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "News letter Type Not Found";
            }
            return response;
        }
    }
}
