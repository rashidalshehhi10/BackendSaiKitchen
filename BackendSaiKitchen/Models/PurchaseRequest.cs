using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class PurchaseRequest
    {
        public PurchaseRequest()
        {
            Files = new HashSet<File>();
        }

        public int PurchaseRequestId { get; set; }
        public int? JobOrderId { get; set; }
        public string PurchaseRequestTitle { get; set; }
        public string PurchaseRequestDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual JobOrder JobOrder { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
