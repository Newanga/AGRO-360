using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace Agro360.Utility
{
    public class Blob
    {
        private static CloudBlobClient _blobClient;
        private static CloudBlobContainer _blobContainer;
        private const  string storageConn = "";


        public static async Task<List<Uri>> GetAllContainersImages(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConn);

            // Create a blob client for interacting with the blob service.
            _blobClient = storageAccount.CreateCloudBlobClient();

            _blobContainer = _blobClient.GetContainerReference(containerName);

            List<Uri> allBlobs = new List<Uri>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var response = await _blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                        allBlobs.Add(blob.Uri);
                }
                blobContinuationToken = response.ContinuationToken;
            } while (blobContinuationToken != null);

            return allBlobs;
        }
        public static async Task DeleteContainerAsync(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConn);

            // Create a blob client for interacting with the blob service.
            _blobClient = storageAccount.CreateCloudBlobClient();

            //Get this from the top Constructor
            _blobContainer = _blobClient.GetContainerReference(containerName);

            try
            {
                // Delete the specified container 
                await _blobContainer.DeleteIfExistsAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }


        }

        public static async Task UploadImageAsync(IEnumerable<IFormFile> HarvestImages,string harvestReportId)
        {
            await CreateContainerAsync(harvestReportId);

            foreach (var image in HarvestImages)
            {    
                try
                {
                    byte[] result = null;
                    using (var fileStream = image.OpenReadStream())
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        result = memoryStream.ToArray();
                    }


                    var blobContainer = _blobClient.GetContainerReference(harvestReportId);
                    var blob = blobContainer.GetBlockBlobReference(image.FileName);
                    blob.Properties.ContentType = image.ContentType;
                    await blob.UploadFromByteArrayAsync(result, 0, result.Length);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

        }
        private static async Task CreateContainerAsync(string newContainerName)
        {
            try
            {
                // Retrieve storage account information from connection string
                var storageAccount = CloudStorageAccount.Parse(storageConn);


                // Create a blob client for interacting with the blob service.
                _blobClient = storageAccount.CreateCloudBlobClient();

                //Creating a new blob container by providing a name
                _blobContainer =  _blobClient.GetContainerReference(newContainerName);
                await _blobContainer.CreateIfNotExistsAsync();
                await _blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            }
            catch (Exception exception)
            {
                throw exception;
            }


        }
    }


}

