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
        static List<File> files = new List<File>();
        static List<Accesory> Accesories = new List<Accesory>();

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Create)]
        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddMeasrmuent(CustomMeasurement measurementVM)
        {
            files.Clear();
            Accesories.Clear();

            Measurement measurement = new Measurement();
            measurement.MeasurementComment = measurementVM.MeasurementComment;



            MeasurementDetail measurementDetail = new MeasurementDetail();
            measurementDetail.MeasurementDetailCeilingHeight = measurementVM.MeasurementDetailCeilingHeight;
            measurementDetail.MeasurementDetailCielingDiameter = measurementVM.MeasurementDetailCielingDiameter;
            measurementDetail.MeasurementDetailCornishHeight = measurementVM.MeasurementDetailCornishHeight;
            measurementDetail.MeasurementDetailCornishDiameter = measurementVM.MeasurementDetailCornishDiameter;
            measurementDetail.MeasurementDetailDoorHeight = measurementVM.MeasurementDetailDoorHeight;
            measurementDetail.MeasurementDetailPlinthHeight = measurementVM.MeasurementDetailPlinthHeight;
            measurementDetail.MeasurementDetailPlinthDiameter = measurementVM.MeasurementDetailPlinthDiameter;
            measurementDetail.MeasurementDetailSkirtingHeight = measurementVM.MeasurementDetailSkirtingHeight;
            measurementDetail.MeasurementDetailSpotLightFromWall = measurementVM.MeasurementDetailSpotLightFromWall;
            measurementDetail.IsActive = true;
            measurementDetail.IsDeleted = false;


            WardrobeDesignInformation wDInfo = new WardrobeDesignInformation();
            wDInfo.WdiclosetType = measurementVM.WdiclosetType;
            wDInfo.WdiboardModel = measurementVM.WdiboardModel;
            wDInfo.WdiselectedColor = measurementVM.WdiselectedColor;
            wDInfo.WdiceilingHeight = measurementVM.WdiceilingHeight;
            wDInfo.WdiclosetHeight = measurementVM.WdiclosetHeight;
            wDInfo.WdistorageDoor = measurementVM.WdistorageDoor;
            wDInfo.WdidoorDesign = measurementVM.WdidoorDesign;
            wDInfo.WdihandleDesign = measurementVM.WdihandleDesign;
            wDInfo.WdidoorMaterial = measurementVM.WdidoorMaterial;
            wDInfo.Wdinotes = measurementVM.Wdinotes;
            wDInfo.IsActive = true;
            wDInfo.IsDeleted = false;




            foreach (var acc in measurementVM.Accesories)
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
                foreach (var file in measurementVM.base64img)
                {
                    var fileUrl = await Helper.Helper.UploadFile(file);

                    if (fileUrl != null)
                    {
                        files.Add(new File()
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


                List<int?> roletypeId = new List<int?>();

                roletypeId.Add((int)roleType.Manager);


                try
                {
                    sendNotificationToHead(Constants.MeasurementAdded,
                     true,
                     Url.ActionLink("AcceptMeasurement", "MeasuementController", new { id = measurement.InquiryWorkscopeId }),
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
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateMeasurementStatus.Id && i.IsActive == true && i.IsDeleted == false).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designPending;
                inquiryWorkscope.DesignAssignedTo = updateMeasurementStatus.DesignAssignedTo;
                inquiryWorkscope.DesignScheduleDate = updateMeasurementStatus.DesignScheduleDate;
                inquiryWorkscopeRepository.Update(inquiryWorkscope);
                try
                {
                    sendNotificationToOneUser(Constants.DesignAssign,
                       false, null, null, (int)inquiryWorkscope.DesignAssignedTo, Constants.branchId, (int)notificationCategory.Design);
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
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateMeasurementStatus.Id && i.IsActive == true && i.IsDeleted == false).Include(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiryWorkscope != null)
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


                try
                {
                    sendNotificationToOneUser("Measurement is rejected \n Reason: " + updateMeasurementStatus.MeasurementComment, false, null, null,
                       (int)inquiryWorkscope.MeasurementAssignedTo, Constants.branchId, (int)notificationCategory.Measurement);
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


        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddUpdateMeasurmentfiles(CustomMeasFiles customMeasFiles)
        {
            files.Clear();
            if (customMeasFiles.base64img != null || customMeasFiles.base64img.Count > 0)
            {
                try
                {
                    var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == customMeasFiles.Ininquiryworkscopeid && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    foreach (var file in customMeasFiles.base64img)
                    {
                        var fileUrl = await Helper.Helper.UploadFile(file);
                        if (fileUrl != null)
                        {
                            files.Add(new File()
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

                            });
                        }
                        else
                        {
                            response.isError = true;
                            response.errorMessage = Constants.wrongFileUpload;
                        }
                    }

                    Measurement measurement = new Measurement() { MeasurementTakenBy = Constants.userId, Files = files };
                    measurement.IsActive = true;
                    measurement.MeasurementComment = customMeasFiles.measurementComment;
                    measurement.IsDeleted = false;
                    inquiryworkscope.InquiryStatusId = (int)inquiryStatus.measurementWaitingForApproval;
                    inquiryworkscope.IsMeasurementDrawing = true;
                    inquiryworkscope.Comments = customMeasFiles.measurementComment;
                    inquiryworkscope.Measurements.Add(measurement);
                    inquiryWorkscopeRepository.Update(inquiryworkscope);
                    List<int?> roletypeId = new List<int?>();

                    roletypeId.Add((int)roleType.Manager);
                    try
                    {
                        sendNotificationToHead(Constants.MeasurementAdded,
                         true,
                         Url.ActionLink("AcceptMeasurement", "MeasuementController", new { id = measurement.InquiryWorkscopeId }),
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
        public object ViewMeasurementById(int inquiryWorkscopeId)
        {
            var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == inquiryWorkscopeId && x.InquiryStatusId != (int)inquiryStatus.measurementPending && x.InquiryStatusId != (int)inquiryStatus.measurementdelayed && x.IsActive == true && x.IsDeleted == false && x.Measurements.Count > 0).Include(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false && y.Files.Any(z => z.IsActive == true && z.IsDeleted == false))).ThenInclude(y => y.Files.Where(z => z.IsActive == true && z.IsDeleted == false)).FirstOrDefault();
            if (inquiryworkscope != null)
            {
                response.data = inquiryworkscope;
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryWorkscopes.Any(y => y.InquiryWorkscopeId == updateInquiryWorkscope.Id && y.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending && y.IsActive==true && y.IsDeleted==false) && x.IsActive==true && x.IsDeleted==false).Include(x=>x.InquiryWorkscopes.Where(y=>y.IsActive==true && y.IsDeleted==false)).FirstOrDefault();
            //var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == updateInquiryWorkscope.Id && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending).FirstOrDefault();
            if (inquiry != null) { 
           foreach(var inquiryWorkscope in inquiry.InquiryWorkscopes)
            {
                inquiryWorkscope.InquiryStatusId = (int?)inquiryStatus.measurementPending;
                //inquiryWorkscope.MeasurementAssignedTo = updateInquiryWorkscope.MeasurementAssignedTo;
                //inquiryWorkscope.MeasurementScheduleDate = updateInquiryWorkscope.MeasurementScheduleDate;
              
            }
           
            inquiryRepository.Update(inquiry);
            List<int?> roletypeId = new List<int?>();

            roletypeId.Add((int)roleType.Manager);
            try
            {
                var user = userRepository.FindByCondition(x => x.UserId == inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo && x.IsActive == true && x.IsDeleted == false).Select(y => new
                {
                    Name = y.UserName
                }).FirstOrDefault();
                sendNotificationToHead(user.Name + " Accepted Measurement of "+inquiry.InquiryId, false, null, null, roletypeId, Constants.branchId, (int)notificationCategory.Measurement);
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
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == updateInquiryWorkscope.Id && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.InquiryStatusId = (int?)inquiryStatus.measurementAssigneeRejected;
                inquiryWorkscope.Comments = updateInquiryWorkscope.MeasurementComment;
                //inquiryWorkscope.MeasurementAssignedTo = updateInquiryWorkscope.MeasurementAssignedTo;
                //inquiryWorkscope.MeasurementScheduleDate = updateInquiryWorkscope.MeasurementScheduleDate;
                inquiryWorkscopeRepository.Update(inquiryWorkscope);
                List<int?> roletypeId = new List<int?>();

                roletypeId.Add((int)roleType.Manager);
                try
                {
                    var user = userRepository.FindByCondition(x => x.UserId == inquiryWorkscope.MeasurementAssignedTo && x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();
                    sendNotificationToHead(user.Name + " Rejected Measerument Assignee Reason: " + updateInquiryWorkscope.MeasurementComment, false, null, null, roletypeId, Constants.branchId, (int)notificationCategory.Measurement);
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
                context.SaveChanges();
            }
            else
            {
                response.errorMessage = "Inquiry Does Not Exist";
                response.isError = true;
            }
            return response;
        }
    }
}