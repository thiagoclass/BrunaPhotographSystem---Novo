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
    public class FotoProdutosController : ControllerBase
    {
        private readonly IFotoProdutoService _fotoProdutoService;

        public FotoProdutosController(IFotoProdutoService fotoProdutoService)
        {
            _fotoProdutoService = fotoProdutoService;
        }



        // GET: api/FotoProdutos
        [HttpGet]
        public ActionResult<IEnumerable<FotoProduto>> GetFotoProdutos()
        {
            return Ok(_fotoProdutoService.BuscarTodos());
        }

        // GET: api/FotoProdutos/5
        [HttpGet("{id}")]
        public ActionResult<FotoProduto> GetFotoProduto(Guid id)
        {
            var fotoProduto = _fotoProdutoService.Buscar(id);

            if (fotoProduto == null)
            {
                return NotFound();
            }

            return fotoProduto;
        }

        // PUT: api/FotoProdutos/5
        [HttpPut("{id}")]
        public IActionResult PutFotoProduto(Guid id, FotoProduto fotoProduto)
        {
            if (id != fotoProduto.Id)
            {
                return BadRequest();
            }

            

            try
            {
                _fotoProdutoService.Atualizar(fotoProduto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoProdutoExists(id))
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

        // POST: api/FotoProdutos
        [HttpPost]
        public ActionResult<FotoProduto> PostFotoProduto(FotoProduto fotoProduto)
        {
            _fotoProdutoService.Criar(fotoProduto);
            

            return CreatedAtAction("GetFotoProduto", new { id = fotoProduto.Id }, fotoProduto);
        }

        // DELETE: api/FotoProdutos/5
        [HttpDelete("{id}")]
        public ActionResult<FotoProduto> DeleteFotoProduto(Guid id)
        {
            var fotoProduto = _fotoProdutoService.Buscar(id);
            if (fotoProduto == null)
            {
                return NotFound();
            }

            _fotoProdutoService.Deletar(id);
            

            return fotoProduto;
        }

        private bool FotoProdutoExists(Guid id)
        {
            return _fotoProdutoService.Buscar(id) != null;
        }
    }
}
