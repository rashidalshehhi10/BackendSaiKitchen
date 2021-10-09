using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class JobOrder
    {
        public JobOrder()
        {
            JobOrderDetails = new HashSet<JobOrderDetail>();
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

        public virtual Branch Factory { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public virtual ICollection<JobOrderDetail> JobOrderDetails { get; set; }
    }
}
