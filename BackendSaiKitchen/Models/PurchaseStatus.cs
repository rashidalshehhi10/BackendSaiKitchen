using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class PurchaseStatus
    {
        public PurchaseStatus()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            PurchaseRequests = new HashSet<PurchaseRequest>();
        }

        public int PurchaseStatusId { get; set; }
        public string PurchaseStatusName { get; set; }
        public string PurchaseStatusDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; }
    }
}
