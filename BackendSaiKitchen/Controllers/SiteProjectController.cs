using BackendSaiKitchen.CustomModel;
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
            List<object> list = new List<object>();
            List<row> row = new List<row>();
            List<int> inds = new List<int>();
            List<int> blkInds = new List<int>();
            List<Block> block = new List<Block>();

            //var r = project.excel[0].Row.Where(x => x.Contains(project.excel[0].Row[j])).FirstOrDefault();
            //var b = project.excel[1].Block.Where(x => x.Contains(project.excel[1].Block[j])).FirstOrDefault();
            //var v = project.excel[2].Villa.Where(x => x.Contains(project.excel[2].Villa[j])).FirstOrDefault();
            //var w = project.excel[3].Workscope.Where(x => x.Contains(project.excel[3].Workscope[j])).FirstOrDefault();

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
                    row.Add(new CustomModel.row
                    {
                        Row = project.excel[0].Row[i],
                        Indexes = inds.ToList()
                    });
                }
                r = project.excel[0].Row[i];
            }
                
            for (int i = 0; i < row.Count; i++)
            {
                inds = new List<int>();
                string b = "";
                row[i].blocks = new List<Block>();
                for (int j = 0; j < project.excel[0].Row.Count; j++)
                {
                    if (project.excel[0].Row[j] == row[i].Row)
                    {
                        
                        if (b != project.excel[1].Block[j])
                        {
                            b = project.excel[1].Block[j];
                            List<Villa> Villa = new List<Villa>();

                            for (int z = 0; z < row[i].Indexes.Count(); z++)
                            {
                                if (project.excel[1].Block[row[i].Indexes[z]] == b)
                                {
                                    Villa.Add(new CustomModel.Villa
                                    {
                                        villa = project.excel[2].Villa[row[i].Indexes[z]],
                                        workScopes = project.excel[3].Workscope[row[i].Indexes[z]],
                                    });
                                } 
                            }
                                
                            row[i].blocks.Add(new Block
                            {
                                block = project.excel[1].Block[j],
                                villas = Villa,
                            });
                        }
                        b = project.excel[1].Block[j];
                    }

                }

                list.Add(new
                {
                    row = row[i],
                    indexes = inds.ToList(),
                    blocks = block.ToList()
                });
            }

            response.data = row;
            return response;
        }
    }
}
