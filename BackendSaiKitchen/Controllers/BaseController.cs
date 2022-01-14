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
using System.Threading.Tasks;
using Log = Serilog.Log;

namespace SaiKitchenBackend.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("Api/[controller]")]
    public class BaseController : Controller
    {
        public Repository<Accesory> accesoryRepository;
        public Repository<Branch> branchRepository;
        public Repository<BranchRole> branchRoleRepository;
        public Repository<BranchType> branchTypeRepository;
        public Repository<CalendarEvent> calendarEventRepository;
        public Repository<ContactStatus> contactStatusRepository;


        public BackendSaiKitchen_dbContext context = new();
        public Repository<Customer> customerRepository;
        public Dictionary<object, object> dicResponse = new();
        public Repository<File> fileRepository;
        public Repository<Inquiry> inquiryRepository;
        public Repository<InquiryStatus> inquiryStatusRepository;

        public Repository<InquiryWorkscope> inquiryWorkscopeRepository;

        public MailService mailService = new();
        public Repository<Measurement> measurementRepository;
        public Repository<Notification> noificationRepository;

        public Notification notification = new();
        public Repository<Payment> paymentRepository;
        public Repository<Permission> permissionRepository;
        public Repository<PermissionRole> permissionRoleRepository;
        public Repository<Promo> promoRepository;
        public Repository<Quotation> quotationRepository;

        public ServiceResponse response = new();
        public Repository<RoleHead> roleHeadRepository;
        public Repository<RoleType> roleTypeRepository;
        public Repository<Setting> settingRepository;
        public TableResponse tableResponse = new();
        public Repository<TermsAndCondition> termsAndConditionsRepository;
        public Repository<User> userRepository;
        public Repository<UserRole> userRoleRepository;
        public Repository<WayOfContact> wayOfContactRepository;
        public Repository<WorkScope> workScopeRepository;
        public Repository<Brand> brandRepository;
        public Repository<ApplianceAccessory> applianceAccessoryRepository;
        public Repository<UnitOfMeasurement> unitOfMeasurementRepository;
        public Repository<JobOrder> jobOrderRepository;
        public Repository<ApplianceAccessoryType> applianceAccessoryTypeRepository;
        public Repository<Newsletter> newsletterRepository;
        public Repository<NewsletterType> newsletterTypeRepository;
        public Repository<NewsletterFrequency> newsletterFrequencyRepository;
        public Repository<ItemColor> itemColorRepository;
        public Repository<Color> colorRepository;
        public Repository<Comment> commentRepository;
        public Repository<PurchaseOrder> purchaseOrderRepository;
        public Repository<Promotion> promotionRepository;
        public Repository<PromotionType> promotionTypeRepository;

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
            FeesRepository = new Repository<Fee>(context);
            promoRepository = new Repository<Promo>(context);
            noificationRepository = new Repository<Notification>(context);
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
            brandRepository = new Repository<Brand>(context);
            applianceAccessoryRepository = new Repository<ApplianceAccessory>(context);
            unitOfMeasurementRepository = new Repository<UnitOfMeasurement>(context);
            jobOrderRepository = new Repository<JobOrder>(context);
            applianceAccessoryTypeRepository = new Repository<ApplianceAccessoryType>(context);
            newsletterRepository = new Repository<Newsletter>(context);
            newsletterTypeRepository = new Repository<NewsletterType>(context);
            newsletterFrequencyRepository = new Repository<NewsletterFrequency>(context);
            itemColorRepository = new Repository<ItemColor>(context);
            colorRepository = new Repository<Color>(context);
            commentRepository = new Repository<Comment>(context);
            purchaseOrderRepository = new Repository<PurchaseOrder>(context);
            promotionRepository = new Repository<Promotion>(context);
            promotionTypeRepository = new Repository<PromotionType>(context);
        }

        public Repository<Fee> FeesRepository { get; set; }

        public
            override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                context.HttpContext.Request.Headers.TryGetValue("UserToken", out StringValues authorizationToken);
                if (authorizationToken.Count > 0)
                {
                    Constants.userToken = authorizationToken[0];
                }

                context.HttpContext.Request.Headers.TryGetValue("UserId", out StringValues userId);
                if (userId.Count > 0)
                {
                    int.TryParse(userId[0], out Constants.userId);
                }

                context.HttpContext.Request.Headers.TryGetValue("userRoleId", out StringValues userRoleId);
                if (userRoleId.Count > 0)
                {
                    int.TryParse(userRoleId[0], out Constants.userRoleId);
                }

                context.HttpContext.Request.Headers.TryGetValue("BranchId", out StringValues branchId);
                if (branchId.Count > 0)
                {
                    int.TryParse(branchId[0], out Constants.branchId);
                }

                context.HttpContext.Request.Headers.TryGetValue("BranchRoleId", out StringValues branchRoleId);
                if (branchRoleId.Count > 0)
                {
                    int.TryParse(branchRoleId[0], out Constants.branchRoleId);
                }
                
                //var va = context.HttpContext.Request.Method;
                //int val;
                //_ = int.TryParse(context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "UserId").Value.FirstOrDefault(), out Constants.userId);
                //Constants.userToken = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "UserToken").Value.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }
        }


        [HttpPost]
        [Route("[action]")]
        public async Task sendNotificationToHead(string content, bool isActionable, string acceptAction,
            string declineAction, List<int?> roleTypeId, int? branchId, int categoryId)
        {
            List<int> branchRoleIds = branchRoleRepository
                .FindByCondition(x => roleTypeId.Contains(x.RoleTypeId) && x.IsActive == true && x.IsDeleted == false)
                .Select(x => x.BranchRoleId).ToList();
            List<NotificationModel> notificationsModel = userRoleRepository.FindByCondition(x =>
                branchRoleIds.Contains(x.BranchRoleId.GetValueOrDefault())
                && x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false && x.User != null).Select(x =>
                new NotificationModel
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
            foreach (NotificationModel notificationModel in notificationsModel)
            {
                Notification notification = new Notification
                {
                    NotificationContent = notificationModel.NotificationContent,
                    IsActionable = notificationModel.IsActionable,
                    IsRead = false,
                    NotificationAcceptAction = notificationModel.NotificationAcceptAction,
                    NotificationDeclineAction = notificationModel.NotificationDeclineAction,
                    UserRoleId = notificationModel.userRoleId,
                    NotificationCategoryId = notificationModel.NotificationCategoryId,
                    User = notificationModel.user
                };
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
                        PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken,
                            notificationModel.NotificationContent, null);
                        // Task.Run(() => PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken, notificationModel.NotificationContent, null));
                        string subject = Enum.GetName(typeof(NotificationCategory), categoryId) + acceptAction;
                        await mailService.SendEmailAsync(new MailRequest
                        {
                            Body = content,
                            Subject = subject,
                            ToEmail = notificationModel.user.UserEmail
                        });
                    }
                }

                catch (Exception ex)
                {
                    Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
                }
            }
            //context.SaveChanges();
        }


        [HttpPost]
        [Route("[action]")]
        public async Task sendNotificationToOneUser(string content, bool isActionable, string acceptAction,
            string declineAction, int userId, int branchId, int categoryId)
        {
            User user = userRepository
                .FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            List<NotificationModel> notificationsModel = userRepository
                .FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false).Select(x =>
                    new NotificationModel
                    {
                        BranchId = branchId,
                        user = x,
                        UserId = x.UserId,
                        NotificationContent = content,
                        NotificationCategoryId = categoryId,
                        IsActionable = isActionable,
                        IsRead = false,
                        NotificationAcceptAction = acceptAction,
                        NotificationDeclineAction = declineAction
                    }).ToList();


            //List<NotificationModel> notifications = notificationModel.ToList();
            foreach (NotificationModel notificationModel in notificationsModel)
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
                        PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken,
                            notificationModel.NotificationContent, null);
                        //Task.Run(() => PushNotification.pushNotification.SendPushNotification(notificationModel.user.UserFcmtoken, notificationModel.NotificationContent, null));

                        string subject = Enum.GetName(typeof(notificationCategory), categoryId) + acceptAction;
                        await mailService.SendEmailAsync(new MailRequest
                        {
                            Body = content,
                            Subject = subject,
                            ToEmail = notificationModel.user.UserEmail
                        });
                    }
                }

                catch (Exception ex)
                {
                    Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
                }
            }
        }

        protected object InquiryDetail(Inquiry inquiry)
        {
            if (inquiry != null)
            {
                Inquirychecklist inquirychecklist = new Inquirychecklist
                {
                    inquiry = inquiry,
                    fees = FeesRepository
                        .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.FeesId != 1).ToList()
                };
                if (inquirychecklist == null)
                {
                    response.isError = true;
                    response.errorMessage = "No Inquiry Found";
                }
                else
                {
                    inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                    response.data = inquirychecklist;
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry Not Found";
            }

            return response;
        }
    }
}