using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using BrunaPhotographSystem.ApiClient;

namespace BrunaPhotographSystem.Presentation.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IAmClient _apiIdentificacao;
        private readonly CoreProdutoClient _apiCore;

        public ProdutoController(IHttpContextAccessor httpContextAccessor, IAmClient apiIdentificacao, CoreProdutoClient apiCore)
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

        public IActionResult CadastrarNovo(Produto produto)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCore.Criar(produto,token);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de cadastrar um produto!");
            return RedirectToAction("Index");
        }

        public IActionResult AtualizarProduto(Produto produto)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiCore.Atualizar(produto, token);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de atualizar informações de um Produto!");
            return RedirectToAction("Index");
        }

        public IActionResult RemoverProduto(Produto produto)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            produto = _apiCore.BuscarProduto(produto.Id, token).Result;
            _apiCore.Deletar(produto, token);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de excluir um Produto!");
            return RedirectToAction("Index");

        }

        // GET: Produto
        public ActionResult Index()
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
            return View(_apiCore.BuscarTodosProdutos(token).Result);
        }


        public ActionResult Create()
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



        public ActionResult Edit(Guid id)
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
            return View(_apiCore.BuscarProduto(id,token).Result);
        }



        public ActionResult Delete(Guid id)
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
            return View(_apiCore.BuscarProduto(id,token).Result);
        }


    }
}