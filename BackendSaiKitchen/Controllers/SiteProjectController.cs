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
    public class SiteProjectController : BaseController
    {
        [HttpPost]
        [Route("[action]")]
        public object AddSiteProject(Root project)
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
                                        int workscopeId = workScopeRepository.FindByCondition(x => x.WorkScopeName.Contains(item) && x.IsActive == true && x.IsDeleted == false).Select(x => x.WorkScopeId).FirstOrDefault();
                                        scopeOfWork.Add(new VillaWorkScope 
                                        { 
                                            WorkScopeId = workscopeId,
                                            IsActive = true,
                                            IsDeleted = false,
                                            CreatedBy = Constants.userId,
                                            CreatedDate = Helper.Helper.GetDateTime()
                                        });
                                    }

                                    villas.Add(new Villa
                                    {
                                        VillaName = project.excel[2].Villa[row[i].Indexes[z]],
                                        VillaWorkScopes = scopeOfWork.ToList()
                                    });
                                } 
                            }

                            rows[i].Blocks.Add(new Block
                            {
                                BlockName = project.excel[1].Block[j],
                                Villas = villas
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
            return response;
        }
    }
}
