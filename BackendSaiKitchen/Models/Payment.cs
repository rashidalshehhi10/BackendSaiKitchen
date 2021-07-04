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
        public int? PaymentAmount { get; set; }
        public int? PaymentType { get; set; }
        public int? PaymentStatus { get; set; }
        public int? InquiryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Inquiry Inquiry { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
