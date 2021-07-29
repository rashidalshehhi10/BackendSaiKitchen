﻿using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class TermsAndConditionsController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetAllTermsAndConditions()
        {
            response.data = termsAndConditionsRepositry.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            return response;
        }
       
        [HttpPost]
        [Route("[action]")]
        public object GetTermsAndConditionsById(int TermsAndConditionsId)
        {
            var termsAndConditions = termsAndConditionsRepositry.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.TermsAndConditionsId == TermsAndConditionsId).FirstOrDefault();
            if (termsAndConditions != null)
            {
                response.data = termsAndConditions;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeleteTermsAndConditionsById(int TermsAndConditionsId)
        {
            var terms = termsAndConditionsRepositry.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.TermsAndConditionsId == TermsAndConditionsId).FirstOrDefault();
            if (terms !=null)
            {
                response.data = terms;
                termsAndConditionsRepositry.Delete(terms);
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Terms And Conditions Not Found";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object AddTermsAndConditions(TermsAndCondition terms)
        {
            var Terms = termsAndConditionsRepositry.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.TermsAndConditionsId == terms.TermsAndConditionsId).FirstOrDefault();
            if (Terms != null)
            {
                response.isError = true;
                response.errorMessage = "Terms And Conditions Already There";
            }
            else
            {
                termsAndConditionsRepositry.Create(terms);
                response.data = terms;
            }
            return response;
        }
    }
}
