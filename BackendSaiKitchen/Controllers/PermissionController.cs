using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    public class PermissionController : BaseController
    {
        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public Object GetPermissions()
        {

            response.data = PermissionRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();

            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public Object AddPermission(Permission permission)
        {

            PermissionRepository.Create(permission);
            context.SaveChanges();
            response.data = permission;

            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public Object AddPermissionRole(PermissionRole permissionRole)
        {
            PermissionRoleRepository.Create(permissionRole);
            context.SaveChanges();
            response.data = permissionRole;
            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public Object UpdatePermissionRole(PermissionRole permissionRole)
        {
            PermissionRoleRepository.Update(permissionRole);
            context.SaveChanges();
            response.data = permissionRole;
            //Request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
            return response;
        }

    }
}
