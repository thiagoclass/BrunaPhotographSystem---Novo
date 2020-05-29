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
    public class FotosController : ControllerBase
    {
        private readonly IFotoService _fotoService;
        private readonly IAlbumService _albumService;

        public FotosController(IFotoService fotoService, IAlbumService albumService)
        {
            _fotoService = fotoService;
            _albumService = albumService;
        }


        // GET: api/Fotos
        [HttpGet]
        public ActionResult<IEnumerable<Foto>> GetFotos()
        {
            return Ok(JsonConvert.SerializeObject(_fotoService.BuscarTodos()));
        }
        //GET: api/Fotos/album/5
        [HttpGet("album/{id}")]
        public ActionResult<Foto> GetFotoDoAlbum(Guid id)
        {
            var album = _albumService.Buscar(id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(_fotoService.BuscarTodosDoAlbum(album)));
        }
        
        //GET: api/Fotos/5
        [HttpGet("{id}")]
        public ActionResult<Foto> GetFoto(Guid id)
        {
            var foto = _fotoService.Buscar(id);

            if (foto == null)
            {
                return NotFound();
            }

            return foto;
        }

        // PUT: api/Fotos/5
        [HttpPut("{id}")]
        public IActionResult PutFoto(Guid id, Foto foto)
        {
            if (id != foto.Id)
            {
                return BadRequest();
            }


            _fotoService.Atualizar(foto);
            try
            {
                
            }
            catch (Exception ex)
            {
                if (!FotoExists(id))
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

        // POST: api/Fotos
        [HttpPost]
        public ActionResult<Foto> PostFoto(Foto foto)
        {
            _fotoService.Criar(foto);

            return CreatedAtAction("GetFoto", new { id = foto.Id }, foto);
        }
        // POST: api/Fotos/5
        [HttpPost("ComId/{id}")]
        public ActionResult<Foto> PostFotoComId(Guid Id, Foto foto)
        {
            foto.Id = Id;
            _fotoService.CriarComId(foto);

            return CreatedAtAction("GetFoto", new { id = foto.Id }, foto);
        }

        // DELETE: api/Fotos/5
        [HttpDelete("{id}")]
        public ActionResult<Foto> DeleteFoto(Guid id)
        {
            var foto = _fotoService.Buscar(id);
            if (foto == null)
            {
                return NotFound();
            }

            _fotoService.Deletar(id);

            return foto;
        }

        private bool FotoExists(Guid id)
        {
            return _fotoService.Buscar(id) != null;
        }
    }
}
