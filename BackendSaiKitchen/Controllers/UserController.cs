using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SaiKitchenBackend.Controllers
{
    //[ApiController]
    //[Route("[controller]")]

    public class UserController : BaseController
    {

        [HttpHead, HttpPost]
        [Route("")]
        public Object User()
        {
            return Ok();
        }

        [AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetAllUserAsync()
        {
            var userList = userRepository.FindByCondition(x => x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true && y.BranchRole.IsDeleted == false && y.Branch.IsActive == true && y.Branch.IsDeleted == false) && x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false)).Include(obj => obj.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false));
            var brnchRole = userRoleRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Join(branchRoleRepository.GetAll(),
                       userRole => userRole.BranchRoleId,
                       branchRole => branchRole.BranchRoleId,
                       (userRole, branchRole) => new { userRole = userRole, branchRole = branchRole }).ToList();
            var brnch = userRoleRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Join(branchRepository.GetAll(),
                      userRole => userRole.BranchId,
                      branch => branch.BranchId,
                      (userRole, branch) => new { userRole = userRole, branch = branch }).ToList();

            await userList.ForEachAsync((x) => { x.UserPassword = null; });
            response.data = userList;
            return response;

        }

        [AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetAllUserofOneBranchAsync(int branchId)
        {
            var userList = userRepository.FindByCondition(x => x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true && y.BranchRole.IsActive == false && y.Branch.IsActive == true && y.Branch.IsActive == false) && x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.BranchId == branchId && y.IsActive == true && y.IsDeleted == false)).Include(obj => obj.UserRoles.Where(x => x.BranchId == branchId && x.BranchRole.IsActive == true && x.IsDeleted == false && x.IsActive == true && x.IsDeleted == false));
            var brnchRole = userRoleRepository.GetAll().Where(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false).Join(branchRoleRepository.GetAll(),
                       userRole => userRole.BranchRoleId,
                       branchRole => branchRole.BranchRoleId,
                       (userRole, branchRole) => new { userRole = userRole, branchRole = branchRole }).ToList();
            await userList.ForEachAsync((x) => { x.UserPassword = null; });
            response.data = userList;

            return response;
        }

        [AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public async Task<object> GetUserByid(int userId)
        {
           
          var user = userRepository.FindByCondition(x=>x.UserId==userId && x.IsActive==true && x.IsDeleted==false).FirstOrDefault();
       
            if (user == null)
            {
                response.isError = true;
                response.errorMessage = "User doesn\'t exist";
            }
            response.data = user;
            return response;
        }

        [AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> RegisterUserAsync(User user)
        {
            User oldUser = userRepository.FindByCondition(x => x.UserEmail == user.UserEmail && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (oldUser == null)
            {
                userRepository.Create(user);
                context.SaveChanges();
                await mailService.SendWelcomeEmailAsync(new PasswordRequest() { ToEmail = user.UserEmail, UserName = user.UserName, Link = Constants.CRMBaseUrl + "/setpassword.html?userId=" + Helper.EnryptString(user.UserId.ToString()) });
                //await SendWelcomeMail(new WelcomeRequest() { ToEmail = user.UserEmail, UserName = user.UserName });
                response.isError = false;
                response.errorMessage = "Success";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "User already Exist";
            }
            return response;
        }


        //[AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetMeasurementUsers(int branchId)
        {
            var users = context.Users.Where(x => x.UserRoles.Any(y => y.BranchId == branchId && y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true && y.IsDeleted == false && y.BranchRole.PermissionRoles.Any(z => z.PermissionId == (int)permission.ManageMeasurement && z.IsActive == true && z.IsDeleted == false && z.PermissionLevelId >= (int)permissionLevel.Create)) && x.IsActive == true && x.IsDeleted == false).Select(x => new User() { UserId = x.UserId, UserName = x.UserName });
            response.data = users;
            return response;
        }


        //[AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetDesignUsers(int branchId)
        {
            var users = context.Users.Where(x => x.UserRoles.Any(y => y.BranchId == branchId && y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true && y.IsDeleted == false && y.BranchRole.PermissionRoles.Any(z => z.PermissionId == (int)permission.ManageDesign && z.IsActive == true && z.IsDeleted == false && z.PermissionLevelId >= (int)permissionLevel.Create)) && x.IsActive == true && x.IsDeleted == false).Select(x => new User() { UserId = x.UserId, UserName = x.UserName });
            response.data = users;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> ForgotPasswordUserAsync(User user)
        {
            User oldUser = userRepository.FindByCondition(x => x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false && y.BranchRole.IsActive == true && y.BranchRole.IsDeleted == false && y.Branch.IsActive == true && y.Branch.IsDeleted == false) && x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false)).Include(obj => obj.UserRoles.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (oldUser != null)
            {
                await mailService.SendForgotEmailAsync(new PasswordRequest() { ToEmail = oldUser.UserEmail, UserName = oldUser.UserName, Link = Constants.CRMBaseUrl + "/setpassword.html?userId=" + Helper.EnryptString(oldUser.UserId.ToString()) });
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
        public Object LoginUser(User user)
        {
            User loggedinUser = context.Users.Where(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword &&
            x.IsActive == true && x.IsDeleted == false && x.UserRoles.Any(y => y.IsActive == true && y.IsDeleted == false && y.Branch.IsActive == true
            && y.Branch.IsDeleted == false && y.BranchRole.IsActive == true && y.BranchRole.IsDeleted == false && y.BranchRole.RoleType.IsActive == true && y.BranchRole.RoleType.IsDeleted == false)).FirstOrDefault();
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
        public Object LogoutUser(User user)
        {
            User loggedinUser = context.Users.Where(x => x.UserId == user.UserId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
            var loggedInUserRoles = userRoleRepository.FindByCondition(x => x.UserId == loggedinUser.UserId && x.IsActive == true && x.IsDeleted == false && x.Branch.IsActive == true && x.Branch.IsDeleted == false).Include(x => x.Branch).Include(x => x.BranchRole.PermissionRoles.Where(z => z.IsActive == true && z.IsDeleted == false)).Include(x => x.BranchRole.RoleType).ToList();
            var branchRole = branchRoleRepository.FindByCondition(x => loggedInUserRoles.Select(z => z.BranchRoleId).Contains(x.BranchRoleId) && x.IsActive == true && x.IsDeleted == false).ToList();

            loggedinUser.UserPassword = "";

            dicResponse.Add("user", loggedinUser);
            dicResponse.Add("branchRole", branchRole);
        }

        [AuthFilter((int)permission.ManageUsers, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public Object DeleteUser(int userId)
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
        public Object SetNewPassword(NewPassword newPassword)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(newPassword.userPassword) && hasUpperChar.IsMatch(newPassword.userPassword) && hasMinimum8Chars.IsMatch(newPassword.userPassword);
            Console.WriteLine(isValidated);

            if (isValidated)
            {
                int userId = int.Parse(Helper.DecryptString(newPassword.userId));
                User user = userRepository.FindByCondition(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
        public Object SetFCMToken(FCMToken fCMToken)
        {
            User user = userRepository.FindByCondition(x => x.UserId == fCMToken.userId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
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
    }
}
