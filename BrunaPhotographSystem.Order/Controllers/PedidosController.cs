using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.InfraStructure.DataAccess.Context;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;

namespace BrunaPhotographSystem.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IFotoProdutoService _fotoProdutoService;
        private readonly IPedidoFotoProdutoService _pedidoFotoProdutoService;

        public PedidosController(IPedidoService pedidoService, IFotoProdutoService fotoProdutoService, IPedidoFotoProdutoService pedidoFotoProdutoService)
        {
            _pedidoService = pedidoService;
            _fotoProdutoService = fotoProdutoService;
            _pedidoFotoProdutoService = pedidoFotoProdutoService;
        }



        // GET: api/Pedidos
        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> GetPedidos()
        {
            return Ok(_pedidoService.BuscarTodos());
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public ActionResult<Pedido> GetPedido(Guid id)
        {
            var pedido = _pedidoService.Buscar(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidos/5
        [HttpPut("{id}")]
        public IActionResult PutPedido(Guid id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }



            try
            {
                _pedidoService.Atualizar(pedido);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Pedidos
        [HttpPost("associarfotoproduto/")]
        public ActionResult<IEnumerable<PedidoFotoProduto>> Associarfotoproduto(Associar associar)
        {
            var associarPedido = _pedidoService.AssociarFotoAProdutoDeUmPedido(associar.fotos,associar.fotosSelecionadas,associar.fotoAssociar,associar.produto,associar.pedido);

            return Ok(associarPedido);
        }
        // POST: api/Pedidos
        [HttpPost("desassociarfotoproduto/")]
        public ActionResult<IEnumerable<PedidoFotoProduto>> Desassociarfotoproduto(Associar associar)
        {
            var desassociarPedido = _pedidoService.DesassociarFotoDeProdutoDeUmPedido(associar.fotos, associar.fotosSelecionadas, associar.fotoAssociar, associar.produto, associar.pedido);

            return Ok(desassociarPedido);
        }
        
        // POST: api/Pedidos
        [HttpPost]
        public ActionResult<Pedido> PostPedido(Pedido pedido)
        {
            List<PedidoFotoProduto> fotoSelecionadas = new List<PedidoFotoProduto>(pedido.PedidoFotoProdutos);
            pedido.PedidoFotoProdutos = null;
            _pedidoService.Criar(pedido);
            if (fotoSelecionadas != null)
            {
                foreach (var fotoSelecionada in fotoSelecionadas)
                {
                    _fotoProdutoService.Criar(fotoSelecionada.FotoProduto);
                    _pedidoFotoProdutoService.Criar(fotoSelecionada);
                }
            }

            return Ok();
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public ActionResult<Pedido> DeletePedido(Guid id)
        {
            var pedido = _pedidoService.Buscar(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _pedidoService.Deletar(id);

            return pedido;
        }

        private bool PedidoExists(Guid id)
        {
            return _pedidoService.Buscar(id) != null;
        }
    }
}
