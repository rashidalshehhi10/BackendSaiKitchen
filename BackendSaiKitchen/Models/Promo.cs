using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Promo
    {
        public Promo()
        {
            Inquiries = new HashSet<Inquiry>();
        }

        public int PromoId { get; set; }
        public string PromoName { get; set; }
        public string PromoDescription { get; set; }
        public string PromoTermsAndCondition { get; set; }
        public string PromoCode { get; set; }
        public bool? IsPercentage { get; set; }
        public string PromoDiscount { get; set; }
        public int? PromoCurrency { get; set; }
        public string PromoStartDate { get; set; }
        public string PromoExpiryDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; }
    }
}
