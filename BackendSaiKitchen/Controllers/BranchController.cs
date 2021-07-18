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
            response.data = branchRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }

        //[AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetBranchRoles()
        {
            response.data = branchRoleRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }
        Dictionary<object, object> dic = new Dictionary<object, object>();
        //[AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetBranchRoleById(int branchRoleId)
        {

            var branchRole = branchRoleRepository.FindByCondition(x => x.BranchRoleId == branchRoleId && x.IsActive == true && x.IsDeleted == false).Include(obj => obj.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false)).Include(obj => obj.RoleHeads.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            //var permissionRole = permissionRoleRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Join(permissionRepository.GetAll(),
            //         permissionRole => permissionRole.PermissionId,
            //         permission => permission.PermissionId,
            //         (permissionRole, permission) => new { permissionRole, permission }).ToList();
            //var roleHead = branchRoleRepository.GetAll().Where(x => x.IsActive == true && x.IsDeleted == false).Join(roleHeadRepository.GetAll().Where(x=>x.IsActive==true && x.IsDeleted==false),
            //                    branchRole => branchRole.BranchRoleId,
            //                    roleHead => roleHead.EmployeeRoleId,
            //                    (branchRole, roleHead) => new { branchRole = branchRole, roleHead = roleHead }).Where(x=>x.branchRole.BranchRoleId==x.roleHead.EmployeeRoleId).ToList();

           var roleHeadsId= branchRole.RoleHeads.Select(x => x.HeadRoleId).ToList();
           var roleHeads = branchRoleRepository.FindByCondition(x => roleHeadsId.Contains(x.BranchRoleId));
            dic.Add("branchRole",branchRole);
            dic.Add("roleHeads",roleHeads);
            response.data = dic;
            return response;
        }

        [AuthFilter((int)permission.ManageBranch, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public Object GetBranchTypes()
        {

            var val = Request.Headers.FirstOrDefault(x => x.Key == "userId").Value.FirstOrDefault();
            response.data = branchTypeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetRoleTypes()
        {
            response.data = roleTypeRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
            return response;
        }

        [AuthFilter((int)permission.ManageBranch, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public Object AddBranch(Branch branch)
        {
            branchRepository.Create(branch);
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
                branchRoleRepository.Create(branchRole);
            }
            else
            {
              var oldBranchrole = branchRoleRepository.FindByCondition(x => x.BranchRoleId == branchRole.BranchRoleId && x.IsActive == true && x.IsDeleted == false).Include(obj => obj.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false)).Include(obj => obj.RoleHeads.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
                oldBranchrole.BranchRoleName = branchRole.BranchRoleName;
                oldBranchrole.BranchRoleDescription = branchRole.BranchRoleDescription;
                oldBranchrole.RoleTypeId = branchRole.RoleTypeId;
                oldBranchrole.PermissionRoles = branchRole.PermissionRoles;
                oldBranchrole.RoleHeads = branchRole.RoleHeads;
                oldBranchrole.IsActive = true;
                oldBranchrole.IsDeleted = false;


                branchRoleRepository.Update(oldBranchrole);
            }
            context.SaveChanges();
            response.data = "";
            //Request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public Object UpdateBranchRole(BranchRole branchRole)
        {
            branchRoleRepository.Update(branchRole);
            context.SaveChanges();
            response.data = branchRole;
            //Request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Delete)]
        [HttpPost]
        [Route("[action]")]
        public Object DeleteBranchRole(int branchRoleId)
        {
            BranchRole branchRole = branchRoleRepository.FindByCondition(x => x.BranchRoleId == branchRoleId)
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.User).FirstOrDefault();
            if (branchRole != null)
            {
                
                foreach (var userrole in branchRole.UserRoles)
                {
                    userRoleRepository.Delete(userrole);
                    userRepository.Delete(userrole.User);
                }
                branchRoleRepository.Delete(branchRole);
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
            Branch branch = branchRepository.FindByCondition(x => x.BranchId == branchId)
                .Include(x => x.UserRoles.Where(y => y.IsActive == true && y.IsDeleted == false))
                .ThenInclude(x => x.User).FirstOrDefault();
            if (branch != null)
            {
                foreach (var userrole in branch.UserRoles)
                {
                    userRoleRepository.Delete(userrole);
                    userRepository.Delete(userrole.User);
                }
                branchRepository.Delete(branch);
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
