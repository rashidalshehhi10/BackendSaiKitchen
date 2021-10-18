#nullable disable

namespace BackendSaiKitchen.Models
{
    public class MeasurementFile
    {
        public int MeasurementFileId { get; set; }
        public int? MeasurementId { get; set; }
        public int? FileId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual File File { get; set; }
        public virtual Measurement Measurement { get; set; }
    }
}