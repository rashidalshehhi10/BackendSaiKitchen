using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class VillaWorkScope
    {
        public int VillaWorkScopeId { get; set; }
        public string VillaWorkScopeName { get; set; }
        public string VillaWorkScopeDescription { get; set; }
        public int? VillaId { get; set; }
        public int? WorkScopeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Villa Villa { get; set; }
        public virtual WorkScope WorkScope { get; set; }
    }
}
