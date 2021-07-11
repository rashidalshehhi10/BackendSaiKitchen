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
        public object InquiryChecklist(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.waitingForAdvance)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
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
        public object ApproveinquiryChecklist(JobOrder jobOrder)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == jobOrder.InquiryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            JobOrder _jobOrder = new JobOrder();
            if (inquiry!=null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.checklistAccepted;

                _jobOrder.JobOrderDescription = jobOrder.JobOrderDescription;
                _jobOrder.JobOrderDeliveryDate = jobOrder.JobOrderDeliveryDate;
                _jobOrder.JobOrderExpectedDeadline = jobOrder.JobOrderExpectedDeadline;
                _jobOrder.JobOrderRequestedComments = jobOrder.JobOrderRequestedComments;
                _jobOrder.IsActive = true;
                _jobOrder.IsDeleted = false;
                inquiry.JobOrders.Add(_jobOrder);
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
                response.data = jobOrder;
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
        public object RejectinquiryChecklist(JobOrder jobOrder)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == jobOrder.InquiryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            JobOrder _jobOrder = new JobOrder();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.checklistRejected;
                
                _jobOrder.JobOrderDelayReason = jobOrder.JobOrderDelayReason;
                _jobOrder.JobOrderDescription = jobOrder.JobOrderDescription;
                _jobOrder.JobOrderDeliveryDate = jobOrder.JobOrderDeliveryDate;
                _jobOrder.JobOrderExpectedDeadline = jobOrder.JobOrderExpectedDeadline;
                _jobOrder.JobOrderRequestedComments = jobOrder.JobOrderRequestedComments;
                _jobOrder.IsActive = true;
                _jobOrder.IsDeleted = false;
                inquiry.JobOrders.Add(_jobOrder);
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
                response.data = jobOrder;
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
