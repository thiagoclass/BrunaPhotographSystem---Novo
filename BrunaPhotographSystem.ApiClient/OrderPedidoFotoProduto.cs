using BrunaPhotographSystem.DomainModel.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BrunaPhotographSystem.ApiClient
{
    public class OrderPedidoFotoProdutoClient:OrderClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;

        public OrderPedidoFotoProdutoClient(HttpClient httpClient, IHttpContextAccessor acessor)
        {
            _httpClient = httpClient;
            _acessor = acessor;

        }


        public async Task<IEnumerable<PedidoFotoProduto>> BuscarTodosPedidoFotoProdutos(string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync("pedidoFotoProdutos");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<PedidoFotoProduto>>(resultado.Content.ReadAsStringAsync().Result);
        }


        public async Task<PedidoFotoProduto> BuscarFotoProduto(Guid id, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"pedidoFotoProdutos/{id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<PedidoFotoProduto>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async void Atualizar(PedidoFotoProduto pedidoFotoProduto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(pedidoFotoProduto), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PutAsync($"pedidoFotoProdutos/{pedidoFotoProduto.Id}", stringContent);
            resultado.EnsureSuccessStatusCode();

        }

        public async void Deletar(PedidoFotoProduto pedidoFotoProduto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.DeleteAsync($"pedidoFotoProdutos/{pedidoFotoProduto.Id}");
            resultado.EnsureSuccessStatusCode();
        }

        public async Task Criar(PedidoFotoProduto pedidoFotoProduto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(pedidoFotoProduto), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync($"pedidoFotoProdutos/", stringContent);
            resultado.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<PedidoFotoProduto>> BuscarTodosDoPedido(Guid pedido, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"pedidoFotoProdutos/pedido/{pedido}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<PedidoFotoProduto>>(resultado.Content.ReadAsStringAsync().Result);
        }
    }
}
