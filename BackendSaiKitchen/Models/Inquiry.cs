using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Inquiry
    {
        public Inquiry()
        {
            InquiryWorkscopes = new HashSet<InquiryWorkscope>();
        }

        public int InquiryId { get; set; }
        public string InquiryCode { get; set; }
        public string InquiryName { get; set; }
        public string InquiryDescription { get; set; }
        public string InquiryStartDate { get; set; }
        public string InquiryDueDate { get; set; }
        public string InquiryEndDate { get; set; }
        public int? CustomerId { get; set; }
        public int? BranchId { get; set; }
        public int? BuildingId { get; set; }
        public bool? IsEscalationRequested { get; set; }
        public int? AddedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual User AddedByNavigation { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Building Building { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<InquiryWorkscope> InquiryWorkscopes { get; set; }
    }
}
