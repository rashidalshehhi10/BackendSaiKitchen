using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            Files = new HashSet<File>();
        }

        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderTitle { get; set; }
        public string PurchaseOrderDescription { get; set; }
        public int? PurchaseStatusId { get; set; }
        public string PurchaseOrderAmount { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string PurchaseOrderExpectedDeliveryDate { get; set; }
        public string PurchaseOrderActualDeliveryDate { get; set; }
        public int? PurchaseRequestId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual PurchaseRequest PurchaseRequest { get; set; }
        public virtual PurchaseStatus PurchaseStatus { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
