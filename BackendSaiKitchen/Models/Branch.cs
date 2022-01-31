using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Branch
    {
        public Branch()
        {
            CommercialProjects = new HashSet<CommercialProject>();
            CustomerBranches = new HashSet<CustomerBranch>();
            Customers = new HashSet<Customer>();
            Inquiries = new HashSet<Inquiry>();
            JobOrders = new HashSet<JobOrder>();
            Notifications = new HashSet<Notification>();
            UserRoles = new HashSet<UserRole>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchContact { get; set; }
        public int? BranchTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual BranchType BranchType { get; set; }
        public virtual ICollection<CommercialProject> CommercialProjects { get; set; }
        public virtual ICollection<CustomerBranch> CustomerBranches { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<JobOrder> JobOrders { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
