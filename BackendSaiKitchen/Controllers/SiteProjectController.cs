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
        public object AddSiteProject(CustomSiteProject project)
        {
            response.data = project;
            return response;
        }
    }
}
