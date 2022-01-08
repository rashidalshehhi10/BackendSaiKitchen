using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Conversations.V1;
using Twilio.Types;

namespace SaiKitchenBackend.Controllers
{
    public class CustomerController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public void SendWhatsapp(string number)
        {
            // Find your Account Sid and Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "ACb1325ec580f878d09d7a1a0d6ef65f95";
            string authToken = "68215878731f6303a8ce550fcb89df78";

            TwilioClient.Init(accountSid, authToken);

            ConversationResource conversation = ConversationResource.Create();

            Console.WriteLine(conversation.Sid);

            //TwilioClient.Init(accountSid, authToken);

            //var message = MessageResource.Create(
            //    from: new Twilio.Types.PhoneNumber("+18316528015"),
            //    body: "Test",
            //    to: new Twilio.Types.PhoneNumber("+971"+number)
            //);

            TwilioClient.Init(accountSid, authToken);

            CreateMessageOptions messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:+971545552471"))
            {
                From = new PhoneNumber("whatsapp:SAI GROUP"),
                Body = "Test Message"
            };
            //messageOptions.MediaUrl = new System.Collections.Generic.List<Uri> {new Uri("https://saikitchenstorage.blob.core.windows.net/files/eb2b1f50-342c-4bb6-b175-75100a319e74.pdf") };
            MessageResource message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);

