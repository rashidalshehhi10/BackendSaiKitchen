using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Log = Serilog.Log;
using System.Linq.Expressions;
using System.Reflection;

namespace SaiKitchenBackend.Controllers
{
    public class InquiryController : BaseController
    {
        [AuthFilter((int)permission.ManageInquiry, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddInquiryAsync(Inquiry inquiry)
        {
            inquiry.InquiryStartDate = Helper.GetDateTime();
            Customer customer = customerRepository.FindByCondition(x =>
                x.CustomerContact == inquiry.Customer.CustomerContact && x.BranchId == inquiry.BranchId &&
                x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (customer != null)
            {
                customer.CustomerName = inquiry.Customer.CustomerName;
                customer.CustomerEmail = inquiry.Customer.CustomerEmail;
                customer.CustomerAddress = inquiry.Customer.CustomerAddress;
                customer.CustomerNationalId = inquiry.Customer.CustomerNationalId;
                customer.ContactStatusId = inquiry.Customer.ContactStatusId;
                customer.WayofContactId = inquiry.Customer.WayofContactId;
                customer.CustomerCountry = inquiry.Customer.CustomerCountry;
                customer.CustomerCity = inquiry.Customer.CustomerCity;
                customer.CustomerNationality = inquiry.Customer.CustomerNationality;
                customer.IsActive = true;
                customer.IsDeleted = false;
                customer.IsEscalationRequested = false;
                inquiry.Customer = customer;
            }
            else
            {
                try
                {
                    Customer anotherBranchCustomer = customerRepository.FindByCondition(x =>
                        x.CustomerContact == inquiry.Customer.CustomerContact && x.IsActive == true &&
                        x.IsDeleted == false).FirstOrDefault();
                    if (anotherBranchCustomer != null)
                    {
                        customer = anotherBranchCustomer;
                        List<int?> roletypeId = new List<int?>
                        {
                            (int)roleType.Manager
                        };
                        sendNotificationToHead(
                            "Our Customer " + anotherBranchCustomer.CustomerName +
                            Constants.inquiryOnAnotherBranchMessage, false, null, null, roletypeId,
                            anotherBranchCustomer.BranchId, (int)notificationCategory.Other);
                    }
                    else
                    {
                        customer = inquiry.Customer;
                    }
                }

                catch (Exception ex)
                {
                    Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
                }
            }

            if (inquiry.Building.BuildingAddress == null || inquiry.Building.BuildingAddress == "")
            {
                inquiry.Building.BuildingAddress = customer.CustomerAddress;
                inquiry.Building.BuildingMakaniMap = customer.CustomerNationalId;
            }

            if (inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo != null)
            {
                sendNotificationToOneUser(
                    Constants.measurementAssign + "" +
                    inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, false, null, null,
                    (int)inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo, (int)inquiry.BranchId,
                    (int)notificationCategory.Measurement);
            }

            if (!(bool)inquiry.IsMeasurementProvidedByCustomer)
            {
                inquiry.MeasurementFees = FeesRepository
                    .FindByCondition(x => x.FeesId == 1 && x.IsActive == true && x.IsDeleted == false).FirstOrDefault()
                    .FeesAmount;
            }
            else
            {
                inquiry.MeasurementFees = "0";
            }
            //if (inquiry.Payments.Count > 0)
            //{

            foreach (Payment payment in inquiry.Payments)
            {
                payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                payment.PaymentModeId = (int)paymentMode.Cash;
                payment.PaymentTypeId = (int)paymenttype.Measurement;
                payment.IsActive = true;
                payment.IsDeleted = false;
                Helper.AddPayment(payment.PaymentAmount);
                payment.CreatedDate = Helper.GetDateTime();
                payment.CreatedBy = Constants.userId;
                payment.UpdatedBy = Constants.userId;
                payment.UpdatedDate = Helper.GetDateTime();
                if (payment.PaymentAmount <= 0)
                {
                    payment.IsActive = false;
                }
            }

            //}
            foreach (InquiryWorkscope inworkscope in inquiry.InquiryWorkscopes)
            {
                inworkscope.InquiryStatusId = (int)inquiryStatus.measurementAssigneePending;
            }

            inquiry.InquiryStatusId = (int)inquiryStatus.measurementAssigneePending;
            inquiryRepository.Create(inquiry);
            context.SaveChanges();

            inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
            //inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserFcmtoken
            response.data = "";
            try
            {
                //(String toEmail, String inquiryCode, String measurementScheduleDate, String assignTo, String contactNumber, String buildingAddress)
                await mailService.SendInquiryEmailAsync(inquiry.Customer.CustomerEmail, inquiry.InquiryCode,
                    inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate,
                    inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserName,
                    inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserMobile,
                    inquiry.Building.BuildingAddress);
            }

            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object AddComment(AddComment comment)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x => x.InquiryId == comment.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryComment = comment.comment;
                inquiry.InquiryCommentsAddedOn = Helper.GetDateTime();
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
                response.data = "Comment Added Successfully";
            }
            else
            {
                response.isError = false;
                response.errorMessage = "Inquiry Not Found ";
            }

            return response;
        }


        [AuthFilter((int)permission.ManageInquiry, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object AddWorkscopetoInquiry(WorkscopeInquiry workscopeInquiry)
        {
            InquiryWorkscope inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(i =>
                i.InquiryId == workscopeInquiry.inquiryWorkscopeId && i.IsActive == true &&
                (i.InquiryStatusId == (int)inquiryStatus.measurementInProgress ||
                 i.InquiryStatusId == (int)inquiryStatus.measurementRejected ||
                 i.InquiryStatusId == (int)inquiryStatus.measurementdelayed ||
                 i.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending ||
                 i.InquiryStatusId == (int)inquiryStatus.measurementAssigneeRejected ||
                 i.InquiryStatusId == (int)inquiryStatus.measurementAssigneeAccepted)).FirstOrDefault();
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.CreatedDate = null;
                foreach (int workscopeId in workscopeInquiry.WorkScopeId.OfType<int>())
                {
                    InquiryWorkscope newInquiryWorkscope = new InquiryWorkscope
                    {
                        InquiryId = inquiryWorkscope.InquiryId,
                        WorkscopeId = workscopeId,
                        InquiryStatusId = inquiryWorkscope.InquiryStatusId,
                        IsActive = true,
                        IsDeleted = false,
                        MeasurementAssignedTo = inquiryWorkscope.MeasurementAssignedTo,
                        MeasurementScheduleDate = inquiryWorkscope.MeasurementScheduleDate,
                        DesignAssignedTo = inquiryWorkscope.DesignAssignedTo,
                        DesignScheduleDate = inquiryWorkscope.DesignScheduleDate
                    };

                    inquiryWorkscopeRepository.Create(newInquiryWorkscope);
                }

                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry doesn't exist";
            }

            return response;
        }

        [AuthFilter((int)permission.ManageInquiry, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public object DeleteWorkscopefromInquiry(WorkscopeInquiry workscopeInquiry)
        {
            //var inquiryWorkscope = context.InquiryWorkscopes.FirstOrDefault(i => i.InquiryWorkscopeId == workscopeInquiry.inquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false);
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.IsActive == true && x.IsDeleted == false && x.InquiryWorkscopes.Any(y =>
                        y.IsActive == true && y.IsDeleted == false &&
                        y.InquiryWorkscopeId == workscopeInquiry.inquiryWorkscopeId))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();
            InquiryWorkscope inquiryworkscope = inquiry.InquiryWorkscopes
                .Where(x => x.InquiryWorkscopeId == workscopeInquiry.inquiryWorkscopeId).FirstOrDefault();
            if (inquiryworkscope != null)
            {
                inquiryworkscope.IsDeleted = true;
                IEnumerable<InquiryWorkscope> _inquiryworkscope =
                    inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false);
                if (_inquiryworkscope.Count() == 0)
                {
                    inquiry.IsDeleted = true;
                }

                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry doesn't exist";
            }

            return response;
        }

        //[AuthFilter((int)permission.ManageInquiry, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetWorkscope()
        {
            response.data = workScopeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            return response;
        }

        [AuthFilter((int)permission.ManageInquiry, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetAllInquiries()
        {
            response.data = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes);
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiriesByBranch(int pageId)
        {
            var inquiry = await inquiryRepository.GetPagedAsync(x => x.OrderByDescending(y => y.InquiryId),
                (x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId),
                pageId, 10, null);
            var inquiries = inquiry.Select(x => new ViewInquiryDetail
            {
                // InquiryWorkscopeId = x.InquiryWorkscopeId,
                InquiryId = x.InquiryId,
                InquiryDescription = x.InquiryDescription, //x.Inquiry.InquiryDescription,
                InquiryStartDate =
                        Helper.GetDateFromString(x
                            .InquiryStartDate), //Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                MeasurementAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation
                            .UserName, //x.MeasurementAssignedToNavigation.UserName,
                InquiryComment = x.InquiryComment, //x.Comments,
                                                   //WorkScopeId = x.WorkscopeId,
                                                   //WorkScopeName = x.Workscope.WorkScopeName,
                DesignScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate, // x.DesignScheduleDate,
                DesignAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation
                            .UserName, // x.DesignAssignedToNavigation.UserName,
                Status = x.InquiryStatusId,
                IsMeasurementProvidedByCustomer =
                        x.IsMeasurementProvidedByCustomer == true
                            ? "yes"
                            : "No", // x.Inquiry.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                IsDesignProvidedByCustomer =
                        x.IsDesignProvidedByCustomer == true
                            ? "Yes"
                            : "No", // x.Inquiry.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                MeasurementScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, // x.MeasurementScheduleDate,
                BuildingAddress = x.Building.BuildingAddress, //x.Inquiry.Building.BuildingAddress,
                BuildingMakaniMap = x.Building.BuildingMakaniMap, //x.Inquiry.Building.BuildingMakaniMap,
                BuildingCondition = x.Building.BuildingCondition, // x.Inquiry.Building.BuildingCondition,
                BuildingFloor = x.Building.BuildingFloor, //x.Inquiry.Building.BuildingFloor,
                BuildingReconstruction =
                        (bool)x.Building.BuildingReconstruction
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                IsOccupied =
                        (bool)x.Building.IsOccupied
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                InquiryEndDate =
                        Helper.GetDateFromString(x
                            .InquiryEndDate), //Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit, // x.Inquiry.Building.BuildingTypeOfUnit,
                IsEscalationRequested = x.IsEscalationRequested, //x.Inquiry.IsEscalationRequested,
                CustomerId = x.CustomerId, // x.Inquiry.CustomerId,
                CustomerCode =
                        "CS" + x.BranchId + "" + x.CustomerId, //"CS" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId,
                CustomerName = x.Customer.CustomerName, // x.Inquiry.Customer.CustomerName,
                CustomerEmail = x.Customer.CustomerEmail, //x.Inquiry.Customer.CustomerEmail,
                CustomerContact = x.Customer.CustomerContact, //x.Inquiry.Customer.CustomerContact,
                CustomerWhatsapp = x.Customer.CustomerWhatsapp, //x.Inquiry.Customer.CustomerWhatsapp,
                BranchId = x.BranchId, //x.Inquiry.BranchId,
                InquiryAddedBy = x.ManagedByNavigation.UserName, //x.Inquiry.AddedByNavigation.UserName,
                InquiryAddedById = x.ManagedBy, // x.Inquiry.AddedBy,
                NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements
                        .Count(y => y.IsDeleted == false), //x.Measurements.Where(y => y.IsDeleted == false).Count(),
                InquiryCode =
                        "IN" + x.BranchId + "" + x.CustomerId + "" +
                        x.InquiryId, //"IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName)
                        .ToList(), // x.Inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList()
                                   //InquiryWorkscopes =x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).ToList()
                CommentAddedOn = x.InquiryCommentsAddedOn,
                DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                QuotationAddedOn = x.QuotationAddedOn,
                FactorName = x.JobOrders.FirstOrDefault().Factory.BranchName
            });
            response.data = inquiries;
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object test ()
        {
            var type = typeof(Inquiry);
            var parameterExprission = Expression.Parameter(typeof(Inquiry), "Inquiry");
            var constant = Expression.Constant(120);
            var property = Expression.Property(parameterExprission, "InquiryId");
            var expression = Expression.Equal(property, constant);
            var property2 = Expression.Property(parameterExprission, "BranchId");
            int s = 1;
            constant = Expression.Constant(s,typeof(int?));
            var experssion2 = Expression.Equal(property2, constant);
            expression = Expression.And(expression, experssion2);
            var lambda = Expression.Lambda<Func<Inquiry, bool>>(expression, parameterExprission);
            var inquiry = inquiryRepository.FindByCondition(lambda);
            response.data = inquiry;
            return response;
        }
        //draw and start and length
        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetPagingInquiriesOfBranch(int branchId,[FromForm] int? draw,[FromForm] int? start,[FromForm] int? length,[FromForm(Name = "columns[0][search][value]")] int? inquiryId,[FromForm(Name = "columns[1][search][value]")] string inquiryCode, [FromForm(Name = "columns[2][search][value]")] int? status, [FromForm(Name = "columns[3][search][value]")] string customerName, [FromForm(Name = "columns[4][search][value]")] string workscopeNames, [FromForm(Name = "columns[6][search][value]")] string measurementScheduleDate, [FromForm(Name = "columns[7][search][value]")] int? measurementAssignTo, [FromForm(Name = "columns[8][search][value]")] string? designScheduleDate, [FromForm(Name = "columns[9][search][value]")] int? designAssignTo, [FromForm(Name = "columns[12][search][value]")] string customerCode, [FromForm(Name = "columns[13][search][value]")] string customerContact, [FromForm(Name = "columns[15][search][value]")] string buildingAddress, [FromForm(Name = "columns[25][search][value]")] int? HandledBy)
        {
            if (draw == null)
            {
                draw = 20;
            }
            if (length == null)
            {
                length = 10;
            }
            if (start == null)
            {
                start = 0;
            }
            var type = typeof(Inquiry);
            var parameterExprission = Expression.Parameter(typeof(Inquiry), "x");
            var constant = Expression.Constant(true, typeof(bool?));
            var property = Expression.Property(parameterExprission, "IsActive");
            var expression = Expression.Equal(property, constant);
            var property2 = Expression.Property(parameterExprission, "IsDeleted");
            constant = Expression.Constant(false, typeof(bool?));
            var experssion2 = Expression.Equal(property2, constant);
            expression = Expression.And(expression, experssion2);
            var property3 = Expression.Property(parameterExprission, "BranchId");
            constant = Expression.Constant(branchId, typeof(int?));
            var experssion3 = Expression.Equal(property3, constant);
            expression = Expression.And(expression, experssion3);
            if (inquiryId != null)
            {
                var _property = Expression.Property(parameterExprission, "InquiryId");
                constant = Expression.Constant(inquiryId);
                var _experssion = Expression.Equal(_property, constant);
                expression = Expression.And(expression, _experssion);
            }
            if (inquiryCode != null)
            {
                var _property = Expression.Property(parameterExprission, "InquiryCode");
                constant = Expression.Constant(inquiryCode);
                var _experssion = Expression.Equal(_property, constant);
                expression = Expression.And(expression, _experssion);
            }
            if (status != null)
            {
                var _property = Expression.Property(parameterExprission, "InquiryStatusId");
                constant = Expression.Constant(status,typeof(int?));
                var _experssion = Expression.Equal(_property, constant);
                expression = Expression.And(expression, _experssion);
            }
            if (customerName != null)
            {
                Expression _property = parameterExprission;
                foreach (var item in "Customer.CustomerName".Split('.'))
                {
                    _property = Expression.PropertyOrField(_property, item);
                }
                constant = Expression.Constant(customerName);
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var _experssion = Expression.Call(_property, method, constant);
                expression = Expression.And(expression, _experssion);
            }
            if (workscopeNames != null)
            {
                Expression _property = parameterExprission;
                foreach (var item in "WorkScope.WorkScopeName".Split('.'))
                {
                    _property = Expression.PropertyOrField(_property, item);
                }
                constant = Expression.Constant(workscopeNames);
                var _experssion = Expression.Equal(_property, constant);
                expression = Expression.And(expression, _experssion);
            }
            if (measurementScheduleDate != null)
            {
                var _parameter = Expression.Parameter(typeof(InquiryWorkscope), "y");
                Expression _property = Expression.Property(_parameter, "MeasurementScheduleDate");
                constant = Expression.Constant(measurementScheduleDate);
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var _experssion = Expression.Call(_property,method,constant);
                var _lambda = Expression.Lambda<Func<InquiryWorkscope, bool>>(_experssion, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(InquiryWorkscope)},
                    Expression.Property(parameterExprission,"InquiryWorkScopes"),_lambda);
                expression = Expression.And(expression, body);
            }
            if (measurementAssignTo != null)
            {
                var _parameter = Expression.Parameter(typeof(InquiryWorkscope), "y");
                Expression _property = Expression.Property(_parameter, "MeasurementAssignedTo");
                constant = Expression.Constant(measurementAssignTo, typeof(int?));
                var _experssion = Expression.Equal(_property, constant);
                var _lambda = Expression.Lambda<Func<InquiryWorkscope, bool>>(_experssion, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(InquiryWorkscope) },
                    Expression.Property(parameterExprission, "InquiryWorkScopes"), _lambda);
                expression = Expression.And(expression, body);
            }
            if (designScheduleDate != null)
            {
                var _parameter = Expression.Parameter(typeof(InquiryWorkscope), "y");
                Expression _property = Expression.Property(_parameter, "DesignScheduleDate");
                constant = Expression.Constant(designScheduleDate);
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var _experssion = Expression.Call(_property, method, constant);
                var _lambda = Expression.Lambda<Func<InquiryWorkscope, bool>>(_experssion, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(InquiryWorkscope) },
                    Expression.Property(parameterExprission, "InquiryWorkScopes"), _lambda);
                expression = Expression.And(expression, body);
            }
            if (designAssignTo != null)
            {
                var _parameter = Expression.Parameter(typeof(InquiryWorkscope), "y");
                Expression _property = Expression.Property(_parameter, "DesignAssignedTo");
                constant = Expression.Constant(designAssignTo,typeof(int?));
                var _experssion = Expression.Equal(_property, constant);
                var _lambda = Expression.Lambda<Func<InquiryWorkscope, bool>>(_experssion, _parameter);
                var body = Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Any),
                    new Type[] { typeof(InquiryWorkscope) },
                    Expression.Property(parameterExprission, "InquiryWorkScopes"), _lambda);
                expression = Expression.And(expression, body);
            }
            if (customerCode != null)
            {
                Expression _property = parameterExprission;
                foreach (var item in "Customer.CustomerId".Split('.'))
                {
                    _property = Expression.PropertyOrField(_property, item);
                }
                string BranchId = branchId.ToString();
                int id = int.Parse(customerCode.Substring(BranchId.Length + 2));
                constant = Expression.Constant(id,typeof(int));
                var _experssion = Expression.Equal(_property, constant);
                expression = Expression.And(expression, _experssion);
            }
            if (customerContact != null)
            {
                Expression _property = parameterExprission;
                foreach (var item in "Customer.CustomerContact".Split('.'))
                {
                    _property = Expression.PropertyOrField(_property, item);
                }
                constant = Expression.Constant(customerContact);
                var _experssion = Expression.Equal(_property, constant);
                expression = Expression.And(expression, _experssion);
            }
            if (buildingAddress != null)
            {
                Expression _property = parameterExprission;
                foreach (var item in "Building.BuildingAddress".Split('.'))
                {
                    _property = Expression.PropertyOrField(_property, item);
                }
                constant = Expression.Constant(buildingAddress);
                var _experssion = Expression.Equal(_property, constant);
                expression = Expression.And(expression, _experssion);
            }
            if (HandledBy != null)
            {
                var _property = Expression.Property(parameterExprission, "ManagedBy");
                constant = Expression.Constant(HandledBy, typeof(int?));
                var _experssion = Expression.Equal(_property, constant);
                expression = Expression.And(expression, _experssion);
            }
            var lambda = Expression.Lambda<Func<Inquiry, bool>>(expression, parameterExprission);
            
            
            //var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.Inquiry.BranchId == branchId && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false && x.IsActive == true && x.IsDeleted == false)
            var inquiries = await inquiryRepository
                .FindByCondition(lambda)
                .Select(x => new ViewInquiryDetail
                {
                    // InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription, //x.Inquiry.InquiryDescription,
                    InquiryStartDate =
                        Helper.GetDateFromString(x
                            .InquiryStartDate), //Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                    MeasurementAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation
                            .UserName,
                    MeasurementAssignToId = x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo
                           ,
                    //x.MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment, //x.Comments,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    DesignScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate, // x.DesignScheduleDate,
                    DesignAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation
                            .UserName,
                    DesignAssignToId =
                        x.InquiryWorkscopes.FirstOrDefault().DesignAssignedTo,
                    ManagedBy = x.ManagedByNavigation.UserName,
                    ManagedById = x.ManagedBy,
                    // x.DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer =
                        x.IsMeasurementProvidedByCustomer == true
                            ? "yes"
                            : "No", // x.Inquiry.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer =
                        x.IsDesignProvidedByCustomer == true
                            ? "Yes"
                            : "No", // x.Inquiry.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, // x.MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress, //x.Inquiry.Building.BuildingAddress,
                    BuildingMakaniMap = x.Building.BuildingMakaniMap, //x.Inquiry.Building.BuildingMakaniMap,
                    BuildingCondition = x.Building.BuildingCondition, // x.Inquiry.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor, //x.Inquiry.Building.BuildingFloor,
                    BuildingReconstruction =
                        (bool)x.Building.BuildingReconstruction
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied =
                        (bool)x.Building.IsOccupied
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate =
                        Helper.GetDateFromString(x
                            .InquiryEndDate), //Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit, // x.Inquiry.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested, //x.Inquiry.IsEscalationRequested,
                    CustomerId = x.CustomerId, // x.Inquiry.CustomerId,
                    CustomerCode =
                        "CS" + x.BranchId + "" + x.CustomerId, //"CS" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId,
                    CustomerName = x.Customer.CustomerName, // x.Inquiry.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail, //x.Inquiry.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact, //x.Inquiry.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp, //x.Inquiry.Customer.CustomerWhatsapp,
                    BranchId = x.BranchId, //x.Inquiry.BranchId,
                    InquiryAddedBy = x.ManagedByNavigation.UserName, //x.Inquiry.AddedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy,
                    InquiryAddedOn = x.CreatedDate,
                    // x.Inquiry.AddedBy,
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements
                        .Count(y => y.IsDeleted == false), //x.Measurements.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode =
                        "IN" + x.BranchId + "" + x.CustomerId + "" +
                        x.InquiryId, //"IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName)
                        .ToList(), // x.Inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList()
                    //InquiryWorkscopes =x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).ToList()
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn,
                    QuotationScheduleDate = x.QuotationScheduleDate,
                    FactorName = x.JobOrders.FirstOrDefault().Factory.BranchName,
                    payments = x.Payments.Where(x => x.IsActive == true && x.IsDeleted == false && x.IsAmountRecieved != true && (x.PaymentStatusId != (int)paymentstatus.InstallmentApproved || x.PaymentStatusId != (int)paymentstatus.PaymentApproved) && x.PaymentModeId == (int)paymentMode.Cheque).ToList()
                }).OrderByDescending(x => x.InquiryId).Skip((int)start).Take((int)length)
                .ToListAsync();
            tableResponse.data = inquiries;
            tableResponse.recordsTotal =  inquiryRepository
                .FindByCondition(lambda).Count();
            tableResponse.recordsFiltered = tableResponse.recordsTotal;
            return tableResponse;
        }

        [HttpPost]
        [Route("[action]")]
        public object EscalationRequest(ChangeManaged change)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == change.inquiryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.EscalationRequestedDate = Helper.GetDateTime();
                inquiry.IsEscalationRequested = true;
                inquiry.EscalationRequestedBy = change.Id;
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
                response.data = inquiry;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetInquiriesOfBranchPage(int branchId,int page)
        {
            //var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.Inquiry.BranchId == branchId && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false && x.IsActive == true && x.IsDeleted == false)
            var inquiries = await inquiryRepository
                .FindByCondition(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false)
                .Select(x => new ViewInquiryDetail
                {
                    // InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription, //x.Inquiry.InquiryDescription,
                    InquiryStartDate =
                        Helper.GetDateFromString(x
                            .InquiryStartDate), //Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                    MeasurementAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation
                            .UserName, //x.MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment, //x.Comments,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    DesignScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate, // x.DesignScheduleDate,
                    DesignAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation
                            .UserName, // x.DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer =
                        x.IsMeasurementProvidedByCustomer == true
                            ? "yes"
                            : "No", // x.Inquiry.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer =
                        x.IsDesignProvidedByCustomer == true
                            ? "Yes"
                            : "No", // x.Inquiry.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, // x.MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress, //x.Inquiry.Building.BuildingAddress,
                    BuildingMakaniMap = x.Building.BuildingMakaniMap, //x.Inquiry.Building.BuildingMakaniMap,
                    BuildingCondition = x.Building.BuildingCondition, // x.Inquiry.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor, //x.Inquiry.Building.BuildingFloor,
                    BuildingReconstruction =
                        (bool)x.Building.BuildingReconstruction
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied =
                        (bool)x.Building.IsOccupied
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate =
                        Helper.GetDateFromString(x
                            .InquiryEndDate), //Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit, // x.Inquiry.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested, //x.Inquiry.IsEscalationRequested,
                    CustomerId = x.CustomerId, // x.Inquiry.CustomerId,
                    CustomerCode =
                        "CS" + x.BranchId + "" + x.CustomerId, //"CS" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId,
                    CustomerName = x.Customer.CustomerName, // x.Inquiry.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail, //x.Inquiry.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact, //x.Inquiry.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp, //x.Inquiry.Customer.CustomerWhatsapp,
                    BranchId = x.BranchId, //x.Inquiry.BranchId,
                    InquiryAddedBy = x.ManagedByNavigation.UserName, //x.Inquiry.AddedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy, // x.Inquiry.AddedBy,
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements
                        .Count(y => y.IsDeleted == false), //x.Measurements.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode =
                        "IN" + x.BranchId + "" + x.CustomerId + "" +
                        x.InquiryId, //"IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName)
                        .ToList(), // x.Inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList()
                    //InquiryWorkscopes =x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).ToList()
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn,
                    FactorName = x.JobOrders.FirstOrDefault().Factory.BranchName
                }).OrderByDescending(x => x.InquiryId).Skip((page - 1) * Constants.PageSize).Take(Constants.PageSize)
                .ToListAsync();
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count;
            tableResponse.recordsFiltered = inquiries.Count;
            return tableResponse;
        }

        //[AuthFilter((int)permission.ManageInquiry, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetInquiriesOfBranch(int branchId)
        {
            //var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.Inquiry.BranchId == branchId && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false && x.IsActive == true && x.IsDeleted == false)
            var inquiries = inquiryRepository
                .FindByCondition(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false)
                .Select(x => new ViewInquiryDetail
                {
                    // InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription, //x.Inquiry.InquiryDescription,
                    InquiryStartDate =
                        Helper.GetDateFromString(x
                            .InquiryStartDate), //Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                    MeasurementAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation
                            .UserName, //x.MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment, //x.Comments,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    DesignScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate, // x.DesignScheduleDate,
                    DesignAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation
                            .UserName, // x.DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer =
                        x.IsMeasurementProvidedByCustomer == true
                            ? "yes"
                            : "No", // x.Inquiry.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer =
                        x.IsDesignProvidedByCustomer == true
                            ? "Yes"
                            : "No", // x.Inquiry.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, // x.MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress, //x.Inquiry.Building.BuildingAddress,
                    BuildingMakaniMap = x.Building.BuildingMakaniMap, //x.Inquiry.Building.BuildingMakaniMap,
                    BuildingCondition = x.Building.BuildingCondition, // x.Inquiry.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor, //x.Inquiry.Building.BuildingFloor,
                    BuildingReconstruction =
                        (bool)x.Building.BuildingReconstruction
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied =
                        (bool)x.Building.IsOccupied
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate =
                        Helper.GetDateFromString(x
                            .InquiryEndDate), //Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit, // x.Inquiry.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested, //x.Inquiry.IsEscalationRequested,
                    CustomerId = x.CustomerId, // x.Inquiry.CustomerId,
                    CustomerCode =
                        "CS" + x.BranchId + "" + x.CustomerId, //"CS" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId,
                    CustomerName = x.Customer.CustomerName, // x.Inquiry.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail, //x.Inquiry.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact, //x.Inquiry.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp, //x.Inquiry.Customer.CustomerWhatsapp,
                    BranchId = x.BranchId, //x.Inquiry.BranchId,
                    InquiryAddedBy = x.ManagedByNavigation.UserName, //x.Inquiry.AddedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy, // x.Inquiry.AddedBy,
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements
                        .Count(y => y.IsDeleted == false), //x.Measurements.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode =
                        "IN" + x.BranchId + "" + x.CustomerId + "" +
                        x.InquiryId, //"IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName)
                        .ToList(), // x.Inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList()
                    //InquiryWorkscopes =x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).ToList()
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn,
                    FactorName = x.JobOrders.FirstOrDefault().Factory.BranchName,
                    payments = x.Payments.Where(x => x.IsActive == true && x.IsDeleted == false && x.IsAmountRecieved == false && (x.PaymentStatusId != (int)paymentstatus.InstallmentApproved || x.PaymentStatusId !=(int) paymentstatus.PaymentApproved)&& x.PaymentModeId == (int)paymentMode.Cheque).ToList()
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

        private static string GetInquiryCode(InquiryWorkscope x)
        {
            return "IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetInquiryStatus()
        {
            var inquiryStatus = inquiryStatusRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .Select(x => new
                {
                    inquiryStatusName = x.InquiryStatusName,
                    inquiryStatusId = x.InquiryStatusId
                });
            response.data = inquiryStatus;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetInquiryStatusForInquiries(int branchId)
        {
            var status = inquiryRepository.FindByCondition(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false).Select(x => new
            {
                inquiryStatusName = x.InquiryStatus.InquiryStatusName,
                inquiryStatusId = x.InquiryStatusId
            }).Distinct();
            response.data = status;
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public object GetManagedByForInquiries(int branchId)
        {
            var ManagedBy = inquiryRepository.FindByCondition(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false).Select(x => new
            {
                ManagedByName = x.ManagedByNavigation.UserName,
                ManagedById = x.ManagedBy
            }).Distinct();
            response.data = ManagedBy;
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object ChangeInquiryManagedBy(ChangeManaged change)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x => x.InquiryId == change.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.ManagedBy = change.Id;
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry Not Found";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetinquiryStatusByBranch()
        {
            List<InquiryStatus> inquiryStatus = inquiryStatusRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .ToList();
            List<object> list = new List<object>();
            foreach (InquiryStatus status in inquiryStatus)
            {
                var inquiry = branchRepository
                    .FindByCondition(
                        x => x.BranchId == Constants.branchId && x.IsActive == true && x.IsDeleted == false).Select(x =>
                        new
                        {
                            inquiryCount = x.Inquiries.Where(y =>
                                y.IsActive == true && y.IsDeleted == false &&
                                y.InquiryStatusId == status.InquiryStatusId).Count(),
                            inquiryStatusId = status.InquiryStatusId,
                            inquiryStatusName = status.InquiryStatusName
                        });
                list.Add(inquiry);
            }
            var inquiries = branchRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId).Select(x => new
            {
                totalinquiries = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                inquiries = list,
            });
            response.data = inquiries;
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object GetinquiryDetailsById(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Measurements.Where(z => z.IsActive == true && z.IsDeleted == false))
                .ThenInclude(m => m.Files.Where(f => f.IsActive == true && f.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Quotations.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Building).Include(x => x.Customer)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.Workscope)
                .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false))
                .FirstOrDefault();

            return InquiryDetail(inquiry);
        }


        [HttpGet]
        [Route("[action]")]
        public void CheckScheduleDate()
        {
            IQueryable<Inquiry> inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == 1);
            foreach (Inquiry inquiry in inquiries)
            {
                IQueryable<InquiryWorkscope> inquiryWorkscopes = inquiryWorkscopeRepository.FindByCondition(x =>
                    x.InquiryId == inquiry.InquiryId && x.IsActive == true && x.IsDeleted == false);
                foreach (InquiryWorkscope inquiryWorkscope in inquiryWorkscopes)
                {
                    if (inquiryWorkscope.InquiryStatusId is (int)inquiryStatus.measurementInProgress or (int)inquiryStatus.measurementRejected)
                    {
                        inquiryWorkscope.InquiryStatusId =
                            Helper.ConvertToDateTime(inquiryWorkscope.MeasurementScheduleDate) <
                            Helper.ConvertToDateTime(Helper.GetDateTime())
                                ? (int)inquiryStatus.measurementdelayed
                                : (int)inquiryStatus.measurementInProgress;
                        inquiry.InquiryStatusId =
                            Helper.ConvertToDateTime(inquiryWorkscope.MeasurementScheduleDate) <
                            Helper.ConvertToDateTime(Helper.GetDateTime())
                                ? (int)inquiryStatus.measurementdelayed
                                : (int)inquiryStatus.measurementInProgress;
                    }
                    else if (inquiryWorkscope.InquiryStatusId is (int)inquiryStatus.designPending or (int)inquiryStatus.designRevisionRequested)
                    {
                        inquiryWorkscope.InquiryStatusId =
                            Helper.ConvertToDateTime(inquiryWorkscope.DesignScheduleDate) <
                            Helper.ConvertToDateTime(Helper.GetDateTime())
                                ? (int)inquiryStatus.designDelayed
                                : (int)inquiryStatus.designPending;
                        inquiry.InquiryStatusId =
                            Helper.ConvertToDateTime(inquiryWorkscope.DesignScheduleDate) <
                            Helper.ConvertToDateTime(Helper.GetDateTime())
                                ? (int)inquiryStatus.designDelayed
                                : (int)inquiryStatus.designPending;
                    }
                    else if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending)
                    {
                        inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.measurementAssigneeRejected;
                        inquiry.InquiryStatusId = (int)inquiryStatus.measurementAssigneeRejected;
                    }
                    else if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.designAssigneePending)
                    {
                        inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designAssigneeRejected;
                        inquiry.InquiryStatusId = (int)inquiryStatus.designAssigneeRejected;
                    }

                    if (inquiry.InquiryStatusId == (int)inquiryStatus.quotationPending)
                    {
                        if (Helper.ConvertToDateTime(inquiry.QuotationScheduleDate) <
                            Helper.ConvertToDateTime(Helper.GetDateTime()))
                        {
                            inquiry.InquiryStatusId = (int)inquiryStatus.quotationDelayed;
                            Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.quotationDelayed);
                            Helper.Each(inquiry.Quotations, x => x.QuotationStatusId = (int)inquiryStatus.quotationDelayed);
                        }
                    }

                    if (inquiry.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress)
                    {
                        var job = jobOrderRepository.FindByCondition(x => x.InquiryId == inquiry.InquiryId && x.IsActive == true && x.IsDeleted == false)
                            .Include(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
                        if (Helper.ConvertToDateTime(job.JobOrderDetails.FirstOrDefault().InstallationStartDate) <
                            Helper.ConvertToDateTime(Helper.GetDateTime()))
                        {
                            inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderDelayed;
                            Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.jobOrderDelayed);
                        }
                    }
                    //inquiryWorkscopeRepository.Update(inquiryWorkscope);
                }

                inquiryRepository.Update(inquiry);
            }

            context.SaveChanges();
        }

        [HttpGet]
        [Route("[action]")]
        public void CheckNotifyScheduleDate()
        {
            IQueryable<Inquiry> inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false));

