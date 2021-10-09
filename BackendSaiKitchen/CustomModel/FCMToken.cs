namespace BackendSaiKitchen.CustomModel
{
    public class FCMToken
    {
        private int UserId;
        public int userId
        {
            get { return UserId; }
            set { UserId = value; }
        }


        private string UserFCMToken;
        public string userFCMToken
        {
            get { return UserFCMToken; }
            set { UserFCMToken = value; }
        }
    }
}
