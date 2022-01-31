using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class CommercialProjectController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object AddCommercialProject(CustomCommercialProject project)
        {
            CommercialProject commercial = new CommercialProject();
            commercial.CommercialProjectName = project.projectname;
            commercial.CommercialProjectStartDate = Helper.Helper.GetDateTime();
            commercial.CommercialProjectLocation = project.location;
            commercial.CommercialProjectNo = project.jobno;
            commercial.CommercialProjectDesription = project.projectdescription;
            commercial.BranchId = Constants.branchId;
            foreach (var apartment in project.excel)
            {
                commercial.Apartments.Add(new Apartment
                {
                    ApartmentName = apartment.ApartmentName
                });
            }
            foreach (var work in project.scopeofWork)
            {
                if (work.WorkScopeId != 0)
                {
                    commercial.ProjectDetails.Add(new ProjectDetail
                    {
                        MaterialId = work.MaterialId,
                        SizeId = work.SizeId,
                        WorkScopeId = work.WorkScopeId,
                        Quantity = work.Quantity,
                    });
                }  
            }
            commercialProjectRepository.Create(commercial);
            context.SaveChanges();
            response.data = commercial;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllCommercialProjects()
        {
            var project = commercialProjectRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == Constants.branchId).Select(x => new
            {
                x.CommercialProjectId,
                x.CommercialProjectName,
                x.CommercialProjectNo,
                x.CommercialProjectDesription,
                x.CommercialProjectStartDate,
                x.ProjectStatusId,
                x.CommercialProjectLocation,
                x.ProjectStatus.ProjectStatusName,
                x.Apartments,
                x.BranchId,
                x.Branch.BranchName
            }).ToList();

            response.data = project;
            return response;

        }

        [HttpPost]
        [Route("[action]")]
        public object GetCommercialProjectById(int CommercialProjectId)
        {
            var project = commercialProjectRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.CommercialProjectId == CommercialProjectId && x.BranchId == Constants.branchId).Select(x => new
            {
                x.CommercialProjectId,
                x.CommercialProjectName,
                x.CommercialProjectNo,
                x.CommercialProjectDesription,
                x.CommercialProjectStartDate,
                x.ProjectStatusId,
                x.CommercialProjectLocation,
                x.ProjectStatus.ProjectStatusName,
                x.Apartments,
                x.BranchId,
                x.Branch.BranchName
            }).FirstOrDefault();
            if (project != null)
            {
                response.data = project;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Commercial project Not Found";
            }
            return response;
        }
    }
}