            //Console.WriteLine(message.Sid);
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetAllCustomer()
        {
            //PushNotification.pushNotification.SendPushNotification();


            return customerRepository
                .FindByCondition(x =>
                    x.IsActive == true && x.IsDeleted == false && x.Branch.IsActive == true &&
                    x.Branch.IsDeleted == false).Include(x => x.Branch)
                .Where(x => x.IsActive == true && x.IsDeleted == false).Include(x => x.User)
                .Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => new CustomerResponse
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    CustomerContact = x.CustomerContact,
                    CustomerEmail = x.CustomerEmail,
                    BranchId = x.Branch.BranchId,
                    BranchName = x.Branch.BranchName,
                    UserId = x.User.UserId,
                    UserName = x.User.UserName,
                    CustomerCity = x.CustomerCity,
                    CustomerCountry = x.CustomerCountry,
                    CustomerNationality = x.CustomerNationality,
                    WayofContactId = x.WayofContactId,
                    ContactStatusId = x.ContactStatusId,
                    CustomerAddress = x.CustomerAddress,
                    CustomerNationalId = x.CustomerNationalId,
                });
        }

        [HttpPost]
        [Route("[action]")]
        public object GetCustomerGraph(int userId)
        {
            var customers = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId && x.Branch.IsActive == true && x.Branch.IsDeleted == false)
                .Include(x => x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false)).ToList();
            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(x => x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false)).Select(x => new
            {
                userId = x.UserId,
                username = x.UserName,
            }).ToList();
            List<object> userCount = new List<object>();
            foreach (var user in users)
            {
                var count = customers.Where(x => x.UserId == user.userId).Count();
                if (count > 0)
                {
                    userCount.Add(new
                    {
                        UserId = user.userId,
                        UserName = user.username,
                        CustomerCount = count
                    });
                }
            }
            int totalCustomers = customers.Count();
            object graph;
            if (userId != 0)
            {
                int customerWithInquiry = customers.Where(x => x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) && (x.ContactStatusId != (int)contactStatus.NeedToContact) && x.UserId == userId).Count();
                int customerWithOutInquiry = customers.Where(x => x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false && (x.ContactStatusId != (int)contactStatus.NeedToContact) && x.UserId == userId).Count();
                int contacted = customers.Where(x => x.ContactStatusId == 1 && x.UserId == userId).Count();
                int needToContact = customers.Where(x => x.ContactStatusId == 2 && x.UserId == userId).Count();
                int lostCustomer = customers.Where(x => x.ContactStatusId == 1 && x.Inquiries.Any(y => y.IsActive == true && y.IsDeleted == false) == false && x.UserId == userId).Count();
                int InquiryInProgress = customers.Where(x => x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) && x.UserId == userId).Count();
                int InquiryInProgressDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.measurementdelayed || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed)) && x.UserId == userId).Count();
                int InquiryInProgressongoing = InquiryInProgress - InquiryInProgressDelay;
                int notResponding = customers.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.UserId == userId).Count();
                int needToContactDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToContact && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date && x.UserId == userId).Count();
                int InquiryNeedToFollowUp = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) && x.UserId == userId).Count();
                int needToFollowUpDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) && x.UserId == userId).Count();
                int needTofollow = InquiryNeedToFollowUp - needToFollowUpDelay;
                int potentialCustomers = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false && x.UserId == userId).Count();
                int potentialCustomersDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false && x.UserId == userId).Count();
                int potentialCustomerFollowUp = potentialCustomers - potentialCustomersDelay;
                int needToFollowUpWithOutInquiry = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false && x.UserId == userId).Count();
                int UnresponsiveInquiry = customers.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) && x.UserId == userId).Count();
                int UnresponsivePotentialCustomers = customers.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false && x.UserId == userId).Count();
                graph = new
                {
                    totalCustomers,
                    userCount,
                    customerWithInquiry,
                    customerWithOutInquiry,
                    contacted,
                    needToContact,
                    lostCustomer,
                    InquiryInProgress,
                    InquiryInProgressongoing,
                    InquiryInProgressDelay,
                    notResponding,
                    needToContactDelay,
                    InquiryNeedToFollowUp,
                    needToFollowUpDelay,
                    needTofollow,
                    potentialCustomers,
                    potentialCustomersDelay,
                    potentialCustomerFollowUp,
                    needToFollowUpWithOutInquiry,
                    UnresponsiveInquiry,
                    UnresponsivePotentialCustomers
                };
            }
            else
            {
                int customerWithInquiry = customers.Where(x => x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) && (x.ContactStatusId != (int)contactStatus.NeedToContact)).Count();
                int customerWithOutInquiry = customers.Where(x => x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false && (x.ContactStatusId != (int)contactStatus.NeedToContact)).Count();
                int contacted = customers.Where(x => x.ContactStatusId == 1 ).Count();
                int needToContact = customers.Where(x => x.ContactStatusId == 2 ).Count();
                int lostCustomer = customers.Where(x => x.ContactStatusId == 1 && x.Inquiries.Any(y => y.IsActive == true && y.IsDeleted == false) == false ).Count();
                int InquiryInProgress = customers.Where(x => x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) ).Count();
                int InquiryInProgressDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.measurementdelayed || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed))).Count();
                int InquiryInProgressongoing = InquiryInProgress - InquiryInProgressDelay;
                int notResponding = customers.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing ).Count();
                int needToContactDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToContact && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date ).Count();
                int InquiryNeedToFollowUp = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false)).Count();
                int needToFollowUpDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) ).Count();
                int needTofollow = InquiryNeedToFollowUp - needToFollowUpDelay;
                int potentialCustomers = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false ) == false ).Count();
                int potentialCustomersDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false).Count();
                int potentialCustomerFollowUp = potentialCustomers - potentialCustomersDelay;
                int needToFollowUpWithOutInquiry = customers.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false ).Count();
                int UnresponsiveInquiry = customers.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false)).Count();
                int UnresponsivePotentialCustomers = customers.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false).Count();
                graph = new
                {
                    totalCustomers,
                    userCount,
                    customerWithInquiry,
                    customerWithOutInquiry,
                    contacted,
                    needToContact,
                    lostCustomer,
                    InquiryInProgress,
                    InquiryInProgressongoing,
                    InquiryInProgressDelay,
                    notResponding,
                    needToContactDelay,
                    InquiryNeedToFollowUp,
                    needToFollowUpDelay,
                    needTofollow,
                    potentialCustomers,
                    potentialCustomersDelay,
                    potentialCustomerFollowUp,
                    needToFollowUpWithOutInquiry,
                    UnresponsiveInquiry,
                    UnresponsivePotentialCustomers
                };
            }

            response.data = graph;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetCustomerGraphOfBranch(int userId, int filter)
        {
            var type = typeof(Customer);
            var parameterExprission = Expression.Parameter(typeof(Customer), "x");
            var constant = Expression.Constant(false, typeof(bool?));
            var property = Expression.Property(parameterExprission, "IsDeleted");
            var expression = Expression.Equal(property, constant);
            var property2 = Expression.Property(parameterExprission, "IsActive");
            constant = Expression.Constant(true, typeof(bool?));
            var experssion2 = Expression.Equal(property2, constant);
            expression = Expression.And(expression, experssion2);
            var property3 = Expression.Property(parameterExprission, "BranchId");
            constant = Expression.Constant(Constants.branchId, typeof(int?));
            var experssion3 = Expression.Equal(property3, constant);
            expression = Expression.And(expression, experssion3);
            Expression _property = parameterExprission;
            foreach (var item in "Branch.IsActive".Split('.'))
            {
                _property = Expression.PropertyOrField(_property, item);
            }
            constant = Expression.Constant(true, typeof(bool?));
            var _experssion = Expression.Equal(_property, constant);
            expression = Expression.And(expression, _experssion);

            Expression __property = parameterExprission;
            foreach (var item in "Branch.IsDeleted".Split('.'))
            {
                __property = Expression.PropertyOrField(__property, item);
            }
            constant = Expression.Constant(false, typeof(bool?));
            var __experssion = Expression.Equal(__property, constant);
            expression = Expression.And(expression, __experssion);

            if (userId != 0)
            {
                    var property1 = Expression.Property(parameterExprission, "UserId");
                    constant = Expression.Constant(userId, typeof(int?));
                    var experssion1 = Expression.Equal(property1, constant);
                    expression = Expression.And(expression, experssion1);
            }
            if (filter == 1)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToContact, typeof(int?));
                var _experssion1 = Expression.NotEqual(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                expression = Expression.And(expression, body);
            }
            else if (filter == 2)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToContact, typeof(int?));
                var _experssion1 = Expression.NotEqual(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                constant = Expression.Constant(false, typeof(bool));
                var ex = Expression.Equal(body, constant);
                expression = Expression.And(expression, ex);
            }
            else if (filter == 3 || filter == 16)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToContact, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);
            }
            else if (filter == 4)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.Contacted, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                expression = Expression.And(expression, body);
            }
            else if (filter == 5 || filter == 11 || filter == 13)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToFollowUp, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                expression = Expression.And(expression, body);
            }
            else if (filter == 6)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.Contacted, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                constant = Expression.Constant(false, typeof(bool));
                var ex = Expression.Equal(body, constant);
                expression = Expression.And(expression, ex);
            }
            else if (filter == 7)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NotResponing, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                constant = Expression.Constant(false, typeof(bool));
                var ex = Expression.Equal(body, constant);
                expression = Expression.And(expression, ex);
            }
            else if (filter == 9 || filter == 14 || filter == 15)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToFollowUp, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                constant = Expression.Constant(false, typeof(bool));
                var ex = Expression.Equal(body, constant);
                expression = Expression.And(expression, ex);
            }
            else if (filter == 10)
            {
                //int InquiryInProgressDelay = customers.Where(x => x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.measurementdelayed || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed))).Count();
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.Contacted, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _property3 = Expression.Property(_parameter, "InquiryStatusId");
                constant = Expression.Constant((int)inquiryStatus.measurementdelayed, typeof(int?));
                var _experssion3 = Expression.NotEqual(_property3, constant);
                experssion1 = Expression.And(experssion1, _experssion3);
                var _property4 = Expression.Property(_parameter, "InquiryStatusId");
                constant = Expression.Constant((int)inquiryStatus.designDelayed, typeof(int?));
                var _experssion4 = Expression.NotEqual(_property4, constant);
                experssion1 = Expression.And(experssion1, _experssion4);
                var _property5 = Expression.Property(_parameter, "InquiryStatusId");
                constant = Expression.Constant((int)inquiryStatus.quotationDelayed, typeof(int?));
                var _experssion5 = Expression.NotEqual(_property5, constant);
                experssion1 = Expression.And(experssion1, _experssion5);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                expression = Expression.And(expression, body);
            }
            else if (filter == 12)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.Contacted, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _property3 = Expression.Property(_parameter, "InquiryStatusId");
                constant = Expression.Constant((int)inquiryStatus.measurementdelayed, typeof(int?));
                var _experssion3 = Expression.Equal(_property3, constant);

                var _property4 = Expression.Property(_parameter, "InquiryStatusId");
                constant = Expression.Constant((int)inquiryStatus.designDelayed, typeof(int?));
                var _experssion4 = Expression.Equal(_property4, constant);
                _experssion3 = Expression.Or(_experssion3, _experssion4);
                var _property5 = Expression.Property(_parameter, "InquiryStatusId");
                constant = Expression.Constant((int)inquiryStatus.quotationDelayed, typeof(int?));
                var _experssion5 = Expression.Equal(_property5, constant);
                _experssion3 = Expression.Or(_experssion3, _experssion5);
                experssion1 = Expression.And(experssion1, _experssion3);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                expression = Expression.And(expression, body);
            }
            

            var lambda = Expression.Lambda<Func<Customer, bool>>(expression, parameterExprission);
            var customers = customerRepository.FindByCondition(lambda).Include(x => x.Inquiries.Where(x => x.IsActive == true  && x.IsDeleted == false))
                            .Select(x =>
                                new
                                {
                                    CustomerId = x.CustomerId,
                                    CustomerName = x.CustomerName,
                                    CustomerContact = x.CustomerContact,
                                    CustomerEmail = x.CustomerEmail,
                                    Code = "CS" + x.Branch.BranchId + "" + x.CustomerId,
                                    BranchId = x.Branch.BranchId,
                                    BranchName = x.Branch.BranchName,
                                    UserId = x.User.UserId,
                                    UserName = x.User.UserName,
                                    CustomerCity = x.CustomerCity,
                                    CustomerCountry = x.CustomerCountry,
                                    CustomerNationality = x.CustomerNationality,
                                    CustomerNotes = x.CustomerNotes,
                                    CustomerNextMeetingDate = x.CustomerNextMeetingDate,
                                    WayofContactId = x.WayofContactId,
                                    WayofContact = x.WayofContact.WayOfContactName,
                                    ContactStatusId = x.ContactStatusId,
                                    ContactStatus = x.ContactStatus.ContactStatusName,
                                    CustomerAddress = x.CustomerAddress,
                                    CustomerNationalId = x.CustomerNationalId,
                                    TotalNoOfInquiries = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == 0 ? "No Inquiries" : x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count().ToString(),
                                    AddedOn = x.CreatedDate,
                                    CustomerAssignedTo = x.CustomerAssignedTo,
                                    CustomerAssignedToName = x.CustomerAssignedToNavigation.UserName,
                                    CustomerAssignedBy = x.CustomerAssignedBy,
                                    CustomerAssignedByName = x.CustomerAssignedByNavigation.UserName,
                                    CustomerAssignedDate = x.CustomerAssignedDate,
                                    EscalationRequestedBy = x.EscalationRequestedBy,
                                    EscalationRequestedByName = x.EscalationRequestedByNavigation.UserName,
                                    EscalationRequestedOn = x.EscalationRequestedOn,
                                    IsEscalationRequested = x.IsEscalationRequested,
                                    EscalatedBy = x.EscalatedBy,
                                    EscalatedByName = x.EscalatedByNavigation.UserName,
                                    EscalatedOn = x.EscalatedOn,
                                    Inquiries=x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).ToList(),

                                }).OrderByDescending(i => i.CustomerId).ToList();



            if (filter == 11 || filter == 15)
            {
                customers = customers.Where(x => Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date >= Helper.ConvertToDateTime(Helper.GetDate())).OrderByDescending(i => i.CustomerId).ToList();
            }
            else if (filter == 13 || filter == 14 || filter == 16)
            {
                customers = customers.Where(x => Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDate())).OrderByDescending(i => i.CustomerId).ToList();
            }
            else if (filter == 8)
            {
                var date = DateTime.Now.Date;
                var helper = Helper.ConvertToDateTime(Helper.GetDate());
                customers = customers.Where(x => ((x.ContactStatusId != (int)contactStatus.Contacted && x.ContactStatusId != (int)contactStatus.NotResponing) 
                && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < DateTime.Now.Date) 
                ||(x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false &&
                (x.InquiryStatusId == (int)inquiryStatus.measurementdelayed || x.InquiryStatusId == (int)inquiryStatus.designDelayed
                || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed)) )).OrderByDescending(i => i.CustomerId).ToList();
            }

            customers.ForEach(x => x.Inquiries.Clear());
            response.data = customers;
            return response;

        }

        //[AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetCustomerOfBranch(int userId, int filter)
        {
            var type = typeof(Customer);
            var parameterExprission = Expression.Parameter(typeof(Customer), "x");
            var constant = Expression.Constant(false, typeof(bool?));
            var property = Expression.Property(parameterExprission, "IsDeleted");
            var expression = Expression.Equal(property, constant);
            var property2 = Expression.Property(parameterExprission, "IsActive");
            constant = Expression.Constant(true, typeof(bool?));
            var experssion2 = Expression.Equal(property2, constant);
            expression = Expression.And(expression, experssion2);
            var property3 = Expression.Property(parameterExprission, "BranchId");
            constant = Expression.Constant(Constants.branchId, typeof(int?));
            var experssion3 = Expression.Equal(property3, constant);
            expression = Expression.And(expression, experssion3);
            Expression _property = parameterExprission;
            foreach (var item in "Branch.IsActive".Split('.'))
            {
                _property = Expression.PropertyOrField(_property, item);
            }
            constant = Expression.Constant(true,typeof(bool?));
            var _experssion = Expression.Equal(_property, constant);
            expression = Expression.And(expression, _experssion);

            Expression __property = parameterExprission;
            foreach (var item in "Branch.IsDeleted".Split('.'))
            {
                __property = Expression.PropertyOrField(__property, item);
            }
            constant = Expression.Constant(false, typeof(bool?));
            var __experssion = Expression.Equal(__property, constant);
            expression = Expression.And(expression, __experssion);
            if (userId != 0 && userId != null)
            {
                if (filter == 16)
                {
                    var property1 = Expression.Property(parameterExprission, "CustomerAssignedTo");
                    constant = Expression.Constant(userId, typeof(int?));
                    var experssion1 = Expression.Equal(property1, constant);
                    expression = Expression.And(expression, experssion1);
                }
                else
                {
                    var property1 = Expression.Property(parameterExprission, "UserId");
                    constant = Expression.Constant(userId, typeof(int?));
                    var experssion1 = Expression.Equal(property1, constant);
                    expression = Expression.And(expression, experssion1);
                }
                
            }
            if (filter == 2)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.Contacted, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                constant = Expression.Constant(false, typeof(bool));
                var ex = Expression.Equal(body, constant);
                expression = Expression.And(expression, ex);
            }
            else if (filter == 3)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToContact, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

            }
            else if (filter == 4)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.Contacted, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);


                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(_experssion2, experssion1);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                expression = Expression.And(expression, body);
            }
            else if(filter == 17)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToFollowUp, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);
            }
            else if (filter == 18)
            {

                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NotResponing, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);
            }
            else if(filter == 19)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToContact, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _property3 = Expression.Property(parameterExprission, "CustomerNextMeetingDate");
                constant = Expression.Constant(Helper.GetDate(), typeof(string));
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var _experssion3 = Expression.Call(_property3,method, constant);
                expression = Expression.And(expression, _experssion3);
            }
            else if (filter == 20)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToContact, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);
            }
            else if (filter == 21)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToFollowUp, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _property3 = Expression.Property(parameterExprission, "CustomerNextMeetingDate");
                constant = Expression.Constant(Helper.GetDate(), typeof(string));
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var _experssion3 = Expression.Call(_property3, method, constant);
                expression = Expression.And(expression, _experssion3);
            }
            else if (filter == 22)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToFollowUp, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);
            }
            else if (filter == 24)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToFollowUp, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                constant = Expression.Constant(false, typeof(bool));
                var ex = Expression.Equal(body, constant);
                expression = Expression.And(expression, ex);
            }
            else if (filter == 23)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NeedToFollowUp, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);


                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(_experssion2, experssion1);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                expression = Expression.And(expression, body);
            }
            else if (filter == 25)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NotResponing, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);


                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(_experssion2, experssion1);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                expression = Expression.And(expression, body);
            }
            else if (filter == 26)
            {
                var _property1 = Expression.Property(parameterExprission, "ContactStatusId");
                constant = Expression.Constant((int)contactStatus.NotResponing, typeof(int?));
                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);

                var _parameter = Expression.Parameter(typeof(Inquiry), "y");
                Expression property1 = Expression.Property(_parameter, "IsActive");
                constant = Expression.Constant(true, typeof(bool?));
                var experssion1 = Expression.Equal(property1, constant);
                var _property2 = Expression.Property(_parameter, "IsDeleted");
                constant = Expression.Constant(false, typeof(bool?));
                var _experssion2 = Expression.Equal(_property2, constant);
                experssion1 = Expression.And(experssion1, _experssion2);
                var _lambda = Expression.Lambda<Func<Inquiry, bool>>(experssion1, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(Inquiry) },
                    Expression.Property(parameterExprission, "Inquiries"), _lambda);
                constant = Expression.Constant(false, typeof(bool));
                var ex = Expression.Equal(body, constant);
                expression = Expression.And(expression, ex);
            }
            else if (filter >= 5 && filter <= 15)
            {

                var _property1 = Expression.Property(parameterExprission, "WayOfContactId");
                switch (filter)
                {
                    case 5:
                        constant = Expression.Constant(1, typeof(int?));
                        break;
                    case 6:
                        constant = Expression.Constant(2, typeof(int?));
                        break;
                    case 7:
                        constant = Expression.Constant(3, typeof(int?));
                        break;
                    case 8:
                        constant = Expression.Constant(4, typeof(int?));
                        break;
                    case 9:
                        constant = Expression.Constant(5, typeof(int?));
                        break;
                    case 10:
                        constant = Expression.Constant(6, typeof(int?));
                        break;
                    case 11:
                        constant = Expression.Constant(7, typeof(int?));
                        break;
                    case 12:
                        constant = Expression.Constant(8, typeof(int?));
                        break;
                    case 13:
                        constant = Expression.Constant(9, typeof(int?));
                        break;
                    case 14:
                        constant = Expression.Constant(10, typeof(int?));
                        break;
                    default :
                        constant = Expression.Constant(11, typeof(int?));
                        break;
                }

                var _experssion1 = Expression.Equal(_property1, constant);
                expression = Expression.And(expression, _experssion1);
            }
            var lambda = Expression.Lambda<Func<Customer, bool>>(expression, parameterExprission);

            System.Collections.Generic.List<CustomerResponse> customers = customerRepository.FindByCondition(lambda)
                .Select(x =>
                    new CustomerResponse
                    {
                        CustomerId = x.CustomerId,
                        CustomerName = x.CustomerName,
                        CustomerContact = x.CustomerContact,
                        CustomerEmail = x.CustomerEmail,
                        Code = "CS" + x.Branch.BranchId + "" + x.CustomerId,
                        BranchId = x.Branch.BranchId,
                        BranchName = x.Branch.BranchName,
                        UserId = x.User.UserId,
                        UserName = x.User.UserName,
                        CustomerCity = x.CustomerCity,
                        CustomerCountry = x.CustomerCountry,
                        CustomerNationality = x.CustomerNationality,
                        CustomerNotes = x.CustomerNotes,
                        CustomerNextMeetingDate = x.CustomerNextMeetingDate,
                        WayofContactId = x.WayofContactId,
                        WayofContact = x.WayofContact.WayOfContactName,
                        ContactStatusId = x.ContactStatusId,
                        ContactStatus = x.ContactStatus.ContactStatusName,
                        CustomerAddress = x.CustomerAddress,
                        CustomerNationalId = x.CustomerNationalId,
                        TotalNoOfInquiries = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == 0 ? "No Inquiries" : x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count().ToString(),
                        AddedOn = x.CreatedDate,
                        CustomerAssignedTo = x.CustomerAssignedTo,
                        CustomerAssignedToName = x.CustomerAssignedToNavigation.UserName,
                        CustomerAssignedBy = x.CustomerAssignedBy,
                        CustomerAssignedByName = x.CustomerAssignedByNavigation.UserName,
                        CustomerAssignedDate = x.CustomerAssignedDate,
                        EscalationRequestedBy = x.EscalationRequestedBy,
                        EscalationRequestedByName = x.EscalationRequestedByNavigation.UserName,
                        EscalationRequestedOn = x.EscalationRequestedOn,
                        IsEscalationRequested = x.IsEscalationRequested,
                        EscalatedBy = x.EscalatedBy,
                        EscalatedByName = x.EscalatedByNavigation.UserName,
                        EscalatedOn = x.EscalatedOn,
                        
                    }).OrderByDescending(i => i.CustomerId).ToList();
            customers.AddRange(customerRepository
                .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.Branch == null)
                .Include(x => x.Inquiries)
                .Select(x => new CustomerResponse
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    CustomerContact = x.CustomerContact,
                    CustomerEmail = x.CustomerEmail,
                    CustomerCity = x.CustomerCity,
                    CustomerCountry = x.CustomerCountry,
                    CustomerNationality = x.CustomerNationality,
                    CustomerNotes = x.CustomerNotes,
                    CustomerNextMeetingDate = x.CustomerNextMeetingDate,
                    WayofContactId = x.WayofContactId,
                    ContactStatusId = x.ContactStatusId,
                    ContactStatus = x.ContactStatus.ContactStatusName,
                    CustomerAddress = x.CustomerAddress,
                    CustomerNationalId = x.CustomerNationalId,
                    CustomerAssignedTo = x.CustomerAssignedTo,
                    CustomerAssignedToName = x.CustomerAssignedToNavigation.UserName,
                    CustomerAssignedBy = x.CustomerAssignedBy,
                    CustomerAssignedByName = x.CustomerAssignedByNavigation.UserName,
                    CustomerAssignedDate = x.CustomerAssignedDate,
                    EscalationRequestedBy = x.EscalationRequestedBy,
                    EscalationRequestedByName = x.EscalationRequestedByNavigation.UserName,
                    EscalationRequestedOn = x.EscalationRequestedOn,
                    IsEscalationRequested = x.IsEscalationRequested,
                    EscalatedBy = x.EscalatedBy,
                    EscalatedByName = x.EscalatedByNavigation.UserName,
                    EscalatedOn = x.EscalatedOn,
                    TotalNoOfInquiries = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == 0 ? "No Inquiries" : x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count().ToString(),
                }).OrderByDescending(i => i.CustomerId).ToList());
            int? total = 0;
            int? contacted = 0;
            int? needToContact = 0;
            int? customerWithoutInquiry = 0;
            int? customerWithInquiry = 0;
            int? direct = 0;
            int? google = 0;
            int? facebook = 0;
            int? linkedin = 0;
            int? twitter = 0;
            int? friends = 0;
            int? website = 0;
            int? mobile = 0;
            int? owner = 0;
            int? instagram = 0;
            int? otner = 0;
            int? needTofollow = 0;
            int? notResponding = 0;
            int? needToContactToday = 0;
            int? needToContactDelay = 0;
            int? needToFollowUpToday = 0;
            int? needToFollowUpDelay = 0;
            int? needToFollowUpWithInquiry = 0;
            int? needToFollowUpWithOutInquiry = 0;
            int? notRespondingWithinquiry = 0;
            int? notRespondingWithoutInquiry = 0;
            var customerss = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId && x.Branch.IsActive == true && x.Branch.IsDeleted == false)
                    .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false)).ToList();
            if (userId != 0 && userId != null && filter != 16)
            {
                total = customerss.Where(x => x.UserId == userId).Count();
                contacted = customerss.Where(x => x.ContactStatusId == 1 && x.UserId == userId).Count();
                needToContact = customerss.Where(x =>
                    x.ContactStatusId == 2
                    //&& (Helper.ConvertToDateTime(x.CustomerNextMeetingDate) <=
                    //    Helper.ConvertToDateTime(Helper.GetDateTime()) || x.CustomerNextMeetingDate == null) 
                        && x.UserId == userId).Count();
                customerWithoutInquiry = customerss.Where(x => x.ContactStatusId == 1 && x.Inquiries.Any(y => y.IsActive == true && y.IsDeleted == false) == false && x.UserId == userId).Count();
                customerWithInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) && x.UserId == userId).Count();
                direct = customerss.Where(x => x.WayofContactId == 1 && x.UserId == userId).Count();
                google = customerss.Where(x => x.WayofContactId == 2 && x.UserId == userId).Count();
                facebook = customerss.Where(x => x.WayofContactId == 3 && x.UserId == userId).Count();
                linkedin = customerss.Where(x => x.WayofContactId == 4 && x.UserId == userId).Count();
                twitter = customerss.Where(x => x.WayofContactId == 5 && x.UserId == userId).Count();
                friends = customerss.Where(x => x.WayofContactId == 6 && x.UserId == userId).Count();
                website = customerss.Where(x => x.WayofContactId == 7 && x.UserId == userId).Count();
                mobile = customerss.Where(x => x.WayofContactId == 8 && x.UserId == userId).Count();
                owner = customerss.Where(x => x.WayofContactId == 9 && x.UserId == userId).Count();
                instagram = customerss.Where(x => x.WayofContactId == 10 && x.UserId == userId).Count();
                otner = customerss.Where(x => x.WayofContactId == 11 && x.UserId == userId).Count();
                needTofollow = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.UserId == userId).Count();
                notResponding = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.UserId == userId).Count();
                needToContactToday = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToContact && x.CustomerNextMeetingDate.Contains(Helper.GetDate()) && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date >= Helper.ConvertToDateTime(Helper.GetDate()).Date && x.UserId == userId).Count();
                needToContactDelay = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToContact && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date && x.UserId == userId).Count();
                needToFollowUpToday = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.CustomerNextMeetingDate.Contains(Helper.GetDate()) && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date >= Helper.ConvertToDateTime(Helper.GetDate()).Date && x.UserId == userId).Count();
                needToFollowUpDelay = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date && x.UserId == userId).Count();
                needToFollowUpWithInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any() && x.UserId == userId).Count();
                needToFollowUpWithOutInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false && x.UserId == userId).Count();
                notRespondingWithinquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.UserId == userId && x.Inquiries.Any()).Count();
                notRespondingWithoutInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false && x.UserId == userId).Count();
            }
            else
            {
                total = customerss.Count;
                contacted = customerss.Where(x => x.ContactStatusId == 1).Count();
                needToContact = customerss.Where(x =>
                    x.ContactStatusId == 2
                    /*(Helper.ConvertToDateTime(x.CustomerNextMeetingDate) <=
                        Helper.ConvertToDateTime(Helper.GetDateTime()) || x.CustomerNextMeetingDate == null)*/).Count();
                customerWithoutInquiry = customerss.Where(x => x.ContactStatusId == 1 && x.Inquiries.Any(y => y.IsActive == true && y.IsDeleted == false) == false).Count();
                customerWithInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any()).Count();
                direct = customerss.Where(x => x.WayofContactId == 1).Count();
                google = customerss.Where(x => x.WayofContactId == 2).Count();
                facebook = customerss.Where(x => x.WayofContactId == 3).Count();
                linkedin = customerss.Where(x => x.WayofContactId == 4).Count();
                twitter = customerss.Where(x => x.WayofContactId == 5).Count();
                friends = customerss.Where(x => x.WayofContactId == 6).Count();
                website = customerss.Where(x => x.WayofContactId == 7).Count();
                mobile = customerss.Where(x => x.WayofContactId == 8).Count();
                owner = customerss.Where(x => x.WayofContactId == 9).Count();
                instagram = customerss.Where(x => x.WayofContactId == 10).Count();
                otner = customerss.Where(x => x.WayofContactId == 11).Count();
                needTofollow = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp).Count();
                notResponding = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing).Count();
                needToContactToday = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToContact && (x.CustomerNextMeetingDate.Contains(Helper.GetDate()) && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date >= Helper.ConvertToDateTime(Helper.GetDateTime()).Date)).Count();
                needToContactDelay = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToContact && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date).Count();
                needToFollowUpToday = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.CustomerNextMeetingDate.Contains(Helper.GetDate()) && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date >= Helper.ConvertToDateTime(Helper.GetDateTime()).Date).Count();
                needToFollowUpDelay = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDateTime()).Date).Count();
                //{x => ((((((x.IsDeleted == False) And (x.BranchId == 1)) And (x.Branch.IsActive == True)) And (x.Branch.IsDeleted == False)) And (x.ContactStatusId == 3)) And x.Inquiries.Any(y => ((y.IsDeleted == False) And (y.IsActive == True))))}
                needToFollowUpWithInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false)).Count();
                needToFollowUpWithOutInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false).Count();
                notRespondingWithoutInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false) == false).Count();
                notRespondingWithinquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing && x.Inquiries.Any(x => x.IsActive == true && x.IsDeleted == false)).Count();
            }
            

            customers.ForEach(x =>
            {
                x.TotalCustomers = total;
                x.ContactedCustomers = contacted;
                x.NeedToContactCustomers = needToContact;
                x.CustomerWithoutInquiry = customerWithoutInquiry;
                x.Direct = direct; 
                x.Google = google;
                x.FaceBook = facebook;
                x.Linkedin = linkedin;
                x.Twitter = twitter;
                x.Friends = friends;
                x.Website = website;
                x.MobileApp = mobile;
                x.OwnerReference = owner;
                x.Instagram = instagram;
                x.Other = otner;
                x.CustomerWithInquiry = customerWithInquiry;
                x.NeedToFollowUp = needTofollow;
                x.NotResponding = notResponding;
                x.NeedToFollowUpToday = needToFollowUpToday;
                x.NeedToFollowUpDelay = needToFollowUpDelay;
                x.NeedToContactToday = needToContactToday;
                x.NeedToContactDelay = needToContactDelay;
                x.NeedToFollowUpWithInquiry = needToFollowUpWithInquiry;
                x.NeedToFollowUpWithOutInquiry = needToFollowUpWithOutInquiry;
                x.NotRespondingWithInquiry = notRespondingWithinquiry;
                x.NotRespondingWithoutInquiry = notRespondingWithoutInquiry;
            });
            if (filter == 19 || filter == 21)
            {
                customers = customers.Where(x => Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date >= Helper.ConvertToDateTime(Helper.GetDate())).OrderByDescending(i => i.CustomerId).ToList();

            }
            else if (filter == 20 || filter == 22)
            {
                customers = customers.Where(x => Helper.ConvertToDateTime(x.CustomerNextMeetingDate).Date < Helper.ConvertToDateTime(Helper.GetDate())).OrderByDescending(i => i.CustomerId).ToList();
            }
            return customers;
        }

        

        [HttpGet]
        [Route("[action]")]
        public object GetCustomerbyUser()
        {
            var Customers = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.BranchId == Constants.branchId)).Select(x => new
            {
                UserId = x.UserId,
                User = x.UserName,
                Customers = x.CustomerUsers.Where(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId).Count(),
            }).ToList();
            response.data = Customers;
            return response;
        }

        [HttpGet]
        [Route("[action]")]
        public object GetCustomerbyAssigned()
        {
            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.BranchId == Constants.branchId)).Select(x => new
            {
                UserId = x.UserId,
                User = x.UserName,
            }).ToList();
            List<object> Customers = new List<object>();
            foreach (var user in users)
            {
                var customer = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId && x.CustomerAssignedTo == user.UserId).Count();
                Customers.Add(new
                {
                    UserId = user.UserId,
                    User = user.User,
                    customers = customer
                });
            }

            response.data = Customers;
            return response;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public object GetCustomerbyId(int customerId)
        {
            CustomerResponse customer = null;
            try
            {
                customer = customerRepository
                    .FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false)
                    .Include(x => x.Branch).Where(x => x.IsActive == true && x.IsDeleted == false).Include(x => x.User)
                    .Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => new CustomerResponse
                    {
                        CustomerId = x.CustomerId,
                        CustomerName = x.CustomerName,
                        CustomerNextMeetingDate = x.CustomerNextMeetingDate,
                        CustomerNotes = x.CustomerNotes,
                        CustomerWhatsapp = x.CustomerWhatsapp,
                        CustomerContact = x.CustomerContact,
                        CustomerEmail = x.CustomerEmail,
                        BranchId = x.Branch.BranchId,
                        BranchName = x.Branch.BranchName,
                        UserId = x.User.UserId,
                        UserName = x.User.UserName,
                        CustomerCity = x.CustomerCity,
                        CustomerCountry = x.CustomerCountry,
                        CustomerNationality = x.CustomerNationality,
                        WayofContactId = x.WayofContactId,
                        ContactStatusId = x.ContactStatusId,
                        CustomerAddress = x.CustomerAddress,
                        CustomerNationalId = x.CustomerNationalId,
                        CustomerAssignedTo = x.CustomerAssignedTo,
                        CustomerAssignedToName = x.CustomerAssignedToNavigation.UserName,
                        CustomerAssignedBy = x.CustomerAssignedBy,
                        CustomerAssignedByName = x.CustomerAssignedByNavigation.UserName,
                        CustomerAssignedDate = x.CustomerAssignedDate,
                    }).FirstOrDefault();
            }
            catch (Exception)
            {
            }

            if (customer == null)
            {
                customer = customerRepository
                    .FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false)
                    .Select(x => new CustomerResponse
                    {
                        CustomerId = x.CustomerId,
                        CustomerName = x.CustomerName,
                        CustomerNextMeetingDate = x.CustomerNextMeetingDate,
                        CustomerNotes = x.CustomerNotes,
                        CustomerWhatsapp = x.CustomerWhatsapp,
                        CustomerContact = x.CustomerContact,
                        CustomerEmail = x.CustomerEmail,
                        CustomerCity = x.CustomerCity,
                        CustomerCountry = x.CustomerCountry,
                        CustomerNationality = x.CustomerNationality,
                        WayofContactId = x.WayofContactId,
                        ContactStatusId = x.ContactStatusId,
                        CustomerAddress = x.CustomerAddress,
                        CustomerNationalId = x.CustomerNationalId,
                        CustomerAssignedTo = x.CustomerAssignedTo,
                        CustomerAssignedToName = x.CustomerAssignedToNavigation.UserName,
                        CustomerAssignedBy = x.CustomerAssignedBy,
                        CustomerAssignedByName = x.CustomerAssignedByNavigation.UserName,
                        CustomerAssignedDate = x.CustomerAssignedDate,
                    }).FirstOrDefault();
            }

            response.data = customer;
            return response;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetCustomerbyContact(string customerContact)
        {
            response.data = customerRepository.FindByCondition(x =>
                x.CustomerContact == customerContact && x.BranchId == Constants.branchId && x.IsActive == true &&
                x.IsDeleted == false).FirstOrDefault();
            if (response.data == null)
            {
                Customer customer = customerRepository.FindByCondition(x =>
                        x.CustomerContact == customerContact && x.IsActive == true && x.IsDeleted == false)
                    .FirstOrDefault();
                if (customer != null)
                {
                    customer.CustomerId = 0;
                    response.data = customer;
                }
                else
                {
                    response.isError = true;
                }
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddWebsiteCustomerAsync([FromForm] CustomCustomer customer)
        {
            Customer oldCustomer = customerRepository.FindByCondition(x =>
                    x.CustomerContact == customer.CustomerContact && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            if (oldCustomer == null)
            {
                customerRepository.Create(new Customer
                {
                    CustomerName = customer.CustomerName,
                    CustomerContact = customer.CustomerContact,
                    CustomerNotes = customer.CustomerNotes,
                    CustomerEmail = customer.CustomerEmail,
                    WayofContactId = customer.WayofContactId,
                    ContactStatusId = customer.ContactStatusId,
                    CreatedDate = Helper.GetDateTime(),
                });

                if (customer.CustomerEmail != null)
                {
                    //|| x.BranchRole.RoleTypeId == (int)roleType.Manager
                    System.Collections.Generic.List<string> emails = userRoleRepository.FindByCondition(x =>
                            x.BranchRole.RoleTypeId == (int)roleType.Sales && x.IsActive == true &&
                            x.IsDeleted == false &&
                            x.User.IsActive == true && x.IsDeleted == false && x.BranchRole.IsActive == true &&
                            x.BranchRole.IsDeleted == false && x.Branch.IsActive == true && x.Branch.IsDeleted == false)
                        .Select(x => x.User.UserEmail).ToList();
                    foreach (string email in emails)
                    {
                        try
                        {
                            await mailService.SendEmailAsync(new MailRequest
                            {
                                Body = customer.CustomerNotes + "          \nName: " + customer.CustomerName +
                                       "\nContact: " + customer.CustomerContact + "\nEmail: " + customer.CustomerEmail,
                                Subject = "Request for Inquiry from New Customer " + customer.CustomerName,
                                ToEmail = email
                            });
                        }

                        catch (Exception)
                        {
                        }
                    }
                }
            }
            else
            {
                oldCustomer.CustomerNotes = customer.CustomerNotes;

                if (customer.CustomerEmail != null)
                {
                    oldCustomer.CustomerEmail = customer.CustomerEmail;
                }

                if (oldCustomer.CustomerEmail != null)
                {
                    //|| x.BranchRole.RoleTypeId == (int)roleType.Manager
                    System.Collections.Generic.List<string> emails = userRoleRepository.FindByCondition(x =>
                            x.BranchRole.RoleTypeId == (int)roleType.Sales && x.IsActive == true &&
                            x.IsDeleted == false &&
                            x.User.IsActive == true && x.IsDeleted == false && x.BranchRole.IsActive == true &&
                            x.BranchRole.IsDeleted == false && x.Branch.IsActive == true && x.Branch.IsDeleted == false)
                        .Select(x => x.User.UserEmail).ToList();
                    foreach (string email in emails)
                    {
                        try
                        {
                            await mailService.SendEmailAsync(new MailRequest
                            {
                                Body = customer.CustomerNotes + "          \nName: " + customer.CustomerName +
                                       "\nContact: " + customer.CustomerContact + "\nEmail: " +
                                       oldCustomer.CustomerEmail,
                                Subject = "Request for Inquiry from Customer " + customer.CustomerName,
                                ToEmail = email
                            });
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                customerRepository.Update(oldCustomer);
            }

            context.SaveChanges();
            return response;
        }


        //[AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public object AddCustomer(Customer customer)
        {
            if (customer.ContactStatusId == (int)contactStatus.NeedToContact && string.IsNullOrEmpty(customer.CustomerNextMeetingDate))
            {
                response.isError = true;
                response.errorMessage = "Please mention next Follow-up Date";
                return response;
            }
            if (customer.CustomerId == 0)
            {
                customer.BranchId = Constants.branchId;
                Customer oldCustomer = customerRepository.FindByCondition(x =>
                    x.CustomerContact == customer.CustomerContact && x.BranchId == customer.BranchId &&
                    x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                if (oldCustomer == null)
                {
                    if (customer.CustomerAssignedTo != null || customer.CustomerAssignedTo != 0)
                    {
                        customer.CustomerAssignedBy = Constants.userId;
                        customer.CustomerAssignedDate = Helper.GetDateTime();
                        customer.UserId = customer.CustomerAssignedTo;
                    }
                    if (Helper.ConvertToDateTime(customer.CustomerNextMeetingDate)> Helper.ConvertToDateTime(Helper.GetDateTime()) 
                        && customer.ContactStatusId == (int)contactStatus.Contacted)
                    {
                        customer.ContactStatusId = (int)contactStatus.NeedToFollowUp;
                    }
                    customer.CreatedDate = Helper.GetDateTime();
                    customer.CreatedBy = Constants.userId;
                    string user = userRepository.FindByCondition(x => x.UserId == customer.CustomerAssignedBy).Select(x => x.UserName).FirstOrDefault();
                    try
                    {
                        sendNotificationToOneUser("You are assigned to manage (" + customer.CustomerName + ") By (" + user + ")",
                            false, null, null, (int)customer.CustomerAssignedTo, Constants.branchId, (int)notificationCategory.Other);

                    }
                    catch (Exception ex)
                    {
                        Sentry.SentrySdk.CaptureMessage(ex.Message);
                    }
                    customerRepository.Create(customer);
                    context.SaveChanges();
                    //await mailService.SendWelcomeEmailAsync(new PasswordRequest() { ToEmail = user.UserEmail, UserName = user.UserName, Link = Constants.CRMBaseUrl + "/setpassword.html?userId=" + Helper.EnryptString(user.UserId.ToString()) });
                    //await SendWelcomeMail(new WelcomeRequest() { ToEmail = user.UserEmail, UserName = user.UserName });
                    response.isError = false;
                    response.errorMessage = "Success";
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "Customer already Exist";
                }
            }
            else
            {
                Customer oldCustomer = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.CustomerId == customer.CustomerId).FirstOrDefault();
                oldCustomer.CustomerName = customer.CustomerName;
                oldCustomer.CustomerContact = customer.CustomerContact;
                oldCustomer.CustomerEmail = customer.CustomerEmail;
                oldCustomer.CustomerCity = customer.CustomerCity;
                oldCustomer.CustomerNotes = customer.CustomerNotes;
                oldCustomer.CustomerCountry = customer.CustomerCountry;
                oldCustomer.CustomerNationality = customer.CustomerNationality;
                oldCustomer.CustomerNationalId = customer.CustomerNationalId;
                oldCustomer.CustomerNextMeetingDate = customer.CustomerNextMeetingDate;
                oldCustomer.ContactStatusId = customer.ContactStatusId;
                //oldCustomer.WayofContactId = customer.WayofContactId;
                oldCustomer.CustomerWhatsapp = customer.CustomerWhatsapp;
                if (customer.CustomerAssignedTo != null || customer.CustomerAssignedTo != 0)
                {
                    oldCustomer.CustomerAssignedTo = customer.CustomerAssignedTo;
                    oldCustomer.CustomerAssignedBy = Constants.userId;
                    oldCustomer.CustomerAssignedDate = Helper.GetDateTime();
                    oldCustomer.UserId = oldCustomer.CustomerAssignedTo;
                }
                if (Helper.ConvertToDateTime(customer.CustomerNextMeetingDate) > Helper.ConvertToDateTime(Helper.GetDateTime())
                        && customer.ContactStatusId == (int)contactStatus.Contacted)
                {
                    oldCustomer.ContactStatusId = (int)contactStatus.NeedToFollowUp;
                }
                oldCustomer.UpdatedDate = Helper.GetDateTime();
                oldCustomer.UpdatedBy = Constants.userId;
                string user = userRepository.FindByCondition(x => x.UserId == oldCustomer.CustomerAssignedBy).Select(x => x.UserName).FirstOrDefault();
                try
                {
                    sendNotificationToOneUser("You are assigned to manage (" + customer.CustomerName + ") By (" + user + ")", false, null, null, (int)customer.CustomerAssignedTo, Constants.branchId, (int)notificationCategory.Other);

                }
                catch (Exception ex)
                {
                    Sentry.SentrySdk.CaptureMessage(ex.Message);
                }
                customerRepository.Update(oldCustomer);
                context.SaveChanges();
                //await mailService.SendWelcomeEmailAsync(new PasswordRequest() { ToEmail = user.UserEmail, UserName = user.UserName, Link = Constants.CRMBaseUrl + "/setpassword.html?userId=" + Helper.EnryptString(user.UserId.ToString()) });
                //await SendWelcomeMail(new WelcomeRequest() { ToEmail = user.UserEmail, UserName = user.UserName });
                response.isError = false;
                response.errorMessage = "Success";
            }

            return response;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public object GetContactStatus()
        {
            response.data = contactStatusRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            return response;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public object GetWayOfContacts()
        {
            response.data = wayOfContactRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            return response;
        }


        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Escalate)]
        [HttpPost]
        [Route("[action]")]
        public object EscalateCustomer(int customerId)
        {
            Customer customer = customerRepository
                .FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            if (customer != null)
            {
                customerRepository.Escalate(customer);
                context.SaveChanges();
                response.data = "Escalated Request Sent";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Customer doesn't Exist";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object CustomerEscalationRequest(int customerId)
        {
            var customer = customerRepository.FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (customer != null)
            {
                customer.IsEscalationRequested = true;
                customer.EscalationRequestedBy = Constants.userId;
                customer.EscalationRequestedOn = Helper.GetDateTime();
                customerRepository.Update(customer);
                context.SaveChanges();
                response.data = customer;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Customer Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object CustomerEscalationApprove(int customerId)
        {
            var customer = customerRepository.FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (customer != null)
            {
                customer.IsActive = false;
                customer.EscalatedBy = Constants.userId;
                customer.EscalatedOn = Helper.GetDateTime();
                customerRepository.Update(customer);
                context.SaveChanges();
                response.data = customer;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Customer Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object CustomerEscalationReject(int customerId)
        {
            var customer = customerRepository.FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (customer != null)
            {
                customer.IsEscalationRequested = false;
                customerRepository.Update(customer);
                context.SaveChanges();
                response.data = customer;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Customer Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetEscalatedCustomerOfBranch(int userId, int filter)
        {
            System.Collections.Generic.List<CustomerResponse> customers = customerRepository.FindByCondition(x => x.IsActive == false && x.IsDeleted == false && x.BranchId == Constants.branchId && x.Branch.IsActive == true && x.Branch.IsDeleted == false)
                .Include(x => x.Inquiries)
                .Include(x => x.Branch).Where(x => x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.User).Where(x => x.IsActive == true && x.IsDeleted == false).Select(x =>
                    new CustomerResponse
                    {
                        CustomerId = x.CustomerId,
                        CustomerName = x.CustomerName,
                        CustomerContact = x.CustomerContact,
                        CustomerEmail = x.CustomerEmail,
                        Code = "CS" + x.Branch.BranchId + "" + x.CustomerId,
                        BranchId = x.Branch.BranchId,
                        BranchName = x.Branch.BranchName,
                        UserId = x.User.UserId,
                        UserName = x.User.UserName,
                        CustomerCity = x.CustomerCity,
                        CustomerCountry = x.CustomerCountry,
                        CustomerNationality = x.CustomerNationality,
                        CustomerNotes = x.CustomerNotes,
                        CustomerNextMeetingDate = x.CustomerNextMeetingDate,
                        WayofContactId = x.WayofContactId,
                        WayofContact = x.WayofContact.WayOfContactName,
                        ContactStatusId = x.ContactStatusId,
                        ContactStatus = x.ContactStatus.ContactStatusName,
                        CustomerAddress = x.CustomerAddress,
                        CustomerNationalId = x.CustomerNationalId,
                        TotalNoOfInquiries = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == 0 ? "No Inquiries" : x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count().ToString(),
                        AddedOn = x.CreatedDate,
                        CustomerAssignedTo = x.CustomerAssignedTo,
                        CustomerAssignedToName = x.CustomerAssignedToNavigation.UserName,
                        CustomerAssignedBy = x.CustomerAssignedBy,
                        CustomerAssignedByName = x.CustomerAssignedByNavigation.UserName,
                        CustomerAssignedDate = x.CustomerAssignedDate,
                        EscalationRequestedBy = x.EscalationRequestedBy,
                        EscalationRequestedOn = x.EscalationRequestedOn,
                        IsEscalationRequested = x.IsEscalationRequested,
                        EscalatedBy = x.EscalatedBy,
                        EscalatedOn = x.EscalatedOn,
                    }).OrderByDescending(i => i.CustomerId).ToList();
            customers.AddRange(customerRepository
                .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.Branch == null)
                .Include(x => x.Inquiries)
                .Select(x => new CustomerResponse
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    CustomerContact = x.CustomerContact,
                    CustomerEmail = x.CustomerEmail,
                    CustomerCity = x.CustomerCity,
                    CustomerCountry = x.CustomerCountry,
                    CustomerNationality = x.CustomerNationality,
                    CustomerNotes = x.CustomerNotes,
                    CustomerNextMeetingDate = x.CustomerNextMeetingDate,
                    WayofContactId = x.WayofContactId,
                    ContactStatusId = x.ContactStatusId,
                    ContactStatus = x.ContactStatus.ContactStatusName,
                    CustomerAddress = x.CustomerAddress,
                    CustomerNationalId = x.CustomerNationalId,
                    CustomerAssignedTo = x.CustomerAssignedTo,
                    CustomerAssignedToName = x.CustomerAssignedToNavigation.UserName,
                    CustomerAssignedBy = x.CustomerAssignedBy,
                    CustomerAssignedByName = x.CustomerAssignedByNavigation.UserName,
                    CustomerAssignedDate = x.CustomerAssignedDate,
                    EscalationRequestedBy = x.EscalationRequestedBy,
                    EscalationRequestedOn = x.EscalationRequestedOn,
                    IsEscalationRequested = x.IsEscalationRequested,
                    EscalatedBy = x.EscalatedBy,
                    EscalatedOn = x.EscalatedOn,
                    TotalNoOfInquiries = x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count() == 0 ? "No Inquiries" : x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false).Count().ToString(),
                }).OrderByDescending(i => i.CustomerId).ToList());
            int? total = 0;
            int? contacted = 0;
            int? needToContact = 0;
            int? customerWithoutInquiry = 0;
            int? customerWithInquiry = 0;
            int? direct = 0;
            int? google = 0;
            int? facebook = 0;
            int? linkedin = 0;
            int? twitter = 0;
            int? friends = 0;
            int? website = 0;
            int? mobile = 0;
            int? owner = 0;
            int? instagram = 0;
            int? otner = 0;
            int? needTofollow = 0;
            int? notResponding = 0;

            var customerss = customerRepository.FindByCondition(x => x.IsActive == false && x.IsDeleted == false && x.BranchId == Constants.branchId && x.Branch.IsActive == true && x.Branch.IsDeleted == false)
                    .Include(x => x.Inquiries.Where(y => y.IsActive == true && y.IsDeleted == false)).ToList();
            if (customerss != null)
            {
                total = customerss.Count;
                contacted = customerss.Where(x => x.ContactStatusId == 1).Count();
                needToContact = customerss.Where(x =>
                    x.ContactStatusId == 2
                        /*(Helper.ConvertToDateTime(x.CustomerNextMeetingDate) <=
                            Helper.ConvertToDateTime(Helper.GetDateTime()) || x.CustomerNextMeetingDate == null)*/).Count();
                customerWithoutInquiry = customerss.Where(x => x.ContactStatusId == 1 && x.Inquiries.Any(y => y.IsActive == true && y.IsDeleted == false) == false).Count();
                customerWithInquiry = customerss.Where(x => x.ContactStatusId == (int)contactStatus.Contacted && x.Inquiries.Any()).Count();
                direct = customerss.Where(x => x.WayofContactId == 1).Count();
                google = customerss.Where(x => x.WayofContactId == 2).Count();
                facebook = customerss.Where(x => x.WayofContactId == 3).Count();
                linkedin = customerss.Where(x => x.WayofContactId == 4).Count();
                twitter = customerss.Where(x => x.WayofContactId == 5).Count();
                friends = customerss.Where(x => x.WayofContactId == 6).Count();
                website = customerss.Where(x => x.WayofContactId == 7).Count();
                mobile = customerss.Where(x => x.WayofContactId == 8).Count();
                owner = customerss.Where(x => x.WayofContactId == 9).Count();
                instagram = customerss.Where(x => x.WayofContactId == 10).Count();
                otner = customerss.Where(x => x.WayofContactId == 11).Count();
                needTofollow = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NeedToFollowUp).Count();
                notResponding = customerss.Where(x => x.ContactStatusId == (int)contactStatus.NotResponing).Count();
            }


            customers.ForEach(x =>
            {
                x.TotalCustomers = total;
                x.ContactedCustomers = contacted;
                x.NeedToContactCustomers = needToContact;
                x.CustomerWithoutInquiry = customerWithoutInquiry;
                x.Direct = direct;
                x.Google = google;
                x.FaceBook = facebook;
                x.Linkedin = linkedin;
                x.Twitter = twitter;
                x.Friends = friends;
                x.Website = website;
                x.MobileApp = mobile;
                x.OwnerReference = owner;
                x.Instagram = instagram;
                x.Other = otner;
                x.CustomerWithInquiry = customerWithInquiry;
                x.NeedToFollowUp = needTofollow;
                x.NotResponding = notResponding;
            });
            return customers;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public object DeleteCustomer(int customerId)
        {
            Customer customer = customerRepository.FindByCondition(x => x.CustomerId == customerId).FirstOrDefault();
            if (customer != null)
            {
                customerRepository.Delete(customer);
                context.SaveChanges();
                response.data = "Deleted";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Customer doesn't Exist";
            }

            return response;
        }
    }
}