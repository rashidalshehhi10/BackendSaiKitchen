using BackendSaiKitchen.Models;
using System.Collections.Generic;

namespace BackendSaiKitchen.CustomModel
{
    public class ViewInquiryDetail
    {
        //public int InquiryWorkscopeId { get; set; }
        public int? InquiryId { get; set; }
        public string InquiryCode { get; set; }
        public string InquiryDescription { get; set; }
        public string InquiryStartDate { get; set; }
        public string InquiryEndDate { get; set; }
        public int? Status { get; set; }
        public string? IsMeasurementProvidedByCustomer { get; set; }
        public string? IsDesignProvidedByCustomer { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string MeasurementAssignTo { get; set; }
        public int? MeasurementAssignToId { get; set; }
        public string InquiryComment { get; set; }

        //public int? WorkScopeId { get; set; }
        //public int? WorkScopeCount { get; set; }
        //public string WorkScopeName { get; set; }
        public int? QuestionaireType { get; set; }
        public string DesignScheduleDate { get; set; }
        public string DesignAssignTo { get; set; }
        public int? DesignAssignToId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerWhatsapp { get; set; }
        public string BuildingAddress { get; set; }
        public string BuildingMakaniMap { get; set; }
        public string BuildingTypeOfUnit { get; set; }
        public string BuildingCondition { get; set; }
        public string BuildingFloor { get; set; }
        public string BuildingReconstruction { get; set; }
        public string IsOccupied { get; set; }
        public int? BranchId { get; set; }
        public bool? IsEscalationRequested { get; set; }
        public string EscalationRequestedBy { get; set; }
        public string EscalationRequestedOn { get; set; }
        public string? InquiryAddedBy { get; set; }
        public string? MeasurementFee { get; set; }
        public int? InquiryAddedById { get; set; }
        public string InquiryAddedOn { get; set; }
        public object Actions { get; set; }
        public int? NoOfRevision { get; set; }
        public string QuotationScheduleDate { get; set; }
        public string WorkscopeNames { get; set; }
        public string FactorName { get; set; }
        public string CommentAddedOn { get; set; }
        public string MeasurementAddedOn { get; set; }
        public string DesignAddedOn { get; set; }
        public string ManagedBy{ get; set; }
        public int? ManagedById { get; set; }

        public string QuotationAddedOn { get; set; }
        public List<Payment> payments { get; set; }
        //public List<InquiryWorkscope> InquiryWorkscopes { get; set; }
    }

    public class UpdateInquirySchedule
    {
        public int InquiryId { get; set; }

        //public int InquiryWorkscopeId { get; set; }
        public int? MeasurementAssignedTo { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public int? InquiryStatusId { get; set; }
        public int? DesignAssignedTo { get; set; }
        public string DesignScheduleDate { get; set; }
        public bool IsProvidedByCustomer { get; set; }
    }

    public class EditFiles
    {
        public int inquiryId { get; set; }
        public List<string> Measurement { get; set; }
        public List<string> Desgin { get; set; }
        public List<string> Quotation { get; set; }
        public string CalculationSheetFile { get; set; }
        public string MEPDrawing { get; set; }
        public string MatrialSheet { get; set; }
        public string DataSheetAppliance { get; set; }
        public string DetailedDesignFile { get; set; }
        public string JobOrderChecklist { get; set; }
        public List<string> AdvancePayment { get; set; }
        public List<string> BeforeInstalltionPayment { get; set; }
        public List<string> AfterDelieveryPayment { get; set; }
        //public List<string> InstallmentPayment { get; set; }

    }

    public class UpdateInquiryWorkscopeStatusModel
    {
        public int Id { get; set; }
        public int? MeasurementAssignedTo { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string MeasurementComment { get; set; }
        public int? DesignAssignedTo { get; set; }
        public string DesignScheduleDate { get; set; }
        public string DesignComment { get; set; }
        public int FeedBackReaction { get; set; }
    }

    public class WorkscopeInquiry
    {
        public int? inquiryWorkscopeId { get; set; }
        public List<int?> WorkScopeId { get; set; }
    }

    public class AddComment
    {
        public int inquiryId { get; set; }
        public int contactStatusId { get; set; }
        public string comment { get; set; }
        public string Date { get; set; }
    }

    public class ChangeManaged
    {
        public int inquiryId { get; set; }
        public int Id { get; set; }
    }
}