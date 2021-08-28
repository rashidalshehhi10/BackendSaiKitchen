#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class File
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public byte[] FileImage { get; set; }
        public string FileUrl { get; set; }
        public string FileContentType { get; set; }
        public bool? IsImage { get; set; }
        public int? MeasurementId { get; set; }
        public int? DesignId { get; set; }
        public int? QuotationId { get; set; }
        public int? Paymentid { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Design Design { get; set; }
        public virtual Measurement Measurement { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Quotation Quotation { get; set; }
    }
}
