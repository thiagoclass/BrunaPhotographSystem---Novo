using BrunaPhotographSystem.DomainModel.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Text;

namespace BrunaPhotographSystem.ApiClient
{
    public class CoreProdutoClient:CoreClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;

        public CoreProdutoClient(HttpClient httpClient, IHttpContextAccessor acessor)
        {
            _httpClient = httpClient;
            _acessor = acessor;

        }

        
        public async Task<IEnumerable<Produto>> BuscarTodosProdutos(string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync("produtos");
            resultado.EnsureSuccessStatusCode();
            //var ki = JsonConvert.DeserializeObject<Task<IEnumerable<Produto>>>(resultado.Content.ReadAsStringAsync().Result);
            return JsonConvert.DeserializeObject<IEnumerable<Produto>>(resultado.Content.ReadAsStringAsync().Result);
        }
        

        public async Task<Produto> BuscarProduto(Guid id, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"produtos/{id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Produto>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async void Atualizar(Produto produto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PutAsync($"produtos/{produto.Id}", stringContent);
            resultado.EnsureSuccessStatusCode();

        }

        public async void Deletar(Produto produto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.DeleteAsync($"produtos/{produto.Id}");
            resultado.EnsureSuccessStatusCode();
        }

        public async void Criar(Produto produto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync($"produtos/", stringContent);
            resultado.EnsureSuccessStatusCode();
        }

    }
}
