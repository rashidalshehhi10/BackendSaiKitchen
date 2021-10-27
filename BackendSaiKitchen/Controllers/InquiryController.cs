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
        public async Task<object> GetPagingInquiriesOfBranch(int branchId,[FromForm] int draw,[FromForm] int start,[FromForm] int length,[FromForm] int? inquiryId,[FromForm] string inquiryCode,[FromForm] int? status,[FromForm] string customerName,[FromForm] string workscopeNames, [FromForm] string measurementScheduleDate, [FromForm] int? measurementAssignTo, [FromForm] string? designScheduleDate, [FromForm] int? designAssignTo, [FromForm] string customerCode, [FromForm] string customerContact, [FromForm] string buildingAddress)
        {
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
                var _experssion = Expression.Equal(_property, constant);
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
            //if (customerCode != null)
            //{
            //    Expression _property = parameterExprission;
            //    foreach (var item in "Customer.CustomerCode".Split('.'))
            //    {
            //        _property = Expression.PropertyOrField(_property, item);
            //    }
            //    constant = Expression.Constant(customerCode);
            //    var _experssion = Expression.Equal(_property, constant);
            //    expression = Expression.And(expression, _experssion);
            //}
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
                }).OrderByDescending(x => x.InquiryId).Skip(start).Take(length)
                .ToListAsync();
            tableResponse.data = inquiries;
            tableResponse.recordsTotal =  inquiryRepository
                .FindByCondition(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false).Count();
            tableResponse.recordsFiltered = tableResponse.recordsTotal;
            return tableResponse;
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
                    FactorName = x.JobOrders.FirstOrDefault().Factory.BranchName
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
            IQueryable<Inquiry> inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
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
                            inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.quotationDelayed;
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
            IQueryable<Inquiry> inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);

            List<int?> roletypeId = new List<int?>
            {
                (int)roleType.Manager
            };
            foreach (Inquiry inquiry in inquiries)
            {
                IQueryable<InquiryWorkscope> inquiryWorkscopes = inquiryWorkscopeRepository.FindByCondition(x =>
                    x.InquiryId == inquiry.InquiryId && x.IsActive == true && x.IsDeleted == false);

                foreach (InquiryWorkscope inquiryWorkscope in inquiryWorkscopes)
                {
                    Measurement measurement = measurementRepository.FindByCondition(m =>
                        m.InquiryWorkscopeId == inquiryWorkscope.InquiryWorkscopeId && m.IsActive == true &&
                        m.IsDeleted == false).FirstOrDefault();

                    if (inquiryWorkscope.InquiryStatusId is (int)inquiryStatus.measurementInProgress or (int)inquiryStatus.measurementRejected)
                    {
                        inquiryWorkscope.InquiryStatusId =
                            Helper.ConvertToDateTime(inquiryWorkscope.MeasurementScheduleDate) <
                            Helper.ConvertToDateTime(Helper.GetDateTime())
                                ? (int)inquiryStatus.measurementdelayed
                                : (int)inquiryStatus.measurementInProgress;

                        if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.measurementdelayed)
                        {
                            sendNotificationToHead(
                                inquiryWorkscope.MeasurementAssignedTo + Constants.MeasurementDelayed, false,
                                null,
                                null,
                                roletypeId, Constants.branchId,
                                (int)notificationCategory.Measurement);

                            sendNotificationToOneUser(
                                inquiryWorkscope.MeasurementAssignedTo + Constants.MeasurementDelayed, false, null,
                                null,
                                Constants.userId, Constants.branchId,
                                (int)notificationCategory.Measurement);
                        }
                    }
                    else if (inquiryWorkscope.InquiryStatusId is (int)inquiryStatus.designPending or (int)inquiryStatus.designRevisionRequested)
                    {
                        inquiryWorkscope.InquiryStatusId =
                            Helper.ConvertToDateTime(inquiryWorkscope.DesignScheduleDate) <
                            Helper.ConvertToDateTime(Helper.GetDateTime())
                                ? (int)inquiryStatus.designDelayed
                                : (int)inquiryStatus.designPending;
                        if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.designDelayed)
                        {
                            sendNotificationToHead(inquiryWorkscope.DesignAssignedTo + Constants.DesignDelayed, true,
                                null,
                                null,
                                roletypeId, (int)inquiry.BranchId,
                                (int)notificationCategory.Design);

                            sendNotificationToOneUser(inquiryWorkscope.DesignAssignedTo + Constants.DesignDelayed,
                                false, null, null,
                                (int)inquiry.ManagedBy, (int)inquiry.BranchId,
                                (int)notificationCategory.Design);
                        }
                    }
                    else if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending)
                    {
                        inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.measurementAssigneeRejected;

                        sendNotificationToHead(
                            inquiryWorkscope.MeasurementAssignedTo + Constants.MeasurementAssigneeDelayed, false,
                            null,
                            null,
                            roletypeId, Constants.branchId,
                            (int)notificationCategory.Measurement);

                        sendNotificationToOneUser(
                            inquiryWorkscope.MeasurementAssignedTo + Constants.MeasurementAssigneeDelayed, false, null,
                            null,
                            Constants.userId, Constants.branchId,
                            (int)notificationCategory.Measurement);
                    }
                    else if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.designAssigneePending)
                    {
                        inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.designAssigneeRejected;

                        sendNotificationToHead(inquiryWorkscope.DesignAssignedTo + Constants.DesignAssigneeDelayed,
                            true,
                            null,
                            null,
                            roletypeId, (int)inquiry.BranchId,
                            (int)notificationCategory.Design);

                        sendNotificationToOneUser(inquiryWorkscope.DesignAssignedTo + Constants.DesignAssigneeDelayed,
                            false, null, null,
                            (int)inquiry.ManagedBy, (int)inquiry.BranchId,
                            (int)notificationCategory.Design);
                    }
                    //inquiryWorkscopeRepository.Update(inquiryWorkscope);
                }

                if (inquiry.InquiryStatusId == (int)inquiryStatus.quotationPending)
                {
                    inquiry.InquiryStatusId =
                        Helper.ConvertToDateTime(inquiry.QuotationScheduleDate) <
                        Helper.ConvertToDateTime(Helper.GetDateTime())
                            ? (int)inquiryStatus.quotationDelayed
                            : (int)inquiryStatus.quotationPending;

                    if (inquiry.InquiryStatusId == (int)inquiryStatus.quotationDelayed)
                    {
                        sendNotificationToHead(inquiry.ManagedBy + Constants.QuotationDelayed, true,
                            null,
                            null,
                            roletypeId, (int)inquiry.BranchId,
                            (int)notificationCategory.Quotation);

                        sendNotificationToOneUser(inquiry.ManagedBy + Constants.QuotationDelayed, false, null, null,
                            (int)inquiry.ManagedBy, (int)inquiry.BranchId,
                            (int)notificationCategory.Quotation);
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
        public object GetCountByBranchId(int branchId)
        {
            var Customers = customerRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.Branch.IsActive == true 
            && x.Branch.IsDeleted == false && (x.BranchId == branchId || x.BranchId == null)).Count();
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
                inquiriesCount = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false).Count(),
                customers =Customers,
                measurementAssinee = x.Inquiries.Where(x =>  x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.measurementAssigneePending && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.MeasurementAssignedTo == Constants.userId)).Count(),
                measurements = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.measurementInProgress || x.InquiryStatusId == (int)inquiryStatus.measurementRejected || x.InquiryStatusId == (int)inquiryStatus.measurementdelayed) && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.MeasurementAssignedTo == Constants.userId)).Count(),
                measurementApprovals = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.measurementWaitingForApproval && x.ManagedBy == Constants.userId)).Count(),
                designAssigne = x.Inquiries.Where(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.designAssigneePending && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.DesignAssignedTo == Constants.userId)).Count(),
                designs = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.designPending || x.InquiryStatusId == (int)inquiryStatus.designDelayed || x.InquiryStatusId == (int)inquiryStatus.designRejected || x.InquiryStatusId == (int)inquiryStatus.designRevisionRequested) && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.DesignAssignedTo == Constants.userId)).Count(),
                designApprovals = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.designWaitingForApproval || x.InquiryStatusId == (int)inquiryStatus.designRejectedByCustomer) && x.ManagedBy == Constants.userId).Count(),
                quotationAssign = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationSchedulePending) && x.InquiryWorkscopes.Any(y => y.IsActive == true && y.IsDeleted == false && y.DesignAssignedTo == Constants.userId)).Count(),
                quotations = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.quotationPending || x.InquiryStatusId == (int)inquiryStatus.quotationRejected || x.InquiryStatusId == (int)inquiryStatus.quotationDelayed || x.InquiryStatusId == (int)inquiryStatus.quotationRevisionRequested) && x.ManagedBy == Constants.userId).Count(),
                quotationApprovals = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && x.InquiryStatusId == (int)inquiryStatus.quotationWaitingForApproval && x.ManagedBy == Constants.userId).Count(),
                uploadcontract = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.contractInProgress || x.InquiryStatusId == (int)inquiryStatus.contractRejected) && x.ManagedBy == Constants.userId).Count(),
                technicalChecklist = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.checklistPending || x.InquiryStatusId == (int)inquiryStatus.commercialChecklistRejected) && x.ManagedBy == Constants.userId).Count(),
                commericalChecklist = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.commercialChecklistPending || x.InquiryStatusId == (int)inquiryStatus.jobOrderFactoryRejected || x.InquiryStatusId == (int)inquiryStatus.specialApprovalRejected)).Count(),
                specialApprovals = x.Inquiries.Where(x => x.IsActive == true && x.IsDeleted == false && (x.InquiryStatusId == (int)inquiryStatus.specialApprovalPending) && x.ManagedBy == Constants.userId).Count(),
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