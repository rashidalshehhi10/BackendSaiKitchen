using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Promotion
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public string PromotionDescription { get; set; }
        public int? PromotionTypeId { get; set; }
        public string PromotionFile { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual PromotionType PromotionType { get; set; }
    }
}
