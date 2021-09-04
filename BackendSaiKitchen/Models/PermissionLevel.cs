using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class PermissionLevel
    {
        public PermissionLevel()
        {
            PermissionRoles = new HashSet<PermissionRole>();
        }

        public int PermissionLevelId { get; set; }
        public string PermissionLevelName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}
