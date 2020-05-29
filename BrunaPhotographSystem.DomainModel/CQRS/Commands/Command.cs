using BrunaPhotographSystem.DomainModel.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.CQRS.Commands
{
    public abstract class Command : QueueMessage
    {
    }
}
