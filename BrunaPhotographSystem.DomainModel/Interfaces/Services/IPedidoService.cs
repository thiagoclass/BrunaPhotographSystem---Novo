using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Interfaces.Services
{
    public interface IPedidoService : IServiceBase<Pedido, Guid>
    {
        IEnumerable<PedidoFotoProduto> AssociarFotoAProdutoDeUmPedido(IEnumerable<Foto> fotos, IList<PedidoFotoProduto> fotosSelecionadas , Guid fotoAssociar, Guid produto, Guid pedido);
        IEnumerable<PedidoFotoProduto> DesassociarFotoDeProdutoDeUmPedido(IEnumerable<Foto> fotos, IList<PedidoFotoProduto> fotosSelecionadas, Guid fotoDesassociar, Guid produto, Guid pedido);
    }
}
