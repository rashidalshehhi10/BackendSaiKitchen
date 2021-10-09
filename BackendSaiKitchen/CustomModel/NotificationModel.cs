using BackendSaiKitchen.Models;

namespace BackendSaiKitchen.CustomModel
{
    public class NotificationModel
    {
        public int userRoleId;
        public User user;
        public int? UserId;
        public string NotificationContent { get; set; }
        public int? NotificationCategoryId { get; set; }
        public bool? IsActionable { get; set; }
        public string NotificationAcceptAction { get; set; }
        public string NotificationDeclineAction { get; set; }
        public int? UserRoleId { get; set; }
        public int? BranchId { get; set; }
        public bool? IsRead { get; set; }

    }
}
