using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class JobOrder
    {
        public JobOrder()
        {
            Files = new HashSet<File>();
            JobOrderDetails = new HashSet<JobOrderDetail>();
            PurchaseRequests = new HashSet<PurchaseRequest>();
        }

        public int JobOrderId { get; set; }
        public string JobOrderCode { get; set; }
        public string JobOrderName { get; set; }
        public string JobOrderDescription { get; set; }
        public string JobOrderRequestedDeadline { get; set; }
        public string JobOrderExpectedDeadline { get; set; }
        public string JobOrderRequestedComments { get; set; }
        public string JobOrderDelayReason { get; set; }
        public string JobOrderDeliveryDate { get; set; }
        public int? InquiryId { get; set; }
        public string MaterialSheetFileUrl { get; set; }
        public string MepdrawingFileUrl { get; set; }
        public string JobOrderChecklistFileUrl { get; set; }
        public string DataSheetApplianceFileUrl { get; set; }
        public bool? IsAppliancesProvidedByClient { get; set; }
        public int? FeedbackReaction { get; set; }
        public string Comments { get; set; }
        public int? FactoryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? SiteMeasurementMatchingWithDesign { get; set; }
        public bool? MaterialConfirmation { get; set; }
        public bool? Mepdrawing { get; set; }
        public bool? QuotationAndCalculationSheetMatchingProposal { get; set; }
        public bool? ApprovedDrawingsAndAvailabilityOfClientSignature { get; set; }
        public bool? AppliancesDataSheet { get; set; }
        public string SiteMeasurementMatchingWithDesignNotes { get; set; }
        public string MaterialConfirmationNotes { get; set; }
        public string MepdrawingNotes { get; set; }
        public string QuotationAndCalculationSheetMatchingProposalNotes { get; set; }
        public string ApprovedDrawingsAndAvailabilityOfClientSignatureNotes { get; set; }
        public string AppliancesDataSheetNotes { get; set; }
        public string DetailedDesignFile { get; set; }
        public bool? IsSpecialApprovalRequired { get; set; }
        public string TechnicalCheckListCompletionDate { get; set; }
        public int? TechnicalCheckListDoneBy { get; set; }
        public string CommercialCheckListCompletionDate { get; set; }
        public int? CommercialCheckListDoneBy { get; set; }
        public string JobOrderApprovalRequestDate { get; set; }
        public string JobOrderCompletionDate { get; set; }
        public int? QualityRemarks { get; set; }
        public int? SpeedOfWorkRemarks { get; set; }
        public int? ServiceOverAllRemarks { get; set; }
        public string EsigntureImh { get; set; }

        public virtual Branch Factory { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public virtual Remark QualityRemarksNavigation { get; set; }
        public virtual Remark ServiceOverAllRemarksNavigation { get; set; }
        public virtual Remark SpeedOfWorkRemarksNavigation { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<JobOrderDetail> JobOrderDetails { get; set; }
        public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; }
    }
}
