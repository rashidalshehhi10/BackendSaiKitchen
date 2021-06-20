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

namespace SaiKitchenBackend.Controllers
{
    public class InquiryController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddInquiryAsync(Inquiry inquiry)
        {
            inquiry.InquiryStartDate = Helper.GetDateTime();
            Customer customer = customerRepository.FindByCondition(x => x.CustomerContact == inquiry.Customer.CustomerContact && x.BranchId == inquiry.BranchId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
                    Customer anotherBranchCustomer = customerRepository.FindByCondition(x => x.CustomerContact == inquiry.Customer.CustomerContact && x.BranchId != inquiry.BranchId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    if (anotherBranchCustomer != null)
                    {
                        List<int?> roletypeId = new List<int?>();
                        roletypeId.Add((int)roleType.Manager);
                        sendNotificationToHead(anotherBranchCustomer.CustomerName + Constants.inquiryOnAnotherBranchMessage, false, null, null, roletypeId, anotherBranchCustomer.BranchId, (int)notificationCategory.Other);
                    }
                }

                catch (Exception ex)
                {

                    Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
                }
            }
            if (inquiry.Building.BuildingAddress == null || inquiry.Building.BuildingAddress == "")
            {
                inquiry.Building.BuildingAddress = customer.CustomerAddress;
            }
            if (inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo != null)
            {
                sendNotificationToOneUser(Constants.measurementAssign + "" + inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, false, null, null, (int)inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo, (int)inquiry.BranchId, (int)notificationCategory.Measurement);
            }
            inquiryRepository.Create(inquiry);
            context.SaveChanges();

            inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
            //inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserFcmtoken
            response.data = inquiry;
            try
            {
                //(String toEmail, String inquiryCode, String measurementScheduleDate, String assignTo, String contactNumber, String buildingAddress)
                await mailService.SendInquiryEmailAsync(inquiry.Customer.CustomerEmail, inquiry.InquiryCode, inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserName, inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserMobile, inquiry.Building.BuildingAddress);
            }

