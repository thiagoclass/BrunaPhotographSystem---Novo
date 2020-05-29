using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.CQRS.Commands
{
    public class UpdatePedidoCommand : PedidoCommand
    {
        public const string ConstQueueName = "update-pedido-command-queue";
        public override string QueueName { get => ConstQueueName; }
        public UpdatePedidoCommand(Pedido pedido)
            :base(pedido)
        {
        }
    }
}
