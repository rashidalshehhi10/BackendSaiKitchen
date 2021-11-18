using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class ApplianceAccessoryCustom
    {
        public int ApplianceAccessoryId { get; set; }
        public string SKUCode { get; set; }
        public string ApplianceAccessoryName { get; set; }
        public string ApplianceAccesoryDescription { get; set; }
        public double ApplianceAccessoryPrice { get; set; }
        public int ApplianceAccessoryTypeId { get; set; }
        public int BrandId { get; set; }
        public int UnitOfMeasurementId { get; set; }
        public string ApplianceAccessoryImgUrl { get; set; }
        public int colorId { get; set; }
        public int itemcolorId { get; set; }

    }

    public class AccessoryCustom
    {
        public int brandId { get; set; }
        public int TypeId { get; set; }
    }
}
