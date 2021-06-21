using Azure.Storage.Blobs;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using BackendSaiKitchen.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SaiKitchenBackend.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("Api/[controller]")]
    public class BaseController : Controller
    {

     
        public DbSaiKitchenContext context = new DbSaiKitchenContext();

        public ServiceResponse response = new ServiceResponse();
        public TableResponse tableResponse = new TableResponse();

        public Notification notification = new Notification();

        public MailService mailService = new MailService();
        public Repository<Accesory> accesoryRepository;
        public Repository<User> userRepository;
        public Repository<Branch> branchRepository;
        public Repository<BranchRole> branchRoleRepository;
        public Repository<Permission> permissionRepository;
        public Repository<PermissionRole> permissionRoleRepository;
        public Repository<UserRole> userRoleRepository;
        public Repository<RoleHead> roleHeadRepository;
        public Repository<BranchType> branchTypeRepository;
        public Repository<Customer> customerRepository;
        public Repository<ContactStatus> contactStatusRepository;
        public Repository<WayOfContact> wayOfContactRepository;
        public Repository<RoleType> roleTypeRepository;
        public Repository<Inquiry> inquiryRepository;
        public Repository<WorkScope> workScopeRepository;
        public Repository<Notification> noificationRepository;
        public Repository<InquiryWorkscope> inquiryWorkscopeRepository;
        public Repository<Measurement> measurementRepository;
        public Repository<File> fileRepository;
        public Repository<Quotation> quotationRepositry;
        public Dictionary<object, object> dicResponse = new Dictionary<object, object>();


        public BaseController()
        {
            userRepository = new Repository<User>(context);
            branchRepository = new Repository<Branch>(context);
            branchRoleRepository = new Repository<BranchRole>(context);
            permissionRepository = new Repository<Permission>(context);
            permissionRoleRepository = new Repository<PermissionRole>(context);
            userRoleRepository = new Repository<UserRole>(context);
            roleHeadRepository = new Repository<RoleHead>(context);
            branchTypeRepository = new Repository<BranchType>(context);
            customerRepository = new Repository<Customer>(context);
            contactStatusRepository = new Repository<ContactStatus>(context);
            wayOfContactRepository = new Repository<WayOfContact>(context);
            roleTypeRepository = new Repository<RoleType>(context);
            inquiryRepository = new Repository<Inquiry>(context);
            workScopeRepository = new Repository<WorkScope>(context);
            noificationRepository = new Repository<Notification>(context);
            inquiryWorkscopeRepository = new Repository<InquiryWorkscope>(context);
            measurementRepository = new Repository<Measurement>(context);
            fileRepository = new Repository<File>(context);
            accesoryRepository = new Repository<Accesory>(context);
            quotationRepositry = new Repository<Quotation>(context);

        }
        override
        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                StringValues authorizationToken;
                context.HttpContext.Request.Headers.TryGetValue("UserToken", out authorizationToken);
                if (authorizationToken.Count > 0)
                {
                    Constants.userToken = authorizationToken[0];
                }

                StringValues userId;
                context.HttpContext.Request.Headers.TryGetValue("UserId", out userId);
                if (userId.Count > 0)
                    int.TryParse(userId[0], out Constants.userId);

                StringValues userRoleId;
                context.HttpContext.Request.Headers.TryGetValue("userRoleId", out userRoleId);
                if (userRoleId.Count > 0)
                    int.TryParse(userRoleId[0], out Constants.userRoleId);

                StringValues branchId;
                context.HttpContext.Request.Headers.TryGetValue("BranchId", out branchId);
                if (branchId.Count > 0)
                    int.TryParse(branchId[0], out Constants.branchId);

                StringValues branchRoleId;
                context.HttpContext.Request.Headers.TryGetValue("BranchRoleId", out branchRoleId);
                if (branchRoleId.Count > 0)
                    int.TryParse(branchRoleId[0], out Constants.branchRoleId);

                //var va = context.HttpContext.Request.Method;
                //int val;
                //_ = int.TryParse(context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "UserId").Value.FirstOrDefault(), out Constants.userId);
                //Constants.userToken = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "UserToken").Value.FirstOrDefault();
            }
            catch (Exception ex) {

                Serilog.Log.Error("Error: UserId="+Constants.userId+" Error="+ ex.Message+" "+ex.ToString()); 
            }
        }


        [HttpPost]
        [Route("[action]")]
        public void sendNotificationToHead(string content, bool isActionable, String acceptAction, String declineAction, List<int?> roleTypeId, int? branchId, int categoryId)
        {
            var branchRoleIds = branchRoleRepository.FindByCondition(x => roleTypeId.Contains(x.RoleTypeId) && x.IsActive == true && x.IsDeleted == false).Select(x => x.BranchRoleId).ToList();
            List<NotificationModel> notificationsModel = userRoleRepository.FindByCondition(x => branchRoleIds.Contains(x.BranchRoleId.GetValueOrDefault()) && x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false).Select(x => new NotificationModel
            {
                userRoleId = x.UserRoleId,
                user = x.User,
                NotificationContent = content,
                NotificationCategoryId = categoryId,
                IsActionable = isActionable,
                IsRead = false,
                BranchId = branchId,
                NotificationAcceptAction = acceptAction,
                UserRoleId = x.UserRoleId,
                NotificationDeclineAction = declineAction
            }).ToList();


            //List<NotificationModel> notifications = notificationModel.ToList();
            foreach (var notificationModel in notificationsModel)
            {
                notification.NotificationContent = notificationModel.NotificationContent;
                notification.IsActionable = notificationModel.IsActionable;
                notification.IsRead = false;
                notification.NotificationAcceptAction = notificationModel.NotificationAcceptAction;
                notification.NotificationDeclineAction = notificationModel.NotificationDeclineAction;
                notification.UserRoleId = notificationModel.userRoleId;
                notification.NotificationCategoryId = notificationModel.NotificationCategoryId;
                notification.User = notificationModel.user;
                notification.UserRoleId = notificationModel.userRoleId;
                notification.BranchId = notificationModel.BranchId;
                notification.IsActive = true;
                notification.IsDeleted = false;


                noificationRepository.Create(notification);

                //context.SaveChanges();
                try
                {
                    if (notificationModel.user.UserFcmtoken != null && notificationModel.user.UserFcmtoken != "")
                    {
                  //      PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken, notificationModel.NotificationContent, null);
                        Task.Run(() => PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken, notificationModel.NotificationContent, null));

                    }
                }

                catch (Exception ex)
                {

                    Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
                }
            }


        }


        [HttpPost]
        [Route("[action]")]
        public void sendNotificationToOneUser(string content, bool isActionable, String acceptAction, String declineAction, int userId, int branchId, int categoryId)
        {
            var user = userRepository.FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            List<NotificationModel> notificationsModel = userRepository.FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false).Select(x => new NotificationModel
            { BranchId = branchId, user = x, UserId = x.UserId, NotificationContent = content, NotificationCategoryId = categoryId, IsActionable = isActionable, IsRead = false, NotificationAcceptAction = acceptAction, NotificationDeclineAction = declineAction }).ToList();


            //List<NotificationModel> notifications = notificationModel.ToList();
            foreach (var notificationModel in notificationsModel)
            {
                notification.NotificationContent = notificationModel.NotificationContent;
                notification.IsActionable = notificationModel.IsActionable;
                notification.IsRead = false;
                notification.NotificationAcceptAction = notificationModel.NotificationAcceptAction;
                notification.NotificationDeclineAction = notificationModel.NotificationDeclineAction;
                notification.NotificationCategoryId = notificationModel.NotificationCategoryId;
                notification.UserId = notificationModel.UserId;
                notification.User = notificationModel.user;
                notification.BranchId = notificationModel.BranchId;
                notification.IsActive = true;
                notification.IsDeleted = false;

                noificationRepository.Create(notification);
                //context.SaveChanges();

                try
                {
                    if (notificationModel.user.UserFcmtoken != null && notificationModel.user.UserFcmtoken != "")
                    {
                      //  PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken, notificationModel.NotificationContent, null);
                        Task.Run(() => PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken, notificationModel.NotificationContent, null));
                    }
                }

                catch (Exception ex)
                {

                    Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
                }
            }

            


        }

    }

}
