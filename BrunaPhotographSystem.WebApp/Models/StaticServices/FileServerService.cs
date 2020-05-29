using BrunaPhotographSystem.InfraStructure.AzureStorage;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BrunaPhotographSystem.DomainService.Services
{
    public static class FileServerService 
    {
        public static void DeleteFile(String blobContainerName, String fileName)
        {
            var _blob = new AzureBlobRepository();
            _blob.DeleteFile(blobContainerName, fileName);
        }
        public static string UploadFile(string appendFileName, Stream fileContent, string blobContainerName, string contentType, string Directory)
        {
            var _blob = new AzureBlobRepository();
            return _blob.UploadFile(appendFileName, fileContent, blobContainerName, contentType, Directory);
        }
    }
}
