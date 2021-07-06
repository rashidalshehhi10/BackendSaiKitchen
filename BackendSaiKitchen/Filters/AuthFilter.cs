using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using BackendSaiKitchen.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackendSaiKitchen.ActionFilters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public ServiceResponse response = new ServiceResponse();
        public int permission { get; set; }
        //private int level;
        public int level { get; set; }
        public AuthFilter(int permission,int level)
        {
            this.permission = permission;
            this.level = level; 
            branchRoleRepository = new Repository<BranchRole>(db);
        }
        public AuthFilter()
        {
            branchRoleRepository = new Repository<BranchRole>(db);
        }

        //public string resource { get; set; }
        //public string action { get; set; }



        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public BackendSaiKitchen_dbContext db = new BackendSaiKitchen_dbContext();
        Repository<BranchRole> branchRoleRepository;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var v = context.HttpContext.Request.Method;
            StringValues branchRoleId;
            context.HttpContext.Request.Headers.TryGetValue("BranchRoleId", out branchRoleId);
            if (branchRoleId.Count > 0)
                int.TryParse(branchRoleId[0], out Constants.branchRoleId);
            try
            {
                var userPermissions = branchRoleRepository.FindByCondition(x => x.BranchRoleId == Constants.branchRoleId && x.IsActive == true && x.IsDeleted == false)
                                    .Include(y => y.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                                    .ThenInclude(x => x.Permission)
                                    .Include(y => y.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                                    .ThenInclude(x => x.PermissionLevel).FirstOrDefault();
                var userperlevel = userPermissions?.PermissionRoles.Where(x => x.PermissionId == permission)?.FirstOrDefault()?.PermissionLevelId;

                if (userperlevel == null || userperlevel - level < 0)
                {

                    response.isError = true;
                    response.errorMessage = Constants.UnAuthorizedUser;
                    context.Result = new OkObjectResult(response);
                }
            }
            catch (Exception)
            {
                response.isError = true;
                response.errorMessage = Constants.UnAuthorizedUser;
                context.Result = new OkObjectResult(response);
            }
        }
    }
}
