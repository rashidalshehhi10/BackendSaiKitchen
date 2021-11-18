using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Brand
    {
        public Brand()
        {
            ApplianceAccessories = new HashSet<ApplianceAccessory>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public int? ApplianceAccessoryTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ApplianceAccessoryType ApplianceAccessoryType { get; set; }
        public virtual ICollection<ApplianceAccessory> ApplianceAccessories { get; set; }
    }
}
