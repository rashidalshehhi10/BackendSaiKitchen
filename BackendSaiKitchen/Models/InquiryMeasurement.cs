#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class InquiryMeasurement
    {
        public int InquiryMeasurementId { get; set; }
        public int? InquiryId { get; set; }
        public int? MeaurementId { get; set; }
        public int? WorkscopeId { get; set; }
        public int? StatusId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Inquiry Inquiry { get; set; }
        public virtual Measurement Meaurement { get; set; }
        public virtual WorkScope Workscope { get; set; }
    }
}
