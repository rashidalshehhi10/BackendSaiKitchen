using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class SiteProject
    {
        public SiteProject()
        {
            Rows = new HashSet<Row>();
        }

        public int SiteProjectId { get; set; }
        public string SiteProjectName { get; set; }
        public int? SiteProjectStatusId { get; set; }
        public bool? SiteProjectIsOnHold { get; set; }
        public string SiteProjectComment { get; set; }
        public string SiteProjectFile { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Row> Rows { get; set; }
    }
}
