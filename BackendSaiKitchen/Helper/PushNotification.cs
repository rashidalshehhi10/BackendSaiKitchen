using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;

namespace BackendSaiKitchen.Helper
{
    public class PushNotification
    {
        public static PushNotification _pushNotification = new PushNotification();

        public static PushNotification pushNotification
        {
            get { return _pushNotification; }
            set { _pushNotification = value; }
        }

        PushNotification()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("sai-kitchencrm-firebase-adminsdk-tmg5e-454a2caa79.json"),
            });
        }
        public async void SendPushNotification(string fcmToken, string title, string body)
        {
            try
            {
                var registrationToken = fcmToken;
                var message = new FirebaseAdmin.Messaging.Message
                { 
                    Token = registrationToken,
                };
                message.Notification = new Notification();
                message.Notification.Body = body;
                message.Notification.Title = title;
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);
            }

            catch (Exception ex)
            {

                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
        }

    }
}
