using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Color
    {
        public Color()
        {
            ItemColors = new HashSet<ItemColor>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public string ColorDescription { get; set; }
        public string ColorCode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<ItemColor> ItemColors { get; set; }
    }
}
