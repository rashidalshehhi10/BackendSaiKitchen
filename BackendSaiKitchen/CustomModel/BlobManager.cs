using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BackendSaiKitchen.Models;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            BlobContainerClient blobcontainer = _blobServiceClient.GetBlobContainerClient("files");


            BlobClient blobclient = blobcontainer.GetBlobClient(File.File.FileName);
            await blobcontainer.CreateIfNotExistsAsync();
            string Content = File.File.FileName.Split('.')[1];
            BlobHttpHeaders httpHeaders = new BlobHttpHeaders
            {
                ContentType = Content
            };

            await blobclient.UploadAsync(File.File.OpenReadStream(), httpHeaders);
        }

        public async Task PostAsync(Blob File)
        {
            string Conn =
                "DefaultEndpointsProtocol=https;AccountName=saikitchenstorage;AccountKey=3T0N76fi775rEzIVVWkx1mb89luBAjrbpr4znDtF0Ca/j6by/5ecteMWzodOeqH9C8MunRyC8iuVqhGJ40R9Gw==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Conn);
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer Container = blobClient.GetContainerReference("files");
            await Container.CreateIfNotExistsAsync();
            string Content = File.File.FileName.Split('.')[1];
            Content = Content.Contains('.') ? Content.Split('.')[1] : Content;
            Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob blob = Container.GetBlockBlobReference(File.File.FileName);
            blob.Properties.ContentType = Content == "pdf" ? "application/" + Content :
                Content == "dwg" ? "application/octet-stream" : "image/" + Content;

            HashSet<string> blocklist = new HashSet<string>();
            Microsoft.AspNetCore.Http.IFormFile file = File.File;
            const int pageSizeInBytes = 10485760;
            long prevLastByte = 0;
            long bytesRemain = file.Length;

            byte[] bytes;

            using (MemoryStream ms = new MemoryStream())
            {
                Stream fileStream = file.OpenReadStream();
                await fileStream.CopyToAsync(ms).ConfigureAwait(false);
                bytes = ms.ToArray();
            }

            do
            {
                long bytesToCopy = Math.Min(bytesRemain, pageSizeInBytes);
                byte[] bytesToSend = new byte[bytesToCopy];

                Array.Copy(bytes, prevLastByte, bytesToSend, 0, bytesToCopy);
                prevLastByte += bytesToCopy;
                bytesRemain -= bytesToCopy;

                //create blockId
                string blockId = Guid.NewGuid().ToString();
                string base64BlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(blockId));

                await blob.PutBlockAsync(
                    base64BlockId,
                    new MemoryStream(bytesToSend, true),
                    null
                );

                blocklist.Add(base64BlockId);
            } while (bytesRemain > 0);

            //post blocklist
            await blob.PutBlockListAsync(blocklist);
        }

        public async Task<byte[]> Read(string fileName)
        {
            BlobContainerClient blobcontainer = _blobServiceClient.GetBlobContainerClient("files");

            BlobClient blobclient = blobcontainer.GetBlobClient(fileName);
            Azure.Response<BlobDownloadInfo> imgDownload = await blobclient.DownloadAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                await imgDownload.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task Delete(string FileName)
        {
            BlobContainerClient blobcontainer = _blobServiceClient.GetBlobContainerClient("files");
            BlobClient blobclient = blobcontainer.GetBlobClient(FileName);
            if (blobclient != null)
            {
                try
                {
                    await blobclient.DeleteAsync();
                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
                }
            }
            
        }
    }
}