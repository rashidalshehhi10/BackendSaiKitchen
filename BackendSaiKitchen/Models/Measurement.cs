using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class Measurement
    {
        public Measurement()
        {
            Files = new HashSet<File>();
        }

        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementDescription { get; set; }
        public int? MeasurementDetailId { get; set; }
        public int? MeasurementStatusId { get; set; }
        public string MeasurementComment { get; set; }
        public int? InquiryWorkscopeId { get; set; }
        public int? MeasurementTakenBy { get; set; }
        public int? KitchenDesignInfoId { get; set; }
        public int? WardrobeDesignInfoId { get; set; }
        public bool? IsMeasurementApproved { get; set; }
        public int? MeasurementApprovedBy { get; set; }
        public string MeasurementApprovedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string AddedDate { get; set; }
        public int? AddedBy { get; set; }

        public virtual InquiryWorkscope InquiryWorkscope { get; set; }
        public virtual KitchenDesignInfo KitchenDesignInfo { get; set; }
        public virtual User MeasurementApprovedByNavigation { get; set; }
        public virtual MeasurementDetail MeasurementDetail { get; set; }
        public virtual User MeasurementTakenByNavigation { get; set; }
        public virtual WardrobeDesignInformation WardrobeDesignInfo { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
