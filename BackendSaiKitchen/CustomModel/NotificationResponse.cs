namespace BackendSaiKitchen.CustomModel
{
    public class NotificationResponse
    {
        public int notificationId { get; set; }
        public string notificationContent { get; set; }
        public int? notificationCategoryId { get; set; }
        public string notificationCategoryName { get; set; }
        public string createdDate { get; set; }
        public bool? isActionable { get; set; }
        public object notificationAcceptAction { get; set; }
        public object notificationDeclineAction { get; set; }
    }
}