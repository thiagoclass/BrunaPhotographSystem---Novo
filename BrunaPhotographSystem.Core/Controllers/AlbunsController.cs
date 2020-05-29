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
    public class AlbunsController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IClienteService _clienteService;

        public AlbunsController(IAlbumService albumService, IClienteService clienteService)
        {
            _albumService = albumService;
            _clienteService = clienteService;
        }

        // GET: api/Albuns
        [HttpGet]
        public ActionResult<IEnumerable<Album>> GetAlbuns()
        {
            return Ok(JsonConvert.SerializeObject(_albumService.BuscarTodos()));
        }

        //GET: api/Albuns/5
        [HttpGet("cliente/{id}")]
        public ActionResult<IEnumerable<Album>> GetAlbumClient(Guid id)
        {
            var cliente = _clienteService.Buscar(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(_albumService.BuscarTodosDoCliente(cliente)));
        }


        //GET: api/Albuns/5
        [HttpGet("{id}")]
        public ActionResult<Album> GetAlbum(Guid id)
        {
            var album = _albumService.Buscar(id);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        // PUT: api/Albuns/5
        [HttpPut("{id}")]
        public IActionResult PutAlbum(Guid id, Album album)
        {
            if (id != album.Id)
            {
                return BadRequest();
            }

            

            try
            {
                _albumService.Atualizar(album);
            }
            catch (Exception ex)
            {
                if (!AlbumExists(id))
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

        // POST: api/Albuns
        [HttpPost]
        public ActionResult<Album> PostAlbum(Album album)
        {
            _albumService.Criar(album);

            return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
        }

        // DELETE: api/Albuns/5
        [HttpDelete("{id}")]
        public ActionResult<Album> DeleteAlbum(Guid id)
        {
            var album = _albumService.Buscar(id);
            if (album == null)
            {
                return NotFound();
            }

            _albumService.Deletar(id);

            return album;
        }

        private bool AlbumExists(Guid id)
        {
            return _albumService.Buscar(id) != null;
        }
    }
}
