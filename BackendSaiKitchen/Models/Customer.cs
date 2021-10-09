using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class Customer
    {
        public Customer()
        {
            CustomerBranches = new HashSet<CustomerBranch>();
            Inquiries = new HashSet<Inquiry>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerNationality { get; set; }
        public string CustomerNationalId { get; set; }
        public string CustomerNotes { get; set; }
        public string CustomerNextMeetingDate { get; set; }
        public int? ContactStatusId { get; set; }
        public int? WayofContactId { get; set; }
        public int? BranchId { get; set; }
        public int? UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsEscalationRequested { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CustomerWhatsapp { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual ContactStatus ContactStatus { get; set; }
        public virtual User User { get; set; }
        public virtual WayOfContact WayofContact { get; set; }
        public virtual ICollection<CustomerBranch> CustomerBranches { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
    }
}
