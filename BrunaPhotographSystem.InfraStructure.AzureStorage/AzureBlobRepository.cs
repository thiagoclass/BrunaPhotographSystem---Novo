using System;
using System.IO;
using System.Linq;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace BrunaPhotographSystem.InfraStructure.AzureStorage
{
    public class AzureBlobRepository
    {
        private CloudStorageAccount _cloudStorageAccount;

        public AzureBlobRepository()
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.StringConnection);
        }

        public void DeleteFile(String blobContainerName,String fileName)
        {
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(blobContainerName);
            // Get a reference to a blob named "myblob.txt".
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference($"{fileName}_Foto.jpeg");
            // Delete the blob.
            blockBlob.Delete();
            blockBlob = blobContainer.GetBlockBlobReference($"{fileName}_MiniFoto.jpeg");
            // Delete the blob.
            blockBlob.Delete();
        }

            public string UploadFile(string appendFileName, Stream fileContent, string blobContainerName, string contentType,string Directory)
        {
            //Crio um client para fazer acesso ao serviço de Blob
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            if (Directory.IndexOf("/") == -1)
            {
                Directory += "/";
            }
            //Crio uma referência a um container mesmo que ele não existe
            var blobContainer = blobClient.GetContainerReference(blobContainerName);

            //Crio o blob caso não exista
            blobContainer.CreateIfNotExists();
            
            //Permite acesso anônimo a URL do blob
            blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            string blobPrefix = Directory;
            
            var blobs = blobContainer.ListBlobsSegmented(blobPrefix,new BlobContinuationToken());
            string nome = $"{blobs.Results.Count(i => i.GetType() == typeof(CloudBlockBlob) && i.Uri.ToString().IndexOf(appendFileName)>-1)+1}{appendFileName}.jpeg";
            //Crio uma referência para o meu blob
            var blob = blobContainer.GetBlockBlobReference(Directory + nome.ToString());

            //Defino o tipo de conteúdo do arquivo
            blob.Properties.ContentType = contentType;

            //Faço upload do meu blob
            blob.UploadFromStream(fileContent);

            //Retorno o endereço do blob
            return blob.Uri.AbsoluteUri;

        }
    }
}
