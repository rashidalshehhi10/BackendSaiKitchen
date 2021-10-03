using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackendSaiKitchen.Controllers
{
    public class CheckListController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetinquiryChecklistDetailsById(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance || x.InquiryStatusId == (int)inquiryStatus.checklistPending || x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Workscope)
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false
                && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentTypeId == (int)paymenttype.AdvancePayment) ||
                (y.PaymentTypeId == (int)paymenttype.Installment && y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            // && (y.PaymentTypeId == (int)paymenttype.AdvancePayment || y.PaymentTypeId == (int)paymenttype.Installment))).FirstOrDefault();
            if (inquiry != null)
            {
                Inquirychecklist inquirychecklist = new Inquirychecklist()
                {
                    inquiry = inquiry,
                    fees = feesRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
                };
                if (inquirychecklist == null)
                {
                    response.isError = true;
                    response.errorMessage = "No Inquiry Found";
                }
                else
                {
                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Workscope)
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false
                && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentTypeId == (int)paymenttype.AdvancePayment) ||
                (y.PaymentTypeId == (int)paymenttype.Installment && y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            // && (y.PaymentTypeId == (int)paymenttype.AdvancePayment || y.PaymentTypeId == (int)paymenttype.Installment))).FirstOrDefault();
            if (inquiry != null)
            {
                Inquirychecklist inquirychecklist = new Inquirychecklist()
                {
                    inquiry = inquiry,
                    fees = feesRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
                };
                if (inquirychecklist == null)
                {
                    response.isError = true;
                    response.errorMessage = "No Inquiry Found";
                }
                else
                {
                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
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
        public object GetAllInquiryChecklist()
        {
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance || x.InquiryStatusId == (int)inquiryStatus.checklistPending));
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
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
            && (x.InquiryStatusId == (int)inquiryStatus.checklistPending || x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected)).Select(x => new CheckListByBranch
            {
                InquiryId = x.InquiryId,
                QuotationNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationId,
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
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
            && (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected)).Select(x => new CheckListByBranch
            {
                InquiryId = x.InquiryId,
                QuotationNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationId,
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
        public object InquiryChecklist(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance || x.InquiryStatusId == (int)inquiryStatus.checklistPending || x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false
                && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentStatusId == (int)paymentstatus.InstallmentApproved)
                && (y.PaymentTypeId == (int)paymenttype.AdvancePayment || y.PaymentTypeId == (int)paymenttype.Installment))).FirstOrDefault();
            if (inquiry != null)
            {
                List<int?> roleTypeId = new List<int?>();
                roleTypeId.Add((int)roleType.Manager);

                try
                {
                    sendNotificationToHead("Inquiry " + inquiryId + "  Waiting for approval on CheckList", false, null, null, roleTypeId, Constants.branchId, (int)notificationCategory.Other);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == approve.inquiryId && x.IsActive == true && x.IsDeleted == false)// && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance || x.InquiryStatusId == (int)inquiryStatus.checklistPending || x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected))
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
                foreach (var joborder in inquiry.JobOrders)
                {
                    joborder.Comments = approve.Comment;
                    joborder.JobOrderChecklistFileUrl = approve.jobOrderChecklistFileUrl;
                    joborder.JobOrderExpectedDeadline = approve.jobOrderExpectedDeadline;
                    joborder.FactoryId = approve.factoryId;
                    joborder.SiteMeasurementMatchingWithDesign = approve.SiteMeasurementMatchingWithDesign;
                    joborder.SiteMeasurementMatchingWithDesignNotes = approve.SiteMeasurementMatchingWithDesignNotes;
                    joborder.MaterialConfirmation = approve.MatrialConfirmation;
                    joborder.MaterialConfirmationNotes = approve.MatrialConfirmationNotes;
                    joborder.Mepdrawing = approve.MEPDrawing;
                    joborder.MepdrawingNotes = approve.MEPDrawingNotes;
                    joborder.QuotationAndCalculationSheetMatchingProposal = approve.QuotationAndCalculationSheetMatchingProposal;
                    joborder.QuotationAndCalculationSheetMatchingProposalNotes = approve.QuotationAndCalculationSheetMatchingProposalNotes;
                    joborder.ApprovedDrawingsAndAvailabilityOfClientSignature = approve.ApprovedDrawingsAndAvailabilityOfClientSignture;
                    joborder.ApprovedDrawingsAndAvailabilityOfClientSignatureNotes = approve.ApprovedDrawingsAndAvailabilityOfClientSigntureNotes;
                    joborder.AppliancesDataSheet = approve.AppliancesDataSheet;
                    joborder.AppliancesDataSheetNotes = approve.AppliancesDataSheetNotes;

                }

                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.commercialChecklistPending;
                }


                if (approve.addFileonChecklists.Count != 0 && approve.addFileonChecklists != null)
                {
                    for (int i = 0; i < approve.addFileonChecklists.Count; i++)
                    {

                        try
                        {
                            foreach (var fileUrl in approve.addFileonChecklists[i].files)
                            {
                                if (fileUrl != null)
                                {
                                    //var fileUrl = await Helper.Helper.UploadFile(file);

                                    switch (approve.addFileonChecklists[i].documentType)
                                    {
                                        case (int)permission.ManageMeasurement:
                                            foreach (var inquiryworkscope in inquiry.InquiryWorkscopes)
                                            {
                                                var measurement = inquiryworkscope.Measurements.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false);
                                                measurement.Files.Add(new Models.File
                                                {
                                                    FileUrl = fileUrl,
                                                    FileName = fileUrl.Split('.')[0],
                                                    FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                                    IsImage = false,
                                                    IsActive = true,
                                                    IsDeleted = false,
                                                });
                                            }
                                            break;
                                        case (int)permission.ManageDesign:
                                            foreach (var inquiryworkscope in inquiry.InquiryWorkscopes)
                                            {
                                                var design = inquiryworkscope.Designs.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false);
                                                design.Files.Add(new Models.File
                                                {
                                                    FileUrl = fileUrl,
                                                    FileName = fileUrl.Split('.')[0],
                                                    FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                                    IsImage = false,
                                                    IsActive = true,
                                                    IsDeleted = false,
                                                });
                                            }
                                            break;
                                        case (int)permission.ManageQuotation:
                                            var quotation = inquiry.Quotations.FirstOrDefault(x => x.IsActive == true && x.IsDeleted == false);
                                            quotation.Files.Add(new Models.File
                                            {
                                                FileUrl = fileUrl,
                                                FileName = fileUrl.Split('.')[0],
                                                FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                                IsImage = false,
                                                IsActive = true,
                                                IsDeleted = false,
                                            });
                                            break;
                                    }
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Sentry.SentrySdk.CaptureMessage(ex.Message);
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
        public object ApproveinquiryCommericalChecklist(CustomCommercialCheckListApproval approve)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == approve.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderFactoryApprovalPending;
                inquiry.InquiryComment = approve.Reason;

                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.jobOrderFactoryApprovalPending;
                    inquiryWorkscope.Comments = approve.Reason;
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
        public object RejectinquiryCommericalChecklist(CustomCommercialCheckListApproval Reject)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == Reject.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.commercialChecklistRejected;
                inquiry.InquiryComment = Reject.Reason;

                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.commercialChecklistRejected;
                    inquiryWorkscope.Comments = Reject.Reason;
                }

                foreach (var joboreder in inquiry.JobOrders)
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == reject.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance || x.InquiryStatusId == (int)inquiryStatus.checklistPending || x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            JobOrder _jobOrder = new JobOrder();
            if (inquiry != null)
            {
                foreach (var reason in reject.Addrejections)
                {
                    var inquiryworkscope = inquiry.InquiryWorkscopes.FirstOrDefault(x => x.InquiryWorkscopeId == reason.inquiryWorkscopeId && x.IsActive == true && x.IsDeleted == false);
                    switch (reason.rejectionType)
                    {
                        case (int)permission.ManageMeasurement:
                            inquiryworkscope.InquiryStatusId = (int)inquiryStatus.measurementRejected;
                            inquiryworkscope.Comments = reason.reason;
                            break;

                        case (int)permission.ManageDesign:
                            inquiryworkscope.InquiryStatusId = (int)inquiryStatus.designRejected;
                            inquiryworkscope.Comments = reason.reason;
                            break;

                        case (int)permission.ManageQuotation:
                            inquiry.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                            foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                            {
                                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationRejected;
                            }
                            inquiry.InquiryComment = reason.reason;
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
