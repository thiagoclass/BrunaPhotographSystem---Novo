using BrunaPhotographSystem.DomainModel.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrunaPhotographSystem.DomainModel.Interfaces.CQRS
{
    public interface IQueue
    {
        Task EnqueueAsync(QueueMessage message);
        Task<string> DequeueAsync(string queueName);
        void Enqueue(QueueMessage message);
        string Dequeue(string queueName);
    }
}