            List<int?> roletypeId = new List<int?>
            {
                (int)roleType.Manager
            };
            foreach (Inquiry inquiry in inquiries)
            {
                var inquiryWorkscope = inquiry.InquiryWorkscopes.FirstOrDefault();
                if (inquiryWorkscope.InquiryStatusId is (int)inquiryStatus.measurementInProgress or (int)inquiryStatus.measurementRejected)
                {
                    Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = Helper.ConvertToDateTime(inquiryWorkscope.MeasurementScheduleDate) <
                             Helper.ConvertToDateTime(Helper.GetDateTime())
                                 ? (int)inquiryStatus.measurementdelayed
                                 : (int)inquiryStatus.measurementInProgress);

                    if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.measurementdelayed)
                    {
                        var user = userRepository.FindByCondition(x =>
                    x.UserId == inquiryWorkscope.MeasurementAssignedTo &&
                    x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();
                        sendNotificationToHead(
                            user.Name + Constants.MeasurementDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                "" + inquiry.CustomerId + "" + inquiry.InquiryId, false,
                            null,
                            null,
                            roletypeId, inquiry.BranchId,
                            (int)notificationCategory.Measurement);

                        sendNotificationToOneUser(
                            user.Name + Constants.MeasurementDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                "" + inquiry.CustomerId + "" + inquiry.InquiryId, false, null,
                            null,
                            (int)inquiry.ManagedBy, (int)inquiry.BranchId,
                            (int)notificationCategory.Measurement);
                    }
                }
                else if (inquiryWorkscope.InquiryStatusId is (int)inquiryStatus.designPending or (int)inquiryStatus.designRevisionRequested)
                {
                    Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = Helper.ConvertToDateTime(inquiryWorkscope.MeasurementScheduleDate) <
                         Helper.ConvertToDateTime(Helper.GetDateTime())
                             ? (int)inquiryStatus.designDelayed
                             : (int)inquiryStatus.designPending);
                    if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.designDelayed)
                    {
                        var user = userRepository.FindByCondition(x =>
                    x.UserId == inquiryWorkscope.DesignAssignedTo &&
                    x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();
                        sendNotificationToHead(user.Name + Constants.DesignDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                "" + inquiry.CustomerId + "" + inquiry.InquiryId, true,
                            null,
                            null,
                            roletypeId, (int)inquiry.BranchId,
                            (int)notificationCategory.Design);

                        sendNotificationToOneUser(user.Name + Constants.DesignDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                "" + inquiry.CustomerId + "" + inquiry.InquiryId,
                            false, null, null,
                            (int)inquiry.ManagedBy, (int)inquiry.BranchId,
                            (int)notificationCategory.Design);
                    }
                }
                else if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending)
                {
                    var user = userRepository.FindByCondition(x =>
                    x.UserId == inquiryWorkscope.MeasurementAssignedTo &&
                    x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();

                    Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.measurementAssigneeRejected);

                    sendNotificationToHead(
                        user.Name + Constants.MeasurementAssigneeDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                "" + inquiry.CustomerId + "" + inquiry.InquiryId, false,
                        null,
                        null,
                        roletypeId, inquiry.BranchId,
                        (int)notificationCategory.Measurement);

                    sendNotificationToOneUser(
                        user.Name + Constants.MeasurementAssigneeDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                "" + inquiry.CustomerId + "" + inquiry.InquiryId, false, null,
                        null,
                        (int)inquiryWorkscope.MeasurementAssignedTo, (int)inquiry.BranchId,
                        (int)notificationCategory.Measurement);
                }
                else if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.designAssigneePending)
                {
                    var user = userRepository.FindByCondition(x =>
                    x.UserId == inquiryWorkscope.DesignAssignedTo &&
                    x.IsActive == true && x.IsDeleted == false).Select(y => new
                    {
                        Name = y.UserName
                    }).FirstOrDefault();
                    Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.designAssigneeRejected);

                    sendNotificationToHead(user.Name + Constants.DesignAssigneeDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                "" + inquiry.CustomerId + "" + inquiry.InquiryId,
                        true,
                        null,
                        null,
                        roletypeId, (int)inquiry.BranchId,
                        (int)notificationCategory.Design);

                    sendNotificationToOneUser(user.Name + Constants.DesignAssigneeDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                "" + inquiry.CustomerId + "" + inquiry.InquiryId,
                        false, null, null,
                        (int)inquiry.ManagedBy, (int)inquiry.BranchId,
                        (int)notificationCategory.Design);
                }
                    //inquiryWorkscopeRepository.Update(inquiryWorkscope);
                

                if (inquiry.InquiryStatusId == (int)inquiryStatus.quotationPending)
                {
                    inquiry.InquiryStatusId =
                        Helper.ConvertToDateTime(inquiry.QuotationScheduleDate) <
                        Helper.ConvertToDateTime(Helper.GetDateTime())
                            ? (int)inquiryStatus.quotationDelayed
                            : (int)inquiryStatus.quotationPending;

                    if (inquiry.InquiryStatusId == (int)inquiryStatus.quotationDelayed)
                    {
                        var user = userRepository.FindByCondition(x =>
                        x.UserId == inquiry.ManagedBy &&
                        x.IsActive == true && x.IsDeleted == false).Select(y => new
                        {
                            Name = y.UserName
                        }).FirstOrDefault();
                        Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.quotationDelayed);
                        Helper.Each(inquiry.Quotations, x => x.QuotationStatusId = (int)inquiryStatus.quotationDelayed);
                        sendNotificationToHead(user.Name + Constants.QuotationDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                    "" + inquiry.CustomerId + "" + inquiry.InquiryId, true,
                            null,
                            null,
                            roletypeId, (int)inquiry.BranchId,
                            (int)notificationCategory.Quotation);

                        sendNotificationToOneUser(user.Name + Constants.QuotationDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                    "" + inquiry.CustomerId + "" + inquiry.InquiryId, false, null, null,
                            (int)inquiry.ManagedBy, (int)inquiry.BranchId,
                            (int)notificationCategory.Quotation);
                    }
                }

                if (inquiry.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress)
                {
                    var job = jobOrderRepository.FindByCondition(x => x.InquiryId == inquiry.InquiryId && x.IsActive == true && x.IsDeleted == false)
                        .Include(x => x.JobOrderDetails.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
                    if (Helper.ConvertToDateTime(job.JobOrderDetails.FirstOrDefault().InstallationStartDate) <
                        Helper.ConvertToDateTime(Helper.GetDateTime()))
                    {
                        inquiry.InquiryStatusId = (int)inquiryStatus.jobOrderDelayed;
                        Helper.Each(inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.jobOrderDelayed);


                        sendNotificationToHead(Constants.JobOrderDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                    "" + inquiry.CustomerId + "" + inquiry.InquiryId, true,
                            null,
                            null,
                            roletypeId, (int)inquiry.BranchId,
                            (int)notificationCategory.JobOrder);

                        sendNotificationToHead(Constants.JobOrderDelayed + " of Inquiry Code: IN" + inquiry.BranchId +
                                                    "" + inquiry.CustomerId + "" + inquiry.InquiryId, true,
                            null,
                            null,
                            roletypeId, (int)job.FactoryId,
                            (int)notificationCategory.JobOrder);
                    }
                }

                inquiryRepository.Update(inquiry);
            }

            context.SaveChanges();
        }

        [AuthFilter((int)permission.ManageInquiry, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x =>
                    x.InquiryId == updateInquirySchedule.InquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes).Include(x => x.ManagedByNavigation.UserRoles)
                .Include(x => x.ManagedByNavigation.UserRoles).FirstOrDefault();
            if (inquiry != null)
            {
                //inquiry.InquiryWorkscopes.AsQueryable<InquiryWorkscope>().Where(x => x.IsActive == true && x.IsDeleted == false).ForEachAsync((x) => { x.DesignAssignedTo = updateMeasurementSchedule.MeasurementAssignedTo; x.MeasurementScheduleDate = updateMeasurementSchedule.MeasurementScheduleDate; x.InquiryStatusId = updateMeasurementSchedule.InquiryStatusId; });
                if (inquiry.InquiryWorkscopes.FirstOrDefault().InquiryStatusId <= 2)
                {
                    updateInquirySchedule.InquiryStatusId =
                        Helper.ConvertToDateTime(updateInquirySchedule.MeasurementScheduleDate) <
                        Helper.ConvertToDateTime(Helper.GetDateTime())
                            ? 2
                            : 1;
                }
                else if (updateInquirySchedule.DesignScheduleDate != null &&
                         inquiry.InquiryWorkscopes.FirstOrDefault().InquiryStatusId <= 4)
                {
                    updateInquirySchedule.InquiryStatusId =
                        Helper.ConvertToDateTime(updateInquirySchedule.DesignScheduleDate) <
                        Helper.ConvertToDateTime(Helper.GetDateTime())
                            ? 4
                            : 3;
                }

                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.MeasurementAssignedTo = updateInquirySchedule.MeasurementAssignedTo;
                    inquiryWorkscope.MeasurementScheduleDate = updateInquirySchedule.MeasurementScheduleDate;
                    inquiryWorkscope.InquiryStatusId = updateInquirySchedule.InquiryStatusId;
                    inquiryWorkscope.DesignAssignedTo = updateInquirySchedule.DesignAssignedTo;
                    inquiryWorkscope.DesignScheduleDate = updateInquirySchedule.DesignScheduleDate;
                }

                inquiryRepository.Update(inquiry);
                context.SaveChanges();
                response.data = inquiry;
                try
                {
                    List<UserRole> userRoles = userRoleRepository
                        .FindByCondition(x =>
                            inquiry.ManagedByNavigation.UserRoles.Where(y => y.UserRoleId == x.UserRoleId).Any() &&
                            x.IsActive == true && x.IsDeleted == false).Include(x => x.BranchRole)
                        .Include(x => x.BranchRole.RoleHeads).ToList();
                    List<int?> roleHeadsId = userRoles.FirstOrDefault().BranchRole.RoleHeads
                        .Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.HeadRoleId).ToList();
                    List<int?> roleTypeId = branchRoleRepository
                        .FindByCondition(x =>
                            roleHeadsId.Contains(x.BranchRoleId) && x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.RoleTypeId).ToList();
                    sendNotificationToHead(
                        inquiry.Customer.CustomerName + Constants.measurementRescheduleBranchMessage +
                        inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " +
                        inquiry.Customer.CustomerName, null, roleTypeId, inquiry.BranchId,
                        (int)notificationCategory.Measurement);
                }
                catch (Exception ex)
                {
                    Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "No Inquiry Exist";
            }

            return response;
        }

        //[AuthFilter((int)permission.ManageInquiry, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object UpdateAssignMeasurement(UpdateInquirySchedule updateInquiry)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == updateInquiry.InquiryId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId != (int)inquiryStatus.designAccepted ||
                     x.InquiryStatusId != (int)inquiryStatus.designAssigneeAccepted ||
                     x.InquiryStatusId != (int)inquiryStatus.designAssigneePending ||
                     x.InquiryStatusId != (int)inquiryStatus.designAssigneeRejected ||
                     x.InquiryStatusId != (int)inquiryStatus.designDelayed ||
                     x.InquiryStatusId != (int)inquiryStatus.designPending ||
                     x.InquiryStatusId != (int)inquiryStatus.designRejected ||
                     x.InquiryStatusId != (int)inquiryStatus.designRejectedByCustomer ||
                     x.InquiryStatusId != (int)inquiryStatus.designWaitingForApproval ||
                     x.InquiryStatusId != (int)inquiryStatus.designWaitingForCustomerApproval))
                .Include(x => x.InquiryWorkscopes).Include(x => x.Customer).FirstOrDefault();
            if (inquiry != null)
            {
                foreach (InquiryWorkscope inworkscope in inquiry.InquiryWorkscopes)
                {
                    inworkscope.InquiryStatusId = (int)inquiryStatus.measurementAssigneePending;
                    inworkscope.MeasurementAssignedTo = updateInquiry.MeasurementAssignedTo;
                    inworkscope.MeasurementScheduleDate = updateInquiry.MeasurementScheduleDate;
                }

                inquiry.InquiryStatusId = (int)inquiryStatus.measurementAssigneePending;
                inquiry.IsMeasurementProvidedByCustomer = updateInquiry.IsProvidedByCustomer;
                inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
                inquiryRepository.Update(inquiry);

                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };
                try
                {
                    sendNotificationToOneUser(
                        Constants.measurementAssign +
                        inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate + " Of Inquiry Code:" +
                        inquiry.InquiryCode, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " +
                        inquiry.Customer.CustomerName, null,
                        (int)inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo, (int)inquiry.BranchId,
                        (int)notificationCategory.Measurement);
                    sendNotificationToHead(
                        inquiry.Customer.CustomerName + Constants.measurementRescheduleBranchMessage +
                        inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate + " Of Inquiry Code:" +
                        inquiry.InquiryCode, false,
                        " Of IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId + " For " +
                        inquiry.Customer.CustomerName, null, roletypeId, inquiry.BranchId,
                        (int)notificationCategory.Measurement);
                }
                catch (Exception ex)
                {
                    Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
                }

                context.SaveChanges();
                response.data = inquiry;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry Not Exist";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object UpdateAssignDesign(UpdateInquirySchedule updateInquiry)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                    x.InquiryId == updateInquiry.InquiryId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId == (int)inquiryStatus.designAssigneePending ||
                     x.InquiryStatusId == (int)inquiryStatus.designAssigneeRejected ||
                     x.InquiryStatusId == (int)inquiryStatus.designDelayed ||
                     x.InquiryStatusId == (int)inquiryStatus.designPending ||
                     x.InquiryStatusId == (int)inquiryStatus.designRejected ||
                     x.InquiryStatusId == (int)inquiryStatus.designRevisionRequested ||
                     x.InquiryStatusId == (int)inquiryStatus.designRejectedByCustomer))
                .Include(x => x.InquiryWorkscopes).Include(x => x.Customer).FirstOrDefault();
            //var inquiryWorkscope = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryWorkscopeId == updateInquiry.InquiryWorkscopeId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId != (int)inquiryStatus.measurementAccepted || x.InquiryStatusId != (int)inquiryStatus.measurementAssigneeAccepted || x.InquiryStatusId != (int)inquiryStatus.measurementAssigneePending || x.InquiryStatusId != (int)inquiryStatus.measurementAssigneeRejected || x.InquiryStatusId != (int)inquiryStatus.measurementdelayed || x.InquiryStatusId != (int)inquiryStatus.measurementPending || x.InquiryStatusId != (int)inquiryStatus.measurementRejected || x.InquiryStatusId != (int)inquiryStatus.measurementWaitingForApproval))
            //    .Include(x => x.Inquiry)
            //    .ThenInclude(y => y.Customer).FirstOrDefault();
            if (inquiry != null)
            {
                inquiry.InquiryStatusId = (int)inquiryStatus.designAssigneePending;
                foreach (InquiryWorkscope inquiryWorkscope in inquiry.InquiryWorkscopes)
                {
                    inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designAssigneePending;
                    inquiryWorkscope.DesignAssignedTo = updateInquiry.DesignAssignedTo;
                    inquiryWorkscope.DesignScheduleDate = updateInquiry.DesignScheduleDate;
                    // inquiryWorkscope.Inquiry.InquiryStatusId = (int)inquiryStatus.designAssigneePending;
                    inquiryWorkscope.Inquiry.IsDesignProvidedByCustomer = updateInquiry.IsProvidedByCustomer;
                }

                inquiryRepository.Update(inquiry);

                List<int?> roletypeId = new List<int?>
                {
                    (int)roleType.Manager
                };
                try
                {
                    sendNotificationToOneUser(
                        Constants.DesignAssign + inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate +
                        " Of Inquiry Code:" + inquiry.InquiryCode, false, null, null,
                        (int)inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo, Constants.branchId,
                        (int)notificationCategory.Measurement);
                    sendNotificationToHead(
                        inquiry.Customer.CustomerName + Constants.designRescheduleBranchMessage +
                        inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate + " Of Inquiry Code:" +
                        inquiry.InquiryCode, false, null, null, roletypeId, Constants.branchId,
                        (int)notificationCategory.Measurement);
                }
                catch (Exception ex)
                {
                    Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
                }

                context.SaveChanges();
                response.data = inquiry;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Inquiry Not Exist";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> EditFiles(EditFiles files)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == files.inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)).ThenInclude(x => x.Measurements.Where(x => x.IsActive == true && x.IsDeleted == false)).ThenInclude(x => x.Files.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)).ThenInclude(x => x.Designs.Where(x => x.IsActive == true && x.IsDeleted == false)).ThenInclude(x => x.Files.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.Quotations.Where(x => x.IsActive == true && x.IsDeleted == false)).ThenInclude(x => x.Files.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.JobOrders.Where(x => x.IsActive == true && x.IsDeleted == false)).ThenInclude(x => x.JobOrderDetails.Where(x => x.IsActive == true && x.IsDeleted == false))
                .Include(x => x.Payments.Where(x => x.IsActive == true && x.IsDeleted == false)).ThenInclude(x => x.Files.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                response.data = "";
                if ((files.Measurement != null && files.Measurement.Any()) || (files.Desgin != null && files.Desgin.Any()))
                {
                    foreach (var inworscope in inquiry.InquiryWorkscopes)
                    {
                        if (files.Measurement != null && files.Measurement.Any())
                        {
                            foreach (var measurement in inworscope.Measurements)
                            {
                                foreach (var file in measurement.Files)
                                {
                                    file.IsActive = false;
                                    try
                                    {
                                        await Helper.DeleteFile(file.FileUrl);
                                    }
                                    catch (Exception e)
                                    {
                                        Sentry.SentrySdk.CaptureMessage(e.Message);
                                    }
                                }
                                foreach (string fileUrl in files.Measurement)
                                {


                                    if (fileUrl != null)
                                    {
                                        measurement.Files.Add(new BackendSaiKitchen.Models.File
                                        {
                                            FileUrl = fileUrl,
                                            FileName = fileUrl.Split('.')[0],
                                            FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                            IsImage = fileUrl.Split('.').Length > 1,
                                            IsActive = true,
                                            IsDeleted = false,
                                            UpdatedBy = Constants.userId,
                                            UpdatedDate = Helper.GetDateTime(),
                                            CreatedBy = Constants.userId,
                                            CreatedDate = Helper.GetDateTime(),

                                        });
                                        response.data += fileUrl + " Added to Measurement \n";
                                    }
                                    else
                                    {
                                        response.isError = true;
                                        response.errorMessage = Constants.wrongFileUpload;
                                    }

                                }
                            }
                        }
                        if (files.Desgin != null && files.Desgin.Any())
                        {
                            foreach (var design in inworscope.Designs)
                            {
                                foreach (var file in design.Files)
                                {
                                    file.IsActive = false;
                                    try
                                    {
                                        await Helper.DeleteFile(file.FileUrl);
                                    }
                                    catch (Exception e)
                                    {
                                        Sentry.SentrySdk.CaptureMessage(e.Message);
                                    }
                                }
                                foreach (string fileUrl in files.Desgin)
                                {


                                    if (fileUrl != null)
                                    {
                                        design.Files.Add(new BackendSaiKitchen.Models.File
                                        {
                                            FileUrl = fileUrl,
                                            FileName = fileUrl.Split('.')[0],
                                            FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                            IsImage = fileUrl.Split('.').Length > 1,
                                            IsActive = true,
                                            IsDeleted = false,
                                            UpdatedBy = Constants.userId,
                                            UpdatedDate = Helper.GetDateTime(),
                                            CreatedBy = Constants.userId,
                                            CreatedDate = Helper.GetDateTime(),

                                        });
                                        response.data += fileUrl + "Added to Design \n";
                                    }
                                    else
                                    {
                                        response.isError = true;
                                        response.errorMessage = Constants.wrongFileUpload;
                                    }

                                }
                            }
                        }

                    }
                }
                if ((files.Quotation != null && files.Quotation.Any()) || (files.CalculationSheetFile != null && files.CalculationSheetFile != string.Empty))
                {
                    foreach (var quotation in inquiry.Quotations)
                    {
                        if (files.CalculationSheetFile != null && files.CalculationSheetFile != string.Empty)
                        {
                            try
                            {
                                await Helper.DeleteFile(quotation.CalculationSheetFile);
                            }
                            catch (Exception e)
                            {
                                Sentry.SentrySdk.CaptureMessage(e.Message);
                            }
                            quotation.CalculationSheetFile = files.CalculationSheetFile;
                            response.data += files.CalculationSheetFile + " Added to Calculation Sheet File in Quotation \n";
                        }
                        if (files.Quotation != null && files.Quotation.Any())
                        {
                            foreach (var file in quotation.Files)
                            {
                                file.IsActive = false;
                                try
                                {
                                    await Helper.DeleteFile(file.FileUrl);
                                }
                                catch (Exception e)
                                {
                                    Sentry.SentrySdk.CaptureMessage(e.Message);
                                }
                                
                            }
                        }
                        foreach (string fileUrl in files.Quotation)
                        {


                            if (fileUrl != null)
                            {
                                quotation.Files.Add(new BackendSaiKitchen.Models.File
                                {
                                    FileUrl = fileUrl,
                                    FileName = fileUrl.Split('.')[0],
                                    FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                    IsImage = fileUrl.Split('.').Length > 1,
                                    IsActive = true,
                                    IsDeleted = false,
                                    UpdatedBy = Constants.userId,
                                    UpdatedDate = Helper.GetDateTime(),
                                    CreatedBy = Constants.userId,
                                    CreatedDate = Helper.GetDateTime(),

                                });
                                response.data += fileUrl + " Added to Quotation \n";
                            }
                            else
                            {
                                response.isError = true;
                                response.errorMessage = Constants.wrongFileUpload;
                            }

                        }
                    }
                }
                foreach (var job in inquiry.JobOrders)
                {
                    if (files.DetailedDesignFile != null && files.DetailedDesignFile != string.Empty)
                    {
                        try
                        {
                            if (job.DetailedDesignFile != null && job.DetailedDesignFile != string.Empty)
                            {
                                await Helper.DeleteFile(job.DetailedDesignFile);
                            }
                        }
                        catch (Exception e)
                        {
                            Sentry.SentrySdk.CaptureMessage(e.Message);
                        }
                        job.DetailedDesignFile = files.DetailedDesignFile;
                        response.data += files.DetailedDesignFile + " Added to Detailed Design File in Job Order \n";
                    }
                    if (files.MatrialSheet != null && files.MatrialSheet != string.Empty)
                    {
                        try
                        {
                            if (job.MaterialSheetFileUrl != null && job.MaterialSheetFileUrl != string.Empty)
                            {
                                await Helper.DeleteFile(job.MaterialSheetFileUrl);
                            }
                        }
                        catch (Exception e)
                        {
                            Sentry.SentrySdk.CaptureMessage(e.Message);
                        }
                        job.MaterialSheetFileUrl = files.MatrialSheet;
                        response.data += files.MatrialSheet + " Added to Material Sheet File in Job Order \n";
                    }
                    if (files.MEPDrawing != null && files.MEPDrawing != string.Empty)
                    {
                        try
                        {
                            if (job.MepdrawingFileUrl != null && job.MepdrawingFileUrl != string.Empty)
                            {
                                await Helper.DeleteFile(job.MepdrawingFileUrl);
                            }
                        }
                        catch (Exception e)
                        {
                            Sentry.SentrySdk.CaptureMessage(e.Message);
                        }
                        job.MepdrawingFileUrl = files.MEPDrawing;
                        response.data += files.MEPDrawing + " Added to Mepdrawing File in Job Order \n";
                    }
                    if (files.JobOrderChecklist != null && files.JobOrderChecklist != string.Empty)
                    {
                        try
                        {
                            if (job.JobOrderChecklistFileUrl != null && job.JobOrderChecklistFileUrl != string.Empty)
                            {
                                await Helper.DeleteFile(job.JobOrderChecklistFileUrl);
                            }
                        }
                        catch (Exception e)
                        {
                            Sentry.SentrySdk.CaptureMessage(e.Message);
                        }
                        job.JobOrderChecklistFileUrl = files.JobOrderChecklist;
                        response.data += files.JobOrderChecklist + " Added to Job Order Checklist File in Job Order \n";
                    }
                    if (files.DataSheetAppliance != null && files.DataSheetAppliance != string.Empty)
                    {
                        try
                        {
                            if (job.DataSheetApplianceFileUrl != null && job.DataSheetApplianceFileUrl != string.Empty)
                            {
                                await Helper.DeleteFile(job.DataSheetApplianceFileUrl);
                            }
                        }
                        catch (Exception e)
                        {
                            Sentry.SentrySdk.CaptureMessage(e.Message);
                        }
                        job.DataSheetApplianceFileUrl = files.DataSheetAppliance;
                        response.data += files.DataSheetAppliance + " Added to Data Sheet Appliance File in Job Order \n";
                    }
                }
                foreach (var payment in inquiry.Payments)
                {
                    if (payment.PaymentTypeId == (int)paymenttype.AdvancePayment)
                    {
                        if (files.AdvancePayment != null && files.AdvancePayment.Any())
                        {
                            foreach (var file in payment.Files)
                            {
                                try
                                {
                                    await Helper.DeleteFile(file.FileUrl);
                                }
                                catch (Exception e)
                                {
                                    Sentry.SentrySdk.CaptureMessage(e.Message);
                                }
                                file.IsActive = false;
                            }
                            foreach (string fileUrl in files.AdvancePayment)
                            {


                                if (fileUrl != null)
                                {
                                    payment.Files.Add(new BackendSaiKitchen.Models.File
                                    {
                                        FileUrl = fileUrl,
                                        FileName = fileUrl.Split('.')[0],
                                        FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                        IsImage = fileUrl.Split('.').Length > 1,
                                        IsActive = true,
                                        IsDeleted = false,
                                        UpdatedBy = Constants.userId,
                                        UpdatedDate = Helper.GetDateTime(),
                                        CreatedBy = Constants.userId,
                                        CreatedDate = Helper.GetDateTime(),

                                    });
                                    response.data += fileUrl + " Added to Advance Payment in Payments \n";
                                }
                                else
                                {
                                    response.isError = true;
                                    response.errorMessage = Constants.wrongFileUpload;
                                }

                            }
                        }
                    } 
                    if (payment.PaymentTypeId == (int)paymenttype.BeforeInstallation)
                    {
                        if (files.BeforeInstalltionPayment != null && files.BeforeInstalltionPayment.Any())
                        {
                            foreach (var file in payment.Files)
                            {
                                try
                                {
                                    await Helper.DeleteFile(file.FileUrl);
                                }
                                catch (Exception e)
                                {
                                    Sentry.SentrySdk.CaptureMessage(e.Message);
                                }
                                file.IsActive = false;
                            }
                            foreach (string fileUrl in files.BeforeInstalltionPayment)
                            {
                                if (fileUrl != null)
                                {
                                    payment.Files.Add(new BackendSaiKitchen.Models.File
                                    {
                                        FileUrl = fileUrl,
                                        FileName = fileUrl.Split('.')[0],
                                        FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                        IsImage = fileUrl.Split('.').Length > 1,
                                        IsActive = true,
                                        IsDeleted = false,
                                        UpdatedBy = Constants.userId,
                                        UpdatedDate = Helper.GetDateTime(),
                                        CreatedBy = Constants.userId,
                                        CreatedDate = Helper.GetDateTime(),

                                    });
                                    response.data += fileUrl + " Added to Before Installation Payment in Payments \n";
                                }
                                else
                                {
                                    response.isError = true;
                                    response.errorMessage = Constants.wrongFileUpload;
                                }
                            }
                        }
                    }
                    if (payment.PaymentTypeId == (int)paymenttype.AfterDelivery)
                    {
                        if (files.AfterDelieveryPayment != null && files.AfterDelieveryPayment.Any())
                        {
                            foreach (var file in payment.Files)
                            {
                                try
                                {
                                    await Helper.DeleteFile(file.FileUrl);
                                }
                                catch (Exception e)
                                {
                                    Sentry.SentrySdk.CaptureMessage(e.Message);
                                }
                                file.IsActive = false;
                            }
                            foreach (string fileUrl in files.AfterDelieveryPayment)
                            {


                                if (fileUrl != null)
                                {
                                    payment.Files.Add(new BackendSaiKitchen.Models.File
                                    {
                                        FileUrl = fileUrl,
                                        FileName = fileUrl.Split('.')[0],
                                        FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                        IsImage = fileUrl.Split('.').Length > 1,
                                        IsActive = true,
                                        IsDeleted = false,
                                        UpdatedBy = Constants.userId,
                                        UpdatedDate = Helper.GetDateTime(),
                                        CreatedBy = Constants.userId,
                                        CreatedDate = Helper.GetDateTime(),

                                    });
                                    response.data += fileUrl + " Added to After Delievery Payment in Payments \n";
                                }
                                else
                                {
                                    response.isError = true;
                                    response.errorMessage = Constants.wrongFileUpload;
                                }

                            }
                        }
                    }
                    //if (payment.PaymentTypeId == (int)paymenttype.Installment)
                    //{
                    //    if (files.InstallmentPayment != null && files.InstallmentPayment.Any())
                    //    {
                    //        foreach (var file in payment.Files)
                    //        {
                    //            try
                    //            {
                    //                await Helper.DeleteFile(file.FileUrl);
                    //            }
                    //            catch (Exception e)
                    //            {
                    //                Sentry.SentrySdk.CaptureMessage(e.Message);
                    //            }
                    //            file.IsActive = false;
                    //        }
                    //        foreach (string fileUrl in files.InstallmentPayment)
                    //        {


                    //            if (fileUrl != null)
                    //            {
                    //                payment.Files.Add(new BackendSaiKitchen.Models.File
                    //                {
                    //                    FileUrl = fileUrl,
                    //                    FileName = fileUrl.Split('.')[0],
                    //                    FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                    //                    IsImage = fileUrl.Split('.').Length > 1,
                    //                    IsActive = true,
                    //                    IsDeleted = false,
                    //                    UpdatedBy = Constants.userId,
                    //                    UpdatedDate = Helper.GetDateTime(),
                    //                    CreatedBy = Constants.userId,
                    //                    CreatedDate = Helper.GetDateTime(),

                    //                });
                    //                response.data += fileUrl + " Added to Installment Payment in Payments \n";
                    //            }
                    //            else
                    //            {
                    //                response.isError = true;
                    //                response.errorMessage = Constants.wrongFileUpload;
                    //            }

                    //        }
                    //    }
                    //}
                }
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetCountByBranchId(int branchId)
        {
            var Customers = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.Branch.IsActive == true
            && x.Branch.IsDeleted == false && (x.BranchId == branchId || string.IsNullOrEmpty(x.BranchId.ToString()))).Count();
            var Branches = branchRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Count();
            var BranchRoles = branchRoleRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Count();
            var Workscopes = workScopeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Count();
            var Promos = promoRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Count();
            var userList = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Count();
            var JoborderStatus = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && (x.BranchId == branchId || x.JobOrders.Any(y => y.IsActive == true && y.IsDeleted == false && y.FactoryId == branchId)) && (x.InquiryStatusId == (int)inquiryStatus.jobOrderInProgress || x.InquiryStatusId == (int)inquiryStatus.jobOrderDelayed || x.InquiryStatusId == (int)inquiryStatus.jobOrderCompleted)).Count();
            var JobOrderApprovals = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && (x.BranchId == branchId || x.JobOrders.Any(y => y.IsActive == true && y.IsDeleted == false && y.FactoryId == branchId)) && (x.InquiryStatusId == (int)inquiryStatus.jobOrderConfirmationPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditRejected)).Count();
            var JobOrderAudit = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.jobOrderAuditPending && (x.BranchId == branchId || x.JobOrders.Any(y => y.IsActive == true && y.IsDeleted == false && y.FactoryId == branchId))).Count();
            var Numbers = branchRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == branchId).Select(x => new
            {
                inquiriesCount = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false),
                customers =Customers,
                measurementAssinee = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.MeasurementAssignedTo == Constants.userId)),
                measurements = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.measurementInProgress || x.InquiryStatusId == (int)inquiryStatus.measurementRejected || x.InquiryStatusId == (int)inquiryStatus.measurementdelayed) && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.MeasurementAssignedTo == Constants.userId)),
                measurementApprovals = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.measurementWaitingForApproval && x.ManagedBy == Constants.userId)),
                designAssigne = x.Inquiries.Count(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.designAssigneePending && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.DesignAssignedTo == Constants.userId)),
                designs = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.designPending || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.designRejected || x.InquiryStatusId == (int)inquiryStatus.designRevisionRequested) && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.DesignAssignedTo == Constants.userId)),
                designApprovals = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.designWaitingForApproval || x.InquiryStatusId == (int)inquiryStatus.designRejectedByCustomer) && x.ManagedBy == Constants.userId),
                quotationAssign = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationSchedulePending) && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.DesignAssignedTo == Constants.userId)),
                quotations = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationPending || x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationRevisionRequested) && x.ManagedBy == Constants.userId),
                quotationApprovals = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForApproval && x.ManagedBy == Constants.userId),
                uploadcontract = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.contractInProgress || x.InquiryStatusId == (int)inquiryStatus.contractRejected) && x.ManagedBy == Constants.userId),
                technicalChecklist = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.checklistPending || x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected) && x.ManagedBy == Constants.userId),
                commericalChecklist = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected || x.InquiryStatusId == (int)inquiryStatus.specialApprovalRejected)),
                specialApprovals = x.Inquiries.Count(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.specialApprovalPending) && x.ManagedBy == Constants.userId),
                joborderAudit = JobOrderAudit,
                joborderStatus = JoborderStatus,
                joborderApprovals = JobOrderApprovals,
                users = userList,
                branches = Branches,
                branchroles = BranchRoles,
                workscopes = Workscopes,
                promos = Promos
            });
            response.data = Numbers;
            return response;
        }

        #region workscope

        [AuthFilter((int)permission.ManageWorkscope, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetAllWorkscope()
        {
            return workScopeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
        }

        [AuthFilter((int)permission.ManageWorkscope, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public object GetWorkscopeById(int workScopeId)
        {
            response.data = workScopeRepository
                .FindByCondition(x => x.WorkScopeId == workScopeId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            if (response.data == null)
            {
                response.isError = true;
                response.errorMessage = "WorkScope doesn't Exist";
            }

            return response;
        }

        [AuthFilter((int)permission.ManageWorkscope, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object AddWorkscope(WorkScope workscope)
        {
            if (workscope.WorkScopeId == 0)
            {
                workScopeRepository.Create(workscope);
                context.SaveChanges();
            }
            else
            {
                WorkScope oldworkScope = workScopeRepository.FindByCondition(x =>
                        x.WorkScopeId == workscope.WorkScopeId && x.IsActive == true && x.IsDeleted == false)
                    .FirstOrDefault();
                if (oldworkScope != null)
                {
                    oldworkScope.WorkScopeName = workscope.WorkScopeName;
                    oldworkScope.WorkScopeDescription = workscope.WorkScopeDescription;
                    oldworkScope.QuestionaireType = workscope.QuestionaireType;
                    workScopeRepository.Update(oldworkScope);
                    context.SaveChanges();
                    response.data = oldworkScope;
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "WorkScope doesn't exist";
                }
            }

            return response;
        }

        [AuthFilter((int)permission.ManageWorkscope, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object EditWorkscope(WorkScope workscope)
        {
            WorkScope oldworkScope = workScopeRepository.FindByCondition(x =>
                x.WorkScopeId == workscope.WorkScopeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldworkScope != null)
            {
                oldworkScope.WorkScopeName = workscope.WorkScopeName;
                oldworkScope.WorkScopeDescription = workscope.WorkScopeDescription;
                oldworkScope.QuestionaireType = workscope.QuestionaireType;
                workScopeRepository.Update(oldworkScope);
                context.SaveChanges();
                response.data = oldworkScope;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "WorkScope doesnt exist";
            }

            return response;
        }

        [AuthFilter((int)permission.ManageWorkscope, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object DeleteWorkscope(int workScopeId)
        {
            WorkScope oldworkScope = workScopeRepository
                .FindByCondition(x => x.WorkScopeId == workScopeId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            if (oldworkScope != null)
            {
                workScopeRepository.Delete(oldworkScope);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "WorkScope doesnt exist";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetWorkscopesByinquiryId(int inquiryId)
        {
            var inquiryworkscope = inquiryWorkscopeRepository.FindByCondition(x =>
                    x.IsActive == true && x.IsDeleted == false && x.InquiryId == inquiryId)
                .Select(x => new
                {
                    x.Workscope.WorkScopeName,
                    x.InquiryWorkscopeId
                });
            if (inquiryworkscope != null)
            {
                response.data = inquiryworkscope;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Not Found";
            }

            return response;
        }

        #endregion


        #region Measurement

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetMeasurementOfBranch(int branchId)
        {
            //var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.MeasurementAssignedTo == Constants.userId && x.Inquiry.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.measurementPending || x.InquiryStatusId == (int)inquiryStatus.measurementdelayed || x.InquiryStatusId == (int)inquiryStatus.measurementRejected) && x.IsActive == true && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false
            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                x.BranchId == branchId &&
                (x.InquiryStatusId == (int)inquiryStatus.measurementInProgress ||
                 x.InquiryStatusId == (int)inquiryStatus.measurementdelayed ||
                 x.InquiryStatusId == (int)inquiryStatus.measurementRejected) && x.IsActive == true &&
                x.IsDeleted == false
                && x.InquiryWorkscopes.Any(y =>
                    y.IsActive == true && y.IsDeleted == false && y.MeasurementAssignedTo == Constants.userId)).Select(
                x => new ViewInquiryDetail
                {
                    //InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.GetDateFromString(x.InquiryStartDate),
                    MeasurementAssignTo = x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    QuestionaireType = x.InquiryWorkscopes.FirstOrDefault().Workscope.QuestionaireType,
                    DesignScheduleDate = x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate,
                    DesignAssignTo = x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer = x.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer = x.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate = x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress,
                    BuildingCondition = x.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor,
                    BuildingReconstruction = (bool)x.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied = (bool)x.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate = Helper.GetDateFromString(x.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested,
                    CustomerId = x.CustomerId,
                    CustomerCode = "CS" + x.BranchId + "" + x.CustomerId,
                    CustomerName = x.Customer.CustomerName,
                    CustomerContact = x.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp,
                    CustomerEmail = x.Customer.CustomerEmail,
                    BranchId = x.BranchId,
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName).ToList(),
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements.Where(y => y.IsDeleted == false)
                        .Count(),
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetMeasurementAssigneeApproval(int branchId)
        {
            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                x.BranchId == branchId && x.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending &&
                x.IsActive == true && x.IsActive == true
                && x.InquiryWorkscopes.Any(y =>
                    y.IsActive == true && y.IsDeleted == false && y.MeasurementAssignedTo == Constants.userId)).Select(
                x => new ViewInquiryDetail
                {
                    //InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.GetDateFromString(x.InquiryStartDate),
                    MeasurementAssignTo = x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    QuestionaireType = x.InquiryWorkscopes.FirstOrDefault().Workscope.QuestionaireType,
                    DesignScheduleDate = x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate,
                    DesignAssignTo = x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer = x.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer = x.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate = x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress,
                    BuildingCondition = x.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor,
                    BuildingReconstruction = (bool)x.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied = (bool)x.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate = Helper.GetDateFromString(x.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested,
                    CustomerId = x.CustomerId,
                    CustomerCode = "CS" + x.BranchId + "" + x.CustomerId,
                    CustomerName = x.Customer.CustomerName,
                    CustomerContact = x.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp,
                    CustomerEmail = x.Customer.CustomerEmail,
                    BranchId = x.BranchId,
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName).ToList(),
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements.Where(y => y.IsDeleted == false)
                        .Count(),
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> UpdateMeasurementDetailsAsync(Inquiry inquiry)
        {
            inquiry.InquiryStartDate = Helper.GetDateTime();
            Customer customer = customerRepository.FindByCondition(x =>
                x.CustomerContact == inquiry.Customer.CustomerContact && x.BranchId == inquiry.BranchId &&
                x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (customer != null)
            {
                customer.CustomerName = inquiry.Customer.CustomerName;
                customer.CustomerEmail = inquiry.Customer.CustomerEmail;
                customer.CustomerAddress = inquiry.Customer.CustomerAddress;
                customer.CustomerNationalId = inquiry.Customer.CustomerNationalId;
                customer.ContactStatusId = inquiry.Customer.ContactStatusId;
                customer.WayofContactId = inquiry.Customer.WayofContactId;
                customer.CustomerCountry = inquiry.Customer.CustomerCountry;
                customer.CustomerCity = inquiry.Customer.CustomerCity;
                customer.CustomerNationality = inquiry.Customer.CustomerNationality;
                customer.IsActive = true;
                customer.IsDeleted = false;
                customer.IsEscalationRequested = false;
                inquiry.Customer = customer;
            }
            else
            {
                try
                {
                    Customer anotherBranchCustomer = customerRepository.FindByCondition(x =>
                        x.CustomerContact == inquiry.Customer.CustomerContact && x.BranchId != inquiry.BranchId &&
                        x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    if (anotherBranchCustomer != null)
                    {
                        List<int?> roletypeId = new List<int?>
                        {
                            (int)roleType.Manager
                        };
                        sendNotificationToHead(
                            anotherBranchCustomer.CustomerName + Constants.inquiryOnAnotherBranchMessage, false, null,
                            null, roletypeId, anotherBranchCustomer.BranchId, (int)notificationCategory.Other);
                    }
                }

                catch (Exception ex)
                {
                    Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
                }
            }

            if (inquiry.Building.BuildingAddress == null || inquiry.Building.BuildingAddress == "")
            {
                inquiry.Building.BuildingAddress = customer.CustomerAddress;
            }

            if (inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo != null)
            {
                sendNotificationToOneUser(
                    Constants.measurementAssign + "" +
                    inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, false, null, null,
                    (int)inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo, (int)inquiry.BranchId,
                    (int)notificationCategory.Measurement);
            }

            inquiryRepository.Create(inquiry);
            context.SaveChanges();

            inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
            //inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserFcmtoken
            response.data = inquiry;
            try
            {
                await mailService.SendInquiryEmailAsync(inquiry.Customer.CustomerEmail, inquiry.InquiryCode,
                    inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate,
                    inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserName,
                    inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserMobile,
                    inquiry.Building.BuildingAddress);
            }

            catch (Exception ex)
            {
                Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex);
            }

            return response;
        }

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object AddMeaurementtoInquiry(Measurement measurement)
        {
            if (measurement.Files.Count > 0)
            {
                Guid obj = Guid.NewGuid();
                using (MemoryStream stream = new MemoryStream(measurement.Files.FirstOrDefault().FileImage))
                {
                    FileStream file = new FileStream(@"Assets/Images/" + obj, FileMode.Create, FileAccess.Write);
                    stream.WriteTo(file);
                    file.Close();
                    stream.Close();
                }

                response.data = measurement;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Kindly upload measurement file";
            }

            return response;
        }

        [AuthFilter((int)permission.ManageMeasurement, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetApprovalMeasurementOfBranch(int branchId)
        {
            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                    x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false &&
                    x.InquiryStatusId == (int)inquiryStatus.measurementWaitingForApproval &&
                    x.ManagedBy == Constants.userId)
                .Select(x => new ViewInquiryDetail
                {
                    // InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription, //x.Inquiry.InquiryDescription,
                    InquiryStartDate =
                        Helper.GetDateFromString(x
                            .InquiryStartDate), //Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                    MeasurementAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation
                            .UserName, //x.MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment, //x.Comments,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    DesignScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate, // x.DesignScheduleDate,
                    DesignAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation
                            .UserName, // x.DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer =
                        x.IsMeasurementProvidedByCustomer == true
                            ? "yes"
                            : "No", // x.Inquiry.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer =
                        x.IsDesignProvidedByCustomer == true
                            ? "Yes"
                            : "No", // x.Inquiry.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, // x.MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress, //x.Inquiry.Building.BuildingAddress,
                    BuildingMakaniMap = x.Building.BuildingMakaniMap, //x.Inquiry.Building.BuildingMakaniMap,
                    BuildingCondition = x.Building.BuildingCondition, // x.Inquiry.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor, //x.Inquiry.Building.BuildingFloor,
                    BuildingReconstruction =
                        (bool)x.Building.BuildingReconstruction
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied =
                        (bool)x.Building.IsOccupied
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate =
                        Helper.GetDateFromString(x
                            .InquiryEndDate), //Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit, // x.Inquiry.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested, //x.Inquiry.IsEscalationRequested,
                    CustomerId = x.CustomerId, // x.Inquiry.CustomerId,
                    CustomerCode =
                        "CS" + x.BranchId + "" + x.CustomerId, //"CS" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId,
                    CustomerName = x.Customer.CustomerName, // x.Inquiry.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail, //x.Inquiry.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact, //x.Inquiry.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp, //x.Inquiry.Customer.CustomerWhatsapp,
                    BranchId = x.BranchId, //x.Inquiry.BranchId,
                    InquiryAddedBy = x.ManagedByNavigation.UserName, //x.Inquiry.AddedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy, // x.Inquiry.AddedBy,
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements.Where(y => y.IsDeleted == false)
                        .Count(), //x.Measurements.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode =
                        "IN" + x.BranchId + "" + x.CustomerId + "" +
                        x.InquiryId, //"IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName)
                        .ToList(), // x.Inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList()
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                    //InquiryWorkscopes =x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).ToList()
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

        #endregion


        #region design

        [AuthFilter((int)permission.ManageDesign, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetDesignOfBranch(int branchId)
        {
            //var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.DesignAssignedTo == Constants.userId && x.Inquiry.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.designPending || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.designRejected) && x.IsActive == true && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false
            //var inquiries = inquiryRepository.FindByCondition(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.designPending || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.designRejected) && x. == Constants.userId)
            //.Select(x => new ViewInquiryDetail()


            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                x.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.designPending ||
                                           x.InquiryStatusId == (int)inquiryStatus.designDelayed ||
                                           x.InquiryStatusId == (int)inquiryStatus.designRejected ||
                                           x.InquiryStatusId == (int)inquiryStatus.designRevisionRequested) &&
                x.IsActive == true && x.IsDeleted == false
                && x.InquiryWorkscopes.Any(y =>
                    y.IsActive == true && y.IsDeleted == false && y.DesignAssignedTo == Constants.userId)).Select(x =>
                new ViewInquiryDetail
                {
                    // InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription, //x.Inquiry.InquiryDescription,
                    InquiryStartDate =
                        Helper.GetDateFromString(x
                            .InquiryStartDate), //Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                    MeasurementAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation
                            .UserName, //x.MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment, //x.Comments,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    DesignScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate, // x.DesignScheduleDate,
                    DesignAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation
                            .UserName, // x.DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer =
                        x.IsMeasurementProvidedByCustomer == true
                            ? "yes"
                            : "No", // x.Inquiry.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer =
                        x.IsDesignProvidedByCustomer == true
                            ? "Yes"
                            : "No", // x.Inquiry.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, // x.MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress, //x.Inquiry.Building.BuildingAddress,
                    BuildingMakaniMap = x.Building.BuildingMakaniMap, //x.Inquiry.Building.BuildingMakaniMap,
                    BuildingCondition = x.Building.BuildingCondition, // x.Inquiry.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor, //x.Inquiry.Building.BuildingFloor,
                    BuildingReconstruction =
                        (bool)x.Building.BuildingReconstruction
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied =
                        (bool)x.Building.IsOccupied
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate =
                        Helper.GetDateFromString(x
                            .InquiryEndDate), //Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit, // x.Inquiry.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested, //x.Inquiry.IsEscalationRequested,
                    CustomerId = x.CustomerId, // x.Inquiry.CustomerId,
                    CustomerCode =
                        "CS" + x.BranchId + "" + x.CustomerId, //"CS" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId,
                    CustomerName = x.Customer.CustomerName, // x.Inquiry.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail, //x.Inquiry.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact, //x.Inquiry.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp, //x.Inquiry.Customer.CustomerWhatsapp,
                    BranchId = x.BranchId, //x.Inquiry.BranchId,
                    InquiryAddedBy = x.ManagedByNavigation.UserName, //x.Inquiry.AddedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy, // x.Inquiry.AddedBy,
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements.Where(y => y.IsDeleted == false)
                        .Count(), //x.Measurements.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode =
                        "IN" + x.BranchId + "" + x.CustomerId + "" +
                        x.InquiryId, //"IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName)
                        .ToList(), // x.Inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList()
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                    //InquiryWorkscopes =x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).ToList()
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

        //[AuthFilter((int)permission.ManageDesign, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetDesignAssigneeApproval(int branchId)
        {
            //var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.DesignAssignedTo == Constants.userId && x.Inquiry.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.designAssigneePending) && x.IsActive == true && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false
            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                x.BranchId == branchId && x.InquiryStatusId == (int)inquiryStatus.designAssigneePending &&
                x.IsActive == true && x.IsActive == true
                && x.InquiryWorkscopes.Any(y =>
                    y.IsActive == true && y.IsDeleted == false && y.DesignAssignedTo == Constants.userId)).Select(x =>
                new ViewInquiryDetail
                {
                    //InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription,
                    InquiryStartDate = Helper.GetDateFromString(x.InquiryStartDate),
                    MeasurementAssignTo = x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    QuestionaireType = x.InquiryWorkscopes.FirstOrDefault().Workscope.QuestionaireType,
                    DesignScheduleDate = x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate,
                    DesignAssignTo = x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer = x.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer = x.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate = x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress,
                    BuildingCondition = x.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor,
                    BuildingReconstruction = (bool)x.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied = (bool)x.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate = Helper.GetDateFromString(x.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested,
                    CustomerId = x.CustomerId,
                    CustomerCode = "CS" + x.BranchId + "" + x.CustomerId,
                    CustomerName = x.Customer.CustomerName,
                    CustomerContact = x.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp,
                    CustomerEmail = x.Customer.CustomerEmail,
                    BranchId = x.BranchId,
                    InquiryCode = "IN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName).ToList(),
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements.Where(y => y.IsDeleted == false)
                        .Count(),
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

        [AuthFilter((int)permission.ManageDesign, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object AddDesigntoInquiry(Measurement measurement)
        {
            if (measurement.Files.Count > 0)
            {
                Guid obj = Guid.NewGuid();
                using (MemoryStream stream = new MemoryStream(measurement.Files.FirstOrDefault().FileImage))
                {
                    FileStream file = new FileStream(@"Assets/Images/" + obj, FileMode.Create, FileAccess.Write);
                    stream.WriteTo(file);
                    file.Close();
                    stream.Close();
                }

                response.data = measurement;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Kindly upload measurement file";
            }

            return response;
        }

        [AuthFilter((int)permission.ManageDesign, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetApprovalDesignOfBranch(int branchId)
        {
            IOrderedQueryable<ViewInquiryDetail> inquiries = inquiryRepository.FindByCondition(x =>
                    x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false &&
                    (x.InquiryStatusId == (int)inquiryStatus.designWaitingForApproval ||
                     x.InquiryStatusId == (int)inquiryStatus.designRejectedByCustomer) &&
                    x.ManagedBy == Constants.userId)
                .Select(x => new ViewInquiryDetail
                {
                    // InquiryWorkscopeId = x.InquiryWorkscopeId,
                    InquiryId = x.InquiryId,
                    InquiryDescription = x.InquiryDescription, //x.Inquiry.InquiryDescription,
                    InquiryStartDate =
                        Helper.GetDateFromString(x
                            .InquiryStartDate), //Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                    MeasurementAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation
                            .UserName, //x.MeasurementAssignedToNavigation.UserName,
                    InquiryComment = x.InquiryComment, //x.Comments,
                    //WorkScopeId = x.WorkscopeId,
                    //WorkScopeName = x.Workscope.WorkScopeName,
                    DesignScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().DesignScheduleDate, // x.DesignScheduleDate,
                    DesignAssignTo =
                        x.InquiryWorkscopes.FirstOrDefault().DesignAssignedToNavigation
                            .UserName, // x.DesignAssignedToNavigation.UserName,
                    Status = x.InquiryStatusId,
                    IsMeasurementProvidedByCustomer =
                        x.IsMeasurementProvidedByCustomer == true
                            ? "yes"
                            : "No", // x.Inquiry.IsMeasurementProvidedByCustomer == true ? "Yes" : "No",
                    IsDesignProvidedByCustomer =
                        x.IsDesignProvidedByCustomer == true
                            ? "Yes"
                            : "No", // x.Inquiry.IsDesignProvidedByCustomer == true ? "Yes" : "No",
                    MeasurementScheduleDate =
                        x.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, // x.MeasurementScheduleDate,
                    BuildingAddress = x.Building.BuildingAddress, //x.Inquiry.Building.BuildingAddress,
                    BuildingMakaniMap = x.Building.BuildingMakaniMap, //x.Inquiry.Building.BuildingMakaniMap,
                    BuildingCondition = x.Building.BuildingCondition, // x.Inquiry.Building.BuildingCondition,
                    BuildingFloor = x.Building.BuildingFloor, //x.Inquiry.Building.BuildingFloor,
                    BuildingReconstruction =
                        (bool)x.Building.BuildingReconstruction
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                    IsOccupied =
                        (bool)x.Building.IsOccupied
                            ? "Yes"
                            : "No", //(bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                    InquiryEndDate =
                        Helper.GetDateFromString(x
                            .InquiryEndDate), //Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                    BuildingTypeOfUnit = x.Building.BuildingTypeOfUnit, // x.Inquiry.Building.BuildingTypeOfUnit,
                    IsEscalationRequested = x.IsEscalationRequested, //x.Inquiry.IsEscalationRequested,
                    CustomerId = x.CustomerId, // x.Inquiry.CustomerId,
                    CustomerCode =
                        "CS" + x.BranchId + "" + x.CustomerId, //"CS" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId,
                    CustomerName = x.Customer.CustomerName, // x.Inquiry.Customer.CustomerName,
                    CustomerEmail = x.Customer.CustomerEmail, //x.Inquiry.Customer.CustomerEmail,
                    CustomerContact = x.Customer.CustomerContact, //x.Inquiry.Customer.CustomerContact,
                    CustomerWhatsapp = x.Customer.CustomerWhatsapp, //x.Inquiry.Customer.CustomerWhatsapp,
                    BranchId = x.BranchId, //x.Inquiry.BranchId,
                    InquiryAddedBy = x.ManagedByNavigation.UserName, //x.Inquiry.AddedByNavigation.UserName,
                    InquiryAddedById = x.ManagedBy, // x.Inquiry.AddedBy,
                    NoOfRevision = x.InquiryWorkscopes.FirstOrDefault().Measurements.Where(y => y.IsDeleted == false)
                        .Count(), //x.Measurements.Where(y => y.IsDeleted == false).Count(),
                    InquiryCode =
                        "IN" + x.BranchId + "" + x.CustomerId + "" +
                        x.InquiryId, //"IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                    WorkscopeNames = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.Workscope.WorkScopeName)
                        .ToList(), // x.Inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.Workscope.WorkScopeName).ToList()
                    CommentAddedOn = x.InquiryCommentsAddedOn,
                    DesignAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.DesignAddedOn).FirstOrDefault(),
                    MeasurementAddedOn = x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .Select(x => x.MeasurementAddedOn).FirstOrDefault(),
                    QuotationAddedOn = x.QuotationAddedOn
                    //InquiryWorkscopes =x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).ToList()
                }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }

        #endregion
    }
}