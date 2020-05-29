using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.ApiClient;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace BrunaPhotographSystem.Controllers
{
    public class DefaultController : Controller
    {
        //private readonly IClienteService _clienteService;
        //private readonly IFotoService _fotoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session=> _httpContextAccessor.HttpContext.Session;
        private readonly IAmClient _apiIdentificacao;
        private readonly CoreClienteClient _apiCore;

        public DefaultController(IHttpContextAccessor httpContextAccessor, IAmClient apiIdentificacao, CoreClienteClient apiCore)
        {
            
            _httpContextAccessor = httpContextAccessor;
            _apiIdentificacao = apiIdentificacao;
            _apiCore = apiCore;


        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Responsivo()
        {
            ViewBag.Fotos = null;// _fotoService.BuscarTodosDoAlbumPeloNome("Trabalhos").ToList();
            return View();
        }
        
            
            
        
        public IActionResult LoginSistema(String UserName,String Password)
        {
            try
            {
                var token = _apiIdentificacao.Login(UserName, Password);

                _session.SetString("username", UserName); 
                _session.SetString("password", Password);
                var cliente = _apiCore.BuscarClientePorEmail(UserName, token.Result);


                _session.Set<Cliente>("cliente", cliente.Result);

                if (cliente.Result.Email == "brunartfotografia@gmail.com")
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