using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class CustomerBranch
    {
        public int CustomerBranchId { get; set; }
        public int? CustomerId { get; set; }
        public int? BranchId { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
