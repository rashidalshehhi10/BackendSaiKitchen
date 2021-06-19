using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class File
    {
        public File()
        {
            QuotationContractFiles = new HashSet<Quotation>();
            QuotationQuotationFiles = new HashSet<Quotation>();
        }

        public int FileId { get; set; }
        public string FileName { get; set; }
        public byte[] FileImage { get; set; }
        public string FileUrl { get; set; }
        public int? MeasurementId { get; set; }
        public int? DesignId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Design Design { get; set; }
        public virtual Measurement Measurement { get; set; }
        [NotMapped]
        public virtual ICollection<Quotation> QuotationContractFiles { get; set; }
        public virtual ICollection<Quotation> QuotationQuotationFiles { get; set; }
    }
}
