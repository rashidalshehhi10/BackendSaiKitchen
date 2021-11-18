using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class ItemColor
    {
        public int ItemColorId { get; set; }
        public string Skucode { get; set; }
        public int? ItemId { get; set; }
        public int? ColorId { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Color Color { get; set; }
        public virtual ApplianceAccessory Item { get; set; }
    }
}
