using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class PaymentMode
    {
        public PaymentMode()
        {
            Payments = new HashSet<Payment>();
        }

        public int PaymentModeId { get; set; }
        public string PaymentModeName { get; set; }
        public string PaymentModeDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
