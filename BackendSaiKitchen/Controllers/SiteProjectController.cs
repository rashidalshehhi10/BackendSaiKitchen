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
            //foreach (var item in  )
            //{
            //   i = item.(x => x.Contains(item.Row[0]));

            //}
            List<object> list = new List<object>();
            List<string> rows = new List<string>();
            List<row> row = new List<row>();
            List<int> inds = new List<int>();
            List<string> block = new List<string>();

            //var r = project.excel[0].Row.Where(x => x.Contains(project.excel[0].Row[j])).FirstOrDefault();
            //var b = project.excel[1].Block.Where(x => x.Contains(project.excel[1].Block[j])).FirstOrDefault();
            //var v = project.excel[2].Villa.Where(x => x.Contains(project.excel[2].Villa[j])).FirstOrDefault();
            //var w = project.excel[3].Workscope.Where(x => x.Contains(project.excel[3].Workscope[j])).FirstOrDefault();

            string r = "";
            for (int i = 0; i < project.excel[0].Row.Count; i++)
            {
                if (project.excel[0].Row[i] != r)
                {
                    rows.Add(project.excel[0].Row[i]);
                }
                r = project.excel[0].Row[i];
            }
                
            for (int i = 0; i < rows.Count; i++)
            {
                inds.Clear();
                inds = new List<int>();
                string b = "";
                for (int j = 0; j < project.excel[0].Row.Count; j++)
                {
                    if (project.excel[0].Row[j] == rows[i])
                    {
                        inds.Add(j);
                        row.Add(new CustomModel.row
                        {
                            Row = rows[i],

                        });
                        if (b != project.excel[1].Block[j])
                        {
                            block.Add(project.excel[1].Block[j]);
                        }
                        b = project.excel[1].Block[j];
                    }

                }

                list.Add(new
                {
                    row = rows[i],
                    indexes = inds.ToList(),
                    blocks = block.ToList()
                });
            }
            

                    //var B = blocks.FindIndex(x => x.block.Contains(b));
                    //if (b != null)
                    //{
                    //    var V = B.Villas.Where(x => x.villa.Contains(v)).FirstOrDefault();
                    //    if (V != null)
                    //    {
                    //        V.workScopes.Add(new workScope { workscope = w });
                    //    }
                    //}

                
                //else
                //{
                //    blocks.Add
              
            //i = project.excel[0].Row.FindIndex(x => x.Contains(project.excel[0].Row[0]));
            response.data = list;
            return response;
        }
    }
}
