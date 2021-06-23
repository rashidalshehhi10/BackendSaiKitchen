using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Design
    {
        public Design()
        {
            FileDesigns = new HashSet<File>();
            FileVideos = new HashSet<File>();
        }

        public int DesignId { get; set; }
        public string DesignName { get; set; }
        public string DesignDescription { get; set; }
        public string DesignComment { get; set; }
        public int? StatusId { get; set; }
        public int? InquiryWorkscopeId { get; set; }
        public int? DesignTakenBy { get; set; }
        public bool? IsDesignApproved { get; set; }
        public int? DesignApprovedBy { get; set; }
        public string DesignApprovedon { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual User DesignApprovedByNavigation { get; set; }
        public virtual User DesignTakenByNavigation { get; set; }
        public virtual InquiryWorkscope InquiryWorkscope { get; set; }
        public virtual ICollection<File> FileDesigns { get; set; }
        public virtual ICollection<File> FileVideos { get; set; }
    }
}
