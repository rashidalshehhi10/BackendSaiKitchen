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
using System.Web;

namespace BackendSaiKitchen.Controllers
{
    public class DesignController : BaseController
    {
        public DesignController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }

        static List<File> files = new List<File>();
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddUpdateDesignfiles(DesignCustomModel designCustomModel)
        {
           
            files.Clear();
            var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == designCustomModel.inquiryWorkScopeId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId==(int)inquiryStatus.designPending || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.designRejected)).FirstOrDefault();
            Design design = new Design();
            
            foreach (var file in designCustomModel.base64f3d)
            {
                var fileUrl = await Helper.Helper.UploadFileToBlob(file);

                if (fileUrl != null)
                {
                    design.Files.Add(new File()
                    {
                        FileUrl = fileUrl.Item1,
                        FileName = fileUrl.Item1.Split('.')[0],
                        FileContentType = fileUrl.Item2,
                        IsImage = true,
                        IsActive = true,
                        IsDeleted = false,
                        UpdatedBy = Constants.userId,
                        UpdatedDate = Helper.Helper.GetDateTime(),
                        CreatedBy = Constants.userId,
                        CreatedDate = Helper.Helper.GetDateTime(),
                        
                    }) ;

                }
                else
                {
                    response.isError = true;
                    response.errorMessage = Constants.wrongFileUpload;
                }
            }
            foreach (var file in designCustomModel.videobase64)
            {
                var fileUrl = await Helper.Helper.UploadUpdateVideo(file);

                if (fileUrl != null)
                {
                    design.Files.Add(new File()
                    {
                        FileUrl = fileUrl.Item1,
                        FileName = fileUrl.Item1.Split('.')[0],
                        FileContentType = fileUrl.Item2,
                        IsImage = false,
                        IsActive = true,
                        IsDeleted = false,
                        UpdatedBy = Constants.userId,
                        UpdatedDate = Helper.Helper.GetDateTime(),
                        CreatedBy = Constants.userId,
                        CreatedDate = Helper.Helper.GetDateTime(),
                    });
                }
            }
            
            List<int?> roletypeId = new List<int?>();

            roletypeId.Add((int)roleType.Manager);
           
            try
            {
                sendNotificationToHead(Constants.DesignAdded, true,
                    Url.Action("AcceptDesing", "DesignController", new { id = inquiryworkscope.InquiryWorkscopeId }),
                    Url.Action("DeclineDesing", "DesignController", new { id = inquiryworkscope.InquiryWorkscopeId }),
                   roletypeId, Constants.branchId, (int)notificationCategory.Design);
            }
            catch (Exception e)
            {
                Sentry.SentrySdk.CaptureMessage(e.Message);
            }

            design.IsActive = true;
            design.IsDeleted = false;
            design.DesignComment = designCustomModel.comment;
            inquiryworkscope.Comments = designCustomModel.comment;
            inquiryworkscope.InquiryStatusId = (int)inquiryStatus.designWaitingForApproval;
            inquiryworkscope.Designs.Add(design);
            inquiryWorkscopeRepository.Update(inquiryworkscope);
            context.SaveChanges();
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object AcceptDesign(UpdateInquiryWorkscopeStatusModel updateInquiryStatus)
        {
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateInquiryStatus.InquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationPending;
                inquiryWorkscope.IsDesignApproved = true;
                inquiryWorkscopeRepository.Update(inquiryWorkscope);
                List<int?> roleTypeId = new List<int?>();
                roleTypeId.Add((int)roleType.Manager);
                 try
                {
                    sendNotificationToHead(inquiryWorkscope.DesignAssignedTo + " Upload the Design", false, null, null, roleTypeId, Constants.branchId, (int)notificationCategory.Design);
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
        public object DeclineDesign(UpdateInquiryWorkscopeStatusModel updateInquiryStatus)
        {
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateInquiryStatus.InquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false).Include(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designRejected;
                inquiryWorkscope.DesignAssignedTo = updateInquiryStatus.DesignAssignedTo;
                inquiryWorkscope.DesignScheduleDate = updateInquiryStatus.DesignScheduleDate;
                inquiryWorkscope.Comments = updateInquiryStatus.DesignComment;
                Helper.Helper.Each(inquiryWorkscope.Designs, i => {
                    i.IsActive = false;
                    i.DesignComment = updateInquiryStatus.DesignComment;
                });

                inquiryWorkscopeRepository.Update(inquiryWorkscope);
                try
                {
                    sendNotificationToOneUser("Your Design is Rejected Please Upload another one", false, null, null, (int)inquiryWorkscope.DesignAssignedTo, Constants.branchId, (int)notificationCategory.Design);
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
        public object ViewDesignById(int inquiryWorkscopeId)
        {
            var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == inquiryWorkscopeId && x.InquiryStatusId == (int)inquiryStatus.designWaitingForApproval  && x.IsActive == true && x.IsDeleted == false && x.Designs.Count > 0).Include(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false && y.Files.Any(z => z.IsActive == true && z.IsDeleted == false))).ThenInclude(y => y.Files.Where(z => z.IsActive == true && z.IsDeleted == false)).FirstOrDefault();
            if (inquiryworkscope != null)
            {
                response.data = inquiryworkscope;
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
        public async Task<object> UplaodVideo(byte[] file)
        {
             
            
            response.data = await Helper.Helper.UploadUpdateVideo(file);
            return response;
        }
    }
}
