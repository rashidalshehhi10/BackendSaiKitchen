using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class InquiryStatus
    {
        public InquiryStatus()
        {
            InquiryWorkscopes = new HashSet<InquiryWorkscope>();
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

        public virtual ICollection<InquiryWorkscope> InquiryWorkscopes { get; set; }
    }
}
