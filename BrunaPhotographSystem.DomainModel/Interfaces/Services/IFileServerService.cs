using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Interfaces.Services
{
    public interface IFileServerService
    {
        void DeleteFile(String blobContainerName, String fileName);
        string UploadFile(string appendFileName, Stream fileContent, string blobContainerName, string contentType, string Directory);
        
    }
}
