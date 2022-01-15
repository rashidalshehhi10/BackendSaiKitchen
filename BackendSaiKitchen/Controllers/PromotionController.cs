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
    public class PromotionController : BaseController
    {

        [HttpPost]
        [Route("[action]")]
        public object AddPromotion(Promotion promotion)
        {
            var oldPromotion = promotionRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.PromotionId == promotion.PromotionId).FirstOrDefault();

            if (oldPromotion == null)
            {
                promotion.IsActive = true;
                promotion.IsDeleted = false;
                promotion.CreatedBy = Constants.userId;
                promotion.CreatedDate = Helper.Helper.GetDateTime();
                promotionRepository.Create(promotion);
                context.SaveChanges();
                response.data = promotion;
            }
            else
            {
                oldPromotion.PromotionDescription = promotion.PromotionDescription;
                oldPromotion.PromotionName = promotion.PromotionName;
                oldPromotion.PromotionFile = promotion.PromotionFile;
                oldPromotion.PromotionTypeId = promotion.PromotionTypeId;

                promotionRepository.Update(oldPromotion);
                context.SaveChanges();
                response.data = oldPromotion;
            }
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object GetPromotionById(int PromotionId)
        {
            var Promotion = promotionRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.PromotionId == PromotionId).FirstOrDefault();
            if (Promotion != null)
            {
                response.data = Promotion;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Promotion Not Found";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllPromotionType()
        {
            var types = promotionTypeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();

            response.data = types;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllPromotions()
        {
            var Promotions = promotionRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Select(x => new 
            {
                x.PromotionId,
                x.PromotionName,
                x.PromotionDescription,
                x.PromotionTypeId,
                x.PromotionType.PromotionTypeName,
                x.PromotionFile
            }).ToList();
            response.data = Promotions;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeletePromotion(int promotionId)
        {
            var promotion = promotionRepository.FindByCondition(x => x.PromotionId == promotionId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();

            if (promotion != null)
            {
                promotion.IsActive = false;
                promotionRepository.Update(promotion);
                context.SaveChanges();
                response.data = "Promotion Deleted";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Promotion Not Found";
            }
            return response;
        }

    }
}

