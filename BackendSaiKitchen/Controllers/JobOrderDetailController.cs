﻿using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Linq;

namespace BackendSaiKitchen.Controllers
{
    public class JobOrderDetailController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object GetInquiryJobOrderDetailsByBranchId(int branchId)
        {
            System.Collections.Generic.List<CheckListByBranch> inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && (x.BranchId == branchId || x.JobOrders.Any(y => y.IsActive == true && y.IsDeleted == false && y.FactoryId == branchId))
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayed || x.InquiryStatusId == (int)inquiryStatus.jobOrderCompleted))
                .Select(x => new CheckListByBranch
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
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.MeasurementAddedOn).FirstOrDefault(),
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
            Models.Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false
            && (x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleApproved || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderReadyForInstallation || x.InquiryStatusId == (int)inquiryStatus.jobOrderCompleted || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayed || x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderFilesDelayed))
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
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                Inquirychecklist inquirychecklist = new Inquirychecklist()
                {
                    inquiry = inquiry,
                    fees = FeesRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
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
            Models.Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayed))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderInProgress;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderInProgress;
                });

                foreach (Models.JobOrder joborder in inquiry.JobOrders)
                {
                    foreach (Models.JobOrderDetail jobOrderDetail in joborder.JobOrderDetails)
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
            Models.Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == job.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                 .FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderInProgress;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderInProgress;
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
            Models.Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == job.inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Comments.Where(x => x.IsActive == true && x.IsDeleted == false))
                .FirstOrDefault();
            //.Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
            //.ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderRescheduleRejected;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderRescheduleRejected;
                });
                foreach (Models.JobOrder joborder in inquiry.JobOrders)
                {
                    Helper.Helper.Each(joborder.JobOrderDetails, x =>
                    {
                        x.IsActive = false;
                    });
                }
                if (job.Reason != string.Empty || job.Reason != null)
                {
                    inquiry.InquiryComment = job.Reason;
                    inquiry.Comments.Add(new Comment
                    {
                        CommentAddedBy = Constants.userId,
                        CommentAddedon = Helper.Helper.GetDateTime(),
                        CommentName = Enum.GetName(typeof(inquiryStatus), inquiry.InquiryStatusId),
                        CommentDetail = job.Reason,
                        InquiryStatusId = inquiry.InquiryStatusId,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedDate = Helper.Helper.GetDateTime(),
                        CreatedBy = Constants.userId,
                    });
                }
                
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
            Models.Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == order.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleApproved))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderDelayed;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderDelayed;
                });
                foreach (Models.JobOrder joborder in inquiry.JobOrders)
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
            Models.Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == ready.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRequested || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleRejected || x.InquiryStatusId == (int)inquiryStatus.jobOrderRescheduleApproved || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayRequested))
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {

                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderReadyForInstallation;
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderReadyForInstallation;
                });

                foreach (Models.JobOrder joborder in inquiry.JobOrders)
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
            Models.Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == install.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderCompleted;
                inquiry.InquiryEndDate = Helper.Helper.GetDateTime();
                Helper.Helper.Each(inquiry.InquiryWorkscopes, x =>
                {
                    x.InquiryStatusId = (int)inquiryStatus.jobOrderCompleted;
                });

                foreach (Models.JobOrder joborder in inquiry.JobOrders)
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
