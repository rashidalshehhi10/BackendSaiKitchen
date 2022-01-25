using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class ProjectDetail
    {
        public int ProjectDetailId { get; set; }
        public int? CommercialProjectId { get; set; }
        public int? MaterialId { get; set; }
        public int? SizeId { get; set; }
        public int? WorkScopeId { get; set; }
        public int? ProjectStatusId { get; set; }
        public int? Quantity { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual CommercialProject CommercialProject { get; set; }
        public virtual Material Material { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }
        public virtual Size Size { get; set; }
        public virtual WorkScope WorkScope { get; set; }
    }
}
