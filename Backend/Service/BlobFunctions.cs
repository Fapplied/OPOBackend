using System.Reflection.Metadata;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Backend.Models;

namespace Backend.Service
{

    public static class BlobFunctions
    {
        private const string ConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=opoblob;AccountKey=kiXPGP0w1tBVyvXY/kWTX9QdSjSsFugRaj0dau/MQ+MNSy9/FhVEoUYQKPDMPHN4TIjul5FWnHYR+AStCaeqVA==;EndpointSuffix=core.windows.net";

        private const string noContainer = "No container found";

        private const string noFilePresent = "No picture present for userid ";
        public static async Task<string> Post(IFormFile formFile, int userid)
        {
            var fileType = formFile.FileName.Split('.').Last();
            var fileTypeLength = fileType.Length;

            if (fileTypeLength is > 4 or < 3)
            {
                return "This extension is not supported";
            }
            var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;
            BlobContainerClient containerClient = new BlobContainerClient(ConnectionString, "profilepictures");

            var containerExists = await containerClient.ExistsAsync();

            if (containerExists.Value is false)
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);

                await blobServiceClient.CreateBlobContainerAsync("profilepictures");
            }

            await Delete(userid);

            var blobClient = containerClient.GetBlobClient(userid+"." + fileType);

            await blobClient.UploadAsync(stream, true);
            
            return blobClient.Uri.AbsoluteUri;
        }

        public static async Task<string?> Get(int userId)
        {
            BlobContainerClient containerClient = new BlobContainerClient(ConnectionString, "profilepictures");

            var containerExists = await containerClient.ExistsAsync();

            if (containerExists.Value is false)
            {
                return noContainer;
            }
            var res = containerClient.GetBlobsAsync();
            var resList = new List<string?>();
            await foreach (var blobItem in res)
            {
                if (blobItem.Name.Split('.')[0].Equals(userId.ToString()))
                {
                    resList.Add(blobItem.Name);
                }
            }
            
            if (resList.Any())
            {
                return resList.First();
            }
            
            return noFilePresent+userId;
        }
        
        public static async Task<string> Delete(int userId)
        {
            BlobContainerClient containerClient = new BlobContainerClient(ConnectionString, "profilepictures");

            var containerExists = await containerClient.ExistsAsync();

            if (containerExists.Value is false)
            {
                return noContainer;
            }
            
            var res = containerClient.GetBlobsAsync();
            var resList = new List<string>();
            
            await foreach (var blobItem in res)
            {
                if (blobItem.Name.Split('.')[0].Equals(userId.ToString()))
                {
                    resList.Add(blobItem.Name);
                }
            }
            
            if (resList.Any())
            {
                foreach (var fileName in resList)
                {
                    await containerClient.GetBlobClient(fileName).DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                }
                return "Deleted for userid "+userId;
            }

            return "Not Found";
        }
    }
}