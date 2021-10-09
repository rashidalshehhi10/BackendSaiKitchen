﻿using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using BackendSaiKitchen.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace BackendSaiKitchen.ActionFilters
{
    public class AuthFilter : ActionFilterAttribute
    {
        private ServiceResponse response = new ServiceResponse();
        private int permission { get; set; }
        private int level { get; set; }
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




        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Do Nothing
        }

        public BackendSaiKitchen_dbContext db = new BackendSaiKitchen_dbContext();
        Repository<BranchRole> branchRoleRepository;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var v = context.HttpContext.Request.Method;
            StringValues branchRoleId;
            context.HttpContext.Request.Headers.TryGetValue("BranchRoleId", out branchRoleId);
            if (branchRoleId.Count > 0)
            {
                int.TryParse(branchRoleId[0], out Constants.branchRoleId);
            }
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
            catch (Exception e)
            {
                Sentry.SentrySdk.CaptureMessage("BranchRoleId= " + Constants.branchRoleId + " \n" + e.Message);
                response.isError = true;
                response.errorMessage = Constants.UnAuthorizedUser;
                context.Result = new OkObjectResult(response);
            }
        }
    }
}
