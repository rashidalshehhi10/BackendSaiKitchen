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
using File = BackendSaiKitchen.Models.File;

namespace BackendSaiKitchen.Controllers
{
    public class MeasurementController : BaseController
    {

        public MeasurementController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }

        private static readonly List<File> files = new List<File>();
        private static readonly List<Accesory> Accesories = new List<Accesory>();

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Create)]
        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddMeasrmuent(CustomMeasurement measurementVM)
        {
            files.Clear();
            Accesories.Clear();

            Measurement measurement = new Measurement
            {
                MeasurementComment = measurementVM.MeasurementComment
            };



            MeasurementDetail measurementDetail = new MeasurementDetail
            {
                MeasurementDetailCeilingHeight = measurementVM.MeasurementDetailCeilingHeight,
                MeasurementDetailCielingDiameter = measurementVM.MeasurementDetailCielingDiameter,
                MeasurementDetailCornishHeight = measurementVM.MeasurementDetailCornishHeight,
                MeasurementDetailCornishDiameter = measurementVM.MeasurementDetailCornishDiameter,
                MeasurementDetailDoorHeight = measurementVM.MeasurementDetailDoorHeight,
                MeasurementDetailPlinthHeight = measurementVM.MeasurementDetailPlinthHeight,
                MeasurementDetailPlinthDiameter = measurementVM.MeasurementDetailPlinthDiameter,
                MeasurementDetailSkirtingHeight = measurementVM.MeasurementDetailSkirtingHeight,
                MeasurementDetailSpotLightFromWall = measurementVM.MeasurementDetailSpotLightFromWall,
                IsActive = true,
                IsDeleted = false
            };


            WardrobeDesignInformation wDInfo = new WardrobeDesignInformation
            {
                WdiclosetType = measurementVM.WdiclosetType,
                WdiboardModel = measurementVM.WdiboardModel,
                WdiselectedColor = measurementVM.WdiselectedColor,
                WdiceilingHeight = measurementVM.WdiceilingHeight,
                WdiclosetHeight = measurementVM.WdiclosetHeight,
                WdistorageDoor = measurementVM.WdistorageDoor,
                WdidoorDesign = measurementVM.WdidoorDesign,
                WdihandleDesign = measurementVM.WdihandleDesign,
                WdidoorMaterial = measurementVM.WdidoorMaterial,
                Wdinotes = measurementVM.Wdinotes,
                IsActive = true,
                IsDeleted = false
            };




            foreach (CustomAccossries acc in measurementVM.Accesories)
            {
                Accesories.Add(new Accesory()
                {
                    AccesoriesName = acc.AccesoriesName,
                    AccesoriesValue = acc.AccesoriesValue,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Constants.userId,
                    CreatedDate = Helper.Helper.GetDateTime(),
                    UpdatedBy = Constants.userId,
                    UpdatedDate = Helper.Helper.GetDateTime(),
                });
            }


            if (measurementVM.base64img.Count > 0)
            {
                foreach (string fileUrl in measurementVM.base64img)
                {
                    // var fileUrl = await Helper.Helper.UploadFile(file);

                    if (fileUrl != null)
                    {
                        files.Add(new File()
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


                measurement.MeasurementTakenBy = Constants.userId;
                wDInfo.Accesories = Accesories;
                measurement.WardrobeDesignInfo = wDInfo;
                measurement.MeasurementDetail = measurementDetail;
                measurement.Files = files;
                measurementRepository.Create(measurement);
                response.data = measurement;


                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };


                try
                {
                    sendNotificationToHead(content: Constants.MeasurementAdded,
                     true, null,
                     Url.ActionLink("DeclineMeasurement", "MeasuementController", new { id = measurement.InquiryWorkscopeId }),
                     roletypeId,
                     Constants.branchId,
                     (int)notificationCategory.Measurement);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }

                context.SaveChanges();
                return response;


            }
            else
            {
                response.isError = true;
                response.errorMessage = "Kindly upload measurement file";
            }
            return response;
        }

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object AcceptMeasurement(UpdateInquiryWorkscopeStatusModel updateMeasurementStatus)
        {
            //var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateMeasurementStatus.Id && i.IsActive == true && i.IsDeleted == false)
            //    .Include(x => x.Inquiry)
            //    .Include(x => x.Workscope).FirstOrDefault();
            //please Provide inquiryId
            Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == updateMeasurementStatus.Id && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.Workscope)
                .Include(x => x.Customer).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designAssigneePending;
                    inquiryWorkscope.DesignAssignedTo = updateMeasurementStatus.DesignAssignedTo;
                    inquiryWorkscope.DesignScheduleDate = updateMeasurementStatus.DesignScheduleDate;
                }
                inquiry.InquiryStatusId = (int)inquiryStatus.designAssigneePending;
                inquiryRepository.Update(inquiry);
                try
                {
                    sendNotificationToOneUser(Constants.DesignAssign + " " + inquiry.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate + " Of Inquiry Code" + inquiry.InquiryCode,
                       false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, (int)inquiry.InquiryWorkscopes.FirstOrDefault().DesignAssignedTo, Constants.branchId, (int)notificationCategory.Design);
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

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object DeclineMeasurement(UpdateInquiryWorkscopeStatusModel updateMeasurementStatus)
        {
            //var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateMeasurementStatus.Id && i.IsActive == true && i.IsDeleted == false).Include(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();

            Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == updateMeasurementStatus.Id && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Customer)
                .Include(x => x.Comments.Where(x => x.IsActive == true && x.IsDeleted == false))
                .FirstOrDefault();
            if (inquiry != null)
            {
                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.measurementRejected;
                    inquiryWorkscope.MeasurementAssignedTo = updateMeasurementStatus.MeasurementAssignedTo;
                    inquiryWorkscope.MeasurementScheduleDate = updateMeasurementStatus.MeasurementScheduleDate;
                    inquiryWorkscope.Comments = updateMeasurementStatus.MeasurementComment;
                    Helper.Helper.Each(inquiryWorkscope.Measurements, i =>
                    {
                        i.IsActive = false;
                        i.MeasurementComment = updateMeasurementStatus.MeasurementComment;
                    });
                    inquiryWorkscopeRepository.Update(inquiryWorkscope);
                }
                inquiry.InquiryComment = updateMeasurementStatus.MeasurementComment;
                inquiry.Comments.Add(new Comment
                {
                    CommentAddedBy = Constants.userId,
                    CommentAddedon = Helper.Helper.GetDateTime(),
                    CommentName = Enum.GetName(typeof(inquiryStatus), inquiry.InquiryStatusId),
                    CommentDetail = updateMeasurementStatus.MeasurementComment,
                    InquiryStatusId = inquiry.InquiryStatusId,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = Helper.Helper.GetDateTime(),
                    CreatedBy = Constants.userId,
                });
                inquiry.InquiryStatusId = (int)inquiryStatus.measurementRejected;
                inquiryRepository.Update(inquiry);


                try
                {
                    sendNotificationToOneUser("Measurement is rejected Of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " Reason: " + inquiry.InquiryComment, false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null,
                       (int)inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo, Constants.branchId, (int)notificationCategory.Measurement);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }

                //context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }
            context.SaveChanges();
            return response;
        }


        //[AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddUpdateMeasurmentfiles(CustomMeasFiles customMeasFiles)
        {
            if (customMeasFiles.base64img is { Count: > 0 })
            {
                try
                {
                    Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == customMeasFiles.inquiryid && x.IsActive == true && x.IsDeleted == false)
                        .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
                    //var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == customMeasFiles.Ininquiryworkscopeid && x.IsActive == true && x.IsDeleted == false)
                    //    .Include(x => x.Inquiry).FirstOrDefault();
                    foreach (InquiryWorkscope inworkscope in inquiry.InquiryWorkscopes)
                    {
                        List<File> Files = new List<File>();

                        foreach (string fileUrl in customMeasFiles.base64img)
                        {


                            if (fileUrl != null)
                            {
                                Files.Add(new File()
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

                        Measurement measurement = new Measurement() { MeasurementTakenBy = Constants.userId };
                        measurement.Files = Files;
                        measurement.IsActive = true;
                        measurement.MeasurementComment = customMeasFiles.measurementComment;
                        measurement.IsDeleted = false;
                        measurement.AddedBy = Constants.userId;
                        measurement.AddedDate = Helper.Helper.GetDateTime();
                        inquiry.InquiryStatusId = (int)inquiryStatus.measurementWaitingForApproval;
                        inworkscope.MeasurementAddedOn = Helper.Helper.GetDateTime();
                        inworkscope.InquiryStatusId = (int)inquiryStatus.measurementWaitingForApproval;
                        inworkscope.IsMeasurementDrawing = true;
                        inworkscope.Comments = customMeasFiles.measurementComment;
                        inworkscope.Measurements.Add(measurement);
                        //inquiryWorkscopeRepository.Update(inworkscope);

                    }

                    inquiryRepository.Update(inquiry);
                    List<int?> roletypeId = new List<int?>
                    {
                        (int)roleType.Manager
                    };
                    try
                    {
                        sendNotificationToHead(content: Constants.MeasurementAdded + "Of Inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId,
                         true, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null,
                         //Url.ActionLink("AcceptMeasurement", "MeasuementController", new { id = measurement.InquiryWorkscopeId }),
                         //Url.ActionLink("DeclineMeasurement", "MeasuementController", new { id = measurement.InquiryWorkscopeId }),
                         roletypeId,
                         Constants.branchId,
                         (int)notificationCategory.Measurement);
                    }
                    catch (Exception e)
                    {
                        Sentry.SentrySdk.CaptureMessage(e.Message);
                    }

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                    response.isError = true;
                    response.errorMessage = Constants.MeasurementFileMissing;
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = Constants.MeasurementFileMissing;
            }
            return response;
        }

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetAllMeasurement()
        {
            return measurementRepository.FindByCondition(m => m.IsActive == true && m.IsDeleted == false);
        }

        //[AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object ViewMeasurementById(int inquiryId)
        {
            //var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == inquiryWorkscopeId && x.InquiryStatusId != (int)inquiryStatus.measurementPending && x.InquiryStatusId != (int)inquiryStatus.measurementdelayed && x.IsActive == true && x.IsDeleted == false && x.Measurements.Count > 0).Include(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false && y.Files.Any(z => z.IsActive == true && z.IsDeleted == false))).ThenInclude(y => y.Files.Where(z => z.IsActive == true && z.IsDeleted == false)).FirstOrDefault();
            Inquiry inquiry = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == inquiryId && x.InquiryStatusId != (int)inquiryStatus.measurementInProgress && x.InquiryStatusId != (int)inquiryStatus.measurementdelayed)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false && y.Measurements.Count > 0))
                .ThenInclude(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false && y.Files.Any(z => z.IsActive == true && z.IsDeleted == false)))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                if (inquiry.InquiryWorkscopes.FirstOrDefault() != null)
                {
                    response.data = inquiry.InquiryWorkscopes.FirstOrDefault();
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = Constants.MeasurementMissing;
                }

            }
            else
            {
                response.isError = true;
                response.errorMessage = Constants.MeasurementMissing;
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object ApproveMeasurementAssignee(UpdateInquiryWorkscopeStatusModel updateInquiryWorkscope)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(y => y.InquiryId == updateInquiryWorkscope.Id && y.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending && y.IsActive == true && y.IsDeleted == false)
               .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
               .Include(x => x.Customer).FirstOrDefault();
            //var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == updateInquiryWorkscope.Id && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int?)inquiryStatus.measurementInProgress;
                    //inquiryWorkscope.MeasurementAssignedTo = updateInquiryWorkscope.MeasurementAssignedTo;
                    //inquiryWorkscope.MeasurementScheduleDate = updateInquiryWorkscope.MeasurementScheduleDate;

                }
                inquiry.InquiryStatusId = (int?)inquiryStatus.measurementInProgress;
                inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                inquiryRepository.Update(inquiry);

                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };
                try
                {
                    var user = userRepository.FindByCondition(x => x.UserId == inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo && x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();
                    sendNotificationToHead(content: user.Name + " Accepted Measurement Of inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId, false, " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roletypeId, Constants.branchId, (int)notificationCategory.Measurement);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry doesnot exist";
                response.isError = true;
            }
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object RejectMeasurementAssignee(UpdateInquiryWorkscopeStatusModel updateInquiryWorkscope)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(y => y.InquiryId == updateInquiryWorkscope.Id && y.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending && y.IsActive == true && y.IsDeleted == false)
               .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
               .Include(x => x.Customer)
               .Include(x => x.Comments.Where(x => x.IsActive == true && x.IsDeleted ==false))
               .FirstOrDefault();
            if (inquiry != null)
            {
                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int?)inquiryStatus.measurementAssigneeRejected;
                    inquiryWorkscope.Comments = updateInquiryWorkscope.MeasurementComment;
                    //inquiryWorkscope.MeasurementAssignedTo = updateInquiryWorkscope.MeasurementAssignedTo;
                    //inquiryWorkscope.MeasurementScheduleDate = updateInquiryWorkscope.MeasurementScheduleDate;

                }
                inquiry.InquiryStatusId = (int?)inquiryStatus.measurementAssigneeRejected;
                inquiry.InquiryComment = updateInquiryWorkscope.MeasurementComment;
                inquiry.Comments.Add(new Comment
                {
                    CommentAddedBy = Constants.userId,
                    CommentAddedon = Helper.Helper.GetDateTime(),
                    CommentName = Enum.GetName(typeof(inquiryStatus), inquiry.InquiryStatusId),
                    CommentDetail = updateInquiryWorkscope.MeasurementComment,
                    InquiryStatusId = inquiry.InquiryStatusId,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = Helper.Helper.GetDateTime(),
                    CreatedBy = Constants.userId,
                });
                inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;

                inquiryRepository.Update(inquiry);
                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };
                try
                {
                    var user = userRepository.FindByCondition(x => x.UserId == inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo && x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();
                    sendNotificationToHead(content: user.Name + " Reject Measurement Of inquiry Code: IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " + inquiry.Customer.CustomerName, null, roletypeId, Constants.branchId, (int)notificationCategory.Measurement);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry does not exist";
                response.isError = true;
            }
            return response;
        }
    }
}