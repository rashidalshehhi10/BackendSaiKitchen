using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class BranchType
    {
        public BranchType()
        {
            Branches = new HashSet<Branch>();
        }

        public int BranchTypeId { get; set; }
        public string BranchTypeName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
