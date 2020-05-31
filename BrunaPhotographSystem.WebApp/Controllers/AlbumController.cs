using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrunaPhotographSystem.DomainModel.Entities;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using Newtonsoft.Json;
using File = BrunaPhotographSystem.DomainModel.Entities.File;
using BrunaPhotographSystem.DomainService;
using BrunaPhotographSystem.DomainService.Services;

namespace BrunaPhotographSystem.Presentation.Controllers
{
    public class AlbumController : Controller
    {
        
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IFotoService _fotoService;
        private readonly IAlbumService _albumService;
        private readonly IClienteService _clienteService;
        

        public AlbumController(IHttpContextAccessor httpContextAccessor,  IFotoService fotoService, IAlbumService albumService, IClienteService clienteService)
        {

            _fotoService = fotoService;
            _albumService = albumService;
            _clienteService = clienteService;
            
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            if (_session.GetString("Alertas") != null)
            {
                ViewBag.Alerta = _session.GetString("Alertas");
                _session.Remove("Alertas");
            }
            try
            {
                if (_session.Get<Cliente>("cliente").Email != "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch
            {
                return RedirectToAction("VoltarAoSite");
            }
            
            var model = _albumService.BuscarTodos();

            return View(model);
        }

        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
        }

        public IActionResult Cadastro()
        {
            try
            {
                if (_session.Get<Cliente>("cliente").Email != "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch
            {
                return RedirectToAction("VoltarAoSite");
            }
            
            var clientes = _clienteService.BuscarTodos();
            ViewBag.Clientes = clientes;
            Album album = new Album();
            return View(album);
        }
        public IActionResult Editar(Guid id)
        {
            try
            {
                if (_session.Get<Cliente>("cliente").Email != "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch
            {
                return RedirectToAction("VoltarAoSite");
            }
            
            return View(_albumService.Buscar(id));
        }
        public async Task<IActionResult> AdicionarFotoDropZone(Guid id)
        {
            byte[] miniImagemByte;
            foreach (var imagem in Request.Form.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagem.CopyToAsync(memoryStream);

                    miniImagemByte = EditorDeImagem.DrawSize(memoryStream.ToArray(), 256);

                }

                Foto foto = new Foto();
                foto.Id = Guid.NewGuid();
                foto.Album.Id = id;
                foto.Descricao = String.Empty;
                foto.Nome = String.Empty;
                
                foto.FotoUrl = FileServerService.UploadFile("_Foto", imagem.OpenReadStream(), BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
                foto.MiniFotoUrl = FileServerService.UploadFile("_MiniFoto", new MemoryStream(miniImagemByte), BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
                _fotoService.CriarComId(foto);

            }
            return Ok();
        }

        public IActionResult AtualizarAlbum(Album album)
        {
            
            _albumService.Atualizar(album);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de atualizar um album.");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverAlbum(Album album)
        {
            
            _albumService.Deletar(album.Id);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de excluir um album.");
            return RedirectToAction("Index");

        }
        public IActionResult Remover(Guid id)
        {
            try
            {
                if (_session.Get<Cliente>("cliente").Email != "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch
            {
                return RedirectToAction("VoltarAoSite");
            }
            
            return View(_albumService.Buscar(id));
        }
        public IActionResult CadastrarNovo(Album album)
        {
            
            _albumService.Criar(album);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de cadastrar um album.");
            return RedirectToAction("Index");
        }
    }
}