using BackendSaiKitchen.ActionFilters;
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
    public class DesignController : BaseController
    {
        public DesignController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }

        static List<File> files = new List<File>();

        
        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddUpdateDesignfiles(DesignCustomModel designCustomModel)
        {

            files.Clear();
            var inquiry = InquiryRepository.FindByCondition(x => x.InquiryId == designCustomModel.inquiryId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.designPending || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.designRevisionRequested || x.InquiryStatusId == (int)inquiryStatus.designRejected))
                .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.Customer).FirstOrDefault();
            if (designCustomModel.base64f3d != null && designCustomModel.base64f3d.Count > 0)
            {
                Design design;
                foreach (var inquiryworkscope in inquiry.InquiryWorkscopes)
                {
                    design = new Design();
                    foreach (var fileUrl in designCustomModel.base64f3d)
                    {


                        if (fileUrl != null)
                        {
                            design.Files.Add(new File
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
                                CreatedDate = Helper.Helper.GetDateTime(),

                            });

                        }
                        else
                        {
                            response.isError = true;
                            response.errorMessage = Constants.wrongFileUpload;
                        }
                    }
                    design.IsActive = true;
                    design.IsDeleted = false;
                    design.DesignComment = designCustomModel.comment;
                    design.DesignAddedBy = Constants.userId;
                    design.DesignAddedDate = Helper.Helper.GetDateTime();
                    inquiryworkscope.Comments = designCustomModel.comment;
                    inquiryworkscope.DesignAddedOn = Helper.Helper.GetDateTime();
                    inquiryworkscope.InquiryStatusId = (int)inquiryStatus.designWaitingForApproval;
                    inquiryworkscope.Designs.Add(design);

                }
                List<int?> roletypeId = new List<int?>();

                roletypeId.Add((int)roleType.Manager);

                try
                {
                    sendNotificationToHead(Constants.DesignAdded + inquiry.Branch + "" + inquiry.CustomerId + "" + inquiry.InquiryId, false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null,
                       roletypeId, Constants.branchId, (int)notificationCategory.Design);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }

                inquiry.InquiryStatusId = (int)inquiryStatus.designWaitingForApproval;
                InquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Please Add Files";
            }

            return response;
        }

        [AuthFilter((int)permission.ManageDesign, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AcceptDesignAsync(UpdateInquiryWorkscopeStatusModel updateInquiryStatus)
        {
            var inquiry = InquiryRepository.FindByCondition(x => x.InquiryId == updateInquiryStatus.Id && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.Customer).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.designWaitingForCustomerApproval;
                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designWaitingForCustomerApproval;
                    inquiryWorkscope.IsDesignApproved = true;
                }

                InquiryRepository.Update(inquiry);
                List<int?> roleTypeId = new List<int?>();
                roleTypeId.Add((int)roleType.Manager);

                try
                {
                    sendNotificationToHead("Design Of inquiry Code:IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Is Waiting for customer Approval"
                       , false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roleTypeId, Constants.branchId, (int)notificationCategory.Design);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
                try
                {
                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    await mailService.SendDesignEmailAsync(inquiry.Customer.CustomerEmail, inquiry.InquiryCode, Constants.CRMBaseUrl + "/viewdesigncustomer.html?inquiryWorkscopeId=" + updateInquiryStatus.Id, Constants.ServerBaseURL + "/api/Design/ClientAcceptDesign?id=" + updateInquiryStatus.Id, Constants.ServerBaseURL + "/api/Design/ClientRejectDesign?id=" + updateInquiryStatus.Id);
                }

                catch (Exception ex)
                {
                    Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
                }
                context.SaveChanges();

            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }

            return response;
        }


        [AuthFilter((int)permission.ManageDesign, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object DeclineDesign(UpdateInquiryWorkscopeStatusModel updateInquiryStatus)
        {
            var inquiry = InquiryRepository.FindByCondition(x => x.InquiryId == updateInquiryStatus.Id && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Customer).Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.designRevisionRequested;
                inquiry.InquiryComment = updateInquiryStatus.DesignComment;
                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designRevisionRequested;
                    inquiryWorkscope.DesignAssignedTo = updateInquiryStatus.DesignAssignedTo;
                    inquiryWorkscope.DesignScheduleDate = updateInquiryStatus.DesignScheduleDate;
                    inquiryWorkscope.Comments = updateInquiryStatus.DesignComment;
                    Helper.Helper.Each(inquiryWorkscope.Designs, i =>
                    {
                        i.IsActive = false;
                        i.DesignComment = updateInquiryStatus.DesignComment;
                    });


                }

                InquiryRepository.Update(inquiry);
                try
                {
                    sendNotificationToOneUser("Your Design Of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " is Rejected Comment: " + inquiry.InquiryComment, false
                        , " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, (int)inquiry.InquiryWorkscopes.FirstOrDefault().DesignAssignedTo, Constants.branchId, (int)notificationCategory.Design);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }

                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }
            return response;

        }

        
        [HttpPost]
        [Route("[action]")]
        public object ViewDesignById(int inquiryId)
        {
            var inquiry = InquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == inquiryId && x.InquiryStatusId != (int)inquiryStatus.measurementPending && x.InquiryStatusId != (int)inquiryStatus.measurementdelayed)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && y.Measurements.Count > 0))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false && y.Files.Any(z => z.IsActive == true && z.IsDeleted == false)))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                if (inquiry.InquiryWorkscopes.FirstOrDefault() != null)
                {
                    response.data = inquiry.InquiryWorkscopes.FirstOrDefault();
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = Constants.DesginMissing;
            }
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object ClientAcceptDesign(UpdateInquiryWorkscopeStatusModel updateInquiryStatus)
        {
            var inquiry = InquiryRepository.FindByCondition(x => x.InquiryId == updateInquiryStatus.Id && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.quotationSchedulePending;
                inquiry.InquiryComment = updateInquiryStatus.DesignComment;
                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationSchedulePending;
                    inquiryWorkscope.IsDesignApproved = true;
                    inquiryWorkscope.FeedbackReaction = updateInquiryStatus.FeedBackReaction;
                    inquiryWorkscope.Designs.FirstOrDefault().DesignCustomerReviewDate = Helper.Helper.GetDateTime();
                    inquiryWorkscope.Comments = updateInquiryStatus.DesignComment;
                    Helper.Helper.Each(inquiryWorkscope.Designs, i =>
                    {
                        i.DesignComment = updateInquiryStatus.DesignComment;
                    });

                }
                InquiryRepository.Update(inquiry);
                List<int?> roleTypeId = new List<int?>();
                roleTypeId.Add((int)roleType.Manager);

                try
                {
                    sendNotificationToHead("Customer Approved the Design Of inquiry Code:IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Comment : " + inquiry.InquiryComment
                        , false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roleTypeId, Constants.branchId, (int)notificationCategory.Design);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }
            return response;

        }

        [HttpPost]
        [Route("[action]")]
        public object ClientRejectDesign(UpdateInquiryWorkscopeStatusModel updateInquiryStatus)
        {
            var inquiry = InquiryRepository.FindByCondition(x => x.InquiryId == updateInquiryStatus.Id && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.designRejectedByCustomer;
                inquiry.InquiryComment = updateInquiryStatus.DesignComment;
                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designRejectedByCustomer;
                    inquiryWorkscope.IsDesignApproved = true;
                    inquiryWorkscope.FeedbackReaction = updateInquiryStatus.FeedBackReaction;
                    inquiryWorkscope.Comments = updateInquiryStatus.DesignComment;
                    Helper.Helper.Each(inquiryWorkscope.Designs, i =>
                    {
                        i.DesignComment = updateInquiryStatus.DesignComment;
                        i.DesignCustomerReviewDate = Helper.Helper.GetDateTime();
                    });
                }
                InquiryRepository.Update(inquiry);

                List<int?> roleTypeId = new List<int?>();
                roleTypeId.Add((int)roleType.Manager);
                try
                {
                    sendNotificationToOneUser("Customer Rejected the Design Of inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Comment : " + inquiry.InquiryComment,
                        false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, (int)inquiry.ManagedBy, (int)inquiry.BranchId, (int)notificationCategory.Design);
                    sendNotificationToHead("Customer Rejected the Design Of inquiry Code: IN" + +inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Comment : " + inquiry.InquiryComment, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roleTypeId, inquiry.BranchId, (int)notificationCategory.Design);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
                context.SaveChanges();
            }

            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }
            return response;

        }

        [HttpPost]
        [Route("[action]")]
        public object ApproveDesignAssignee(UpdateInquiryWorkscopeStatusModel updateInquiry)
        {
            var inquiry = InquiryRepository.FindByCondition(x => x.InquiryId == updateInquiry.Id && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.designPending;
                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designPending;
                }
                InquiryRepository.Update(inquiry);
                List<int?> roletypeId = new List<int?>();
                roletypeId.Add((int)roleType.Manager);
                try
                {
                    var user = UserRepository.FindByCondition(x => x.UserId == inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo && x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();
                    sendNotificationToHead(user.Name + " Accepted Designing The Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId
                        , false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roletypeId, Constants.branchId, (int)notificationCategory.Measurement);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
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
        public object RejectDesignAssignee(UpdateInquiryWorkscopeStatusModel updateInquiry)
        {
            var inquiry = InquiryRepository.FindByCondition(x => x.InquiryId == updateInquiry.Id && x.IsActive == true && x.IsDeleted == false)
                   .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                   .Include(x => x.Customer).FirstOrDefault();

            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.designAssigneeRejected;
                inquiry.InquiryComment = updateInquiry.DesignComment;
                foreach (var inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designAssigneeRejected;
                    inquiryWorkscope.Comments = updateInquiry.DesignComment;
                }
                InquiryRepository.Update(inquiry);
                List<int?> roletypeId = new List<int?>();
                roletypeId.Add((int)roleType.Manager);
                try
                {
                    var user = UserRepository.FindByCondition(x => x.UserId == inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo && x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();
                    sendNotificationToHead(user.Name + " Rejected Designing The Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Comment: " + inquiry.InquiryComment
                        , false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roletypeId, Constants.branchId, (int)notificationCategory.Measurement);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
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
