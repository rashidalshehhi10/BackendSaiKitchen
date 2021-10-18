using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Serilog;
using System;

namespace BackendSaiKitchen.Helper
{
    public class PushNotification
    {
        public static PushNotification pushNotification = new();

        private PushNotification()
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("sai-kitchencrm-firebase-adminsdk-tmg5e-454a2caa79.json")
            });
        }

        public async void SendPushNotification(string fcmToken, string title, string body)
        {
            try
            {
                string registrationToken = fcmToken;
                Message message = new Message
                {
                    //Topic="Test", 
                    Token = registrationToken
                };
                message.Notification = new Notification
                {
                    Body = body,
                    Title = title
                };
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);
            }

            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }
        }
    }
}