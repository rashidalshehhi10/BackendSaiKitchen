using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class File
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public byte[] FileImage { get; set; }
        public string FileUrl { get; set; }
        public int? MeasurementId { get; set; }
        public int? DesignFileId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Design DesignFile { get; set; }
        public virtual Measurement Measurement { get; set; }
    }
}
