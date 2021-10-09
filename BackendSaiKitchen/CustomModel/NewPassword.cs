namespace BackendSaiKitchen.CustomModel
{
    public class NewPassword
    {
        private string UserId;
        public string userId
        {
            get { return UserId; }
            set { UserId = value; }
        }

        private string UserPassword;
        public string userPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
    }
}
