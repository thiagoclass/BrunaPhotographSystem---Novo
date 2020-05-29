
using BrunaPhotographSystem.DomainModel.CQRS;
using BrunaPhotographSystem.DomainModel.Interfaces.CQRS;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrunaPhotographSystem.InfraStructure.AzureQueue
{
    public class AzureStorageQueue : IQueue
    {
        public async Task EnqueueAsync(QueueMessage message)
        {
            //Cria uma referência a conta de Armazenamento do Azure
            CloudStorageAccount storageAccountClient =
                CloudStorageAccount.Parse(Properties.Resources.StringConnection);

            //Cria um objeto cliente de queue/fila a partir da conta de armazenamento
            CloudQueueClient queueClient = storageAccountClient.CreateCloudQueueClient();

            //Crio um objeto que referencia uma determinada queue/fila
            CloudQueue cloudQueue = queueClient.GetQueueReference(message.QueueName);

            //Crio a fila se ela não existir
            bool result =  cloudQueue.CreateIfNotExistsAsync().Result;

            //Crio uma mensagem a partir do objeto serializado
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(message));

            //Adiciono a mensagem na queue/fila
            await cloudQueue.AddMessageAsync(cloudQueueMessage);
        }

        public async Task<string> DequeueAsync(string queueName)
        {
            //Cria uma referência a conta de Armazenamento do Azure
            CloudStorageAccount storageAccountClient =
                CloudStorageAccount.Parse(Properties.Resources.StringConnection);

            //Cria um objeto cliente de queue/fila a partir da conta de armazenamento
            CloudQueueClient queueClient = storageAccountClient.CreateCloudQueueClient();

            //Crio um objeto que referencia uma determinada queue/fila
            CloudQueue cloudQueue = queueClient.GetQueueReference(queueName);

            //Crio a fila se ela não existir
            await cloudQueue.CreateIfNotExistsAsync();

            //Adiciono a mensagem na queue/fila
            var message = cloudQueue.GetMessageAsync().Result;
            await cloudQueue.DeleteMessageAsync(message);
            return message.AsString;
        }

        public void Enqueue(QueueMessage message)
        {
            throw new NotImplementedException();
        }

        public string Dequeue(string queueName)
        {
            throw new NotImplementedException();
        }
    }
}
