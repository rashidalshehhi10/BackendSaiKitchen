using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class NewsletterType
    {
        public NewsletterType()
        {
            Newsletters = new HashSet<Newsletter>();
        }

        public int NewsletterTypeId { get; set; }
        public string NewsletterTypeName { get; set; }
        public string NewsletterTypeDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Newsletter> Newsletters { get; set; }
    }
}
