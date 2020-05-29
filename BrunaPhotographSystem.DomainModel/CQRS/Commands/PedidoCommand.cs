using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.CQRS.Commands
{
    public abstract class PedidoCommand : Command
    {
        public Pedido Pedido { get; set; }

        protected PedidoCommand(Pedido pedido)
        {
            Pedido = pedido;
        }
    }
}
