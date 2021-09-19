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
    public class JobOrderController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object JobOrderFactoryApprove(CustomJobOrder order)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryApprovalPending))
                 .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (var joborder in inquiry.JobOrders)
                {
                    JobOrderDetail jobOrderDetail = new JobOrderDetail();
                    jobOrderDetail.MaterialAvailabilityDate = order.materialAvailablityDate;
                    jobOrderDetail.MaterialDeliveryFinalDate = order.materialDeliveryFinalDate;
                    jobOrderDetail.ProductionCompletionDate = order.productionCompletionDate;
                    jobOrderDetail.ShopDrawingCompletionDate = order.shopDrawingCompletionDate;
                    jobOrderDetail.WoodenWorkCompletionDate = order.woodenWorkCompletionDate;
                    jobOrderDetail.JobOrderDetailDescription = order.Notes;
                    jobOrderDetail.CreatedBy = Constants.userId;
                    jobOrderDetail.CreatedDate = Helper.Helper.GetDateTime();
                    joborder.JobOrderDetails.Add(jobOrderDetail);
                }
                //jobOrderDetail.jo
                inquiryRepository.Update(inquiry);
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry Not Found";
            }
            return response ;
        }

        [HttpPost]
        [Route("[action]")]
        public object JobOrderFactoryReject(JobOrderFactoryReject job)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == job.inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryApprovalPending)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderFactoryRejected;
                inquiry.InquiryComment = job.Reason;
                //Helper.Helper.Each(inquiry.JobOrders, x =>
                //{
                //    x.IsActive = false;
                //});
                response.data = "JobOrder Factory Rejected";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetInquiryJobOrderFactoryByBranchId(int branchId)
        {
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryApprovalPending)).Select(x => new CheckListByBranch
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
