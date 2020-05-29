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
    public class PedidoFotoProdutosController : ControllerBase
    {
        private readonly IPedidoFotoProdutoService _pedidoFotoProdutoService;
        private readonly IPedidoService _pedidoService;
        public PedidoFotoProdutosController(IPedidoFotoProdutoService pedidoFotoProdutoService, IPedidoService pedidoService)
        {
            _pedidoFotoProdutoService = pedidoFotoProdutoService;
            _pedidoService = pedidoService;
        }


        // GET: api/PedidoFotoProdutos
        [HttpGet]
        public ActionResult<IEnumerable<PedidoFotoProduto>> GetPedidoFotoProdutos()
        {
            return Ok(_pedidoFotoProdutoService.BuscarTodos());
        }

        // GET: api/PedidoFotoProdutos/5
        [HttpGet("{id}")]
        public ActionResult<PedidoFotoProduto> GetPedidoFotoProduto(Guid id)
        {
            var pedidoFotoProduto = _pedidoFotoProdutoService.Buscar(id);

            if (pedidoFotoProduto == null)
            {
                return NotFound();
            }

            return pedidoFotoProduto;
        }
        // GET: api/PedidoFotoProdutos/5
        [HttpGet("pedido/{id}")]
        public ActionResult<PedidoFotoProduto> GetPedidoFotoProdutoDoPedido(Guid id)
        {
            var pedido = _pedidoService.Buscar(id);

            if (pedido == null)
            {
                return NotFound();
            }
            var pedidoFotoProduto = _pedidoFotoProdutoService.BuscarTodosDoPedido(id);
            

            return Ok(pedidoFotoProduto);
        }
        // PUT: api/PedidoFotoProdutos/5
        [HttpPut("{id}")]
        public IActionResult PutPedidoFotoProduto(Guid id, PedidoFotoProduto pedidoFotoProduto)
        {
            if (id != pedidoFotoProduto.Id)
            {
                return BadRequest();
            }

            try
            {
                _pedidoFotoProdutoService.Atualizar(pedidoFotoProduto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoFotoProdutoExists(id))
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

        // POST: api/PedidoFotoProdutos
        [HttpPost]
        public ActionResult<PedidoFotoProduto> PostPedidoFotoProduto(PedidoFotoProduto pedidoFotoProduto)
        {
            _pedidoFotoProdutoService.Criar(pedidoFotoProduto);

            return CreatedAtAction("GetPedidoFotoProduto", new { id = pedidoFotoProduto.Id }, pedidoFotoProduto);
        }

        // DELETE: api/PedidoFotoProdutos/5
        [HttpDelete("{id}")]
        public ActionResult<PedidoFotoProduto> DeletePedidoFotoProduto(Guid id)
        {
            var pedidoFotoProduto = _pedidoFotoProdutoService.Buscar(id);
            if (pedidoFotoProduto == null)
            {
                return NotFound();
            }

            _pedidoFotoProdutoService.Deletar(id);
            
            return pedidoFotoProduto;
        }

        private bool PedidoFotoProdutoExists(Guid id)
        {
            return _pedidoFotoProdutoService.Buscar(id) != null;
        }
    }
}
