using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class PromotionType
    {
        public PromotionType()
        {
            Promotions = new HashSet<Promotion>();
        }

        public int PromotionTypeId { get; set; }
        public string PromotionTypeName { get; set; }
        public string PromotionTypeDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
