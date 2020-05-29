
using BrunaPhotographSystem.DomainModel.CQRS.Commands;
using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.CQRS.Commands
{
    public class AddPedidoCommand : PedidoCommand
    {
        public const string ConstQueueName = "add-pedido-command-queue";
        public override string QueueName { get => ConstQueueName; }

        public AddPedidoCommand(Pedido pedido)      
            :base(pedido)
        {
        }
    }
}
