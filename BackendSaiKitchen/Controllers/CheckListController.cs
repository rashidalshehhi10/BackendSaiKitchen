using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using Constants = BackendSaiKitchen.Helper.Constants;

namespace BackendSaiKitchen.Controllers
{
    public class CheckListController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetinquiryChecklistDetailsById(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
                    && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance ||
                        x.InquiryStatusId == (int)inquiryStatus.checklistPending ||
                        x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected ||
                        x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected))
                //                .Select(x => new {
                //quotation = x.Quotations.FirstOrDefault(y => y.IsActive == true && y.IsDeleted ==false).Files.Where(y => y.IsActive == true && y.IsDeleted == false),
                //payment = x.Payments.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).Files.Where(y => y.IsActive == true && y.IsDeleted ==false)
                //                }).FirstOrDefault();
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
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
            if (inquiry != null)
            {
                Inquirychecklist inquirychecklist = new Inquirychecklist
                {
                    inquiry = inquiry,
                    fees = FeesRepository
                        .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
                };
                if (inquirychecklist == null)
                {
                    response.isError = true;
                    response.errorMessage = "No Inquiry Found";
                }
                else
                {
                    //inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    response.data = inquirychecklist;
                }
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
        public object GetinquiryCommercialChecklistDetailsById(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
                    && (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending ||
                        x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected ||
                        x.InquiryStatusId == (int)inquiryStatus.specialApprovalRejected))
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
        public object GetAllInquiryChecklist()
        {
            IQueryable<Inquiry> inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false
                && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance ||
                    x.InquiryStatusId == (int)inquiryStatus.checklistPending));
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
        public object GetInquiryChecklistByBranchId(int branchId)
        {
            List<CheckListByBranch> inquiries = inquiryRepository.FindByCondition(x =>
                x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
                && (x.InquiryStatusId == (int)inquiryStatus.checklistPending ||
                    x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected)).Select(x =>
                new CheckListByBranch
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

        [HttpPost]
        [Route("[action]")]
        public object GetInquiryCommercialChecklistByBranchId(int branchId)
        {
            List<CheckListByBranch> inquiries = inquiryRepository.FindByCondition(x =>
                x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
                && (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending ||
                    x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected ||
                    x.InquiryStatusId == (int)inquiryStatus.specialApprovalRejected)).Select(x => new CheckListByBranch
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
                        InquiryComment =x.InquiryComment,
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
        public object InquiryChecklist(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
                    && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance ||
                        x.InquiryStatusId == (int)inquiryStatus.checklistPending ||
                        x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false
                                                                       && (y.PaymentStatusId ==
                                                                           (int)paymentstatus.PaymentApproved ||
                                                                           y.PaymentStatusId ==
                                                                           (int)paymentstatus.InstallmentApproved)
                                                                       && (y.PaymentTypeId ==
                                                                           (int)paymenttype.AdvancePayment ||
                                                                           y.PaymentTypeId ==
                                                                           (int)paymenttype.Installment)))
                .FirstOrDefault();
            if (inquiry != null)
            {
                List<int?> roleTypeId = new List<int?>
                {
                    (int)roleType.Manager
                };

                try
                {
                    sendNotificationToHead(content: "Inquiry " + inquiryId + "  Waiting for approval on CheckList", false, null,
                        null, roleTypeId, Constants.branchId, (int)notificationCategory.Other);
                }
                catch (Exception e)
                {
                    SentrySdk.CaptureMessage(e.Message);
                }

                inquiry.InquiryStatusId = (int)inquiryStatus.checklistPending;
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Does Not exist";
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object ApproveinquiryChecklist(CustomCheckListapprove approve)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x =>
                    x.InquiryId == approve.inquiryId && x.IsActive == true &&
                    x.IsDeleted ==
                    false) // && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance || x.InquiryStatusId == (int)inquiryStatus.checklistPending || x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(y => y.Files.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(z => z.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Files.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Files.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            //JobOrder _jobOrder = new JobOrder();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.commercialChecklistPending;
                inquiry.InquiryComment = approve.Comment;
                foreach (JobOrder joborder in inquiry.JobOrders)
                {
                    joborder.Comments = approve.Comment;
                    // joborder.JobOrderChecklistFileUrl = approve.jobOrderChecklistFileUrl;
                    joborder.JobOrderExpectedDeadline = approve.jobOrderExpectedDeadline;
                    joborder.FactoryId = approve.factoryId;
                    joborder.SiteMeasurementMatchingWithDesign = approve.SiteMeasurementMatchingWithDesign;
                    joborder.SiteMeasurementMatchingWithDesignNotes = approve.SiteMeasurementMatchingWithDesignNotes;
                    joborder.MaterialConfirmation = approve.MatrialConfirmation;
                    joborder.MaterialConfirmationNotes = approve.MatrialConfirmationNotes;
                    joborder.Mepdrawing = approve.MEPDrawing;
                    joborder.MepdrawingNotes = approve.MEPDrawingNotes;
                    joborder.QuotationAndCalculationSheetMatchingProposal =
                        approve.QuotationAndCalculationSheetMatchingProposal;
                    joborder.QuotationAndCalculationSheetMatchingProposalNotes =
                        approve.QuotationAndCalculationSheetMatchingProposalNotes;
                    joborder.ApprovedDrawingsAndAvailabilityOfClientSignature =
                        approve.ApprovedDrawingsAndAvailabilityOfClientSignture;
                    joborder.ApprovedDrawingsAndAvailabilityOfClientSignatureNotes =
                        approve.ApprovedDrawingsAndAvailabilityOfClientSigntureNotes;
                    joborder.AppliancesDataSheet = approve.AppliancesDataSheet;
                    joborder.AppliancesDataSheetNotes = approve.AppliancesDataSheetNotes;
                    joborder.JobOrderChecklistFileUrl = approve.jobOrderChecklistFileUrl;
                    joborder.TechnicalCheckListDoneBy = approve.userId;
                    joborder.TechnicalCheckListCompletionDate = Helper.Helper.GetDateTime();
                }

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.commercialChecklistPending;
                }

                if (approve.addFileonChecklists.Count != 0 && approve.addFileonChecklists != null)
                {
                    for (int i = 0; i < approve.addFileonChecklists.Count; i++)
                    {
                        try
                        {
                            foreach (string fileUrl in approve.addFileonChecklists[i].files)
                            {
                                if (fileUrl != null)
                                {
                                    //var fileUrl = await Helper.Helper.UploadFile(file);

                                    switch (approve.addFileonChecklists[i].documentType)
                                    {
                                        case (int)permission.ManageMeasurement:
                                            foreach (InquiryWorkscope inquiryworkscope in inquiry.InquiryWorkscopes)
                                            {
                                                Measurement measurement = inquiryworkscope.Measurements.FirstOrDefault(x =>
                                                    x.IsActive == true && x.IsDeleted == false);
                                                measurement.Files.Add(new File
                                                {
                                                    FileUrl = fileUrl,
                                                    FileName = fileUrl.Split('.')[0],
                                                    FileContentType = fileUrl.Split('.').Length > 1
                                                        ? fileUrl.Split('.')[1]
                                                        : "mp4",
                                                    IsImage = false,
                                                    IsActive = true,
                                                    IsDeleted = false
                                                });
                                            }

                                            break;
                                        case (int)permission.ManageDesign:
                                            foreach (InquiryWorkscope inquiryworkscope in inquiry.InquiryWorkscopes)
                                            {
                                                Design design = inquiryworkscope.Designs.FirstOrDefault(x =>
                                                    x.IsActive == true && x.IsDeleted == false);
                                                design.Files.Add(new File
                                                {
                                                    FileUrl = fileUrl,
                                                    FileName = fileUrl.Split('.')[0],
                                                    FileContentType = fileUrl.Split('.').Length > 1
                                                        ? fileUrl.Split('.')[1]
                                                        : "mp4",
                                                    IsImage = false,
                                                    IsActive = true,
                                                    IsDeleted = false
                                                });
                                            }

                                            break;
                                        case (int)permission.ManageQuotation:
                                            Quotation quotation = inquiry.Quotations.FirstOrDefault(x =>
                                                x.IsActive == true && x.IsDeleted == false);
                                            quotation.Files.Add(new File
                                            {
                                                FileUrl = fileUrl,
                                                FileName = fileUrl.Split('.')[0],
                                                FileContentType = fileUrl.Split('.').Length > 1
                                                    ? fileUrl.Split('.')[1]
                                                    : "mp4",
                                                IsImage = false,
                                                IsActive = true,
                                                IsDeleted = false
                                            });
                                            break;

                                        default:
                                            SentrySdk.CaptureMessage(approve.addFileonChecklists[i].documentType +
                                                                     " type of document try to upload");
                                            break;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SentrySdk.CaptureMessage(ex.Message);
                        }
                    }
                }

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
        public object GetInquirySpecialApprovalByBranchId(int branchId)
        {
            List<CheckListByBranch> inquiries = inquiryRepository.FindByCondition(x =>
                x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
                && x.InquiryStatusId == (int)inquiryStatus.specialApprovalPending).Select(x => new CheckListByBranch
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

        [HttpPost]
        [Route("[action]")]
        public object GetinquirySpecialApprovalDetailsById(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
                    && x.InquiryStatusId == (int)inquiryStatus.specialApprovalPending)
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
        public object ApproveSpecialApproval(Approval approve)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == approve.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    x.InquiryStatusId == (int)inquiryStatus.specialApprovalPending)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderConfirmationPending;
                inquiry.InquiryComment = approve.Reason;

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.jobOrderConfirmationPending;
                    inquiryWorkscope.Comments = approve.Reason;
                }


                response.data = "Special Approval Approved";
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
        public object RejectSpecialApproval(Approval Reject)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == Reject.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    x.InquiryStatusId == (int)inquiryStatus.specialApprovalPending)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.specialApprovalRejected;
                inquiry.InquiryComment = Reject.Reason;

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.specialApprovalRejected;
                    inquiryWorkscope.Comments = Reject.Reason;
                }

                //foreach (var joboreder in inquiry.JobOrders)
                //{
                //    joboreder.IsActive = false;
                //}

                response.data = "Special Approval Rejected";
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
        public object ApproveinquiryCommericalChecklist(commerical approve)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == approve.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending ||
                     x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected || 
                     x.InquiryStatusId == (int)inquiryStatus.specialApprovalRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                foreach (JobOrder joborder in inquiry.JobOrders)
                {
                    joborder.IsSpecialApprovalRequired = approve.IsSpecialApprovalRequired;
                    joborder.CommercialCheckListCompletionDate = Helper.Helper.GetDateTime();
                    joborder.CommercialCheckListDoneBy = approve.userId;
                }

                if (approve.IsSpecialApprovalRequired)
                {
                    inquiry.InquiryStatusId = (int)inquiryStatus.specialApprovalPending;
                    inquiry.InquiryDescription = approve.Reason;

                    foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                    {
                        inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.specialApprovalPending;
                        inquiryWorkscope.Comments = approve.Reason;
                    }
                }
                else
                {
                    inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderConfirmationPending;
                    inquiry.InquiryComment = approve.Reason;

                    foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                    {
                        inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.jobOrderConfirmationPending;
                        inquiryWorkscope.Comments = approve.Reason;
                    }
                }

                response.data = "Commerical Checklist Approved";
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
        public object RejectinquiryCommericalChecklist(Approval Reject)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == Reject.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending ||
                     x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected ||
                     x.InquiryStatusId == (int)inquiryStatus.specialApprovalRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.commercialChecklistRejected;
                inquiry.InquiryComment = Reject.Reason;

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.commercialChecklistRejected;
                    inquiryWorkscope.Comments = Reject.Reason;
                }

                foreach (JobOrder joboreder in inquiry.JobOrders)
                {
                    joboreder.IsActive = false;
                }

                response.data = "Commerical Checklist Rejected";
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
        public object RejectinquiryChecklist(CustomCheckListReject reject)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == reject.inquiryId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance ||
                     x.InquiryStatusId == (int)inquiryStatus.checklistPending ||
                     x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();
            JobOrder _jobOrder = new JobOrder();
            if (inquiry != null)
            {
                foreach (Addrejection reason in reject.Addrejections)
                {
                    InquiryWorkscope inquiryworkscope = inquiry.InquiryWorkscopes.FirstOrDefault(x =>
                        x.InquiryWorkscopeId == reason.inquiryWorkscopeId && x.IsActive == true &&
                        x.IsDeleted == false);
                    switch (reason.rejectionType)
                    {
                        case (int)permission.ManageMeasurement:
                            inquiry.InquiryStatusId = (int)inquiryStatus.measurementRejected;
                            inquiryworkscope.InquiryStatusId = (int)inquiryStatus.measurementRejected;
                            inquiryworkscope.Comments = reason.reason;
                            break;

                        case (int)permission.ManageDesign:
                            inquiry.InquiryStatusId = (int)inquiryStatus.designRejected;
                            inquiryworkscope.InquiryStatusId = (int)inquiryStatus.designRejected;
                            inquiryworkscope.Comments = reason.reason;
                            break;

                        case (int)permission.ManageQuotation:
                            inquiry.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                            foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                            {
                                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                            }

                            inquiry.InquiryComment = reason.reason;
                            break;

                        default:
                            SentrySdk.CaptureMessage(reason.rejectionType +
                                                     " reason.rejectionType");
                            break;
                    }
                }

                inquiryRepository.Update(inquiry);
                context.SaveChanges();
                response.data = inquiry;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Does not exist";
            }

            return response;
        }
    }
}