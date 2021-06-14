using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    
    public class BlobManager: IBlobManager
    {
       

        private readonly BlobServiceClient _blobServiceClient;
        public BlobManager(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Upload(Blob File )
        {

            var blobcontainer = _blobServiceClient.GetBlobContainerClient("files");

            var blobclient = blobcontainer.GetBlobClient(File.File.FileName);
            await blobcontainer.CreateIfNotExistsAsync();
            string Content = File.File.FileName.Split('.')[1];
            BlobHttpHeaders httpHeaders = new BlobHttpHeaders();
            httpHeaders.ContentType = Content == "pdf" ? "application/" + Content : "image/" + Content;

            await blobclient.UploadAsync(File.File.OpenReadStream(),httpHeaders);
        }
    }
}
