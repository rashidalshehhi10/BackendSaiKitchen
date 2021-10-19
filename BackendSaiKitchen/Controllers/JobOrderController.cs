﻿using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System.Linq;

namespace BackendSaiKitchen.Controllers
{
    public class JobOrderController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object JobOrderFactoryApprove(CustomJobOrder order)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending ||
                     x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditRejected))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderAuditPending;
                Helper.Helper.Each(inquiry.InquiryWorkscopes,
                    x => { x.InquiryStatusId = (int)inquiryStatus.jobOrderAuditPending; });
                foreach (JobOrder joborder in inquiry.JobOrders)
                {
                    JobOrderDetail jobOrderDetail = new JobOrderDetail
                    {
                        MaterialRequestDate = order.materialRequestDate,
                        MaterialDeliveryFinalDate = order.materialDeliveryFinalDate,
                        ProductionCompletionDate = order.productionCompletionDate,
                        ShopDrawingCompletionDate = order.shopDrawingCompletionDate,
                        WoodenWorkCompletionDate = order.woodenWorkCompletionDate,
                        CountertopFixingDate = order.counterTopFixingDate,
                        JobOrderDetailDescription = order.Notes,
                        CreatedBy = Constants.userId,
                        CreatedDate = Helper.Helper.GetDateTime(),
                        InstallationStartDate = order.installationStartDate,
                        IsActive = true,
                        IsDeleted = false
                    };
                    joborder.JobOrderDetails.Add(jobOrderDetail);
                }


                inquiryRepository.Update(inquiry);

                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry Not Found";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object JobOrderFactoryReject(JobOrderFactory job)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == job.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending ||
                     x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditRejected))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderFactoryRejected;
                Helper.Helper.Each(inquiry.InquiryWorkscopes,
                    x => { x.InquiryStatusId = (int)inquiryStatus.jobOrderFactoryRejected; });
                inquiry.InquiryComment = job.Reason;
                //Helper.Helper.Each(inquiry.JobOrders, x =>
                //{
                //    x.IsActive = false;
                //});
                response.data = "JobOrder Factory Rejected";
                inquiryRepository.Update(inquiry);
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
        public object GetinquiryJobOrderFactoryDetailsById(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
                    && (x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending ||
                        x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditRejected ||
                        x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditPending))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Workscope)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();
            // && (y.PaymentTypeId == (int)paymenttype.AdvancePayment || y.PaymentTypeId == (int)paymenttype.Installment))).FirstOrDefault();
            return InquiryDetail(inquiry);
        }

        [HttpPost]
        [Route("[action]")]
        public object GetInquiryJobOrderFactoryByBranchId(int branchId)
        {
            System.Collections.Generic.List<CheckListByBranch> inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false
                    && (x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending ||
                        x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditRejected) && x.JobOrders.Any(y =>
                        y.IsActive == true && y.IsDeleted == false && y.FactoryId == branchId))
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
        public object GetInquirieslistofAuditbyBranch(int branchId)
        {
            System.Collections.Generic.List<CheckListByBranch> inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false
                    && x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditPending &&
                    x.JobOrders.Any(y => y.IsActive == true && y.IsDeleted == false))
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
        public object ApproveAudit(Approval approve)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == approve.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditPending)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderInProgress;
                inquiry.InquiryComment = approve.Reason;

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.jobOrderInProgress;
                    inquiryWorkscope.Comments = approve.Reason;
                }


                response.data = "Job Order Audit Approved";
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Does not exist";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object RejectAudit(Approval Reject)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == Reject.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditPending)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderAuditRejected;
                inquiry.InquiryComment = Reject.Reason;

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.jobOrderAuditRejected;
                    inquiryWorkscope.Comments = Reject.Reason;
                }


                response.data = "Job Order Audit Rejected";
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Does not exist";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetInquiryJobOrderByBranchId(int branchId)
        {
            System.Collections.Generic.List<CheckListByBranch> inquiries = inquiryRepository.FindByCondition(x =>
                x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
                && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesPending ||
                    x.InquiryStatusId == (int)inquiryStatus.contractInProgress ||
                    x.InquiryStatusId == (int)inquiryStatus.contractRejected)).Select(x => new CheckListByBranch
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
        public object GetinquiryJobOrderDetailsById(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
                    && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesPending ||
                        x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayed ||
                        x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesDelayed ||
                        x.InquiryStatusId == (int)inquiryStatus.contractInProgress ||
                        x.InquiryStatusId == (int)inquiryStatus.contractRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Workscope)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();
            // && (y.PaymentTypeId == (int)paymenttype.AdvancePayment || y.PaymentTypeId == (int)paymenttype.Installment))).FirstOrDefault();
            return InquiryDetail(inquiry);
        }

        [HttpPost]
        [Route("[action]")]
        public object ChangeFactory(Factroy factroy)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == factroy.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (JobOrder joborder in inquiry.JobOrders)
                {
                    joborder.FactoryId = factroy.FactoryId;
                }

                inquiryRepository.Update(inquiry);
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