using BackendSaiKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomCheckListapprove
    {
        public int inquiryId { get; set; }
        //public int inquirystatusId { get; set; }
        public string InstallationDate { get; set; }
        public string? Comment { get; set; }
        public string? fileplace { get; set; }
        public byte[] file { get; set; }
    }

    public class CustomCheckListReject
    {
        public int inquiryId { get; set; }
        public string Comment { get; set; }
        public int inquiystatusId { get; set; }
        public List<string> Change { get; set; }

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
        public string MeasurementScheduleDate { get; set; }
        public string MeasurementAssignTo { get; set; }
        public string InquiryComment { get; set; }
        public int? WorkScopeId { get; set; }
        public int? WorkScopeCount { get; set; }
        public string WorkScopeName { get; set; }
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
    }
}
