using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class UnitOfMeasurement
    {
        public UnitOfMeasurement()
        {
            ApplianceAccessories = new HashSet<ApplianceAccessory>();
        }

        public int UnitOfMeasurementId { get; set; }
        public string UnitOfMeasurementName { get; set; }
        public string UnitOfMeasurementDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<ApplianceAccessory> ApplianceAccessories { get; set; }
    }
}
