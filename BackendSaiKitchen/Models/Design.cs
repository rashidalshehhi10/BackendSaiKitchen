using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Design
    {
        public Design()
        {
            Files = new HashSet<File>();
        }

        public int DesignId { get; set; }
        public string DesignName { get; set; }
        public string DesignDescription { get; set; }
        public int? DesignStatusId { get; set; }
        public int? InquiryWorkscopeId { get; set; }
        public int? DesignTakenBy { get; set; }
        public bool? IsDesignApproved { get; set; }
        public int? DesignApproveBy { get; set; }
        public string DesignApprovedon { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public virtual InquiryWorkscope InquiryWorkscope { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
