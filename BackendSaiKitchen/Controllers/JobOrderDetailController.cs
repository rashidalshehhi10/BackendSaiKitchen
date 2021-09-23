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
    public class JobOrderDetailController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetInquiryJobOrderDetailsByBranchId(int branchId)
        {
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryAccepted || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleApproved || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderReadyForInstallation || x.InquiryStatusId == (int)inquiryStatus.jobOrderCompleted)).Select(x => new CheckListByBranch
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

        [HttpPost]
        [Route("[action]")]
        public object GetinquiryJobOrderDetailsById(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryAccepted || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleApproved || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderReadyForInstallation || x.InquiryStatusId == (int)inquiryStatus.jobOrderCompleted))
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
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false )).FirstOrDefault();
            
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
        public object RequestForRescheduling(CustomJobOrder order)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryAccepted || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderRescheduleRequested;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderRescheduleRequested;
                });

                foreach (var joborder in inquiry.JobOrders)
                {
                    foreach (var jobOrderDetail in joborder.JobOrderDetails)
                    {
                        jobOrderDetail.JobOrderDetailDescription = order.Notes;
                        jobOrderDetail.CreatedBy = Constants.userId;
                        jobOrderDetail.CreatedDate = Helper.Helper.GetDateTime();
                        jobOrderDetail.InstallationStartDate = order.installationStartDate;
                        //jobOrderDetail.InstallationEndDate = order.installationEndDate;
                        jobOrderDetail.IsNewlyRequested = false;
                        jobOrderDetail.IsFromFactory = branchRepository?.FindByCondition(x => x.BranchId == inquiry.JobOrders.FirstOrDefault().FactoryId && x.IsActive == true && x.IsDeleted == false)?.FirstOrDefault()?.BranchTypeId == 3 ? true : false;
                    }
                }

                response.data = "JobOrder Detail Reschedule Requested";
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
        public object JobOrderDetailRescheduleApprove(JobOrderFactory job)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == job.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                 .FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderRescheduleApproved;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderRescheduleApproved;
                });
               
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
        public object JobOrderDetailRescheduleReject(JobOrderFactory job)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == job.inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
                //.Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                //.ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderRescheduleRejected;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderRescheduleRejected;
                });
                foreach (var joborder in inquiry.JobOrders)
                {
                    Helper.Helper.Each(joborder.JobOrderDetails, x =>
                    {
                        x.IsActive = false;
                    });
                }
                inquiry.InquiryComment = job.Reason;
               
                response.data = "JobOrder Detail Reschedule Rejected";
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
        public object JobOrderDelayRequested(CustomJobOrder order)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryAccepted || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleApproved))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderDelayRequested;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderDelayRequested;
                });
                foreach (var joborder in inquiry.JobOrders)
                {
                    Helper.Helper.Each(joborder.JobOrderDetails, x =>
                    {
                        x.InstallationStartDate = order.installationStartDate;
                        x.JobOrderDetailDescription = order.Notes;
                    });

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
        public object ReadyToInstall(Install ready)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == ready.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryAccepted || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleApproved || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayRequested))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {

                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderReadyForInstallation;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderReadyForInstallation;
                });

                foreach (var joborder in inquiry.JobOrders)
                {
                    Helper.Helper.Each(joborder.JobOrderDetails, x =>
                    {
                        x.InstallationStartDate = ready.installationStartDate;
                    });
                }
                inquiryRepository.Update(inquiry);
                response.data = "JobOrder Ready To Install";
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
        public object JobOrderCompleted(Install install)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == install.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderCompleted;

                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderCompleted;
                });

                foreach (var joborder in inquiry.JobOrders)
                {
                    Helper.Helper.Each(joborder.JobOrderDetails, x =>
                    {
                        x.InstallationEndDate = Helper.Helper.GetDateTime();
                        x.Remarks = install.Remark;
                        x.JobOrderDetailDescription = install.JobOrderDetailsDescription;
                    });
                }
                response.data = "JobOrder Completed";
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
    }
}
