using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void SendWhatsapp(String number)
        {

            // Find your Account Sid and Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "ACb1325ec580f878d09d7a1a0d6ef65f95";
            string authToken = "68215878731f6303a8ce550fcb89df78";

            TwilioClient.Init(accountSid, authToken);

            var conversation = ConversationResource.Create();

            Console.WriteLine(conversation.Sid);

            //TwilioClient.Init(accountSid, authToken);

            //var message = MessageResource.Create(
            //    from: new Twilio.Types.PhoneNumber("+18316528015"),
            //    body: "Test",
            //    to: new Twilio.Types.PhoneNumber("+971"+number)
            //);

            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:+971545552471"));
            messageOptions.From = new PhoneNumber("whatsapp:SAI GROUP");
            messageOptions.Body = "Test Message";
            //messageOptions.MediaUrl = new System.Collections.Generic.List<Uri> {new Uri("https://saikitchenstorage.blob.core.windows.net/files/eb2b1f50-342c-4bb6-b175-75100a319e74.pdf") };
            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);

            //Console.WriteLine(message.Sid);
        }
        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public Object GetAllCustomer()
        {
            //PushNotification.pushNotification.SendPushNotification();


            return CustomerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.Branch.IsActive == true && x.Branch.IsDeleted == false).Include(x => x.Branch).Where(x => x.IsActive == true && x.IsDeleted == false).Include(x => x.User).Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => new CustomerResponse
            { CustomerId = x.CustomerId, CustomerName = x.CustomerName, CustomerContact = x.CustomerContact, CustomerEmail = x.CustomerEmail, BranchId = x.Branch.BranchId, BranchName = x.Branch.BranchName, UserId = x.User.UserId, UserName = x.User.UserName, CustomerCity = x.CustomerCity, CustomerCountry = x.CustomerCountry, CustomerNationality = x.CustomerNationality, WayofContactId = x.WayofContactId, ContactStatusId = x.ContactStatusId, CustomerAddress = x.CustomerAddress, CustomerNationalId = x.CustomerNationalId });
        }

        //[AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public Object GetCustomerOfBranch(int branchId)
        {
            List<CustomerResponse> customers = CustomerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId && x.Branch.IsActive == true && x.Branch.IsDeleted == false)
               .Include(x => x.Inquiries)
                .Include(x => x.Branch).Where(x => x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.User).Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => new CustomerResponse
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
                    ContactStatusId = x.ContactStatusId,
                    ContactStatus = x.ContactStatus.ContactStatusName,
                    CustomerAddress = x.CustomerAddress,
                    CustomerNationalId = x.CustomerNationalId,
                    TotalNoOfInquiries = x.Inquiries.Count == 0 ? "No Inquiries" : x.Inquiries.Count.ToString()
                }).ToList();
            customers.AddRange(CustomerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.Branch == null).Include(x => x.Inquiries)
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
                 TotalNoOfInquiries = x.Inquiries.Count == 0 ? "No Inquiries" : x.Inquiries.Count.ToString()
             }).ToList());
            int? total = customers.Count;
            int? contacted = customers.Where(x => x.ContactStatusId == 1).Count();
            int? needToContact = customers.Where(x => x.ContactStatusId == 2 && (Helper.ConvertToDateTime(x.CustomerNextMeetingDate) <= Helper.ConvertToDateTime(Helper.GetDateTime()) || x.CustomerNextMeetingDate == null)).Count();
            int? customerWithoutInquiry = customers.Where(x => x.TotalNoOfInquiries == "No Inquiries").Count();

            customers.ForEach(x =>
            {
                x.TotalCustomers = total;
                x.ContactedCustomers = contacted;
                x.NeedToContactCustomers = needToContact;
                x.CustomerWithoutInquiry = customerWithoutInquiry;
            });
            return customers;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetCustomerbyId(int customerId)
        {
            CustomerResponse customer = null;
            try
            {
                customer = CustomerRepository.FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false).Include(x => x.Branch).Where(x => x.IsActive == true && x.IsDeleted == false).Include(x => x.User).Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => new CustomerResponse
                { CustomerId = x.CustomerId, CustomerName = x.CustomerName, CustomerNextMeetingDate = x.CustomerNextMeetingDate, CustomerNotes = x.CustomerNotes, CustomerWhatsapp = x.CustomerWhatsapp, CustomerContact = x.CustomerContact, CustomerEmail = x.CustomerEmail, BranchId = x.Branch.BranchId, BranchName = x.Branch.BranchName, UserId = x.User.UserId, UserName = x.User.UserName, CustomerCity = x.CustomerCity, CustomerCountry = x.CustomerCountry, CustomerNationality = x.CustomerNationality, WayofContactId = x.WayofContactId, ContactStatusId = x.ContactStatusId, CustomerAddress = x.CustomerAddress, CustomerNationalId = x.CustomerNationalId }).FirstOrDefault();
            }
            catch (Exception e) 
            {
                Sentry.SentrySdk.CaptureMessage(e.Message);
            }
            if (customer == null)
            {
                customer = CustomerRepository.FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false).Select(x => new CustomerResponse
                { CustomerId = x.CustomerId, CustomerName = x.CustomerName, CustomerNextMeetingDate = x.CustomerNextMeetingDate, CustomerNotes = x.CustomerNotes, CustomerWhatsapp = x.CustomerWhatsapp, CustomerContact = x.CustomerContact, CustomerEmail = x.CustomerEmail, CustomerCity = x.CustomerCity, CustomerCountry = x.CustomerCountry, CustomerNationality = x.CustomerNationality, WayofContactId = x.WayofContactId, ContactStatusId = x.ContactStatusId, CustomerAddress = x.CustomerAddress, CustomerNationalId = x.CustomerNationalId }).FirstOrDefault();

            }
            response.data = customer;
            return response;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public Object GetCustomerbyContact(String customerContact)
        {
            response.data = CustomerRepository.FindByCondition(x => x.CustomerContact == customerContact && x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (response.data == null)
            {
                Customer customer = CustomerRepository.FindByCondition(x => x.CustomerContact == customerContact && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
            Customer oldCustomer = CustomerRepository.FindByCondition(x => x.CustomerContact == customer.CustomerContact && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldCustomer == null)
            {
                CustomerRepository.Create(new Customer { CustomerName = customer.CustomerName, CustomerContact = customer.CustomerContact, CustomerNotes = customer.CustomerNotes, CustomerEmail = customer.CustomerEmail, WayofContactId = customer.WayofContactId, ContactStatusId = customer.ContactStatusId });

                if (customer.CustomerEmail != null)
                {
                    var emails = UserRoleRepository.FindByCondition(x => (x.BranchRole.RoleTypeId == (int)roleType.Sales) && x.IsActive == true && x.IsDeleted == false && x.User.IsActive == true && x.IsDeleted == false && x.BranchRole.IsActive == true && x.BranchRole.IsDeleted == false && x.Branch.IsActive == true && x.Branch.IsDeleted == false).Select(x => x.User.UserEmail).ToList();
                    foreach (var email in emails)
                    {
                        try
                        {
                            await mailService.SendEmailAsync(new MailRequest
                            {
                                Body = customer.CustomerNotes + "          \nName: " + customer.CustomerName + "\nContact: " + customer.CustomerContact + "\nEmail: " + customer.CustomerEmail,
                                Subject = "Request for Inquiry from New Customer " + customer.CustomerName,
                                ToEmail = email
                            });
                        }

                        catch (Exception e) 
                        {
                            Sentry.SentrySdk.CaptureMessage(e.Message);
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
                    var emails = UserRoleRepository.FindByCondition(x => (x.BranchRole.RoleTypeId == (int)roleType.Sales) && x.IsActive == true && x.IsDeleted == false && x.User.IsActive == true && x.IsDeleted == false && x.BranchRole.IsActive == true && x.BranchRole.IsDeleted == false && x.Branch.IsActive == true && x.Branch.IsDeleted == false).Select(x => x.User.UserEmail).ToList();
                    foreach (var email in emails)
                    {
                        try
                        {

                            await mailService.SendEmailAsync(new MailRequest
                            {
                                Body = customer.CustomerNotes + "          \nName: " + customer.CustomerName + "\nContact: " + customer.CustomerContact + "\nEmail: " + oldCustomer.CustomerEmail,
                                Subject = "Request for Inquiry from Customer " + customer.CustomerName,
                                ToEmail = email
                            });
                        }
                        catch (Exception e) { Sentry.SentrySdk.CaptureMessage(e.Message); }
                    }
                }
                CustomerRepository.Update(oldCustomer);
            }
            context.SaveChanges();
            return response;
        }


        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public Object AddCustomer(Customer customer)
        {
            if (customer.CustomerId == 0)
            {
                customer.BranchId = Constants.branchId;
                Customer oldCustomer = CustomerRepository.FindByCondition(x => x.CustomerContact == customer.CustomerContact && x.BranchId == customer.BranchId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                if (oldCustomer == null)
                {
                    CustomerRepository.Create(customer);
                    context.SaveChanges();
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
                CustomerRepository.Update(customer);
                context.SaveChanges();
                response.isError = false;
                response.errorMessage = "Success";


            }

            return response;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetContactStatus()
        {
            response.data = ContactStatusRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            return response;
        }

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetWayOfContacts()
        {
            response.data = WayOfContactRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            return response;
        }


        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Escalate)]
        [HttpPost]
        [Route("[action]")]
        public Object EscalateCustomer(int customerId)
        {
            Customer customer = CustomerRepository.FindByCondition(x => x.CustomerId == customerId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (customer != null)
            {
                CustomerRepository.Escalate(customer);
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

        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public Object DeleteCustomer(int customerId)
        {
            Customer customer = CustomerRepository.FindByCondition(x => x.CustomerId == customerId).FirstOrDefault();
            if (customer != null)
            {
                CustomerRepository.Delete(customer);
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
