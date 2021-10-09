using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    public class BranchController : BaseController
    {
        //[AuthFilter((int)permission.ManageBranch, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetBranches()
        {
            response.data = BranchRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }

        //[AuthFilter((int)permission.ManageBranch, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetBranchByType(int typeId)
        {

            response.data = BranchRepository.FindByCondition(x => x.BranchTypeId == typeId && x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }
        //[AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetBranchRoles()
        {
            response.data = BranchRoleRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }
        readonly Dictionary<object, object> dic = new Dictionary<object, object>();
        //[AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetBranchRoleById(int branchRoleId)
        {

            var branchRole = BranchRoleRepository.FindByCondition(x => x.BranchRoleId == branchRoleId && x.IsActive == true && x.IsDeleted == false).Include(obj => obj.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false)).Include(obj => obj.RoleHeads.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            
            var roleHeadsId = branchRole.RoleHeads.Select(x => x.HeadRoleId).ToList();
            var roleHeads = BranchRoleRepository.FindByCondition(x => roleHeadsId.Contains(x.BranchRoleId));
            dic.Add("branchRole", branchRole);
            dic.Add("roleHeads", roleHeads);
            response.data = dic;
            return response;
        }

        [HttpGet]
        [Route("[action]")]
        public Object GetBranchById(int branchId)
        {
            var branch = BranchRepository.FindByCondition(x => x.BranchId == branchId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (branch != null)
            {
                response.data = branch;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Branch Not Found";

            }
            return response;
        }

        [AuthFilter((int)permission.ManageBranch, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public Object GetBranchTypes()
        {

            Request.Headers.FirstOrDefault(x => x.Key == "userId").Value.FirstOrDefault();
            response.data = BranchTypeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetRoleTypes()
        {
            response.data = RoleTypeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }

        [AuthFilter((int)permission.ManageBranch, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public Object AddBranch(Branch branch)
        {
            if (branch.BranchId != 0)
            {
                BranchRepository.Update(branch);
            }
            else
            {
                BranchRepository.Create(branch);
            }
            context.SaveChanges();
            response.data = branch;
            return response;
        }

        //[AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public Object AddBranchRole(BranchRole branchRole)
        {
            if (branchRole.BranchRoleId == 0)
            {
                branchRole.IsActive = true;
                branchRole.IsDeleted = false;
                BranchRoleRepository.Create(branchRole);
            }
            else
            {
                var oldBranchrole = BranchRoleRepository.FindByCondition(x => x.BranchRoleId == branchRole.BranchRoleId && x.IsActive == true && x.IsDeleted == false).Include(obj => obj.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false)).Include(obj => obj.RoleHeads.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
                oldBranchrole.BranchRoleName = branchRole.BranchRoleName;
                oldBranchrole.BranchRoleDescription = branchRole.BranchRoleDescription;
                oldBranchrole.RoleTypeId = branchRole.RoleTypeId;
                oldBranchrole.PermissionRoles = branchRole.PermissionRoles;
                oldBranchrole.RoleHeads = branchRole.RoleHeads;
                oldBranchrole.IsActive = true;
                oldBranchrole.IsDeleted = false;


                BranchRoleRepository.Update(oldBranchrole);
            }
            context.SaveChanges();
            response.data = "";
            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public Object UpdateBranchRole(BranchRole branchRole)
        {
            BranchRoleRepository.Update(branchRole);
            context.SaveChanges();
            response.data = branchRole;
            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public Object DeleteBranchRole(int branchRoleId)
        {
            BranchRole branchRole = BranchRoleRepository.FindByCondition(x => x.BranchRoleId == branchRoleId)
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.User).FirstOrDefault();
            if (branchRole != null)
            {

                foreach (var userrole in branchRole.UserRoles)
                {
                    UserRoleRepository.Delete(userrole);
                    UserRepository.Delete(userrole.User);
                }
                BranchRoleRepository.Delete(branchRole);
                context.SaveChanges();
                response.data = "Deleted";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Branch Role doesn't Exist";
            }
            return response;
        }


        [AuthFilter((int)permission.ManageBranch, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public Object DeleteBranch(int branchId)
        {
            Branch branch = BranchRepository.FindByCondition(x => x.BranchId == branchId)
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.User).FirstOrDefault();
            if (branch != null)
            {
                foreach (var userrole in branch.UserRoles)
                {
                    UserRoleRepository.Delete(userrole);
                    UserRepository.Delete(userrole.User);
                }
                BranchRepository.Delete(branch);
                context.SaveChanges();
                response.data = "Deleted";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Branch doesn't Exist";
            }
            return response;
        }

    }
}
