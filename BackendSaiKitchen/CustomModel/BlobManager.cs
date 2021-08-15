using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BackendSaiKitchen.Models;
using System.IO;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{

    public class BlobManager : IBlobManager
    {


        private readonly BlobServiceClient _blobServiceClient;
        public BlobManager(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Upload(Blob File)
        {

            var blobcontainer = _blobServiceClient.GetBlobContainerClient("files");

            var blobclient = blobcontainer.GetBlobClient(File.File.FileName);
            await blobcontainer.CreateIfNotExistsAsync();
            string Content = File.File.FileName.Split('.')[1];
            BlobHttpHeaders httpHeaders = new BlobHttpHeaders();
            httpHeaders.ContentType = Content == "pdf" ? "application/" + Content : "image/" + Content;

            await blobclient.UploadAsync(File.File.OpenReadStream(), httpHeaders);
        }

        public async Task<byte[]> Read(string fileName)
        {
            var blobcontainer = _blobServiceClient.GetBlobContainerClient("files");

            var blobclient = blobcontainer.GetBlobClient(fileName);
            var imgDownload = await blobclient.DownloadAsync();
            using (MemoryStream ms =new MemoryStream())
            {
                await imgDownload.Value.Content.CopyToAsync(ms);
                return ms.ToArray();

            }
        }
    }
}
