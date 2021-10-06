using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Notifications = new HashSet<Notification>();
        }

        public int UserRoleId { get; set; }
        public int? UserId { get; set; }
        public int? BranchId { get; set; }
        public int? BranchRoleId { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual BranchRole BranchRole { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
