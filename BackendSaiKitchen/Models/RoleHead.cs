using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class RoleHead
    {
        public int RoleHeadId { get; set; }
        public int? EmployeeRoleId { get; set; }
        public int? HeadRoleId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual BranchRole EmployeeRole { get; set; }
    }
}
