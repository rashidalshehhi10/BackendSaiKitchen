using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        //[AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetCustomerOfBranch(int branchId)
        {
            System.Collections.Generic.List<CustomerResponse> customers = customerRepository.FindByCondition(x =>
                    x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId && x.Branch.IsActive == true &&
                    x.Branch.IsDeleted == false)
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
                        TotalNoOfInquiries = x.Inquiries.Count == 0 ? "No Inquiries" : x.Inquiries.Count.ToString(),
                        AddedOn = x.CreatedDate,
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
                    TotalNoOfInquiries = x.Inquiries.Count == 0 ? "No Inquiries" : x.Inquiries.Count.ToString()
                }).OrderByDescending(i => i.CustomerId).ToList());
            int? total = customers.Count;
            int? contacted = customers.Where(x => x.ContactStatusId == 1).Count();
            int? needToContact = customers.Where(x =>
                x.ContactStatusId == 2 &&
                (Helper.ConvertToDateTime(x.CustomerNextMeetingDate) <=
                    Helper.ConvertToDateTime(Helper.GetDateTime()) || x.CustomerNextMeetingDate == null)).Count();
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

        [HttpGet]
        [Route("[action]")]
        public object GetCustomerbyUser()
        {
            var Customers = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.BranchId == Constants.branchId)).Select(x => new
            {
                User = x.UserName,
                Customers = x.Customers.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
            }).ToList();
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
                        CustomerNationalId = x.CustomerNationalId
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
                        CustomerNationalId = x.CustomerNationalId
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
                    ContactStatusId = customer.ContactStatusId
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


        [AuthFilter((int)permission.ManageCustomer, (int)permissionLevel.Create)]
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
                customerRepository.Update(customer);
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