using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddUpdateDesignfiles(DesignCustomModel designCustomModel)
        {
            files.Clear();
            var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == designCustomModel.inquiryWorkScopeId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId==(int)inquiryStatus.designPending || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.designRejected)).FirstOrDefault();
            Design design = new Design();
            foreach (var file in designCustomModel.base64f3d)
            {
                string fileUrl = await Helper.Helper.UploadFileToBlob(file);

                if (fileUrl != null)
                {
                    files.Add(new File()
                    {
                        FileUrl = fileUrl,
                        FileName = fileUrl.Split('.')[0],
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
            List<int?> roletypeId = new List<int?>();

            roletypeId.Add((int)roleType.Manager);
            sendNotificationToHead(Constants.DesignAdded + Constants.userId, true,
                Url.Action("AcceptDesing", "DesignController", new { id = inquiryworkscope.InquiryWorkscopeId }),
                Url.Action("DeclineDesing", "DesignController", new { id = inquiryworkscope.InquiryWorkscopeId }),
               roletypeId, Constants.branchId,(int)notificationCategory.Design);
            design.IsActive = true;
            design.IsDeleted = false;
            design.DesignComment = designCustomModel.comment;
            design.Files = files;
            inquiryworkscope.Comments = designCustomModel.comment;
            inquiryworkscope.InquiryStatusId = (int)inquiryStatus.designWaitingForApproval;
            inquiryworkscope.Designs.Add(design);
            inquiryWorkscopeRepository.Update(inquiryworkscope);
            context.SaveChanges();
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object AcceptDesign(int id)
        {
            var inquiryWS = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == id && i.IsActive == true && i.IsDeleted == false).FirstOrDefault();
            if (inquiryWS != null)
            {
                inquiryWS.InquiryStatusId = (int)inquiryStatus.quotationPending;
                inquiryWS.IsDesignApproved = true;
                inquiryWorkscopeRepository.Update(inquiryWS);
                context.SaveChanges();
                List<int?> roleTypeId = new List<int?>();
                roleTypeId.Add((int)roleType.Manager);

                sendNotificationToHead(inquiryWS.DesignAssignedTo + " Upload the Design", false, null, null, roleTypeId, Constants.branchId, (int)notificationCategory.Design);

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
        public object DeclineDesign(int id)
        {
            var inquiryWS = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == id && i.IsActive == true && i.IsDeleted == false).FirstOrDefault();
            if (inquiryWS != null)
            {
                inquiryWS.InquiryStatusId = (int)inquiryStatus.designRejected;
                inquiryWorkscopeRepository.Update(inquiryWS);

                sendNotificationToOneUser("Your Design is Rejected Please Upload another one", false, null, null, (int)inquiryWS.DesignAssignedTo, Constants.branchId, (int)notificationCategory.Design);
                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }
            return response;

        }
    }
}
