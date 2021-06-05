using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class BlobController : BaseController
    {
        private readonly IBlobManager _blobManager;
        public BlobController(IBlobManager blobManager)
        {
            _blobManager = blobManager;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Add_Updatefile([FromForm]Blob File,File file)
        {
            
            if (File.File!=null)
            {
                if (File.File.ContentType == "image/png" || File.File.ContentType == "image/jpeg" || File.File.ContentType == "application/pdf")
                {
                    await _blobManager.Uplaod(File);

                    fileRepository.Create(file);
                    context.SaveChanges();
                    response.data = file;
                }
                else
                {
                    response.isError = true;
                    response.errorMessage="you can only upload type jpeg,png or pdf";
                }
               
            }
           
            return Ok();
        }
    }
}
