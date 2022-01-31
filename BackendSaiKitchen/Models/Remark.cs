using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Remark
    {
        public Remark()
        {
            JobOrderQualityRemarksNavigations = new HashSet<JobOrder>();
            JobOrderServiceOverAllRemarksNavigations = new HashSet<JobOrder>();
            JobOrderSpeedOfWorkRemarksNavigations = new HashSet<JobOrder>();
        }

        public int RemarksId { get; set; }
        public string RemarksName { get; set; }
        public string RemarksDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<JobOrder> JobOrderQualityRemarksNavigations { get; set; }
        public virtual ICollection<JobOrder> JobOrderServiceOverAllRemarksNavigations { get; set; }
        public virtual ICollection<JobOrder> JobOrderSpeedOfWorkRemarksNavigations { get; set; }
    }
}
