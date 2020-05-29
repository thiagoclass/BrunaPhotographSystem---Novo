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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace BrunaPhotographSystem.Core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }


        // GET: api/Produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            return Ok(JsonConvert.SerializeObject(_produtoService.BuscarTodos()));
        }

        //GET: api/Produtos/5
        [HttpGet("{id}")]
        public ActionResult<Produto> GetProduto(Guid id)
        {
            var produto = _produtoService.Buscar(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public IActionResult PutProduto(Guid id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }



            try
            {
                _produtoService.Atualizar(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        // POST: api/Produtos
        [HttpPost]
        public ActionResult<Produto> PostProduto(Produto produto)
        {
            _produtoService.Criar(produto);

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public ActionResult<Produto> DeleteProduto(Guid id)
        {
            var produto = _produtoService.Buscar(id);
            if (produto == null)
            {
                return NotFound();
            }

            _produtoService.Deletar(id);

            return produto;
        }

        private bool ProdutoExists(Guid id)
        {
            return _produtoService.Buscar(id) != null;
        }
    }
}
