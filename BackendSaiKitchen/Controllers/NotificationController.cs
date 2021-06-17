using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    public class NotificationController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public Object GeAllNotification()
        {
            response.data = noificationRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public Object GeAllNotificationofUser(int userId)
        {
            User user = userRepository.FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false).Include(x => x.UserRoles).FirstOrDefault();
            Console.WriteLine(user);
            response.data = noificationRepository.FindByCondition(x => (x.UserId == user.UserId || user.UserRoles.Contains(x.UserRole)) && x.IsActive == true && x.IsDeleted == false).Select(x => new NotificationResponse() { notificationId = x.NotificationId, notificationContent = x.NotificationContent, notificationAcceptAction = x.NotificationAcceptAction, notificationDeclineAction = x.NotificationDeclineAction, notificationCategoryId = x.NotificationCategoryId, notificationCategoryName = x.NotificationCategory.NotificationCategoryName, isActionable = x.IsActionable,createdDate=x.CreatedDate }).OrderByDescending(x => x.notificationId);
            return response;
        }


    }
}
