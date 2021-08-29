using BackendSaiKitchen.CustomModel;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System.IO;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class FileController : BaseController
    {
        public FileController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }

        //[HttpPost]
        //[Route("[action]")]
        //public async Task<object> TestUpload(byte[] blob)
        //{
        //    try
        //    {
        //        var Url = await Helper.Helper.PostFile(blob, "pdf");
        //        response.data = Url;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.isError = true;
        //        response.errorMessage = ex.Message;

        //    }
        //    return response;
        //}
        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> UploadFile()
        {
            foreach (var file in Request.Form.Files)
            {
                var FileDataContent = file;
                //var FileDataContent = Request.Form.Files["file"];
                if (FileDataContent != null && FileDataContent.Length > 0)
                {
                    // take the input stream, and save it to a temp folder using  
                    // the original file.part name posted  
                    var stream = FileDataContent.OpenReadStream();
                    var fileName = Path.GetFileName(FileDataContent.FileName);

                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);

                    response.data = await Helper.Helper.UploadFormDataFile(ms.ToArray(), FileDataContent.ContentType);

                    //var UploadPath = Server.MapPath("~/App_Data/uploads");
                    //Directory.CreateDirectory(UploadPath);
                    //string path = Path.Combine(UploadPath, fileName);
                    //try
                    //{
                    //    if (System.IO.File.Exists(path))
                    //        System.IO.File.Delete(path);
                    //    using (var fileStream = System.IO.File.Create(path))
                    //    {
                    //        stream.CopyTo(fileStream);
                    //    }
                    //    // Once the file part is saved, see if we have enough to merge it  
                    //    Shared.Utils UT = new Shared.Utils();
                    //    UT.MergeFile(path);
                    //}
                    //catch (IOException ex)
                    //{
                    //    // handle  
                    //}
                }
            }
            return response;
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> TestUploadFile()
        {
            foreach (var file in Request.Form.Files)
            {
                var FileDataContent = file;
                if (FileDataContent != null && FileDataContent.Length > 0)
                {
                    var stream = FileDataContent.OpenReadStream();
                    var fileName = Path.GetFileName(FileDataContent.FileName);

                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);

                    response.data = await Helper.Helper.UploadFormDataFile(ms.ToArray(), FileDataContent.ContentType);


                }
            }
            return response;
        }
    }
}
