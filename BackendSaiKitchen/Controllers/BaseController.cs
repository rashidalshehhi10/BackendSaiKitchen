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
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("Api/[controller]")]
    public class BaseController : Controller
    {


        private BackendSaiKitchen_dbContext Context = new BackendSaiKitchen_dbContext();
        
        public BackendSaiKitchen_dbContext context
        {
            get { return Context; }
            set { Context = value; }
        }

        private ServiceResponse _Response = new ServiceResponse();
        public ServiceResponse response
        {
            get { return _Response; }
            set { _Response = value; }
        }
        private TableResponse TableResponse = new TableResponse();
        public TableResponse tableResponse
        {
            get { return TableResponse; }
            set { TableResponse = value; }
        }
        private MailService MailService = new MailService();
        public MailService mailService
        {
            get { return MailService; }
            set { MailService = value; }
        }

        private Notification notification = new Notification();
        public Notification Notification 
        { 
            get { return notification; } 
            set { notification = value; }
        }
        private Repository<Accesory> accesoryRepository;
        public Repository<Accesory> AccesoryRepository
        {
            get { return AccesoryRepository; }
            set { AccesoryRepository = value; }
        }

        private Repository<User> userRepository;
        public Repository<User> UserRepository
        {
            get { return userRepository; }
            set { userRepository = value; }
        }

        private Repository<Branch> branchRepository;
        public Repository<Branch> BranchRepository
        {
            get { return branchRepository; }
            set { branchRepository = value; }
        }
        private Repository<BranchRole> branchRoleRepository;
        public Repository<BranchRole> BranchRoleRepository
        {
            get { return branchRoleRepository; }
            set { branchRoleRepository = value; }
        }

        private Repository<Permission> permissionRepository;
        public Repository<Permission> PermissionRepository
        {
            get { return permissionRepository; }
            set { permissionRepository = value; }
        }
        private Repository<PermissionRole> permissionRoleRepository;
        public Repository<PermissionRole> PermissionRoleRepository
        {
            get { return permissionRoleRepository; }
            set { permissionRoleRepository = value; }
        }
        private Repository<UserRole> userRoleRepository;
        public Repository<UserRole> UserRoleRepository
        {
            get { return userRoleRepository; }
            set { userRoleRepository = value; }
        }
        private Repository<BranchType> branchTypeRepository;
        public Repository<BranchType> BranchTypeRepository
        {
            get { return branchTypeRepository; }
            set { branchTypeRepository = value; }
        }
        private Repository<Customer> customerRepository;
        public Repository<Customer> CustomerRepository
        {
            get { return customerRepository; }
            set { customerRepository = value; }
        }
        private Repository<ContactStatus> contactStatusRepository;
        public Repository<ContactStatus> ContactStatusRepository
        {
            get { return contactStatusRepository; }
            set { contactStatusRepository = value; }
        }
        private Repository<WayOfContact> wayOfContactRepository;
        public Repository<WayOfContact> WayOfContactRepository
        {
            get { return wayOfContactRepository; }
            set { wayOfContactRepository = value; }
        }
        private Repository<RoleType> roleTypeRepository;
        public Repository<RoleType> RoleTypeRepository
        {
            get { return roleTypeRepository; }
            set { roleTypeRepository = value; }
        }
        private Repository<Inquiry> inquiryRepository;
        public Repository<Inquiry> InquiryRepository
        {
            get { return inquiryRepository; }
            set { inquiryRepository = value; }
        }
        private Repository<WorkScope> workScopeRepository;
        public Repository<WorkScope> WorkScopeRepository
        {
            get { return workScopeRepository; }
            set { workScopeRepository = value; }
        }
        private Repository<Fee> feesRepository;
        public Repository<Fee> FeesRepository
        {
            get { return feesRepository; }
            set { feesRepository = value; }
        }
        public Repository<Promo> promoRepository;
        private Repository<Notification> notificationRepository;
        public Repository<Notification> NotificationRepository
        {
            get { return notificationRepository; }
            set { notificationRepository = value; }
        }
        private Repository<InquiryWorkscope> inquiryWorkscopeRepository;
        public Repository<InquiryWorkscope> InquiryWorkscopeRepository
        {
            get { return inquiryWorkscopeRepository; }
            set { inquiryWorkscopeRepository = value; }
        }
        private Repository<Measurement> measurementRepository;
        public Repository<Measurement> MeasurementRepository
        {
            get { return measurementRepository; }
            set { measurementRepository = value; }
        }
        private Repository<File> fileRepository;
        public Repository<File> FileRepository
        {
            get { return fileRepository; }
            set { fileRepository = value; }
        }
        private Repository<Quotation> quotationRepository;
        public Repository<Quotation> QuotationRepository
        {
            get { return quotationRepository; }
            set { quotationRepository = value; }
        }
        private Repository<TermsAndCondition> termsAndConditionsRepository;
        public Repository<TermsAndCondition> TermsAndConditionsRepository
        {
            get { return termsAndConditionsRepository; }
            set { termsAndConditionsRepository = value; }
        }
        private Repository<Payment> paymentRepository;
        public Repository<Payment> PaymentRepository
        {
            get { return paymentRepository; }
            set { paymentRepository = value; }
        }
        private Repository<CalendarEvent> calendarEventRepository;
        public Repository<CalendarEvent> CalendarEventRepository
        {
            get { return calendarEventRepository; }
            set { calendarEventRepository = value; }
        }
        private Repository<Setting> settingRepository;
        public Repository<Setting> SettingRepository
        {
            get { return settingRepository; }
            set { settingRepository = value; }
        }
        private Repository<InquiryStatus> inquiryStatusRepository;
        public Repository<InquiryStatus> InquiryStatusRepository
        {
            get { return inquiryStatusRepository; }
            set { inquiryStatusRepository = value; }
        }
        private Repository<RoleHead> roleHeadRepository;
        public Repository<RoleHead> RoleHeadRepository
        {
            get { return roleHeadRepository; }
            set { roleHeadRepository = value; }
        }

        private Dictionary<object, object> dicResponse = new Dictionary<object, object>();
        public Dictionary<object, object> DicResponse
        {
            get { return DicResponse; }
            set { DicResponse = value; }
        }



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
            feesRepository = new Repository<Fee>(context);
            promoRepository = new Repository<Promo>(context);
            notificationRepository = new Repository<Notification>(context);
            inquiryWorkscopeRepository = new Repository<InquiryWorkscope>(context);
            measurementRepository = new Repository<Measurement>(context);
            fileRepository = new Repository<File>(context);
            accesoryRepository = new Repository<Accesory>(context);
            quotationRepository = new Repository<Quotation>(context);
            termsAndConditionsRepository = new Repository<TermsAndCondition>(context);
            paymentRepository = new Repository<Payment>(context);
            calendarEventRepository = new Repository<CalendarEvent>(context);
            settingRepository = new Repository<Setting>(context);
            inquiryStatusRepository = new Repository<InquiryStatus>(context);
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
                {
                    _ = int.TryParse(userId[0], out Constants.userId);
                }

                StringValues userRoleId;
                context.HttpContext.Request.Headers.TryGetValue("userRoleId", out userRoleId);
                if (userRoleId.Count > 0)
                {
                    _ = int.TryParse(userRoleId[0], out Constants.userRoleId);
                }
                StringValues branchId;
                context.HttpContext.Request.Headers.TryGetValue("BranchId", out branchId);
                if (branchId.Count > 0)
                    _ = int.TryParse(branchId[0], out Constants.branchId);

                StringValues branchRoleId;
                context.HttpContext.Request.Headers.TryGetValue("BranchRoleId", out branchRoleId);
                if (branchRoleId.Count > 0)
                {
                    _ = int.TryParse(branchRoleId[0], out Constants.branchRoleId);
                }

            }
            catch (Exception ex)
            {

                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
        }


        [HttpPost]
        [Route("[action]")]
        public async void sendNotificationToHead(string content, bool isActionable, String acceptAction, String declineAction, List<int?> roleTypeId, int? branchId, int categoryId)
        {

            var branchRoleIds = branchRoleRepository.FindByCondition(x => roleTypeId.Contains(x.RoleTypeId) && x.IsActive == true && x.IsDeleted == false).Select(x => x.BranchRoleId).ToList();
            List<NotificationModel> notificationsModel = userRoleRepository.FindByCondition(x => branchRoleIds.Contains(x.BranchRoleId.GetValueOrDefault())
            && x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false && x.User != null).Select(x => new NotificationModel
            {
                UserRoleId = x.UserRoleId,
                User = x.User,
                NotificationContent = content,
                NotificationCategoryId = categoryId,
                IsActionable = isActionable,
                IsRead = false,
                BranchId = branchId,
                NotificationAcceptAction = acceptAction,
                NotificationDeclineAction = declineAction
            }).ToList();


            //List<NotificationModel> notifications = notificationModel.ToList();
            foreach (var notificationModel in notificationsModel)
            {
                Notification notification = new Notification();

                notification.NotificationContent = notificationModel.NotificationContent;
                notification.IsActionable = notificationModel.IsActionable;
                notification.IsRead = false;
                notification.NotificationAcceptAction = notificationModel.NotificationAcceptAction;
                notification.NotificationDeclineAction = notificationModel.NotificationDeclineAction;
                notification.UserRoleId = notificationModel.UserRoleId;
                notification.NotificationCategoryId = notificationModel.NotificationCategoryId;
                notification.User = notificationModel.User;
                notification.UserRoleId = notificationModel.UserRoleId;
                notification.BranchId = notificationModel.BranchId;
                notification.IsActive = true;
                notification.IsDeleted = false;


                NotificationRepository.Create(notification);

                //context.SaveChanges();
                try
                {
                    if (notificationModel.User.UserFcmtoken != null && notificationModel.User.UserFcmtoken != "")
                    {
                        PushNotification.pushNotification.SendPushNotification(notificationModel.User.UserFcmtoken, notificationModel.NotificationContent, null);
                        // Task.Run(() => PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken, notificationModel.NotificationContent, null));
                        string subject = Enum.GetName(typeof(NotificationCategory), categoryId) + acceptAction;
                        await mailService.SendEmailAsync(new MailRequest
                        {
                            Body = content,
                            Subject = subject,
                            ToEmail = notificationModel.User.UserEmail
                        });
                    }
                }

                catch (Exception ex)
                {

                    Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
                }
            }
            //context.SaveChanges();

        }


        [HttpPost]
        [Route("[action]")]
        public async void sendNotificationToOneUser(string content, bool isActionable, String acceptAction, String declineAction, int userId, int branchId, int categoryId)
        {
            var user = userRepository.FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            List<NotificationModel> notificationsModel = userRepository.FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false).Select(x => new NotificationModel
            { BranchId = branchId, User = x, UserId = x.UserId, NotificationContent = content, NotificationCategoryId = categoryId, IsActionable = isActionable, IsRead = false, NotificationAcceptAction = acceptAction, NotificationDeclineAction = declineAction }).ToList();


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
                notification.User = notificationModel.User;
                notification.BranchId = notificationModel.BranchId;
                notification.IsActive = true;
                notification.IsDeleted = false;

                NotificationRepository.Create(notification);
                //context.SaveChanges();

                try
                {
                    if (notificationModel.User.UserFcmtoken != null && notificationModel.User.UserFcmtoken != "")
                    {
                        PushNotification.pushNotification.SendPushNotification(notificationModel.User.UserFcmtoken, notificationModel.NotificationContent, null);
                        //Task.Run(() => PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken, notificationModel.NotificationContent, null));

                        string subject = Enum.GetName(typeof(notificationCategory), categoryId) + acceptAction;
                        await mailService.SendEmailAsync(new MailRequest
                        {
                            Body = content,
                            Subject = subject,
                            ToEmail = notificationModel.User.UserEmail
                        });
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
