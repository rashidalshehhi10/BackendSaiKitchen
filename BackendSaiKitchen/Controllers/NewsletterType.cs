using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{

    public class NewsletterType : BaseController
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
    }
}
