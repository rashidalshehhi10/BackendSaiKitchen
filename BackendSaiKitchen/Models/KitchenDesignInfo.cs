using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class KitchenDesignInfo
    {
        public KitchenDesignInfo()
        {
            Appliances = new HashSet<Appliance>();
            Measurements = new HashSet<Measurement>();
        }

        public int Kdiid { get; set; }
        public string KdikitchenType { get; set; }
        public string KdiboardModel { get; set; }
        public string KdiselectedColor { get; set; }
        public string KdibaseUnitHeight { get; set; }
        public string KdiwallUnitHeight { get; set; }
        public string Kdiworktop { get; set; }
        public string Kdinotes { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Appliance> Appliances { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
