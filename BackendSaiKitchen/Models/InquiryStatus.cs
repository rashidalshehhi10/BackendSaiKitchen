using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class InquiryStatus
    {
        public InquiryStatus()
        {
            Comments = new HashSet<Comment>();
            Inquiries = new HashSet<Inquiry>();
            InquiryWorkscopes = new HashSet<InquiryWorkscope>();
            Quotations = new HashSet<Quotation>();
        }

        public int InquiryStatusId { get; set; }
        public string InquiryStatusName { get; set; }
        public string InquiryStatusDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<InquiryWorkscope> InquiryWorkscopes { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
