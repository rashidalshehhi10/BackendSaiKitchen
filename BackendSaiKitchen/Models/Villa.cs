using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Villa
    {
        public Villa()
        {
            VillaWorkScopes = new HashSet<VillaWorkScope>();
        }

        public int VillaId { get; set; }
        public string VillaName { get; set; }
        public string VillaDescription { get; set; }
        public int? BlockId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Block Block { get; set; }
        public virtual ICollection<VillaWorkScope> VillaWorkScopes { get; set; }
    }
}
