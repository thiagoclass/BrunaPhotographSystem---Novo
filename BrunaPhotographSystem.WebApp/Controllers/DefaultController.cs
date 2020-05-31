using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrunaPhotographSystem.DomainModel.Entities;

using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;

namespace BrunaPhotographSystem.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IFotoService _fotoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session=> _httpContextAccessor.HttpContext.Session;


        public DefaultController(IHttpContextAccessor httpContextAccessor, IFotoService fotoService, IClienteService clienteService)
        {
            
            _httpContextAccessor = httpContextAccessor;
            _clienteService = clienteService;
            _fotoService = fotoService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Responsivo()
        {
            ViewBag.Carrousel = _fotoService.BuscarTodosDoAlbumPeloNome("Carrousel").ToList();
            ViewBag.Trabalhos = _fotoService.BuscarTodosDoAlbumPeloNome("Trabalhos").ToList();
            return View();
        }
        
            
            
        
        public IActionResult LoginSistema(String UserName,String Password)
        {
            try
            {
                //var token = _apiIdentificacao.Login(UserName, Password);

                _session.SetString("username", UserName); 
                _session.SetString("password", Password);
                //var cliente = _apiCore.BuscarCliente(UserName, token.Result);
                var cliente = _clienteService.BuscarPeloUsuario(UserName);
                
                _session.Set<Cliente>("cliente", cliente);

                if (cliente.Email == "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("SistemaAdministrador");
                }
                else
                {
                    return RedirectToAction("SistemaCliente");
                }
            }
            catch
            {
                return RedirectToAction("Responsivo");
            }
        }
        
        public IActionResult SistemaCliente()
        {
            
            return View();
            
            
            
        }
        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
        }
        public IActionResult SistemaAdministrador(String UserName,String Password)
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
            return View();
        }
        public IActionResult Redirecionar(string controllerAction)
        {
            
            
            return RedirectToAction(controllerAction.Split("/")[1].ToString(), controllerAction.Split("/")[0].ToString());
        }
    }
}