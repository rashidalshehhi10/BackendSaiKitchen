﻿using System;

namespace BackendSaiKitchen.CustomModel
{
    public class ViewInquiry
    {

        public int InquiryWorkscopeId { get; set; }
        public int? InquiryId { get; set; }
        public string InquiryCode { get; set; }
        public string InquiryDescription { get; set; }
        public string InquiryStartDate { get; set; }
        public string InquiryEndDate { get; set; }
        public int? Status { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string MeasurementAssignTo { get; set; }
        public string InquiryComment { get; set; }
        public int? WorkScopeId { get; set; }
        public string WorkScopeName { get; set; }
        public string DesignScheduleDate { get; set; }
        public string DesignAssignTo { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string BuildingAddress { get; set; }
        public string BuildingTypeOfUnit { get; set; }
        public string BuildingCondition { get; set; }
        public string BuildingFloor { get; set; }
        public String BuildingReconstruction { get; set; }
        public int? BranchId { get; set; }
        public bool? IsEscalationRequested { get; set; }
        public string? InquiryAddedBy { get; set; }
        public Object Actions { get; set; }

    }
    public class UpdateInquirySchedule
    {
        public int InquiryId { get; set; }
        public int? MeasurementAssignedTo { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public int? InquiryStatusId { get; set; }
        public int? DesignAssignedTo { get; set; }
        public string DesignScheduleDate { get; set; }
    }
    public class UpdateMeasurementStatusModel
    {
        public int InquiryWorkscopeId { get; set; }
        public int? MeasurementAssignedTo { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string MeasurementComment { get; set; }
        public int? DesignAssignedTo { get; set; }
        public string DesignScheduleDate { get; set; }
        public string DesignComment { get; set; }
    }
}
