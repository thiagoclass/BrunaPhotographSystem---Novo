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
using BrunaPhotographSystem.ApiClient;
using File = BrunaPhotographSystem.DomainModel.Entities.File;
using BrunaPhotographSystem.DomainService;
using BrunaPhotographSystem.DomainService.Services;

namespace BrunaPhotographSystem.Presentation.Controllers
{
    public class AlbumController : Controller
    {
        
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IAmClient _apiIdentificacao;
        private readonly CoreFotoClient _apiCoreFoto;
        private readonly CoreAlbumClient _apiCoreAlbum;
        private readonly CoreClienteClient _apiCoreCliente;
        

        public AlbumController(IHttpContextAccessor httpContextAccessor, IAmClient apiIdentificacao, CoreFotoClient apiCoreFoto, CoreAlbumClient apiCoreAlbum, CoreClienteClient apiCoreCliente)
        {
            _apiIdentificacao = apiIdentificacao;
            _apiCoreFoto = apiCoreFoto;
            _apiCoreAlbum = apiCoreAlbum;
            _apiCoreCliente = apiCoreCliente;
            
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
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            var model = _apiCoreAlbum.BuscarTodosAlbuns(token).Result;

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
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            var clientes = _apiCoreCliente.BuscarTodosClientes(token).Result;
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
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            return View(_apiCoreAlbum.BuscarAlbum(id,token).Result);
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
                var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
                foto.FotoUrl = FileServerService.UploadFile("_Foto", imagem.OpenReadStream(), BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
                foto.MiniFotoUrl = FileServerService.UploadFile("_MiniFoto", new MemoryStream(miniImagemByte), BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
                _apiCoreFoto.CriarComId(foto,token);

            }
            return Ok();
        }

        public IActionResult AtualizarAlbum(Album album)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCoreAlbum.Atualizar(album,token);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de atualizar um album.");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverAlbum(Album album)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCoreAlbum.Deletar(album,token);
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
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            return View(_apiCoreAlbum.BuscarAlbum(id,token).Result);
        }
        public IActionResult CadastrarNovo(Album album)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCoreAlbum.Criar(album,token);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de cadastrar um album.");
            return RedirectToAction("Index");
        }
    }
}