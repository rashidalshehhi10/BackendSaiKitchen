using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Files = new HashSet<File>();
        }

        public int PaymentId { get; set; }
        public string PaymentName { get; set; }
        public string PaymentDetail { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? PaymentModeId { get; set; }
        public decimal? PaymentAmountinPercentage { get; set; }
        public string PaymentExpectedDate { get; set; }
        public int? FeesId { get; set; }
        public int? InquiryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Fee Fees { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
