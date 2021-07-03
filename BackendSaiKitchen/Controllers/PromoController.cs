using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    public class PromoController : BaseController
    {

        [AuthFilter((int)permission.ManagePromo, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetAllPromo()
        {
            return promoRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);

        }

        [AuthFilter((int)permission.ManagePromo, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public object GetPromoById(int promoId)
        {
            response.data = promoRepository.FindByCondition(x => x.PromoId == promoId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (response.data == null)
            {
                response.isError = true;
                response.errorMessage = "Promo doesn't Exist";
            }
            return response;
        }

        [AuthFilter((int)permission.ManagePromo, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public object AddPromo(Promo promo)
        {
            if (promo.PromoId==0)
            {
                promoRepository.Create(promo);
                context.SaveChanges();
            }
            else
            {
                Promo oldPromo = promoRepository.FindByCondition(x => x.PromoId == promo.PromoId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                if (oldPromo != null)
                {
                    oldPromo.PromoName = promo.PromoName;
                    oldPromo.PromoStartDate = promo.PromoStartDate;
                    oldPromo.PromoCode = promo.PromoCode;
                    oldPromo.PromoCurrency = promo.PromoCurrency;
                    oldPromo.PromoDescription = promo.PromoDescription;
                    oldPromo.PromoDiscount = promo.PromoDiscount;
                    oldPromo.PromoExpiryDate = promo.PromoExpiryDate;
                    oldPromo.PromoTermsAndCondition = promo.PromoTermsAndCondition;
                    oldPromo.IsPercentage = promo.IsPercentage;
                    promoRepository.Update(oldPromo);
                    context.SaveChanges();
                    response.data = oldPromo;
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "Promo doesn't exist";
                }
            }
            return response;
        }

        [AuthFilter((int)permission.ManagePromo, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object EditPromo(Promo promo)
        {
            Promo oldPromo = promoRepository.FindByCondition(x => x.PromoId == promo.PromoId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldPromo != null)
            {
                oldPromo.PromoName = promo.PromoName;
                oldPromo.PromoStartDate = promo.PromoStartDate;
                oldPromo.PromoCode = promo.PromoCode;
                oldPromo.PromoCurrency = promo.PromoCurrency;
                oldPromo.PromoDescription = promo.PromoDescription;
                oldPromo.PromoDiscount = promo.PromoDiscount;
                oldPromo.PromoExpiryDate = promo.PromoExpiryDate;
                oldPromo.PromoTermsAndCondition = promo.PromoTermsAndCondition;
                oldPromo.IsPercentage = promo.IsPercentage;
                promoRepository.Update(oldPromo);
                context.SaveChanges();
                response.data = oldPromo;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Promo doesnt exist";
            }
            return response;
        }

        [AuthFilter((int)permission.ManagePromo, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public object DeletePromo(int promoId)
        {
            Promo oldPromo = promoRepository.FindByCondition(x => x.PromoId == promoId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldPromo != null)
            {
                promoRepository.Delete(oldPromo);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Promo doesn't exist";
            }
            return response;
        }

    }
}
