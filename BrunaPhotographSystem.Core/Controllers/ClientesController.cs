using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrunaPhotographSystem.DomainModel.Entities;
using Microsoft.AspNetCore.Authorization;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using Newtonsoft.Json;

namespace BrunaPhotographSystem.Core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetClientes()
        {
            return Ok(JsonConvert.SerializeObject(_clienteService.BuscarTodos()));
        }

        //GET: api/Clientes/5
        
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(Guid id)
        {
            var cliente = _clienteService.Buscar(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        //GET: api/Clientes/5
        
        [HttpGet("email/{email}")]
        public ActionResult<Cliente> GetClienteByEmail(String email)
        {
            var cliente = _clienteService.BuscarPeloUsuario(email);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }
        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public IActionResult PutCliente(Guid id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }



            try
            {
                _clienteService.Atualizar(cliente);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        [HttpPost]
        public ActionResult<Cliente> PostCliente(Cliente cliente)
        {
            _clienteService.Criar(cliente);

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public ActionResult<Cliente> DeleteCliente(Guid id)
        {
            var cliente = _clienteService.Buscar(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _clienteService.Deletar(id);

            return cliente;
        }

        private bool ClienteExists(Guid id)
        {
            return _clienteService.Buscar(id) != null;
        }

    }
}
