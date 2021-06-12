using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using File = BackendSaiKitchen.Models.File;

namespace BackendSaiKitchen.Controllers
{
    public class MeasuementController : BaseController
    {

        public MeasuementController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }
        static List<File> files = new List<File>();
        static List<Accesory> Accesories =new List<Accesory>();
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
                        AccesoriesName=acc.AccesoriesName,
                        AccesoriesValue=acc.AccesoriesValue,
                        IsActive=true,
                        IsDeleted=false,
                        CreatedBy=Constants.userId,
                        CreatedDate=Helper.Helper.GetDateTime(),
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
                context.SaveChanges();
                response.data = measurement;
               

                List<int?> roletypeId = new List<int?>();

                roletypeId.Add((int)roleType.Manager);

                sendNotificationToHead(
                    measurement.MeasurementTakenByNavigation.UserName + " Added a New Measurement",
                    true,
                    Url.ActionLink("AcceptMeasurement", "MeasuementController", new { id = measurement.InquiryWorkscopeId }),
                    Url.ActionLink("DeclineMeasurement", "MeasuementController", new { id = measurement.InquiryWorkscopeId}),
                    roletypeId,
                    Constants.branchId,
                    (int)notificationCategory.Measurement);

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
        public object AcceptMeasurement(int id)
        {
            var inquiryWS = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == id && i.IsActive == true && i.IsDeleted == false).FirstOrDefault();
            inquiryWS.InquiryStatusId = (int)inquiryStatus.measurementAccpeted;
            inquiryWorkscopeRepository.Update(inquiryWS);
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object DeclineMeasurement(int id)
        {
            var inquiryWS = inquiryWorkscopeRepository.FindByCondition(i => i.InquiryWorkscopeId == id && i.IsActive == true && i.IsDeleted == false).FirstOrDefault();
            inquiryWS.InquiryStatusId = (int)inquiryStatus.measurementRejected;
            inquiryWorkscopeRepository.Update(inquiryWS);
            context.SaveChanges();
            return response;

        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddUpdateMeasurmentfiles(CustomMeasFiles customMeasFiles)
        {
            files.Clear();
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
            inquiryworkscope.InquiryStatusId = (int) inquiryStatus.measurementWaitingForApproval;
            inquiryworkscope.Measurements.Add(measurement);
            inquiryWorkscopeRepository.Update(inquiryworkscope);
            context.SaveChanges();
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object GetAllMeasurement()
        {
            return measurementRepository.FindByCondition(m => m.IsActive == true && m.IsDeleted == false);
        }

    }
}