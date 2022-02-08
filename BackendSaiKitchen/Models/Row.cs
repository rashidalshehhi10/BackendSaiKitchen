using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Row
    {
        public Row()
        {
            Blocks = new HashSet<Block>();
        }

        public int RowId { get; set; }
        public string RowName { get; set; }
        public byte[] RowDescription { get; set; }
        public int? SiteProjectId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual SiteProject SiteProject { get; set; }
        public virtual ICollection<Block> Blocks { get; set; }
    }
}
