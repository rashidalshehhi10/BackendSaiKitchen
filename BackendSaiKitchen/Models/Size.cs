using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Size
    {
        public Size()
        {
            ProjectDetails = new HashSet<ProjectDetail>();
        }

        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public string SizeDescription { get; set; }
        public decimal? SizeHeight { get; set; }
        public decimal? SizeWidth { get; set; }
        public int? MaterialId { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Material Material { get; set; }
        public virtual ICollection<ProjectDetail> ProjectDetails { get; set; }
    }
}
