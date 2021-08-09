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
    public class CheckListController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetinquiryChecklistDetailsById(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance || x.InquiryStatusId == (int)inquiryStatus.checklistPending))
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
                && (y.PaymentStatusId == (int)paymentstatus.PaymentApproved || y.PaymentTypeId == (int)paymenttype.AdvancePayment)||
                (y.PaymentTypeId==(int)paymenttype.Installment && y.PaymentStatusId ==(int)paymentstatus.InstallmentApproved))).FirstOrDefault();
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
            && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance || x.InquiryStatusId == (int)inquiryStatus.checklistPending)).Select(x => new CheckListByBranch
            {
                InquiryId = x.InquiryId,
                QuotationNo = "INV" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" +x.Quotations.OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive ==true && y.IsDeleted== false).QuotationId,
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
            });
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
            && (x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance|| x.InquiryStatusId == (int)inquiryStatus.checklistPending))
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
            if(inquiry != null)
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
        public async Task<object> ApproveinquiryChecklist(CustomCheckListapprove approve)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == approve.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(z => z.IsActive == true && z.IsDeleted == false)
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            JobOrder _jobOrder = new JobOrder();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.checklistAccepted;
                _jobOrder.JobOrderExpectedDeadline = approve.InstallationDate;
                _jobOrder.JobOrderRequestedComments = approve.Comment;

                var fileurl = await Helper.Helper.UploadFile(approve.file);
                if (approve.fileplace != null)
                {
                    foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                    {
                        switch (approve.fileplace)
                        {
                            case "Measurement":

                                break;
                            case "Design":

                                break;
                            case "Quotation":

                                break;
                        }
                    }
                }
                inquiry.JobOrders.Add(_jobOrder);
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == reject.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            JobOrder _jobOrder = new JobOrder();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = reject.inquiystatusId;
                inquiry.InquiryComment = reject.Comment;
                //Need To Change
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
