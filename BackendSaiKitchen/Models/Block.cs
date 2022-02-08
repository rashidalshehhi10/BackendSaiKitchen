using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Block
    {
        public Block()
        {
            Villas = new HashSet<Villa>();
        }

        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public string BlockDescription { get; set; }
        public int? RowId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Row Row { get; set; }
        public virtual ICollection<Villa> Villas { get; set; }
    }
}
