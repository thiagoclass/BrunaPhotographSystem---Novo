using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class Pedido:EntityBase<Guid>
       {
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public List<PedidoFotoProduto> PedidoFotoProdutos { get; set; }
        public Pedido()
        {
            Id = Guid.NewGuid();
            Cliente = new Cliente();
        }
    }
}
