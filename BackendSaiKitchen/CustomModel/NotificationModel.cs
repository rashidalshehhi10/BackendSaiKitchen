using BackendSaiKitchen.Models;

namespace BackendSaiKitchen.CustomModel
{
    public class NotificationModel
    {
        private int? userRoleId;
        public int? UserRoleId
        {
            get { return userRoleId; }
            set { userRoleId = value; }
        }
        private User user;
        public User User
        {
            get { return user; }
            set { user = value; }
        }
        private int? userId;
        public int? UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string NotificationContent { get; set; }
        public int? NotificationCategoryId { get; set; }
        public bool? IsActionable { get; set; }
        public string NotificationAcceptAction { get; set; }
        public string NotificationDeclineAction { get; set; }
        public int? BranchId { get; set; }
        public bool? IsRead { get; set; }

    }
}
