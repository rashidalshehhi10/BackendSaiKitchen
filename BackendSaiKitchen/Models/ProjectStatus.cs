using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class ProjectStatus
    {
        public ProjectStatus()
        {
            CommercialProjects = new HashSet<CommercialProject>();
        }

        public int ProjectStatusId { get; set; }
        public string ProjectStatusName { get; set; }
        public string ProjectStatusDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<CommercialProject> CommercialProjects { get; set; }
    }
}
