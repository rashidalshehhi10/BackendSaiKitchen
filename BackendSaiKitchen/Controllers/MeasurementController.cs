﻿using BackendSaiKitchen.CustomModel;
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
using System.Buffers.Text;

namespace BackendSaiKitchen.Controllers
{
    public class MeasuementController : BaseController
    {
        private IBlobManager _blobManager;

        public MeasuementController(IBlobManager blobManager)
        {
            _blobManager = blobManager;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddMeasrmuent(CustomMeasurement measurementVM)
        {
            if (measurementVM.files.Count > 0)
            {
                Measurement measurement = new Measurement();

                MeasurementDetail measurementDetail = new MeasurementDetail() ;
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
                wDInfo.Accesories = measurementVM.accesories;

                InquiryWorkscope inquiryWorkscope = new InquiryWorkscope();
                inquiryWorkscope.DesignAssignedTo = measurementVM.DesignAssignedTo;
                inquiryWorkscope.DesignScheduleDate = measurementVM.DesignScheduleDate;

                Measurement _measurement = new Measurement();
                _measurement.Files = measurementVM.files;
                _measurement.WardrobeDesignInfo = wDInfo;
                _measurement.MeasurementDetail = measurementDetail;
                _measurement.InquiryWorkscope = inquiryWorkscope;


                foreach (var file in _measurement.Files)
                {
                    
                    if (file != null)
                    {
                        var stream = new MemoryStream(file.FileImage);
                        Guid guid = Guid.NewGuid();
                        var exet = Helper.Helper.GuessFileTypebyte(file.FileImage);
                        IFormFile blob = new FormFile(stream, 0,file.FileImage.Length, "azure", guid + "." + exet);


                        if (exet == "png" || exet == "jpg" || exet == "pdf")
                        {
                            await _blobManager.Uplaod(new Blob() { File = blob });
                            file.FileImage = null;
                            file.FileUrl = Helper.Constants.AzureUrl + guid;
                            file.FileName = guid + "." + exet;
                            file.MeasurementId = measurement.MeasurementId;
                            measurementRepository.Create(measurement);

                        }
                        else
                        {
                            response.isError = true;
                            response.errorMessage = "you can only upload type jpeg, png or pdf";
                        }

                    }
                }
                  
                    List<int?> roletypeId = new List<int?>();
                    
                    roletypeId.Add((int)roleType.Manager);

                    sendNotificationToHead(
                        measurementVM.DesignAssignedTo + " Added a New Measurement",
                        true,
                        Url.ActionLink("Accept", "MeasuementController", new { id = measurementVM.MeasurementId}),
                        Url.ActionLink("Decline", "MeasuementController", new { id = measurementVM.MeasurementId }),
                        roletypeId,
                        (int)Helper.Constants.branchId,
                        (int)notificationCategory.Measurement);



                    measurementRepository.Create(_measurement);
                    context.SaveChanges();
                    response.data = _measurement;
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
        public IActionResult Accept(int id)
        {

            var measurement = measurementRepository.FindByCondition(m => m.MeasurementId == id && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();
            measurement.MeasurementStatusId = (int)inquiryStatus.measurementaccpeted;
            measurementRepository.Update(measurement);
            return Ok();


        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Decline(int id)
        {

            var measurement = measurementRepository.FindByCondition(m => m.MeasurementId == id && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();
            measurement.InquiryWorkscope.InquiryStatusId = (int)inquiryStatus.measurementrejected;
            measurementRepository.Update(measurement);
            context.SaveChanges();
           
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> Add_UpdateMeasurmentfiles(CustomMeasFiles customMeasFiles)
        {
            var measurement = measurementRepository.FindByCondition(m => m.InquiryWorkscopeId == customMeasFiles.Ininquiryworkscopeid && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();
            

            foreach (var File in customMeasFiles.Base64img )
            {
                Models.File file = new Models.File();

                if (File != null)
                {
                    MemoryStream stream = new MemoryStream(File);
                    Guid guid = Guid.NewGuid();
                    var exet = Helper.Helper.GuessFileTypebyte(File);

                    IFormFile blob = new FormFile(stream, 0, File.Length, "azure", guid + "." + exet);


                    if (exet == "png" || exet == "jpg" || exet == "pdf")
                    {
                        await _blobManager.Uplaod(new Blob() { File = blob });
                        file.FileUrl = Helper.Constants.AzureUrl + guid;
                        file.FileName = guid + "." + exet;
                        file.MeasurementId = measurement.MeasurementId;
                        measurementRepository.Create(measurement);
                    }
                    else
                    {
                        response.isError = true;
                        response.errorMessage = "you can only upload type jpeg, png or pdf";
                    }

                }
            }
            List<int?> roletypeId = new List<int?>();

            roletypeId.Add((int)roleType.Manager);
            sendNotificationToHead(
                measurement.MeasurementTakenByNavigation + " Added a New Measurement",
                true,
                Url.ActionLink("Accept", "MeasuementController", new { id = measurement.MeasurementId }),
                Url.ActionLink("Decline", "MeasuementController", new { id = measurement.MeasurementId }),
                roletypeId,
                (int)Helper.Constants.branchId,
                (int)notificationCategory.Measurement);
            return response;
        }

    }
}
