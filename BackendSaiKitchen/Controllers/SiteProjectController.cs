using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class SiteProjectController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object AddSiteProject(Root project)
        {
            if (project.excel.Any())
            {
                SiteProject siteProject = new SiteProject();
                List<CustomRow> row = new List<CustomRow>();
                List<int> inds = new List<int>();
                List<Row> rows = new List<Row>();

                siteProject.SiteProjectName = project.SiteProjectName;
                siteProject.SiteProjectDescription = project.SiteProjectDescription;
                siteProject.BranchId = project.BranchId;
                siteProject.SiteProjectLocation = project.SiteProjectLocation;

                string r = "";
                for (int i = 0; i < project.excel[0].Row.Count; i++)
                {
                    inds.Clear();
                    if (project.excel[0].Row[i] != r)
                    {
                        r = project.excel[0].Row[i];
                        for (int j = 0; j < project.excel[0].Row.Count; j++)
                        {
                            if (project.excel[0].Row[j] == r)
                            {
                                inds.Add(j);
                            }
                        }
                        row.Add(new CustomModel.CustomRow
                        {
                            Row = project.excel[0].Row[i],
                            Indexes = inds.ToList()
                        });

                        rows.Add(new Row
                        {
                            RowName = project.excel[0].Row[i],
                            IsActive = true,
                            IsDeleted = false,
                            CreatedBy = Constants.userId,
                            CreatedDate = Helper.Helper.GetDateTime(),
                        });
                    }
                    r = project.excel[0].Row[i];
                }

                for (int i = 0; i < row.Count; i++)
                {
                    inds = new List<int>();
                    string b = "";
                    row[i].blocks = new List<CustomBlock>();
                    for (int j = 0; j < project.excel[0].Row.Count; j++)
                    {
                        if (project.excel[0].Row[j] == row[i].Row)
                        {

                            if (b != project.excel[1].Block[j])
                            {
                                b = project.excel[1].Block[j];
                                List<CustomVilla> _Villa = new List<CustomVilla>();
                                List<Villa> villas = new List<Villa>();
                                List<VillaWorkScope> scopeOfWork = new List<VillaWorkScope>();
                                for (int z = 0; z < row[i].Indexes.Count(); z++)
                                {
                                    if (project.excel[1].Block[row[i].Indexes[z]] == b)
                                    {
                                        scopeOfWork.Clear();
                                        _Villa.Add(new CustomModel.CustomVilla
                                        {
                                            villa = project.excel[2].Villa[row[i].Indexes[z]],
                                            workScopes = project.excel[3].Workscope[row[i].Indexes[z]],
                                        });

                                        foreach (var item in project.excel[3].Workscope[row[i].Indexes[z]].Split(','))
                                        {
                                            int workscopeId = workScopeRepository.FindByCondition(x => x.WorkScopeName.Contains(item.Trim()) && x.IsActive == true && x.IsDeleted == false).Select(x => x.WorkScopeId).FirstOrDefault();
                                            if (workscopeId != 0)
                                            {
                                                scopeOfWork.Add(new VillaWorkScope
                                                {
                                                    WorkScopeId = workscopeId,
                                                    IsActive = true,
                                                    IsDeleted = false,
                                                    CreatedBy = Constants.userId,
                                                    CreatedDate = Helper.Helper.GetDateTime()
                                                });
                                            }

                                        }

                                        villas.Add(new Villa
                                        {
                                            VillaName = project.excel[2].Villa[row[i].Indexes[z]],
                                            VillaWorkScopes = scopeOfWork.ToList(),
                                            IsActive = true,
                                            IsDeleted = false,
                                            CreatedBy = Constants.userId,
                                            CreatedDate = Helper.Helper.GetDateTime(),
                                        });
                                    }
                                }

                                rows[i].Blocks.Add(new Block
                                {
                                    BlockName = project.excel[1].Block[j],
                                    Villas = villas,
                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = Constants.userId,
                                    CreatedDate = Helper.Helper.GetDateTime(),
                                });

                                row[i].blocks.Add(new CustomBlock
                                {
                                    block = project.excel[1].Block[j],
                                    villas = _Villa,
                                });
                            }
                            b = project.excel[1].Block[j];
                        }

                    }

                }
                siteProject.Rows = rows;
                siteProjectRepository.Create(siteProject);
                context.SaveChanges();
                response.data = siteProject;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Please Add Excel";
            }
            
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object AddExcelBySiteProjectId(AddExcel excel)
        {
            var site = siteProjectRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.SiteProjectId ==excel.SiteProjectId)
                .FirstOrDefault();
            if (site != null)
            {
                List<CustomRow> row = new List<CustomRow>();
                List<int> inds = new List<int>();
                List<Row> rows = new List<Row>();

                string r = "";
                for (int i = 0; i < excel.excel[0].Row.Count; i++)
                {
                    inds.Clear();
                    if (excel.excel[0].Row[i] != r)
                    {
                        r = excel.excel[0].Row[i];
                        for (int j = 0; j < excel.excel[0].Row.Count; j++)
                        {
                            if (excel.excel[0].Row[j] == r)
                            {
                                inds.Add(j);
                            }
                        }
                        row.Add(new CustomModel.CustomRow
                        {
                            Row = excel.excel[0].Row[i],
                            Indexes = inds.ToList()
                        });

                        rows.Add(new Row
                        {
                            RowName = excel.excel[0].Row[i],
                            IsActive = true,
                            IsDeleted = false,
                            CreatedBy = Constants.userId,
                            CreatedDate = Helper.Helper.GetDateTime(),
                        });
                    }
                    r = excel.excel[0].Row[i];
                }

                for (int i = 0; i < row.Count; i++)
                {
                    inds = new List<int>();
                    string b = "";
                    row[i].blocks = new List<CustomBlock>();
                    for (int j = 0; j < excel.excel[0].Row.Count; j++)
                    {
                        if (excel.excel[0].Row[j] == row[i].Row)
                        {

                            if (b != excel.excel[1].Block[j])
                            {
                                b = excel.excel[1].Block[j];
                                List<CustomVilla> _Villa = new List<CustomVilla>();
                                List<Villa> villas = new List<Villa>();
                                List<VillaWorkScope> scopeOfWork = new List<VillaWorkScope>();
                                for (int z = 0; z < row[i].Indexes.Count(); z++)
                                {
                                    if (excel.excel[1].Block[row[i].Indexes[z]] == b)
                                    {
                                        scopeOfWork.Clear();
                                        _Villa.Add(new CustomModel.CustomVilla
                                        {
                                            villa = excel.excel[2].Villa[row[i].Indexes[z]],
                                            workScopes = excel.excel[3].Workscope[row[i].Indexes[z]],
                                        });

                                        foreach (var item in excel.excel[3].Workscope[row[i].Indexes[z]].Split(','))
                                        {
                                            int workscopeId = workScopeRepository.FindByCondition(x => x.WorkScopeName.Contains(item) && x.IsActive == true && x.IsDeleted == false).Select(x => x.WorkScopeId).FirstOrDefault();
                                            if (workscopeId != 0)
                                            {
                                                scopeOfWork.Add(new VillaWorkScope
                                                {
                                                    WorkScopeId = workscopeId,
                                                    IsActive = true,
                                                    IsDeleted = false,
                                                    CreatedBy = Constants.userId,
                                                    CreatedDate = Helper.Helper.GetDateTime()
                                                });
                                            }

                                        }

                                        villas.Add(new Villa
                                        {
                                            VillaName = excel.excel[2].Villa[row[i].Indexes[z]],
                                            VillaWorkScopes = scopeOfWork.ToList()
                                        });
                                    }
                                }

                                rows[i].Blocks.Add(new Block
                                {
                                    BlockName = excel.excel[1].Block[j],
                                    Villas = villas
                                });

                                row[i].blocks.Add(new CustomBlock
                                {
                                    block = excel.excel[1].Block[j],
                                    villas = _Villa,
                                });
                            }
                            b = excel.excel[1].Block[j];
                        }

                    }

                }
                site.Rows = rows;
                siteProjectRepository.Update(site);
                context.SaveChanges();
                response.data = site    ;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "SiteProject Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetSiteProjectByBranchId(int BranchId)
        {
            var sites = siteProjectRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.BranchId == BranchId).Select(x => new
            {
                x.SiteProjectId,
                x.SiteProjectName,
                x.SiteProjectDescription,
                x.SiteProjectComment,
                x.SiteProjectFile,
                x.SiteProjectLocation,
                x.SiteProjectIsOnHold,
                x.SiteProjectStatusId,
            }).ToList();
            response.data = sites;
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetSiteProjectDetailbyId(int ProjectSiteId)
        {
            var site = siteProjectRepository.FindByCondition(x => x.SiteProjectId == ProjectSiteId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Rows.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.Blocks.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.Villas.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.VillaWorkScopes.Where(x => x.IsActive == true && x.IsDeleted == false))
                .ThenInclude(x => x.WorkScope).FirstOrDefault();
            if (site != null)
            {
                response.data = site;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "SiteProject Not Found";
            }
            return response;
        }
    }
}
