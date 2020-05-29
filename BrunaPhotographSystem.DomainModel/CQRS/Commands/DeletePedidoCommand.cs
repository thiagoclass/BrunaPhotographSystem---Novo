using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.CQRS.Commands
{
    public class DeletePedidoCommand : PedidoCommand
    {
        public const string ConstQueueName = "delete-pedido-command-queue";
        public override string QueueName { get => ConstQueueName; }

        public DeletePedidoCommand(Pedido pedido)
            :base(pedido)
        {
        }
    }
}
