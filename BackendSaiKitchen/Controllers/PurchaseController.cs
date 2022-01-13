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
        public object GetInquirypurchaseRequestByBranchId(int branchId)
        {
            System.Collections.Generic.List<CheckListByBranch> inquiries = inquiryRepository.FindByCondition(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false
                    && (x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress) && x.JobOrders.Any(y =>
                        y.IsActive == true && y.IsDeleted == false && y.PurchaseRequests.Any(z => z.IsActive == true && z.IsDeleted == false && z.PurchaseStatusId == (int)purchaseStatus.purchaseRequested)))
                .Select(x => new CheckListByBranch
                {
                    InquiryId = x.InquiryId,
                    QuotationNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations
                        .OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false)
                        .QuotationId,
                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.Helper.GetDateFromString(x.InquiryStartDate),
                    WorkScopeName = x.InquiryWorkscopes.Select(y => y.Workscope.WorkScopeName).ToList(),
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
                    InquiryAddedBy = x.ManagedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy,
                    NoOfRevision = x.Quotations.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
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

        [HttpPost]
        [Route("[action]")]
        public object GetPurchaseOrderedByInquiryId(int inquiryId)
        {
            var Purchase = purchaseOrderRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.PurchaseStatusId == (int)purchaseStatus.purchaseOrdered &&
                        x.PurchaseRequest.IsActive == true && x.PurchaseRequest.IsDeleted == false &&
                        x.PurchaseRequest.JobOrder.IsActive == true && x.PurchaseRequest.JobOrder.IsDeleted == false &&
                        x.PurchaseRequest.JobOrder.Inquiry.IsActive == true && x.PurchaseRequest.JobOrder.Inquiry.IsDeleted == false && x.PurchaseRequest.JobOrder.Inquiry.InquiryId == inquiryId).Select(x => new
                        {
                            x.PurchaseOrderId,
                            x.PurchaseOrderTitle,
                            x.PurchaseOrderDescription,
                            x.Files,
                            x.PurchaseOrderAmount,
                            x.PurchaseOrderDate,
                            x.PurchaseOrderExpectedDeliveryDate,
                            x.PurchaseStatusId,
                            x.PurchaseStatus.PurchaseStatusName,
                        }).ToList();

            response.data = Purchase;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetPurchaseOrderedByBranchId(int branchId)
        {
            var Purchase = purchaseOrderRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.PurchaseStatusId == (int)purchaseStatus.purchaseOrdered &&
            x.PurchaseRequest.IsActive == true && x.PurchaseRequest.IsDeleted == false &&
            x.PurchaseRequest.JobOrder.IsActive == true && x.PurchaseRequest.JobOrder.IsDeleted == false &&
            x.PurchaseRequest.JobOrder.Inquiry.IsActive == true && x.PurchaseRequest.JobOrder.Inquiry.IsDeleted == false && x.PurchaseRequest.JobOrder.Inquiry.BranchId == branchId).Select(x => new
            {
                x.PurchaseOrderId,
                x.PurchaseOrderTitle,
                x.PurchaseOrderDescription,
                x.Files,
                x.PurchaseOrderAmount,
                x.PurchaseOrderDate,
                x.PurchaseOrderExpectedDeliveryDate,
                x.PurchaseStatusId,
                x.PurchaseStatus.PurchaseStatusName,
            }).ToList();

            response.data = Purchase;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetPurchaseOrderedById(int purchaseOrderId)
        {
            var Purchase = purchaseOrderRepository.FindByCondition(x => x.PurchaseOrderId == purchaseOrderId && x.IsActive == true && x.IsDeleted == false && x.PurchaseStatusId == (int)purchaseStatus.purchaseOrdered &&
            x.PurchaseRequest.IsActive == true && x.PurchaseRequest.IsDeleted == false &&
            x.PurchaseRequest.JobOrder.IsActive == true && x.PurchaseRequest.JobOrder.IsDeleted == false &&
            x.PurchaseRequest.JobOrder.Inquiry.IsActive == true && x.PurchaseRequest.JobOrder.Inquiry.IsDeleted == false).Select(x => new
            {
                x.PurchaseOrderId,
                x.PurchaseOrderTitle,
                x.PurchaseOrderDescription,
                x.Files,
                x.PurchaseOrderAmount,
                x.PurchaseOrderDate,
                x.PurchaseOrderExpectedDeliveryDate,
                x.PurchaseStatusId,
                x.PurchaseStatus.PurchaseStatusName,
            }).FirstOrDefault();

            response.data = Purchase;
            return response;
        }

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

        [HttpPost]
        [Route("[action]")]
        public object AddPurchaseOrder(PurchaseOrderCustomModel purchase)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == purchase.inquiryId && x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress)
                .Include(x => x.JobOrders.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.PurchaseRequests.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.PurchaseOrders.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (var job in inquiry.JobOrders)

                {
                    foreach (var purreq in job.PurchaseRequests)
                    {
                        foreach (var order in purchase.lpo)
                        {
                            PurchaseOrder Order = new PurchaseOrder();
                            Order.PurchaseOrderExpectedDeliveryDate = order.PurchaseOrderExpectedDeliveryDate;
                            purreq.PurchaseStatusId = (int)purchaseStatus.purchaseOrdered;
                            Order.PurchaseStatusId = (int)purchaseStatus.purchaseOrdered;
                            Order.PurchaseOrderAmount = order.PurchaseOrderAmount;
                            Order.PurchaseOrderDate = Helper.Helper.GetDateTime();
                            Order.IsActive = true;
                            Order.IsDeleted = false;
                            Order.CreatedBy = Constants.userId;
                            Order.CreatedDate = Helper.Helper.GetDateTime();
                            foreach (string fileUrl in order.files)
                            {
                                if (fileUrl != null && fileUrl != string.Empty)
                                {
                                    Order.Files.Add(new File
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
                            purreq.PurchaseOrders.Add(Order);
                        }
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

        [HttpPost]
        [Route("[action]")]
        public object RecievePurchaseOrder(int purchaseOrderId)
        {
            var Purchase = purchaseOrderRepository.FindByCondition(x => x.PurchaseOrderId == purchaseOrderId && x.IsActive == true && x.IsDeleted == false &&
            x.PurchaseRequest.IsActive == true && x.PurchaseRequest.IsDeleted == false &&
            x.PurchaseRequest.JobOrder.IsActive == true && x.PurchaseRequest.JobOrder.IsDeleted == false &&
            x.PurchaseRequest.JobOrder.Inquiry.IsActive == true && x.PurchaseRequest.JobOrder.Inquiry.IsDeleted == false).FirstOrDefault();
            if (Purchase != null)
            {
                Purchase.PurchaseStatusId = (int)purchaseStatus.purchaseDelivered;
                Purchase.PurchaseOrderActualDeliveryDate = Helper.Helper.GetDateTime();
                purchaseOrderRepository.Update(Purchase);
                context.SaveChanges();
                response.data = Purchase;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Purchase Not Found";
            }

            return response;
        }
    }
}