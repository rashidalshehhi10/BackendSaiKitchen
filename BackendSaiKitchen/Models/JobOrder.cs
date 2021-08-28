#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class JobOrder
    {
        public int JobOrderId { get; set; }
        public string JobOrderName { get; set; }
        public string JobOrderDescription { get; set; }
        public string JobOrderRequestedDeadline { get; set; }
        public string JobOrderExpectedDeadline { get; set; }
        public string JobOrderRequestedComments { get; set; }
        public string JobOrderDelayReason { get; set; }
        public string JobOrderDeliveryDate { get; set; }
        public int? InquiryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? FactoryId { get; set; }

        public virtual Branch Factory { get; set; }
        public virtual Inquiry Inquiry { get; set; }
    }
}
