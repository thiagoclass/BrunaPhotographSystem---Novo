using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class PedidoFotoProduto:EntityBase<Guid>
    {
        
        public Pedido Pedido { get; set; }
        public FotoProduto FotoProduto { get; set; }
        public PedidoFotoProduto()
        {
            Id = Guid.NewGuid();
            FotoProduto = new FotoProduto();
        }
    }
}
