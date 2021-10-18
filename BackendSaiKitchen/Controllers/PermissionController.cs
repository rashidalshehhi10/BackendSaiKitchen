using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    public class PermissionController : BaseController
    {
        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Read)]
        [HttpGet]
        [Route("[action]")]
        public object GetPermissions()
        {

            response.data = permissionRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();

            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public object AddPermission(Permission permission)
        {

            permissionRepository.Create(permission);
            context.SaveChanges();
            response.data = permission;

            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Create)]
        [HttpPost]
        [Route("[action]")]
        public object AddPermissionRole(PermissionRole permissionRole)
        {
            permissionRoleRepository.Create(permissionRole);
            context.SaveChanges();
            response.data = permissionRole;
            //Request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
            return response;
        }

        [AuthFilter((int)permission.ManageBranchRole, (int)permissionLevel.Update)]
        [HttpPost]
        [Route("[action]")]
        public object UpdatePermissionRole(PermissionRole permissionRole)
        {
            permissionRoleRepository.Update(permissionRole);
            context.SaveChanges();
            response.data = permissionRole;
            //Request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
            return response;
        }

    }
}
