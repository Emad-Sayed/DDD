using Application.Common.Interfaces;
using Application.Common.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BlobStorage.AzureBS.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private BlobContainerClient _blobContainerClient;

        public PhotoRepository(AzureConfigurations azureConfigurations)
        {
            _blobContainerClient = new BlobContainerClient(azureConfigurations.ConnectionString, azureConfigurations.BlobContainerName);
            _blobContainerClient.CreateIfNotExists();
        }

        public async Task<string> UploadPhoto(IFormFile photo)
        {
            var photoName =  Guid.NewGuid().ToString();
            var extension = Path.GetExtension(photo.FileName);

            var photoPath = photoName + extension;

            BlobClient blobClient = _blobContainerClient.GetBlobClient(photoPath);

            using (Stream uploadFileStream = photo.OpenReadStream())
            {
                await blobClient.UploadAsync(uploadFileStream);
            }

            return photoPath;
        }

        public void DeletePhoto(string name)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(name);
            blobClient.Delete();
        }
    }
}
