using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System.Linq;

namespace BackendSaiKitchen.Controllers
{
    public class TermsAndConditionsController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetAllTermsAndConditions()
        {
            response.data = TermsAndConditionsRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetTermsAndConditionsById(int TermsAndConditionsId)
        {
            var termsAndConditions = TermsAndConditionsRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.TermsAndConditionsId == TermsAndConditionsId).FirstOrDefault();
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
            var terms = TermsAndConditionsRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.TermsAndConditionsId == TermsAndConditionsId).FirstOrDefault();
            if (terms != null)
            {
                response.data = terms;
                TermsAndConditionsRepository.Delete(terms);
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
            var Terms = TermsAndConditionsRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.TermsAndConditionsId == terms.TermsAndConditionsId).FirstOrDefault();
            if (Terms != null)
            {
                response.isError = true;
                response.errorMessage = "Terms And Conditions Already There";
            }
            else
            {
                TermsAndConditionsRepository.Create(terms);
                response.data = terms;
            }
            return response;
        }
    }
}
