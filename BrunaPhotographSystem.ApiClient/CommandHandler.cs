using BrunaPhotographSystem;
using BrunaPhotographSystem.DomainModel.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrunaPhotographSystem.ApiClient
{
    public class CommandHandler
    {
        private readonly OrderPedidoClient _apiOrderPedido;
        private readonly IAmClient _apiIAm;
        public CommandHandler(OrderPedidoClient apiOrderPedido, IAmClient apiIAm)
        {
            _apiOrderPedido = apiOrderPedido;
            _apiIAm = apiIAm;
        }

        public async void Handle(AddPedidoCommand command)
        {
            
            var token = _apiIAm.Login("brunartfotografia@gmail.com", "N$Tlogin1").Result;
            await _apiOrderPedido.CriarDireto(command.Pedido, token);

        }

    //    public void Handle(UpdatePedidoCommand command)
    //    {
    //        _pedidoService.Atualizar(command.Pedido);
    //    }

    //    public void Handle(DeletePedidoCommand command)
    //    {
    //        _pedidoService.Deletar(command.Pedido.Id);
    //    }
    }
}
