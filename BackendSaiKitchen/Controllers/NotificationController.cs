using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaiKitchenBackend.Controllers
{
    public class NotificationController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetPagedNotification(int userId, int pageId)
        {

            //response.data = noificationRepository.GetPagedAsync(h => h.OrderBy(l => l.NotificationId), g => g.GroupId == _categoryId, skip, take);

            response.data = await noificationRepository.GetPagedAsync(x => x.OrderBy(y => y.NotificationId), x => x.IsActive == true && x.IsDeleted == false && x.UserId == userId, pageId, 10, null);
            return response;
        }
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
            response.data = noificationRepository.FindByCondition(x => (x.UserId == user.UserId || user.UserRoles.Contains(x.UserRole)) && x.IsActive == true && x.IsDeleted == false).Select(x => new NotificationResponse()
            {
                notificationId = x.NotificationId,
                notificationContent = x.NotificationContent,
                notificationAcceptAction = x.NotificationAcceptAction,
                notificationDeclineAction = x.NotificationDeclineAction,
                notificationCategoryId = x.NotificationCategoryId,
                notificationCategoryName = x.NotificationCategory.NotificationCategoryName,
                isActionable = x.IsActionable,
                createdDate = x.CreatedDate
            }).OrderByDescending(x => x.notificationId).ToList().Take(30);
            return response;
        }


    }
}
