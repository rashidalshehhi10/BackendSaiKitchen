using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class JobOrderDetailController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetInquiryJobOrderDetailsByBranchId(int branchId)
        {
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryAccepted)).Select(x => new CheckListByBranch
            {
                InquiryId = x.InquiryId,
                QuotationNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationId,
                InquiryDescription = x.InquiryDescription,
                InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).First(),
                WorkScopeCount = x.InquiryWorkscopes.Count,
                Status = x.InquiryStatusId,
                BuildingAddress = x.Building.BuildingAddress,
                BuildingCondition = x.Building.BuildingCondition,
                BuildingFloor = x.Building.BuildingFloor,
                BuildingReconstruction = (bool)x.Building.BuildingReconstruction ? "Yes" : "No",
                IsOccupied = (bool)x.Building.IsOccupied ? "Yes" : "No",
                InquiryEndDate = Helper.Helper.GetDateFromString(x.InquiryEndDate),
                BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit,
                IsEscalationRequested = x.IsEscalationRequested,
                CustomerId = x.CustomerId,
                CustomerCode = "CS" + x.BranchId + "" + x.CustomerId,
                CustomerName = x.Customer.CustomerName,
                CustomerEmail = x.Customer.CustomerEmail,
                CustomerContact = x.Customer.CustomerContact,
                BranchId = x.BranchId,
                InquiryAddedBy = x.AddedByNavigation.UserName,
                InquiryAddedById = x.AddedBy,
                NoOfRevision = x.Quotations.Where(y => y.IsDeleted == false).Count(),
                InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId
            }).ToList();
            if (inquiries != null)
            {
                response.data = inquiries;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "there is no inquiries to Check";
            }
            return response;
        }
    }
}
