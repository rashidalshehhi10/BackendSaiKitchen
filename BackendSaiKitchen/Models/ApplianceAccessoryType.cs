using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class ApplianceAccessoryType
    {
        public ApplianceAccessoryType()
        {
            ApplianceAccessories = new HashSet<ApplianceAccessory>();
        }

        public int ApplianceAccessoryTypeId { get; set; }
        public string ApplianceAccessoryTypeName { get; set; }
        public string ApplianceAccessoryTypeDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<ApplianceAccessory> ApplianceAccessories { get; set; }
    }
}
