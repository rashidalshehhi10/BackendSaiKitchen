using BackendSaiKitchen.Models;
using System;
using System.Collections.Generic;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomCommercialCheckListApproval
    {
        public int inquiryId { get; set; }
        public string Reason { get; set; }
    }
    public class CustomCheckListapprove
    {
        public int inquiryId { get; set; }
        public int factoryId { get; set; }
        public string Comment { get; set; }
        public List<AddFileonChecklist> addFileonChecklists { get; set; }
    }
    public class AddFileonChecklist
    {
        public int documentType { get; set; }
        public List<string>? files { get; set; }
    }
    public class CustomCheckListReject
    {
        public int inquiryId { get; set; }
        public List<Addrejection> Addrejections { get; set; }
    }

    public class Addrejection
    {
        public int inquiryWorkscopeId { get; set; }
        public int rejectionType { get; set; }
        public string reason { get; set; }
    }
    public class Inquirychecklist
    {
        public Inquiry inquiry { get; set; }
        public List<Fee> fees { get; set; }
    }

    public class CheckListByBranch
    {

        public string QuotationNo { get; set; }
        public int InquiryWorkscopeId { get; set; }
        public int? InquiryId { get; set; }
        public string InquiryCode { get; set; }
        public string InquiryDescription { get; set; }
        public string InquiryStartDate { get; set; }
        public string InquiryEndDate { get; set; }
        public int? Status { get; set; }
        public bool? IsMeasurementProvidedByCustomer { get; set; }
        public bool? IsDesignProvidedByCustomer { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string MeasurementAssignTo { get; set; }
        public string InquiryComment { get; set; }
        public int? WorkScopeId { get; set; }
        public int? WorkScopeCount { get; set; }
        public List<string> WorkScopeName { get; set; }
        public int? QuestionaireType { get; set; }
        public string DesignScheduleDate { get; set; }
        public string DesignAssignTo { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
        public string BuildingAddress { get; set; }
        public string BuildingTypeOfUnit { get; set; }
        public string BuildingCondition { get; set; }
        public string BuildingFloor { get; set; }
        public String BuildingReconstruction { get; set; }
        public String IsOccupied { get; set; }
        public int? BranchId { get; set; }
        public bool? IsEscalationRequested { get; set; }
        public string? InquiryAddedBy { get; set; }
        public string? MeasurementFee { get; set; }
        public int? InquiryAddedById { get; set; }
        public Object Actions { get; set; }
        public int? NoOfRevision { get; set; }
        public string CommentAddedOn { get; set; }
        public string MeasurementAddedOn { get; set; }
        public string DesignAddedOn { get; set; }
        public string QuotationAddedOn { get; set; }
    }
}
