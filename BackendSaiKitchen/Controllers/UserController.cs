﻿using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SaiKitchenBackend.Controllers
{
    //[ApiController]
    //[Route("[controller]")]

    public class UserController : BaseController
    {
        [HttpHead]
        [HttpPost]
        [Route("")]
        public object User()
        {
            return Ok();
        }

        //[AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetAllUserAsync()
        {
            //var userList = userRepository.FindByCondition(x => x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true && y.BranchRole.IsDeleted == false && y.Branch.IsActive == true && y.Branch.IsDeleted == false) && x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false)).Include(obj => obj.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false));
            //var brnchRole = userRoleRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Join(branchRoleRepository.GetAll(),
            //           userRole => userRole.BranchRoleId,
            //           branchRole => branchRole.BranchRoleId,
            //           (userRole, branchRole) => new { userRole = userRole, branchRole = branchRole }).ToList();
            //var brnch = userRoleRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Join(branchRepository.GetAll(),
            //          userRole => userRole.BranchId,
            //          branch => branch.BranchId,
            //          (userRole, branch) => new { userRole = userRole, branch = branch }).ToList();
            IQueryable<User> userList = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Branch)
                .Include(y => y.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.BranchRole).AsNoTracking();
            await userList.ForEachAsync(x => { x.UserPassword = null; });

            response.data = userList.ToList();
            return response;
        }

        [HttpGet]
        [Route("[action]")]
        public object GetAuthUser()
        {
            var users = userRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && 
            x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false && y.BranchId == Constants.branchId && y.Branch.IsActive == true && y.Branch.IsDeleted == false && 
            y.BranchRole.IsActive == true && y.BranchRole.IsDeleted == false &&
            y.BranchRole.PermissionRoles.Any(z => z.PermissionLevelId >= (int)permissionLevel.Create && z.PermissionId == (int)permission.ManageCustomer))).ToList();
            response.data = users;
            return response;
        }

        [AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetAllUserofOneBranchAsync(int branchId)
        {
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<User, System.Collections.Generic.IEnumerable<UserRole>> userList = userRepository
                .FindByCondition(x =>
                    x.UserRoles.Any(y =>
                        y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true &&
                        y.BranchRole.IsActive == false && y.Branch.IsActive == true && y.Branch.IsActive == false) &&
                    x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y =>
                        y.BranchId == branchId && y.IsActive == true && y.IsDeleted == false)).Include(obj =>
                    obj.UserRoles.Where(x =>
                        x.BranchId == branchId && x.BranchRole.IsActive == true && x.IsDeleted == false &&
                        x.IsActive == true && x.IsDeleted == false));
            var brnchRole = userRoleRepository.GetAll()
                .Where(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false).Join(
                    branchRoleRepository.GetAll(),
                    userRole => userRole.BranchRoleId,
                    branchRole => branchRole.BranchRoleId,
                    (userRole, branchRole) => new { userRole, branchRole }).ToList();
            await userList.ForEachAsync(x => { x.UserPassword = null; });
            response.data = userList;

            return response;
        }

        [AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetUserByid(int userId)
        {
            User user = userRepository
                .FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Branch)
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(y => y.BranchRole).FirstOrDefault();

            if (user == null)
            {
                response.isError = true;
                response.errorMessage = "User doesn\'t exist";
            }

            response.data = user;
            return response;
        }

        //[AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> RegisterUserAsync(User user)
        {
            if (user.UserId != 0)
            {
                User oldUser = userRepository
                    .FindByCondition(x => x.UserId == user.UserId && x.IsActive == true && x.IsDeleted == false)
                    .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false)).AsNoTracking()
                    .FirstOrDefault();
                if (oldUser != null)
                {
                    oldUser.UserRoles.ToList().ForEach(x => x.IsDeleted = true);
                    oldUser.UserRoles.Add(user.UserRoles.FirstOrDefault());
                    oldUser.UserEmail = user.UserEmail;
                    oldUser.UserName = user.UserName;
                    oldUser.UserMobile = user.UserMobile;
                    userRepository.Update(oldUser);
                    context.SaveChanges();

                    response.isError = false;
                    response.errorMessage = "Success";
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "User doesn't Exist";
                }
            }
            else
            {
                User oldUser = userRepository
                    .FindByCondition(x => x.UserEmail == user.UserEmail && x.IsActive == true && x.IsDeleted == false)
                    .AsNoTracking().FirstOrDefault();
                if (oldUser == null)
                {
                    userRepository.Create(user);
                    context.SaveChanges();
                    await mailService.SendWelcomeEmailAsync(new PasswordRequest
                    {
                        ToEmail = user.UserEmail,
                        UserName = user.UserName,
                        Link = Constants.CRMBaseUrl + "/setpassword.html?userId=" +
                               Helper.EnryptString(user.UserId.ToString())
                    });
                    //await SendWelcomeMail(new WelcomeRequest() { ToEmail = user.UserEmail, UserName = user.UserName });
                    response.isError = false;
                    response.errorMessage = "Success";
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "User already Exist";
                }
            }

            return response;
        }


        //[AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetMeasurementUsers(int branchId)
        {
            IQueryable<User> users = context.Users.Where(x => x.UserRoles.Any(y =>
                    y.BranchId == branchId && y.IsActive == true && y.IsDeleted == false &&
                    y.BranchRole.IsActive == true &&
                    y.IsDeleted == false && y.BranchRole.PermissionRoles.Any(z =>
                        z.PermissionId == (int)permission.ManageMeasurement && z.IsActive == true &&
                        z.IsDeleted == false &&
                        z.PermissionLevelId >= (int)permissionLevel.Create)) && x.IsActive == true &&
                x.IsDeleted == false)
                .Select(x => new User { UserId = x.UserId, UserName = x.UserName });
            response.data = users;
            return response;
        }


        //[AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetDesignUsers(int branchId)
        {
            IQueryable<User> users = context.Users.Where(x => x.UserRoles.Any(y =>
                    y.BranchId == branchId && y.IsActive == true && y.IsDeleted == false &&
                    y.BranchRole.IsActive == true &&
                    y.IsDeleted == false && y.BranchRole.PermissionRoles.Any(z =>
                        z.PermissionId == (int)permission.ManageDesign && z.IsActive == true && z.IsDeleted == false &&
                        z.PermissionLevelId >= (int)permissionLevel.Create)) && x.IsActive == true &&
                x.IsDeleted == false)
                .Select(x => new User { UserId = x.UserId, UserName = x.UserName });
            response.data = users;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> ForgotPasswordUserAsync(User user)
        {
            User oldUser = userRepository.FindByCondition(x =>
                    x.UserEmail == user.UserEmail && x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y =>
                        y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true &&
                        y.BranchRole.IsDeleted == false && y.Branch.IsActive == true && y.Branch.IsDeleted == false) &&
                    x.IsActive == true && x.IsDeleted == false &&
                    x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false))
                .Include(obj => obj.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (oldUser != null)
            {
                await mailService.SendForgotEmailAsync(new PasswordRequest
                {
                    ToEmail = oldUser.UserEmail,
                    UserName = oldUser.UserName,
                    Link = Constants.CRMBaseUrl + "/setpassword.html?userId=" +
                           Helper.EnryptString(oldUser.UserId.ToString())
                });
                //await SendWelcomeMail(new WelcomeRequest() { ToEmail = user.UserEmail, UserName = user.UserName });
                response.isError = false;
                response.errorMessage = "Success";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Email doesn't Exist";
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object LoginUser(User user)
        {
            User loggedinUser = context.Users.Where(x =>
                x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword &&
                x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.IsActive == true &&
                    y.IsDeleted == false && y.Branch.IsActive == true
                    && y.Branch.IsDeleted == false && y.BranchRole.IsActive == true &&
                    y.BranchRole.IsDeleted == false && y.BranchRole.RoleType.IsActive == true &&
                    y.BranchRole.RoleType.IsDeleted == false)).FirstOrDefault();
            if (loggedinUser != null)
            {
                loggedinUser.UserToken = Helper.GenerateToken(loggedinUser.UserId);
                loggedinUser.IsOnline = true;
                loggedinUser.UserFcmtoken = user.UserFcmtoken;
                context.Users.Update(loggedinUser);
                context.SaveChanges();
                getLoginUserData(loggedinUser);
                response.data = loggedinUser;
            }
            else
            {
                response.isError = true;
                response.errorMessage = Constants.loginErrormessage;
                response.data = "";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object LogoutUser(User user)
        {
            User loggedinUser = context.Users
                .Where(x => x.UserId == user.UserId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (loggedinUser != null)
            {
                loggedinUser.UserToken = "";
                loggedinUser.IsOnline = false;
                loggedinUser.UserFcmtoken = "";
                context.Users.Update(loggedinUser);
                context.SaveChanges();

                response.data = "";
            }
            else
            {
                response.isError = true;
                response.errorMessage = Constants.loginErrormessage;
                response.data = "";
            }

            return response;
        }

        private void getLoginUserData(User loggedinUser)
        {
            System.Collections.Generic.List<UserRole> loggedInUserRoles = userRoleRepository
                .FindByCondition(x =>
                    x.UserId == loggedinUser.UserId && x.IsActive == true && x.IsDeleted == false &&
                    x.Branch.IsActive == true && x.Branch.IsDeleted == false).Include(x => x.Branch)
                .Include(x => x.BranchRole.PermissionRoles.Where(z => z.IsActive == true && z.IsDeleted == false))
                .Include(x => x.BranchRole.RoleType).ToList();
            System.Collections.Generic.List<BranchRole> branchRole = branchRoleRepository.FindByCondition(x =>
                loggedInUserRoles.Select(z => z.BranchRoleId).Contains(x.BranchRoleId) && x.IsActive == true &&
                x.IsDeleted == false).ToList();

            loggedinUser.UserPassword = "";

            dicResponse.Add("user", loggedinUser);
            dicResponse.Add("branchRole", branchRole);
        }

        [AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public object DeleteUser(int userId)
        {
            User user = userRepository.FindByCondition(x => x.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                userRepository.Delete(user);
                context.SaveChanges();
                response.data = "Deleted";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "User doesn't Exist";
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object SetNewPassword(NewPassword newPassword)
        {
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasMinimum8Chars = new Regex(@".{8,}");

            bool isValidated = hasNumber.IsMatch(newPassword.userPassword) &&
                              hasUpperChar.IsMatch(newPassword.userPassword) &&
                              hasMinimum8Chars.IsMatch(newPassword.userPassword);
            Console.WriteLine(isValidated);

            if (isValidated)
            {
                int userId = int.Parse(Helper.DecryptString(newPassword.userId));
                User user = userRepository
                    .FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false)
                    .FirstOrDefault();
                if (user != null)
                {
                    user.UserPassword = newPassword.userPassword;
                    user.UserToken = Helper.GenerateToken(user.UserId);
                    user.IsOnline = true;
                    context.Users.Update(user);
                    context.SaveChanges();


                    getLoginUserData(user);

                    response.data = user;
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "User doesn't Exist";
                    response.data = "";
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Password should contain number, upper case character & atleast 8 characters";
                response.data = "";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object SetFCMToken(FCMToken fCMToken)
        {
            User user = userRepository
                .FindByCondition(x => x.UserId == fCMToken.userId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            if (user != null)
            {
                user.UserFcmtoken = fCMToken.userFCMToken;
                user.IsOnline = true;
                context.Users.Update(user);
                context.SaveChanges();
                response.isError = false;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "User doesn't Exist";
                response.data = "";
            }

            return response;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetInquiryCreateUserAsync()
        {
            //var userList = userRepository.FindByCondition(x => x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true && y.BranchRole.IsDeleted == false && y.Branch.IsActive == true && y.Branch.IsDeleted == false) && x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false)).Include(obj => obj.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false));
            //var brnchRole = userRoleRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Join(branchRoleRepository.GetAll(),
            //           userRole => userRole.BranchRoleId,
            //           branchRole => branchRole.BranchRoleId,
            //           (userRole, branchRole) => new { userRole = userRole, branchRole = branchRole }).ToList();
            //var brnch = userRoleRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Join(branchRepository.GetAll(),
            //          userRole => userRole.BranchId,
            //          branch => branch.BranchId,
            //          (userRole, branch) => new { userRole = userRole, branch = branch }).ToList();
            IQueryable<User> userList = userRepository.FindByCondition(x =>
                    x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y =>
                        y.BranchRole.PermissionRoles.Any(z =>
                            z.PermissionId == (int)permission.ManageInquiry &&
                            z.PermissionLevelId >= (int)permissionLevel.Create && z.IsActive == true &&
                            z.IsDeleted == false) && y.IsActive == true && y.IsDeleted == false &&
                        y.BranchId == Constants.branchId))
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.Branch)
                .Include(y => y.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.BranchRole).AsNoTracking();
            await userList.ForEachAsync(x => { x.UserPassword = null; });

            response.data = userList.ToList();

            return response;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<object> MySqlDBConnection()
        {
            using var connection = new MySqlConnection("Server=147.182.217.248;Database=db_social;User Id=sameeradmin;Password=eW3Tn$wC42;");
            await connection.OpenAsync();

            using var command = new MySqlCommand("SELECT * FROM sp_whatsapp_sessions;", connection);
            using var reader = await command.ExecuteReaderAsync();
            object value = null;
            while (await reader.ReadAsync())
            {
                 value = reader.GetValue(0);
                // do something with 'value'
               
            }

            return value;
        }
    }
}