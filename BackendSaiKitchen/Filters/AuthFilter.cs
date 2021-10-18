using BackendSaiKitchen.Models;
using BackendSaiKitchen.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Sentry;
using System;
using System.Linq;
using Constants = BackendSaiKitchen.Helper.Constants;

namespace BackendSaiKitchen.ActionFilters
{
    public class AuthFilter : ActionFilterAttribute
    {
        private readonly Repository<BranchRole> branchRoleRepository;

        public BackendSaiKitchen_dbContext db = new();
        public ServiceResponse response = new();

        public AuthFilter(int permission, int level)
        {
            this.permission = permission;
            this.level = level;
            branchRoleRepository = new Repository<BranchRole>(db);
        }

        public AuthFilter()
        {
            branchRoleRepository = new Repository<BranchRole>(db);
        }

        public int permission { get; set; }

        //private int level;
        public int level { get; set; }

        //public string resource { get; set; }
        //public string action { get; set; }


        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string v = context.HttpContext.Request.Method;
            context.HttpContext.Request.Headers.TryGetValue("BranchRoleId", out StringValues branchRoleId);
            if (branchRoleId.Count > 0)
            {
                int.TryParse(branchRoleId[0], out Constants.branchRoleId);
            }

            try
            {
                BranchRole userPermissions = branchRoleRepository.FindByCondition(x =>
                        x.BranchRoleId == Constants.branchRoleId && x.IsActive == true && x.IsDeleted == false)
                    .Include(y => y.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                    .ThenInclude(x => x.Permission)
                    .Include(y => y.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                    .ThenInclude(x => x.PermissionLevel).FirstOrDefault();
                int? userperlevel = userPermissions?.PermissionRoles.Where(x => x.PermissionId == permission)
                    ?.FirstOrDefault()?.PermissionLevelId;

                if (userperlevel == null || userperlevel - level < 0)
                {
                    response.isError = true;
                    response.errorMessage = Constants.UnAuthorizedUser;
                    context.Result = new OkObjectResult(response);
                }
            }
            catch (Exception e)
            {
                SentrySdk.CaptureMessage("BranchRoleId= " + Constants.branchRoleId + " \n" + e.Message);
                response.isError = true;
                response.errorMessage = Constants.UnAuthorizedUser;
                context.Result = new OkObjectResult(response);
            }
        }
    }
}