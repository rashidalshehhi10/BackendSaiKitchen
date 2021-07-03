﻿using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using BackendSaiKitchen.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.ActionFilters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public int permission { get; set; }
        //private int level;
        public int level { get; set; }
        public AuthFilter(int permission,int level)
        {
            this.permission = permission;
            this.level = level;
        }

        //public string resource { get; set; }
        //public string action { get; set; }



        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var v = context.HttpContext.Request.Method;
            BackendSaiKitchen_dbContext db = new BackendSaiKitchen_dbContext();
            Repository<BranchRole> branchRoleRepository = new Repository<BranchRole>(db);
            
            try
            {
                var userPermissions = branchRoleRepository.FindByCondition(x => x.BranchRoleId == 1047 && x.IsActive == true && x.IsDeleted == false)
                                    .Include(y => y.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                                    .ThenInclude(x => x.Permission)
                                    .Include(y => y.PermissionRoles.Where(x => x.IsActive == true && x.IsDeleted == false))
                                    .ThenInclude(x => x.PermissionLevel).FirstOrDefault();

                var userperlevel = userPermissions.PermissionRoles.Where(x => x.PermissionId == permission).FirstOrDefault().PermissionLevelId;

                if (userperlevel - level < 0)
                    context.Result = new UnauthorizedResult();
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult(); 
            }
            
            
                
                

        }
    }
}
