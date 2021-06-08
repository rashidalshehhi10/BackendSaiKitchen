using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

      /*  [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Add_Updatefiles(byte[] files)
        {
            //foreach (var File in files)
            //{

                if (files != null)
                {
                    var stream = new MemoryStream(files);
                //string s = Convert.ToBase64String(files).Substring(0,5);
                string exet = Helper.Helper.GuessFileType(files);
                switch (s.ToUpper())
                {
                    case "IVBOR":
                        exet = "png";
                        break;
                    case "/9J/4":
                        exet = "jpg";
                        break;
                    case "AAAAF":
                        exet = "mp4";
                        break;
                    case "JVBER":
                        exet = "pdf";
                        break;
                    case "AAABA":
                        exet= "ico";
                        break;
                    case "UMFYI":
                        exet = "rar";
                        break;
                    case "E1XYD":
                        exet = "rtf";
                        break;
                    case "U1PKC":
                        exet = "txt";
                        break;
                    case "MQOWM":
                    case "77U/M":
                        exet = "srt";
                        break;
                    default:
                        s = "";
                        break;
                }
                IFormFile blob = new FormFile(stream, 0, files.Length, "Name."+ exet, "FileName."+exet);

                    if (exet == "png" || exet == "jpg" || exet == "application/pdf")
                    {
                        await _blobManager.Uplaod(new Blob() { File = blob });
                    }
                    else
                    {
                        response.isError = true;
                        response.errorMessage = "you can only upload type jpeg, png or pdf";
                    }

                }
           // }
            return Ok();
        }*/

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Add_Updatefile([FromForm]Blob File)
        {
                if (File.File != null)
                {
                    if (File.File.ContentType == "image/png" || File.File.ContentType == "image/jpeg" || File.File.ContentType == "application/pdf")
                    {
                        await _blobManager.Uplaod(File);
                    }
                    else
                    {
                        response.isError = true;
                        response.errorMessage = "you can only upload type jpeg,png or pdf";
                    }
            }
            return Ok();
        }
    }
}
