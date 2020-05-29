using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using BrunaPhotographSystem.ApiClient;

namespace BrunaPhotographSystem.Presentation.Controllers
{
    public class PedidoController : Controller
    {
        private readonly OrderPedidoClient _apiOrderPedido;
        private readonly CoreClienteClient _apiCoreCliente;
        private readonly CoreProdutoClient _apiCoreProduto;
        private readonly CoreAlbumClient _apiCoreAlbum;
        private readonly CoreFotoClient _apiCoreFoto;
        private readonly OrderFotoProdutoClient _apiFotoProduto;
        private readonly OrderPedidoFotoProdutoClient _apiPedidoFotoProduto;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IAmClient _apiIdentificacao;

        public PedidoController(OrderPedidoClient apiOrderPedido, IHttpContextAccessor httpContextAccessor, CoreClienteClient apiCoreCliente, CoreProdutoClient apiCoreProduto, CoreAlbumClient apiCoreAlbum, CoreFotoClient apiCoreFoto, OrderFotoProdutoClient apiFotoProduto, OrderPedidoFotoProdutoClient apiPedidoFotoProduto, IAmClient apiIdentificacao)
        {
            _apiOrderPedido = apiOrderPedido;
            _httpContextAccessor = httpContextAccessor;
            _apiCoreCliente = apiCoreCliente;
            _apiCoreProduto = apiCoreProduto;
            _apiCoreAlbum = apiCoreAlbum;
            _apiCoreFoto = apiCoreFoto;
            _apiPedidoFotoProduto = apiPedidoFotoProduto;
            _apiFotoProduto = apiFotoProduto;
            _apiIdentificacao = apiIdentificacao;
        }

