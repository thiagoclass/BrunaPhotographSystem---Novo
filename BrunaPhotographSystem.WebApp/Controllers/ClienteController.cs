using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrunaPhotographSystem.DomainModel.Entities;

using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using BrunaPhotographSystem.ApiClient;

namespace BrunaPhotographSystem.Presentation.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IAmClient _apiIdentificacao;
        private readonly CoreClienteClient _apiCore;
        public ClienteController( IHttpContextAccessor httpContextAccessor, IAmClient apiIdentificacao, CoreClienteClient apiCore)
        {
            _apiIdentificacao = apiIdentificacao;
            _apiCore = apiCore;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
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

            var clientes = _apiCore.BuscarTodosClientes(token).Result;
            return View(clientes);
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
            return View();
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

            var cliente = _apiCore.BuscarCliente(id,token).Result;
            return View(cliente);
        }
        public IActionResult AtualizarCliente(Cliente cliente)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCore.Atualizar(cliente, token);

            _session.SetString("Alertas", "Parabéns!!!| Você acabou de atualizar um cliente.");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverCliente(Cliente cliente)
        {

            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCore.Deletar(cliente, token);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de excluir um cliente.");
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

            var cliente = _apiCore.BuscarCliente(id,token).Result;
            return View(cliente);
        }
        public IActionResult CadastrarNovo(Cliente cliente)
        {

            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCore.Criar(cliente, token);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de cadastrar um cliente.");
            return RedirectToAction("Index");
        }
    }
}