using BackendSaiKitchen.CustomModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("[action]")]
        public async Task<object> UploadFile()
        {
            foreach (var file in Request.Form.Files)
            {
                var FileDataContent = file;
                if (FileDataContent != null && FileDataContent.Length > 0)
                {  
                    var stream = FileDataContent.OpenReadStream();
                    var fileName = Path.GetFileName(FileDataContent.FileName);

                    var ContentType = FileDataContent.ContentType.Contains('.') ? FileDataContent.ContentType.Split('.')[1] : (FileDataContent.ContentType.Contains("application") ? fileName.Split('.')[1] : FileDataContent.ContentType);

                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);

                    response.data = await Helper.Helper.UploadFormDataFile(ms.ToArray(), ContentType);

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
                var file = await FileRepository.FindByCondition(x => x.FileUrl == FileUrl && x.IsActive == true && x.IsDeleted == false).FirstOrDefaultAsync();
                if (file != null)
                {
                    file.IsDeleted = true;
                    file.IsActive = false;
                    FileRepository.Update(file);
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
                var video = await FileRepository.FindByCondition(x => x.FileUrl == VideoId.ToString() && x.IsActive == true && x.IsDeleted == false).FirstOrDefaultAsync();
                if (video != null)
                {
                    video.IsActive = false;
                    video.IsDeleted = true;
                    FileRepository.Update(video);
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
                var file = await FileRepository.FindByCondition(x => x.FileUrl == fileName && x.IsActive == true && x.IsDeleted == false).FirstOrDefaultAsync();
                if (file != null)
                {
                    file.IsDeleted = true;
                    file.IsActive = false;
                    FileRepository.Update(file);
                }

            }
            else
            {
                response.isError = true;
                response.errorMessage = "Video Not Deleted";
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
