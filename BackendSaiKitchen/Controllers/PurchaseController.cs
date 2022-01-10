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
    public class PurchaseController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object AddPurchaseRequest(PurchaseCustomModel purchase)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == purchase.inquiryId && x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress)
                .Include(x => x.JobOrders.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.PurchaseRequests.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (var job in inquiry.JobOrders)
                {
                    foreach (var lp in purchase.lpo)
                    {
                        PurchaseRequest purchaseRequest = new PurchaseRequest();
                        //purchaseRequest.PurchaseRequestTitle = lp.PurchaseRequestTitle;
                        //purchaseRequest.PurchaseRequestDescription = lp.PurchaseRequestDescription;
                        purchaseRequest.PurchaseRequestFinalDeliveryRequestedDate = lp.PurchaseRequestFinalDeliveryRequestedDate;
                        purchaseRequest.IsActive = true;
                        purchaseRequest.IsDeleted = false;
                        purchaseRequest.CreatedBy = Constants.userId;
                        purchaseRequest.CreatedDate = Helper.Helper.GetDateTime();
                        foreach (string fileUrl in lp.files)
                        {
                            if (fileUrl != null && fileUrl != string.Empty)
                            {
                                purchaseRequest.Files.Add(new File
                                {
                                    FileUrl = fileUrl,
                                    FileName = fileUrl.Split('.')[0],
                                    FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                    IsImage = fileUrl.Split('.').Length > 1,
                                    IsActive = true,
                                    IsDeleted = false,
                                    UpdatedBy = Constants.userId,
                                    UpdatedDate = Helper.Helper.GetDateTime(),
                                    CreatedBy = Constants.userId,
                                    CreatedDate = Helper.Helper.GetDateTime()
                                });
                            }
                        }
                        job.PurchaseRequests.Add(purchaseRequest);
                    }
                    
                }
                inquiryRepository.Update(inquiry);
                response.data = inquiry;
                context.SaveChanges();
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
