using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class CommercialProject
    {
        public CommercialProject()
        {
            Apartments = new HashSet<Apartment>();
            ProjectDetails = new HashSet<ProjectDetail>();
        }

        public int CommercialProjectId { get; set; }
        public string CommercialProjectName { get; set; }
        public string CommercialProjectDesription { get; set; }
        public string CommercialProjectStartDate { get; set; }
        public int? ProjectStatusId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CommercialProjectNo { get; set; }
        public string CommercialProjectLocation { get; set; }

        public virtual ProjectStatus ProjectStatus { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<ProjectDetail> ProjectDetails { get; set; }
    }
}
