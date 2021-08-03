﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Quotation
    {
        public Quotation()
        {
            Files = new HashSet<File>();
            Payments = new HashSet<Payment>();
        }

        public int QuotationId { get; set; }
        public int? InquiryId { get; set; }
        public string Description { get; set; }
        public string QuotationValidityDate { get; set; }
        public string AdvancePayment { get; set; }
        public string BeforeInstallation { get; set; }
        public string AfterDelivery { get; set; }
        public string Amount { get; set; }
        public string TotalAmount { get; set; }
        public string Vat { get; set; }
        public string Discount { get; set; }
        public bool? IsInstallment { get; set; }
        public int? NoOfInstallment { get; set; }
        public int? FeedBackReactionId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Inquiry Inquiry { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
