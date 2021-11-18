﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class ApplianceAccessory
    {
        public ApplianceAccessory()
        {
            ItemColors = new HashSet<ItemColor>();
        }

        public int ApplianceAccessoryId { get; set; }
        public string ApplianceAccessoryName { get; set; }
        public string ApplianceAccesoryDescription { get; set; }
        public int? ApplianceAccessoryTypeId { get; set; }
        public int? UnitOfMeasurementId { get; set; }
        public double? ApplianceAccessoryPrice { get; set; }
        public int? BrandId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ApplianceAccessoryType ApplianceAccessoryType { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }
        public virtual ICollection<ItemColor> ItemColors { get; set; }
    }
}
