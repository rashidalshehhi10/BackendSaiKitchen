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
        public object JobOrderFactoryReject(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryApprovalPending)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderFactoryRejected;
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
    }
}
