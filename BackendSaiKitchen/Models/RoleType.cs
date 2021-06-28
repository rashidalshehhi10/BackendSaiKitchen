using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class RoleType
    {
        public RoleType()
        {
            BranchRoles = new HashSet<BranchRole>();
        }

        public int RoleTypeId { get; set; }
        public string RoleTypeName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<BranchRole> BranchRoles { get; set; }
    }
}
