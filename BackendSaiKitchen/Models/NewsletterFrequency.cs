using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class NewsletterFrequency
    {
        public NewsletterFrequency()
        {
            Newsletters = new HashSet<Newsletter>();
        }

        public int NewsletterFrequencyId { get; set; }
        public string NewsletterFrequencyName { get; set; }
        public string NewsletterFrequencyDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Newsletter> Newsletters { get; set; }
    }
}
