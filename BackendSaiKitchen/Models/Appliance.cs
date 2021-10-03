using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Appliance
    {
        public int AppliancesId { get; set; }
        public int? KitchenDesignInfoId { get; set; }
        public string AppliancesName { get; set; }
        public bool? AppliancesValue { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual KitchenDesignInfo KitchenDesignInfo { get; set; }
    }
}
