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
using BrunaPhotographSystem.ApiClient;
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
        private readonly IAmClient _apiIdentificacao;
        private readonly CoreFotoClient _apiCoreFoto;
        private readonly CoreAlbumClient _apiCoreAlbum;
        

        public FotoController(IHttpContextAccessor httpContextAccessor, IAmClient apiIdentificacao, CoreFotoClient apiCoreFoto, CoreAlbumClient apiCoreAlbum)
        {
            _apiIdentificacao = apiIdentificacao;
            _apiCoreFoto = apiCoreFoto;
            _apiCoreAlbum = apiCoreAlbum;
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
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            return View(_apiCoreFoto.BuscarTodosFotos(token).Result);
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
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            var albuns = _apiCoreAlbum.BuscarTodosAlbuns(token).Result;
            ViewBag.Albuns = albuns;

            return View();
        }
        //public IActionResult IndexSelect()
        //{

        //    var albuns = _albumService.BuscarTodosDoCliente(_session.Get<Cliente>("cliente"));
        //    ViewBag.FotosPreSelecionadas = _session.Get<List<Foto>>("FotosPreSelecionadas");
        //    ViewBag.FotosSelecionadas = _session.Get<List<Foto>>("FotosSelecionadas");
        //    if (ViewBag.FotosPreSelecionadas == null)
        //    {
        //        List<Foto> fotos = new List<Foto>();
        //        foreach (var album in albuns)
        //        {
        //            fotos.AddRange(_fotoService.ReadAllPreselect(album));
        //        }
        //        ViewBag.FotosPreSelecionadas = fotos;
        //    }
        //    if (ViewBag.FotosSelecionadas == null)
        //    {
        //        List<Foto> fotos = new List<Foto>();
        //        foreach (var album in albuns)
        //        {
        //            fotos.AddRange(_fotoService.ReadAllSelect(album));
        //        }
        //        ViewBag.FotosSelecionadas = fotos;
        //    }
        //    _session.Set<List<Foto>>("FotosPreSelecionadas", (List<Foto>)ViewBag.FotosPreSelecionadas);
        //    _session.Set<List<Foto>>("FotosSelecionadas", (List<Foto>)ViewBag.FotosSelecionadas); 
        //    return View();
        //}
        //public IActionResult SelectFoto(Guid select)
        //{
        //    ViewBag.FotosPreSelecionadas = _session.Get<List<Foto>>("FotosPreSelecionadas");
        //    ViewBag.FotosSelecionadas = _session.Get<List<Foto>>("FotosSelecionadas");
        //    List<Foto> fotosPreSelecionadas = ViewBag.FotosPreSelecionadas;
        //    List<Foto> fotosSelecionadas = ViewBag.FotosSelecionadas;
        //    foreach (var foto in fotosPreSelecionadas)
        //    {
        //        if(foto.Id == select)
        //        {
        //            foto.Situacao = 1;
        //            fotosSelecionadas.Add(foto);
        //        }
        //    }
        //    foreach (var foto in fotosSelecionadas)
        //    {
        //        fotosPreSelecionadas.Remove(foto);
        //    }
        //    ViewBag.FotosPreSelecionadas= fotosPreSelecionadas;
        //    ViewBag.FotosSelecionadas= fotosSelecionadas;
        //    _session.Set<List<Foto>>("FotosPreSelecionadas", (List<Foto>)ViewBag.FotosPreSelecionadas);
        //    _session.Set<List<Foto>>("FotosSelecionadas", (List<Foto>)ViewBag.FotosSelecionadas);

        //    return View("IndexSelect");
        //}
        //public IActionResult UnSelectFoto(Guid select)
        //{
        //    ViewBag.FotosPreSelecionadas = _session.Get<List<Foto>>("FotosPreSelecionadas");
        //    ViewBag.FotosSelecionadas = _session.Get<List<Foto>>("FotosSelecionadas");
        //    List<Foto> fotosPreSelecionadas = ViewBag.FotosPreSelecionadas;
        //    List<Foto> fotosSelecionadas = ViewBag.FotosSelecionadas;
        //    foreach (var foto in fotosSelecionadas)
        //    {
        //        if (foto.Id == select)
        //        {
        //            foto.Situacao = 0;
        //            fotosPreSelecionadas.Add(foto);
        //        }
        //    }
        //    foreach (var foto in fotosPreSelecionadas)
        //    {
        //        fotosSelecionadas.Remove(foto);
        //    }
        //    ViewBag.FotosPreSelecionadas = fotosPreSelecionadas;
        //    ViewBag.FotosSelecionadas = fotosSelecionadas;
        //    _session.Set<List<Foto>>("FotosPreSelecionadas", (List<Foto>)ViewBag.FotosPreSelecionadas);
        //    _session.Set<List<Foto>>("FotosSelecionadas", (List<Foto>)ViewBag.FotosSelecionadas);
        //    return View("IndexSelect");
        //}

        //public IActionResult CadastrarSelecaoFotos()
        //{
        //    ViewBag.FotosPreSelecionadas = _session.Get<List<Foto>>("FotosPreSelecionadas");
        //    ViewBag.FotosSelecionadas = _session.Get<List<Foto>>("FotosSelecionadas");
        //    List<Foto> fotosPreSelecionadas = ViewBag.FotosPreSelecionadas;
        //    List<Foto> fotosSelecionadas = ViewBag.FotosSelecionadas;

        //    foreach (var foto in fotosSelecionadas)
        //    {
        //        _fotoService.AtualizarSituacao(foto);
        //    }
        //    foreach (var foto in fotosPreSelecionadas)
        //    {
        //        _fotoService.AtualizarSituacao(foto);
        //    }
        //    _session.Set<List<Foto>>("FotosPreSelecionadas", null);
        //    _session.Set<List<Foto>>("FotosSelecionadas", null);
        //    _session.SetString("Alertas", "Parabéns!!!| Você acabou de cadastrar a seleção de fotos do seu album.");

        //    return RedirectToAction("SistemaCliente","Default");
        //}

        


        public async Task<IActionResult> CadastrarNovo(Foto foto, IFormFile imagem)
        {
            byte[] miniImagemByte;
            byte[] imagemByte;
            using (var memoryStream = new MemoryStream())
            {
                await imagem.CopyToAsync(memoryStream);

                miniImagemByte = EditorDeImagem.DrawSize(memoryStream.ToArray(), 256);
                imagemByte = EditorDeImagem.AddWaterMark(memoryStream.ToArray());
            }
            
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            foto.FotoUrl = FileServerService.UploadFile("_Foto", new MemoryStream(imagemByte), BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer,imagem.ContentType, foto.Album.Id.ToString());
            foto.MiniFotoUrl = FileServerService.UploadFile("_MiniFoto",new MemoryStream(miniImagemByte),BrunaPhotographSystem.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
            _apiCoreFoto.Criar(foto,token);
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
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            return View(_apiCoreFoto.BuscarFoto(id,token).Result);
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
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            return View(_apiCoreFoto.BuscarFoto(id,token).Result);

        }
        public IActionResult AtualizarFoto(Foto foto)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCoreFoto.Atualizar(foto,token);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de atualizar informações de uma foto!");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverFoto(Foto foto)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            foto = _apiCoreFoto.BuscarFoto(foto.Id,token).Result;
            _apiCoreFoto.Deletar(foto,token);
            FileServerService.DeleteFile(InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer,foto.FotoUrl.Substring(62).Split("_")[0].ToString());
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de excluir uma foto!");
            return RedirectToAction("Index");

        }

    }
}