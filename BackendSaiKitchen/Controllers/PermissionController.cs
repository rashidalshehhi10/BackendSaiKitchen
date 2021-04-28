using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SaiKitchenBackend.Controllers
{
    public class PermissionController : BaseController
    {

        [HttpGet]
        [Route("[action]")]
        public Object GetPermissions()
        {

            response.data = permissionRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public Object AddPermission(Permission permission)
        {

            permissionRepository.Create(permission);
            context.SaveChanges();
            response.data = permission;

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public Object AddPermissionRole(PermissionRole permissionRole)
        {
            permissionRoleRepository.Create(permissionRole);
            context.SaveChanges();
            response.data = permissionRole;
            //Request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public Object UpdatePermissionRole(PermissionRole permissionRole)
        {
            permissionRoleRepository.Update(permissionRole);
            context.SaveChanges();
            response.data = permissionRole;
            //Request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
            return response;
        }

    }
}
