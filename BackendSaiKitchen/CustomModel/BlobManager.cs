using Azure.Storage.Blobs;
using BackendSaiKitchen.Models;
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

        public async Task Uplaod(Blob File )
        {
            var blobcontainer = _blobServiceClient.GetBlobContainerClient("saikitchenfrontend");

            var blobclient = blobcontainer.GetBlobClient(File.File.FileName);

            await blobclient.UploadAsync(File.File.OpenReadStream(),true);
        }
    }
}
