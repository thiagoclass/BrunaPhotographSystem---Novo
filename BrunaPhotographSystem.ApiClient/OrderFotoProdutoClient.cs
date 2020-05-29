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
    public class OrderFotoProdutoClient:OrderClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;

        public OrderFotoProdutoClient(HttpClient httpClient, IHttpContextAccessor acessor)
        {
            _httpClient = httpClient;
            _acessor = acessor;

        }


        public async Task<IEnumerable<FotoProduto>> BuscarTodosFotoProdutos(string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync("fotoProdutos");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<FotoProduto>>(resultado.Content.ReadAsStringAsync().Result);
        }


        public async Task<FotoProduto> BuscarFotoProduto(Guid id, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"fotoProdutos/{id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<FotoProduto>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async void Atualizar(FotoProduto fotoProduto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(fotoProduto), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PutAsync($"fotoProdutos/{fotoProduto.Id}", stringContent);
            resultado.EnsureSuccessStatusCode();

        }

        public async void Deletar(FotoProduto fotoProduto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.DeleteAsync($"fotoProdutos/{fotoProduto.Id}");
            resultado.EnsureSuccessStatusCode();
        }

        public async Task Criar(FotoProduto fotoProduto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(fotoProduto), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync($"fotoProdutos/", stringContent);
            resultado.EnsureSuccessStatusCode();
        }

    }
}
