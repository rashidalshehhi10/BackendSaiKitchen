using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class JobOrderDetail
    {
        public int JobOrderDetailId { get; set; }
        public int? JobOrderId { get; set; }
        public string JobOrderDetailName { get; set; }
        public string JobOrderDetailDescription { get; set; }
        public string MaterialAvailabilityDate { get; set; }
        public string ShopDrawingCompletionDate { get; set; }
        public string MissingDocuments { get; set; }
        public string ProductionCompletionDate { get; set; }
        public string WoodenWorkCompletionDate { get; set; }
        public string MaterialDeliveryFinalDate { get; set; }
        public string CountertopFixingDate { get; set; }
        public string InstallationDate { get; set; }
        public bool? IsNewlyRequested { get; set; }
        public bool? IsFromFactory { get; set; }
        public string Remarks { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual JobOrder JobOrder { get; set; }
    }
}
