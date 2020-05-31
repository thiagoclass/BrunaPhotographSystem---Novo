using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;

using File = BrunaPhotographSystem.DomainModel.Entities.File;
using BrunaPhotographSystem.DomainService;
using BrunaPhotographSystem.DomainService.Services;
using System.Drawing;

namespace BrunaPhotographSystem.Presentation.Controllers
{
    public class FotoController : Controller
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
       
        private readonly IFotoService _fotoService;
        private readonly IAlbumService _albumService;
        

        public FotoController(IHttpContextAccessor httpContextAccessor, IFotoService fotoService, IAlbumService albumService)
        {
            
            _fotoService = fotoService;
            _albumService = albumService;
            _httpContextAccessor = httpContextAccessor;
            
        }

        // GET: Foto
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
            
            return View(_fotoService.BuscarTodos());
        }

        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
        }

        public IActionResult Create()
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
            
            var albuns = _albumService.BuscarTodos();
            ViewBag.Albuns = albuns;

            return View();
        }
        


        public async Task<IActionResult> CadastrarNovo(Foto foto, IFormFile imagem)
        {
            byte[] miniImagemByte;
            byte[] imagemByte;
            using (var memoryStream = new MemoryStream())
            {
                await imagem.CopyToAsync(memoryStream);

                miniImagemByte = EditorDeImagem.DrawSize(memoryStream.ToArray(), 256);
                //imagemByte = EditorDeImagem.AddWaterMark(memoryStream.ToArray());
                imagemByte = memoryStream.ToArray();
            }
            
            
            foto.FotoUrl = FileServerService.UploadFile("_Foto", new MemoryStream(imagemByte), BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer,imagem.ContentType, foto.Album.Id.ToString());
            foto.MiniFotoUrl = FileServerService.UploadFile("_MiniFoto",new MemoryStream(miniImagemByte),BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
            _fotoService.Criar(foto);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de cadastrar uma foto!");
            return RedirectToAction("Index");
        }



        public IActionResult Edit(Guid id)
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
            
            return View(_fotoService.Buscar(id));
        }




        public IActionResult Delete(Guid id)
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
            
            return View(_fotoService.Buscar(id));

        }
        public IActionResult AtualizarFoto(Foto foto)
        {
            
            _fotoService.Atualizar(foto);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de atualizar informações de uma foto!");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverFoto(Foto foto)
        {
            
            foto = _fotoService.Buscar(foto.Id);
            _fotoService.Deletar(foto.Id);
            FileServerService.DeleteFile(InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer,foto.FotoUrl.Substring(62).Split("_")[0].ToString());
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de excluir uma foto!");
            return RedirectToAction("Index");

        }

    }
}