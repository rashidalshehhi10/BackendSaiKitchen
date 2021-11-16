using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Newsletter
    {
        public int NewsletterId { get; set; }
        public string NewsletterHeading { get; set; }
        public string NewsletterBody { get; set; }
        public string NewsletterAttachmentUrl { get; set; }
        public int? NewsletterTypeId { get; set; }
        public int? NewsletterFrequencyId { get; set; }
        public string NewsletterSendingDate { get; set; }
        public int? AddedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual User AddedByNavigation { get; set; }
        public virtual NewsletterFrequency NewsletterFrequency { get; set; }
        public virtual NewsletterType NewsletterType { get; set; }
    }
}
