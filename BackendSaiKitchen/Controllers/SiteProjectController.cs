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
            List<Block> blocks = new List<Block>();
            for (int j = 0; j < project.excel[0].Row.Count(); j++)
            {
                var r = project.excel[0].Row.Where(x => x.Contains(project.excel[0].Row[j])).FirstOrDefault();
                var b = project.excel[1].Block.Where(x => x.Contains(project.excel[1].Block[j])).FirstOrDefault();
                var v = project.excel[2].Villa.Where(x => x.Contains(project.excel[2].Villa[j])).FirstOrDefault();
                var w = project.excel[3].Workscope.Where(x => x.Contains(project.excel[3].Workscope[j])).FirstOrDefault();
                //if (j != 0)
                //{


                //    var B = blocks.Where(x => x.block.Contains(b)).FirstOrDefault();
                //    if (b != null)
                //    {
                //        var V = B.Villas.Where(x => x.villa.Contains(v)).FirstOrDefault();
                //        if (V != null)
                //        {
                //            V.workScopes.Add(new workScope { workscope = w });
                //        }
                //    }

                //}
                //else
                //{
                //    blocks.Add
                //}
            }
            //i = project.excel[0].Row.FindIndex(x => x.Contains(project.excel[0].Row[0]));
            response.data = blocks;
            return response;
        }
    }
}