            catch (Exception ex)
            {

                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public Object AddWorkscopetoInquiry(WorkscopeInquiry workscopeInquiry)
        {
            var inquiryWorkscope = context.InquiryWorkscopes.AsNoTracking().FirstOrDefault(i => i.InquiryWorkscopeId == workscopeInquiry.inquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false && i.Measurements.Count < 1);
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.CreatedDate = null;
                foreach (var workscopeId in workscopeInquiry.WorkScopeId.OfType<int>())
                {

                    InquiryWorkscope newInquiryWorkscope = new InquiryWorkscope();
                    newInquiryWorkscope.InquiryId = inquiryWorkscope.InquiryId;
                    newInquiryWorkscope.WorkscopeId = workscopeId;
                    newInquiryWorkscope.InquiryStatusId = inquiryWorkscope.InquiryStatusId;
                    newInquiryWorkscope.IsActive = true;
                    newInquiryWorkscope.IsDeleted = false;
                    newInquiryWorkscope.MeasurementAssignedTo = inquiryWorkscope.MeasurementAssignedTo;
                    newInquiryWorkscope.MeasurementScheduleDate = inquiryWorkscope.MeasurementScheduleDate;
                    newInquiryWorkscope.DesignAssignedTo = inquiryWorkscope.DesignAssignedTo;
                    newInquiryWorkscope.DesignScheduleDate = inquiryWorkscope.DesignScheduleDate;

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

        [HttpPost]
        [Route("[action]")]
        public Object DeleteWorkscopefromInquiry(int inquiryWorkscopeId)
        {
            var inquiryWorkscope = context.InquiryWorkscopes.FirstOrDefault(i => i.InquiryWorkscopeId == inquiryWorkscopeId && i.IsActive == true && i.IsDeleted == false );
            if (inquiryWorkscope != null)
            {
                inquiryWorkscope.IsDeleted = true;
                inquiryWorkscopeRepository.Update(inquiryWorkscope);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry doesn't exist";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public Object GetWorkscope()
        {
            response.data = workScopeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public Object GetAllInquiries()
        {
            response.data = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).Include(x => x.InquiryWorkscopes);
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public Object GetInquiriesOfBranch(int branchId)
        {

            var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.Inquiry.BranchId == branchId && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false && x.IsActive == true
            && x.IsDeleted == false).Select(x => new ViewInquiryDetail()
            {
                InquiryWorkscopeId = x.InquiryWorkscopeId,
                InquiryId = x.InquiryId,
                InquiryDescription = x.Inquiry.InquiryDescription,
                InquiryStartDate = Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                MeasurementAssignTo = x.MeasurementAssignedToNavigation.UserName,
                InquiryComment = x.Comments,
                WorkScopeId = x.WorkscopeId,
                WorkScopeName = x.Workscope.WorkScopeName,
                DesignScheduleDate = x.DesignScheduleDate,
                DesignAssignTo = x.DesignAssignedToNavigation.UserName,
                Status = x.InquiryStatusId,
                MeasurementScheduleDate = x.MeasurementScheduleDate,
                BuildingAddress = x.Inquiry.Building.BuildingAddress,
                BuildingCondition = x.Inquiry.Building.BuildingCondition,
                BuildingFloor = x.Inquiry.Building.BuildingFloor,
                BuildingReconstruction = (bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                IsOccupied = (bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                InquiryEndDate = Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                BuildingTypeOfUnit = x.Inquiry.Building.BuildingTypeOfUnit,
                IsEscalationRequested = x.Inquiry.IsEscalationRequested,
                CustomerId = x.Inquiry.CustomerId,
                CustomerName = x.Inquiry.Customer.CustomerName,
                CustomerContact = x.Inquiry.Customer.CustomerContact,
                BranchId = x.Inquiry.BranchId,
                InquiryAddedBy = x.Inquiry.AddedByNavigation.UserName,
                NoOfRevision = x.Measurements.Where(y => y.IsDeleted == false).Count(),
                InquiryCode = "IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId
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
        [HttpGet]
        [Route("[action]")]
        public void CheckScheduleDate()
        {
            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            foreach (var inquiry in inquiries)
            {
                var inquiryWorkscopes = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryId == inquiry.InquiryId && x.IsActive == true && x.IsDeleted == false);
                foreach (var inquiryWorkscope in inquiryWorkscopes)
                {
                    if (inquiryWorkscope.InquiryStatusId < 3)
                    {
                        inquiryWorkscope.InquiryStatusId = Helper.ConvertToDateTime(inquiryWorkscope.MeasurementScheduleDate) < Helper.ConvertToDateTime(Helper.GetDateTime()) ? 2 : 1;

                    }
                    else if (inquiryWorkscope.InquiryStatusId < 5)
                    {
                        inquiryWorkscope.InquiryStatusId = Helper.ConvertToDateTime(inquiryWorkscope.DesignScheduleDate) < Helper.ConvertToDateTime(Helper.GetDateTime()) ? 4 : 3;
                    }
                    inquiryWorkscopeRepository.Update(inquiryWorkscope);

                }
            }
            context.SaveChanges();
        }

        [HttpGet]
        [Route("[action]")]
        public void CheckNotifyScheduleDate()
        {

            var inquiries = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);
            foreach (var inquiry in inquiries)
            {
                var inquiryWorkscopes = inquiryWorkscopeRepository.FindByCondition(x => x.InquiryId == inquiry.InquiryId && x.IsActive == true && x.IsDeleted == false);
                foreach (var inquiryWorkscope in inquiryWorkscopes)
                {
                    var measurement = measurementRepository.FindByCondition(m => m.InquiryWorkscopeId == inquiryWorkscope.InquiryWorkscopeId && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();

                    List<int?> roletypeId = new List<int?>();
                    roletypeId.Add((int)roleType.Manager);

                    if (inquiryWorkscope.InquiryStatusId < (int)inquiryStatus.designPending)
                    {
                        inquiryWorkscope.InquiryStatusId = Helper.ConvertToDateTime(inquiryWorkscope.MeasurementScheduleDate) < Helper.ConvertToDateTime(Helper.GetDateTime()) ? (int)inquiryStatus.measurementdelayed : (int)inquiryStatus.measurementPending;

                        if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.measurementdelayed)
                        {
                            sendNotificationToHead(inquiryWorkscope.MeasurementAssignedTo + Constants.MeasurementDelayed, false,
                              null,
                              null,
                              roletypeId, Constants.branchId,
                              (int)notificationCategory.Measurement);

                            sendNotificationToOneUser(inquiryWorkscope.MeasurementAssignedTo + Constants.MeasurementDelayed, false, null, null,
                                Constants.userId, Constants.branchId,
                                (int)notificationCategory.Measurement);
                        }



                    }
                    else if (inquiryWorkscope.InquiryStatusId < (int)inquiryStatus.quotationPending)
                    {
                        inquiryWorkscope.InquiryStatusId = Helper.ConvertToDateTime(inquiryWorkscope.DesignScheduleDate) < Helper.ConvertToDateTime(Helper.GetDateTime()) ? (int)inquiryStatus.designDelayed : (int)inquiryStatus.designPending;
                        if (inquiryWorkscope.InquiryStatusId == (int)inquiryStatus.designDelayed)
                        {

                            sendNotificationToHead(inquiryWorkscope.MeasurementAssignedTo + Constants.DesignDelayed, true,
                              null,
                              null,
                              roletypeId, (int)inquiry.BranchId,
                              (int)notificationCategory.Design);

                            sendNotificationToOneUser(inquiryWorkscope.MeasurementAssignedTo + Constants.DesignDelayed, false, null, null,
                                (int)inquiry.AddedBy, (int)inquiry.BranchId,
                                (int)notificationCategory.Design);
                        }
                    }
                    inquiryWorkscopeRepository.Update(inquiryWorkscope);

                }
            }
            context.SaveChanges();
        }

        [HttpPost]
        [Route("[action]")]
        public Object UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == updateInquirySchedule.InquiryId && x.IsActive == true && x.IsDeleted == false).Include(x => x.InquiryWorkscopes).Include(x => x.AddedByNavigation.UserRoles).Include(x => x.AddedByNavigation.UserRoles).FirstOrDefault();
            if (inquiry != null)
            {
                //inquiry.InquiryWorkscopes.AsQueryable<InquiryWorkscope>().Where(x => x.IsActive == true && x.IsDeleted == false).ForEachAsync((x) => { x.DesignAssignedTo = updateMeasurementSchedule.MeasurementAssignedTo; x.MeasurementScheduleDate = updateMeasurementSchedule.MeasurementScheduleDate; x.InquiryStatusId = updateMeasurementSchedule.InquiryStatusId; });
                if (inquiry.InquiryWorkscopes.FirstOrDefault().InquiryStatusId <= 2)
                {
                    updateInquirySchedule.InquiryStatusId = Helper.ConvertToDateTime(updateInquirySchedule.MeasurementScheduleDate) < Helper.ConvertToDateTime(Helper.GetDateTime()) ? 2 : 1;

                }
                else if (updateInquirySchedule.DesignScheduleDate != null && inquiry.InquiryWorkscopes.FirstOrDefault().InquiryStatusId <= 4)
                {
                    updateInquirySchedule.InquiryStatusId = Helper.ConvertToDateTime(updateInquirySchedule.DesignScheduleDate) < Helper.ConvertToDateTime(Helper.GetDateTime()) ? 4 : 3;
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
                    var userRoles = userRoleRepository.FindByCondition(x => inquiry.AddedByNavigation.UserRoles.Where(y => y.UserRoleId == x.UserRoleId).Any() && x.IsActive == true && x.IsDeleted == false).Include(x => x.BranchRole).Include(x => x.BranchRole.RoleHeads).ToList();
                    var roleHeadsId = userRoles.FirstOrDefault().BranchRole.RoleHeads.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.HeadRoleId).ToList();
                    var roleTypeId = branchRoleRepository.FindByCondition(x => roleHeadsId.Contains(x.BranchRoleId) && x.IsActive == true && x.IsDeleted == false).Select(x => x.RoleTypeId).ToList();

                    sendNotificationToHead(inquiry.Customer.CustomerName + Constants.measurementRescheduleBranchMessage + inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, false, null, null, roleTypeId, inquiry.BranchId, (int)notificationCategory.Measurement);
                }
                catch (Exception ex)
                {

                    Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "No Inquiry Exist";
            }
            return response;
        }


        #region workscope

        [HttpPost]
        [Route("[action]")]
        public object GetAllWorkscope()
        {
            return workScopeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false);

        }
        [HttpGet]
        [Route("[action]")]
        public object GetWorkscopeById(int workScopeId)
        {
            response.data = workScopeRepository.FindByCondition(x => x.WorkScopeId == workScopeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (response.data == null)
            {
                response.isError = true;
                response.errorMessage = "WorkScope doesn't Exist";
            }
            return response;
        }
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
                WorkScope oldworkScope = workScopeRepository.FindByCondition(x => x.WorkScopeId == workscope.WorkScopeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
        [HttpPost]
        [Route("[action]")]
        public object EditWorkscope(WorkScope workscope)
        {
            WorkScope oldworkScope = workScopeRepository.FindByCondition(x => x.WorkScopeId == workscope.WorkScopeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
        [HttpPost]
        [Route("[action]")]
        public object DeleteWorkscope(int workScopeId)
        {
            WorkScope oldworkScope = workScopeRepository.FindByCondition(x => x.WorkScopeId == workScopeId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
        #endregion


        #region Measurement 


        [HttpPost]
        [Route("[action]")]
        public Object GetMeasurementOfBranch(int branchId)
        {
            var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.MeasurementAssignedTo == Constants.userId && x.Inquiry.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.measurementPending || x.InquiryStatusId == (int)inquiryStatus.measurementdelayed || x.InquiryStatusId == (int)inquiryStatus.measurementRejected) && x.IsActive == true && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false
            && x.IsDeleted == false).Select(x => new ViewInquiryDetail()
            {
                InquiryWorkscopeId = x.InquiryWorkscopeId,
                InquiryId = x.InquiryId,
                InquiryDescription = x.Inquiry.InquiryDescription,
                InquiryStartDate = Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                MeasurementAssignTo = x.MeasurementAssignedToNavigation.UserName,
                InquiryComment = x.Comments,
                WorkScopeId = x.WorkscopeId,
                WorkScopeName = x.Workscope.WorkScopeName,
                QuestionaireType = x.Workscope.QuestionaireType,
                DesignScheduleDate = x.DesignScheduleDate,
                DesignAssignTo = x.DesignAssignedToNavigation.UserName,
                Status = x.InquiryStatusId,
                MeasurementScheduleDate = x.MeasurementScheduleDate,
                BuildingAddress = x.Inquiry.Building.BuildingAddress,
                BuildingCondition = x.Inquiry.Building.BuildingCondition,
                BuildingFloor = x.Inquiry.Building.BuildingFloor,
                BuildingReconstruction = (bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                IsOccupied = (bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                InquiryEndDate = Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                BuildingTypeOfUnit = x.Inquiry.Building.BuildingTypeOfUnit,
                IsEscalationRequested = x.Inquiry.IsEscalationRequested,
                CustomerId = x.Inquiry.CustomerId,
                CustomerName = x.Inquiry.Customer.CustomerName,
                CustomerContact = x.Inquiry.Customer.CustomerContact,
                BranchId = x.Inquiry.BranchId,
                InquiryCode = "IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                NoOfRevision = x.Measurements.Where(y => y.IsDeleted == false).Count()
            }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<object> UpdateMeasurementDetailsAsync(Inquiry inquiry)
        {
            inquiry.InquiryStartDate = Helper.GetDateTime();
            Customer customer = customerRepository.FindByCondition(x => x.CustomerContact == inquiry.Customer.CustomerContact && x.BranchId == inquiry.BranchId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
                    Customer anotherBranchCustomer = customerRepository.FindByCondition(x => x.CustomerContact == inquiry.Customer.CustomerContact && x.BranchId != inquiry.BranchId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    if (anotherBranchCustomer != null)
                    {

                        List<int?> roletypeId = new List<int?>();
                        roletypeId.Add((int)roleType.Manager);
                        sendNotificationToHead(anotherBranchCustomer.CustomerName + Constants.inquiryOnAnotherBranchMessage, false, null, null, roletypeId, anotherBranchCustomer.BranchId, (int)notificationCategory.Other);
                    }
                }

                catch (Exception ex)
                {

                    Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
                }
            }
            if (inquiry.Building.BuildingAddress == null || inquiry.Building.BuildingAddress == "")
            {
                inquiry.Building.BuildingAddress = customer.CustomerAddress;
            }
            if (inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo != null)
            {
                sendNotificationToOneUser(Constants.measurementAssign + "" + inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, false, null, null, (int)inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedTo, (int)inquiry.BranchId, (int)notificationCategory.Measurement);
            }
            inquiryRepository.Create(inquiry);
            context.SaveChanges();

            inquiry.InquiryCode = "IN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId;
            //inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserFcmtoken
            response.data = inquiry;
            try
            {
                await mailService.SendInquiryEmailAsync(inquiry.Customer.CustomerEmail, inquiry.InquiryCode, inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementScheduleDate, inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserName, inquiry.InquiryWorkscopes.FirstOrDefault().MeasurementAssignedToNavigation.UserMobile, inquiry.Building.BuildingAddress);
            }

            catch (Exception ex)
            {
                Serilog.Log.Error("Error: UserId=" + Constants.userId + " Error=" + ex.Message + " " + ex.ToString());
            }
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public Object AddMeaurementtoInquiry(Measurement measurement)
        {
            if (measurement.Files.Count > 0)
            {
                Guid obj = Guid.NewGuid();
                using (var stream = new MemoryStream(measurement.Files.FirstOrDefault().FileImage))
                {
                    FileStream file = new FileStream(@"Assets/Images/" + obj.ToString(), FileMode.Create, FileAccess.Write);
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

        [HttpPost]
        [Route("[action]")]
        public Object GetApprovalMeasurementOfBranch(int branchId)
        {
            var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.Inquiry.AddedBy == Constants.userId && x.Inquiry.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.measurementWaitingForApproval) && x.IsActive == true && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false
         && x.IsDeleted == false && x.Measurements.Any(y => y.IsActive == true && y.IsDeleted == false)).Include(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).Select(x => new ViewInquiryDetail()
         {
             InquiryWorkscopeId = x.InquiryWorkscopeId,
             InquiryId = x.InquiryId,
             InquiryDescription = x.Inquiry.InquiryDescription,
             InquiryStartDate = Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
             MeasurementAssignTo = x.MeasurementAssignedToNavigation.UserName,
             WorkScopeId = x.WorkscopeId,
             WorkScopeName = x.Workscope.WorkScopeName,
             QuestionaireType = x.Workscope.QuestionaireType,
             DesignScheduleDate = x.DesignScheduleDate,
             DesignAssignTo = x.DesignAssignedToNavigation.UserName,
             Status = x.InquiryStatusId,
             InquiryComment = x.Comments,
             MeasurementScheduleDate = x.MeasurementScheduleDate,
             BuildingAddress = x.Inquiry.Building.BuildingAddress,
             BuildingCondition = x.Inquiry.Building.BuildingCondition,
             BuildingFloor = x.Inquiry.Building.BuildingFloor,
             BuildingReconstruction = (bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
             IsOccupied = (bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
             InquiryEndDate = Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
             BuildingTypeOfUnit = x.Inquiry.Building.BuildingTypeOfUnit,
             IsEscalationRequested = x.Inquiry.IsEscalationRequested,
             CustomerId = x.Inquiry.CustomerId,
             CustomerName = x.Inquiry.Customer.CustomerName,
             CustomerContact = x.Inquiry.Customer.CustomerContact,
             BranchId = x.Inquiry.BranchId,
             InquiryCode = "IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
             InquiryAddedBy = x.Inquiry.AddedByNavigation.UserName,
             NoOfRevision = x.Measurements.Where(y => y.IsDeleted == false).Count()
         }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;


        }

        #endregion


        #region design

        [HttpPost]
        [Route("[action]")]
        public Object GetDesignOfBranch(int branchId)
        {
            var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.DesignAssignedTo == Constants.userId && x.Inquiry.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.designPending || x.InquiryStatusId == (int)inquiryStatus.designDelayed) && x.IsActive == true && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false
            && x.IsDeleted == false).Select(x => new ViewInquiryDetail()
            {
                InquiryWorkscopeId = x.InquiryWorkscopeId,
                InquiryId = x.InquiryId,
                InquiryDescription = x.Inquiry.InquiryDescription,
                InquiryStartDate = Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
                MeasurementAssignTo = x.MeasurementAssignedToNavigation.UserName,
                WorkScopeId = x.WorkscopeId,
                WorkScopeName = x.Workscope.WorkScopeName,
                QuestionaireType = x.Workscope.QuestionaireType,
                DesignScheduleDate = x.DesignScheduleDate,
                DesignAssignTo = x.DesignAssignedToNavigation.UserName,
                Status = x.InquiryStatusId,
                InquiryComment = x.Comments,
                MeasurementScheduleDate = x.MeasurementScheduleDate,
                BuildingAddress = x.Inquiry.Building.BuildingAddress,
                BuildingCondition = x.Inquiry.Building.BuildingCondition,
                BuildingFloor = x.Inquiry.Building.BuildingFloor,
                BuildingReconstruction = (bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
                IsOccupied = (bool)x.Inquiry.Building.IsOccupied ? "Yes" : "No",
                InquiryEndDate = Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
                BuildingTypeOfUnit = x.Inquiry.Building.BuildingTypeOfUnit,
                IsEscalationRequested = x.Inquiry.IsEscalationRequested,
                CustomerId = x.Inquiry.CustomerId,
                CustomerName = x.Inquiry.Customer.CustomerName,
                CustomerContact = x.Inquiry.Customer.CustomerContact,
                BranchId = x.Inquiry.BranchId,
                InquiryCode = "IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
                NoOfRevision = x.Measurements.Where(y => y.IsDeleted == false).Count()
            }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;
        }



        [HttpPost]
        [Route("[action]")]
        public Object AddDesigntoInquiry(Measurement measurement)
        {
            if (measurement.Files.Count > 0)
            {
                Guid obj = Guid.NewGuid();
                using (var stream = new MemoryStream(measurement.Files.FirstOrDefault().FileImage))
                {
                    FileStream file = new FileStream(@"Assets/Images/" + obj.ToString(), FileMode.Create, FileAccess.Write);
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

        [HttpPost]
        [Route("[action]")]
        public Object GetApprovalDesignOfBranch(int branchId)
        {
            var inquiries = inquiryWorkscopeRepository.FindByCondition(x => x.Inquiry.AddedBy == Constants.userId && x.Inquiry.BranchId == branchId && (x.InquiryStatusId == (int)inquiryStatus.designWaitingForApproval) && x.IsActive == true && x.Inquiry.IsActive == true && x.Inquiry.IsDeleted == false
         && x.IsDeleted == false && x.Measurements.Any(y => y.IsActive == true && y.IsDeleted == false)).Include(x => x.Measurements.Where(y => y.IsActive == true && y.IsDeleted == false)).Select(x => new ViewInquiryDetail()
         {
             InquiryWorkscopeId = x.InquiryWorkscopeId,
             InquiryId = x.InquiryId,
             InquiryDescription = x.Inquiry.InquiryDescription,
             InquiryStartDate = Helper.GetDateFromString(x.Inquiry.InquiryStartDate),
             MeasurementAssignTo = x.MeasurementAssignedToNavigation.UserName,
             WorkScopeId = x.WorkscopeId,
             WorkScopeName = x.Workscope.WorkScopeName,
             QuestionaireType = x.Workscope.QuestionaireType,
             DesignScheduleDate = x.DesignScheduleDate,
             DesignAssignTo = x.DesignAssignedToNavigation.UserName,
             Status = x.InquiryStatusId,
             InquiryComment = x.Comments,
             MeasurementScheduleDate = x.MeasurementScheduleDate,
             BuildingAddress = x.Inquiry.Building.BuildingAddress,
             BuildingCondition = x.Inquiry.Building.BuildingCondition,
             BuildingFloor = x.Inquiry.Building.BuildingFloor,
             BuildingReconstruction = (bool)x.Inquiry.Building.BuildingReconstruction ? "Yes" : "No",
             InquiryEndDate = Helper.GetDateFromString(x.Inquiry.InquiryEndDate),
             BuildingTypeOfUnit = x.Inquiry.Building.BuildingTypeOfUnit,
             IsEscalationRequested = x.Inquiry.IsEscalationRequested,
             CustomerId = x.Inquiry.CustomerId,
             CustomerName = x.Inquiry.Customer.CustomerName,
             CustomerContact = x.Inquiry.Customer.CustomerContact,
             BranchId = x.Inquiry.BranchId,
             InquiryCode = "IN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId,
             InquiryAddedBy = x.Inquiry.AddedByNavigation.UserName,
             NoOfRevision = x.Designs.Where(y => y.IsDeleted == false).Count()
         }).OrderByDescending(x => x.InquiryId);
            tableResponse.data = inquiries;
            tableResponse.recordsTotal = inquiries.Count();
            tableResponse.recordsFiltered = inquiries.Count();
            return tableResponse;


        }
        #endregion
    }
}