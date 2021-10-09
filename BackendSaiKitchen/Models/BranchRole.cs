using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class BranchRole
    {
        public BranchRole()
        {
            PermissionRoles = new HashSet<PermissionRole>();
            RoleHeads = new HashSet<RoleHead>();
            UserRoles = new HashSet<UserRole>();
        }

        public int BranchRoleId { get; set; }
        public string BranchRoleName { get; set; }
        public string BranchRoleDescription { get; set; }
        public int? RoleTypeId { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual RoleType RoleType { get; set; }
        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
        public virtual ICollection<RoleHead> RoleHeads { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
