using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.CQRS
{
    public abstract class QueueMessage : Message
    {
        public abstract string QueueName { get; }
    }
}
