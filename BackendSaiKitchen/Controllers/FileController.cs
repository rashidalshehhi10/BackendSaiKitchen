using BackendSaiKitchen.CustomModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
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
            
            foreach (Microsoft.AspNetCore.Http.IFormFile file in Request.Form.Files)
            {
                Microsoft.AspNetCore.Http.IFormFile FileDataContent = file;
                //var FileDataContent = Request.Form.Files["file"];
                if (FileDataContent != null && FileDataContent.Length > 0)
                {
                    // take the input stream, and save it to a temp folder using  
                    // the original file.part name posted  
                    Stream stream = FileDataContent.OpenReadStream();
                    string fileName = Path.GetFileName(FileDataContent.FileName);

                    string ContentType = FileDataContent.ContentType.Contains('.')
                        ? FileDataContent.ContentType.Split('.')[1]
                        : FileDataContent.ContentType.Contains("application")
                            ? fileName.Split('.')[1]
                            : FileDataContent.ContentType;

                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);

                    response.data = await Helper.Helper.UploadFormDataFile(ms.ToArray(), ContentType);

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

        [HttpPost]
        [Route("[action]")]
        public async Task<object> Delete(string FileUrl)
        {
            if (FileUrl != null)
            {
                response.data = await Helper.Helper.DeleteFile(FileUrl);
                Models.File file = await fileRepository
                    .FindByCondition(x => x.FileUrl == FileUrl && x.IsActive == true && x.IsDeleted == false)
                    .FirstOrDefaultAsync();
                if (file != null)
                {
                    file.IsDeleted = true;
                    file.IsActive = false;
                    fileRepository.Update(file);
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "File Not Found";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> DeleteVideo(long VideoId)
        {
            if (VideoId > 0)
            {
                response.data = await Helper.Helper.DeleteVideo(VideoId);
                Models.File video = await fileRepository
                    .FindByCondition(x => x.FileUrl == VideoId.ToString() && x.IsActive == true && x.IsDeleted == false)
                    .FirstOrDefaultAsync();
                if (video != null)
                {
                    video.IsActive = false;
                    video.IsDeleted = true;
                    fileRepository.Update(video);
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Video Not Deleted";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> DeleteFileFromBlob(string fileName)
        {
            if (fileName != null)
            {
                response.data = await Helper.Helper.DeleteFileFromBlob(fileName);
                Models.File file = await fileRepository
                    .FindByCondition(x => x.FileUrl == fileName && x.IsActive == true && x.IsDeleted == false)
                    .FirstOrDefaultAsync();
                if (file != null)
                {
                    file.IsDeleted = true;
                    file.IsActive = false;
                    fileRepository.Update(file);
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "File Not Deleted";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetFile(string FileName)
        {
            if (FileName != null)
            {
                long s;
                try
                {
                    if (!long.TryParse(FileName, out s))
                    {
                        var file = await Helper.Helper.GetFile(FileName);
                        response.data = file;
                    }
                }
                catch (Exception ex)
                {
                    response.isError = true;
                    response.errorMessage = ex.Message;
                }

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Please Enter The File Name";
                
            }
            return response;
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Test(String Vid)
        {
            try
            {
                response.data = Helper.Helper.GetVimeoVideoDownloadURL(Vid);

            }
            catch (Exception e)
            {
                response.isError = true;
                response.errorMessage = e.Message;

            }
            return Ok();
            
        }
    }
}