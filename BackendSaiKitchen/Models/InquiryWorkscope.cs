using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class InquiryWorkscope
    {
        public InquiryWorkscope()
        {
            Designs = new HashSet<Design>();
            Measurements = new HashSet<Measurement>();
        }

        public int InquiryWorkscopeId { get; set; }
        public int? InquiryId { get; set; }
        public int? WorkscopeId { get; set; }
        public bool? IsMeasurementDrawing { get; set; }
        public int? MeasurementAssignedTo { get; set; }
        public int? DesignAssignedTo { get; set; }
        public int? InquiryStatusId { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string DesignScheduleDate { get; set; }
        public bool? IsDesignApproved { get; set; }
        public string Comments { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual User DesignAssignedToNavigation { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public virtual InquiryStatus InquiryStatus { get; set; }
        public virtual User MeasurementAssignedToNavigation { get; set; }
        public virtual WorkScope Workscope { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
