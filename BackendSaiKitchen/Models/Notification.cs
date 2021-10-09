using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string NotificationContent { get; set; }
        public int? NotificationCategoryId { get; set; }
        public bool? IsActionable { get; set; }
        public string NotificationAcceptAction { get; set; }
        public string NotificationDeclineAction { get; set; }
        public int? UserId { get; set; }
        public int? BranchId { get; set; }
        public int? UserRoleId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual NotificationCategory NotificationCategory { get; set; }
        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
