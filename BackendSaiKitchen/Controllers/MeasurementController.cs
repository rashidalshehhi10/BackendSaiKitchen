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
                measurementRepository.Create(measurement);
                response.data = measurement;


                List<int?> roletypeId = new List<int?>();

                roletypeId.Add((int)roleType.Manager);

             
                try
                {
                    sendNotificationToHead(
                     measurement.MeasurementTakenByNavigation.UserName + " Added a New Measurement",
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
                measurement.Files = files;
                return response;


            }
            else
            {
                response.isError = true;
                response.errorMessage = "Kindly upload measurement file";
            }
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object AcceptMeasurement(UpdateInquiryWorkscopeStatusModel updateMeasurementStatus)
        {
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateMeasurementStatus.InquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designPending;
                inquiryWorkscope.DesignAssignedTo = updateMeasurementStatus.DesignAssignedTo;
                inquiryWorkscope.DesignScheduleDate = updateMeasurementStatus.DesignScheduleDate;
                inquiryWorkscopeRepository.Update(inquiryWorkscope);
                try {
                    sendNotificationToOneUser("you are assign for the new design",
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

        [HttpPost]
        [Route("[action]")]
        public object DeclineMeasurement(UpdateInquiryWorkscopeStatusModel updateMeasurementStatus)
        {
            var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == updateMeasurementStatus.InquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false).Include(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.measurementRejected;
                inquiryWorkscope.MeasurementAssignedTo = updateMeasurementStatus.MeasurementAssignedTo;
                inquiryWorkscope.MeasurementScheduleDate = updateMeasurementStatus.MeasurementScheduleDate;
                inquiryWorkscope.Comments = updateMeasurementStatus.MeasurementComment;
                Helper.Helper.Each(inquiryWorkscope.Measurements, i => {
                    i.IsActive = false;
                    i.MeasurementComment = updateMeasurementStatus.MeasurementComment;
                });
                inquiryWorkscopeRepository.Update(inquiryWorkscope);
             
             
                try
                {
                    sendNotificationToOneUser("measurements is rejected \n Reason: " + updateMeasurementStatus.MeasurementComment, false, null, null,
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
        [HttpPost]
        [Route("[action]")]
        public object GetAllMeasurement()
        {
            return measurementRepository.FindByCondition(m => m.IsActive == true && m.IsDeleted == false);
        }

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
    }
}