        public IActionResult VoltarAoSite()
        {
            _session.Remove("Pedido");
            _session.Remove("Produto");
            _session.Remove("Fotos");
            _session.Remove("FotosSelecionadas");

            return RedirectToAction("Responsivo", "Default");
        }
        public IActionResult AdicionarFotos(Guid pedido, Guid produto, String dataPedido)
        {

            IEnumerable<PedidoFotoProduto> fotosDoProdutoSelecionadas = new List<PedidoFotoProduto>();
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            var albuns = _apiCoreAlbum.BuscarTodosDoCliente((Cliente)_session.Get<Cliente>("cliente"),token).Result;
            if (_session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas") != null && _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas").Count() > 0)
            {
                if (_session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas") == null)
                {
                    fotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas").Where(p => p.FotoProduto.Produto.Id == produto);
                }
                else
                {
                    fotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
                }
            }
            else
            {
                if (_session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas") != null)
                {
                    fotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
                }
            }

            List<Foto> fotos = new List<Foto>();
            foreach (var album in albuns)
            {
                
                fotos.AddRange(_apiCoreFoto.BuscarTodosDoAlbum(album, token).Result);
            }
            if (fotosDoProdutoSelecionadas != null)
            {
                foreach (var foto in fotosDoProdutoSelecionadas)
                {
                    for (int i = 0; i < fotos.Count; i++)
                    {
                        if (fotos[i].Id == foto.FotoProduto.Foto.Id)
                        {
                            fotos.RemoveAt(i);
                        }
                    }

                }
            }
            ViewBag.Fotos = fotos;
            ViewBag.FotosDoProdutoSelecionadas = fotosDoProdutoSelecionadas;
            ViewBag.Pedido = pedido;
            ViewBag.Produto = produto;
            ViewBag.DataPedido = dataPedido;
            _session.Set<String>("DataPedido", dataPedido);
            _session.Set<List<Foto>>("Fotos", fotos);
            _session.Set<Guid>("Produto", produto);
            _session.Set<Guid>("Pedido", pedido);
            _session.Set<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas", fotosDoProdutoSelecionadas);
            return View();
        }
        public IActionResult EditarFotos(Guid pedido, Guid produto)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            IEnumerable<PedidoFotoProduto> fotosDoProdutoSelecionadas;
            var albuns = _apiCoreAlbum.BuscarTodosDoCliente((Cliente)_session.Get<Cliente>("cliente"),token).Result;
            if (_session.Get("FotosSelecionadas") == null)
            {
                fotosDoProdutoSelecionadas = _apiPedidoFotoProduto.BuscarTodosDoPedido(pedido,token).Result.Where(p => p.FotoProduto.Produto.Id == produto);
            }
            else
            {
                if (_session.Get("FotosDoProdutoSelecionadas") == null)
                {
                    fotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas").Where(p => p.FotoProduto.Produto.Id == produto);
                }
                else
                {
                    fotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
                }
            }
            List<Foto> fotos = new List<Foto>();
            foreach (var album in albuns)
            {
                
                fotos.AddRange(_apiCoreFoto.BuscarTodosDoAlbum(album,token).Result);
            }
            foreach (var foto in fotosDoProdutoSelecionadas)
            {
                for (int i = 0; i < fotos.Count; i++)
                {
                    if (fotos[i].Id == foto.FotoProduto.Foto.Id)
                    {
                        fotos.RemoveAt(i);
                    }
                }

            }
            ViewBag.Fotos = fotos;
            ViewBag.FotosDoProdutoSelecionadas = fotosDoProdutoSelecionadas;
            ViewBag.Pedido = pedido;
            ViewBag.Produto = produto;
            _session.Set<List<Foto>>("Fotos", fotos);
            _session.Set<Guid>("Produto", produto);
            _session.Set<Guid>("Pedido", pedido);
            _session.Set<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas", fotosDoProdutoSelecionadas);
            return View();
        }
        public IActionResult Index()
        {
            _session.Remove("Pedido");
            _session.Remove("Produto");
            _session.Remove("Fotos");
            _session.Remove("FotosSelecionadas");
            _session.Remove("FotosDoProdutoSelecionadas");
            if (_session.GetString("Alertas") != null)
            {
                ViewBag.Alerta = _session.GetString("Alertas");
                _session.Remove("Alertas");
            }
            try
            {
                if (_session.Get<Cliente>("cliente").Email == "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch
            {
                return RedirectToAction("VoltarAoSite");
            }
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            return View(_apiOrderPedido.BuscarTodosPedidos(token).Result);
        }
        public IActionResult Create(Guid id, DateTime Data)
        {
            try
            {
                if (_session.Get<Cliente>("cliente").Email == "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch
            {
                return RedirectToAction("VoltarAoSite");
            }

            var clientes = _session.Get<Cliente>("cliente");
            ViewBag.Clientes = clientes;
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            var produtos = _apiCoreProduto.BuscarTodosProdutos(token).Result;
            ViewBag.Produtos = produtos;
            Pedido pedido;
            if (id == Guid.Empty)
            {
                pedido = new Pedido();
            }
            else
            {
                pedido = new Pedido() { Id = id, DataPedido = Data };
            }


            if (_session.Get<Guid>("Pedido") != Guid.Empty)
            {
                pedido.Id = _session.Get<Guid>("Pedido");
            }

            _session.Set<IEnumerable<Produto>>("Produtos", produtos);
            _session.Set<Guid>("Pedido", pedido.Id);
            pedido.DataPedido = DateTime.Now;
            IEnumerable<PedidoFotoProduto> fotosSelecionadas = null;
            if (_session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas") != null)
            {
                fotosSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas");
            }
            if (_session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas") != null)
            {
                List<PedidoFotoProduto> fotosSelecionadasAtualizada = new List<PedidoFotoProduto>();
                IEnumerable<PedidoFotoProduto> fotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
                if (fotosSelecionadas != null)
                {
                    if (fotosDoProdutoSelecionadas.Count() > 0 && fotosSelecionadas.Count() > 0)
                    {
                        fotosSelecionadasAtualizada = fotosSelecionadas.Where(p => p.FotoProduto.Produto.Id != fotosDoProdutoSelecionadas.FirstOrDefault().FotoProduto.Produto.Id).ToList();
                    }
                    else
                    {
                        fotosSelecionadasAtualizada = fotosSelecionadas.Where(p => p.FotoProduto.Produto.Id != _session.Get<Guid>("Produto")).ToList();
                    }
                }

                foreach (var fotosDoProdutoSelecionada in fotosDoProdutoSelecionadas)
                {
                    fotosSelecionadasAtualizada.Add(fotosDoProdutoSelecionada);
                }

                _session.Set<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas", fotosSelecionadasAtualizada);
                _session.Remove("FotosDoProdutoSelecionadas");
            }

            ViewBag.FotosSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas");
            return View(pedido);
        }
        public IActionResult Edit(Guid id)
        {
            try
            {
                if (_session.Get<Cliente>("cliente").Email == "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch
            {
                return RedirectToAction("VoltarAoSite");
            }
            var clientes = _session.Get<Cliente>("cliente");
            ViewBag.Clientes = clientes;
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            var produtos = _apiCoreProduto.BuscarTodosProdutos(token).Result;
            ViewBag.Produtos = produtos;
            _session.Set<Guid>("pedido", id);
            ViewBag.PedidoId = id;
            IEnumerable<PedidoFotoProduto> fotosSelecionadas;
            if (_session.Get("FotosSelecionadas") == null)
            {
                fotosSelecionadas = _apiPedidoFotoProduto.BuscarTodosDoPedido(id,token).Result;
                _session.Set<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas", fotosSelecionadas);
            }
            else
            {
                fotosSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas");
            }
            if (_session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas") != null)
            {
                List<PedidoFotoProduto> fotosSelecionadasAtualizada = new List<PedidoFotoProduto>();
                IEnumerable<PedidoFotoProduto> fotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
                if (fotosDoProdutoSelecionadas.Count() > 0)
                {
                    fotosSelecionadasAtualizada = fotosSelecionadas.Where(p => p.FotoProduto.Produto.Id != fotosDoProdutoSelecionadas.FirstOrDefault().FotoProduto.Produto.Id).ToList();
                }
                else
                {
                    fotosSelecionadasAtualizada = fotosSelecionadas.Where(p => p.FotoProduto.Produto.Id != _session.Get<Guid>("Produto")).ToList();
                }
                foreach (var fotosDoProdutoSelecionada in fotosDoProdutoSelecionadas)
                {
                    fotosSelecionadasAtualizada.Add(fotosDoProdutoSelecionada);
                }

                _session.Set<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas", fotosSelecionadasAtualizada);
                _session.Remove("FotosDoProdutoSelecionadas");
            }
            ViewBag.FotosSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas");
            return View(_apiOrderPedido.BuscarPedido(id,token).Result);
        }
        public IActionResult AtualizarPedido(Pedido pedido)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            IEnumerable<PedidoFotoProduto> fotoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas");
            foreach (var fotoSelecionada in fotoSelecionadas)
            {

                _apiPedidoFotoProduto.Deletar(fotoSelecionada,token);
                _apiFotoProduto.Deletar(fotoSelecionada.FotoProduto,token);
                _apiFotoProduto.Criar(fotoSelecionada.FotoProduto,token);
                _apiPedidoFotoProduto.Criar(fotoSelecionada,token);

            }
            _apiOrderPedido.Atualizar(pedido,token);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de atualizar um pedido.");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverPedido(Pedido pedido)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            _apiOrderPedido.Deletar(pedido,token);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de excluir um pedido.");
            return RedirectToAction("Index");

        }
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (_session.Get<Cliente>("cliente").Email == "brunartfotografia@gmail.com")
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch
            {
                return RedirectToAction("VoltarAoSite");
            }
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            return View(_apiOrderPedido.BuscarPedido(id,token).Result);
        }
        public async Task<IActionResult> CadastrarNovo(Pedido pedido)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            pedido.PedidoFotoProdutos = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosSelecionadas").ToList();
            await _apiOrderPedido.Criar(pedido,token);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de cadastrar um pedido.");
            return RedirectToAction("Index");
        }
        public IActionResult AdicionarAssociarFoto(Guid fotoAssociar)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            ViewBag.Fotos = _session.Get<IEnumerable<Foto>>("Fotos");
            ViewBag.FotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
            ViewBag.Pedido = _session.Get<Guid>("Pedido");
            ViewBag.Produto = _session.Get<Guid>("Produto");
            var fotos = ViewBag.Fotos;
            IEnumerable<PedidoFotoProduto> fotosDoProdutoSelecionadas = ViewBag.FotosDoProdutoSelecionadas;
            var pedido = ViewBag.Pedido;
            var produto = ViewBag.Produto;

            fotosDoProdutoSelecionadas = _apiOrderPedido.AssociarFotoAProdutoDeUmPedido(fotos, fotosDoProdutoSelecionadas.ToList(), fotoAssociar, produto, pedido,token).Result;
            _session.Set<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas", fotosDoProdutoSelecionadas);
            return RedirectToAction("AdicionarFotos", new { produto = produto.ToString(), pedido = pedido.ToString(), dataPedido = _session.Get<String>("DataPedido").ToString() });
        }
        public IActionResult AdicionarDesassociarFoto(Guid fotoDesassociar)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            ViewBag.Fotos = _session.Get<IEnumerable<Foto>>("Fotos");
            ViewBag.FotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
            ViewBag.Pedido = _session.Get<Guid>("Pedido");
            ViewBag.Produto = _session.Get<Guid>("Produto");
            var fotos = ViewBag.Fotos;
            IEnumerable<PedidoFotoProduto> fotosDoProdutoSelecionadas = ViewBag.FotosDoProdutoSelecionadas;
            var pedido = ViewBag.Pedido;
            var produto = ViewBag.Produto;

            fotosDoProdutoSelecionadas = _apiOrderPedido.DesassociarFotoDeProdutoDeUmPedido(fotos, fotosDoProdutoSelecionadas.ToList(), fotoDesassociar, produto, pedido,token).Result;
            _session.Set<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas", fotosDoProdutoSelecionadas);
            return RedirectToAction("AdicionarFotos", new { produto = produto.ToString(), pedido = pedido.ToString(), dataPedido = _session.Get<String>("DataPedido").ToString() });
        }
        public IActionResult EditarAssociarFoto(Guid fotoAssociar)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            ViewBag.Fotos = _session.Get<IEnumerable<Foto>>("Fotos");
            ViewBag.FotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
            ViewBag.Pedido = _session.Get<Guid>("Pedido");
            ViewBag.Produto = _session.Get<Guid>("Produto");
            var fotos = ViewBag.Fotos;
            IEnumerable<PedidoFotoProduto> fotosDoProdutoSelecionadas = ViewBag.FotosDoProdutoSelecionadas;
            var pedido = ViewBag.Pedido;
            var produto = ViewBag.Produto;

            fotosDoProdutoSelecionadas = _apiOrderPedido.AssociarFotoAProdutoDeUmPedido(fotos, fotosDoProdutoSelecionadas.ToList(), fotoAssociar, produto, pedido,token).Result;
            _session.Set<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas", fotosDoProdutoSelecionadas);
            return RedirectToAction("EditarFotos", new { produto = produto.ToString(), pedido = pedido.ToString() });
        }
        public IActionResult EditarDesassociarFoto(Guid fotoDesassociar)
        {
            var token = _apiIdentificacao.Login(_session.GetString("username"), _session.GetString("password")).Result;
            ViewBag.Fotos = _session.Get<IEnumerable<Foto>>("Fotos");
            ViewBag.FotosDoProdutoSelecionadas = _session.Get<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas");
            ViewBag.Pedido = _session.Get<Guid>("Pedido");
            ViewBag.Produto = _session.Get<Guid>("Produto");
            var fotos = ViewBag.Fotos;
            IEnumerable<PedidoFotoProduto> fotosDoProdutoSelecionadas = ViewBag.FotosDoProdutoSelecionadas;
            var pedido = ViewBag.Pedido;
            var produto = ViewBag.Produto;

            fotosDoProdutoSelecionadas = _apiOrderPedido.DesassociarFotoDeProdutoDeUmPedido(fotos, fotosDoProdutoSelecionadas.ToList(), fotoDesassociar, produto, pedido, token).Result;
            _session.Set<IEnumerable<PedidoFotoProduto>>("FotosDoProdutoSelecionadas", fotosDoProdutoSelecionadas);
            return RedirectToAction("EditarFotos", new { produto = produto.ToString(), pedido = pedido.ToString() });
        }

    }
}