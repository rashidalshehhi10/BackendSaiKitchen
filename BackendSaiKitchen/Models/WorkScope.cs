using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class WorkScope
    {
        public WorkScope()
        {
            InquiryWorkscopes = new HashSet<InquiryWorkscope>();
            Materials = new HashSet<Material>();
            ProjectDetails = new HashSet<ProjectDetail>();
            VillaWorkScopes = new HashSet<VillaWorkScope>();
        }

        public int WorkScopeId { get; set; }
        public string WorkScopeName { get; set; }
        public string WorkScopeDescription { get; set; }
        public int? QuestionaireType { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<InquiryWorkscope> InquiryWorkscopes { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<ProjectDetail> ProjectDetails { get; set; }
        public virtual ICollection<VillaWorkScope> VillaWorkScopes { get; set; }
    }
}
