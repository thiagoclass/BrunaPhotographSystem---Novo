using BrunaPhotographSystem.DomainModel.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BrunaPhotographSystem.ApiClient
{
    public class CoreClienteClient:CoreClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;

        public CoreClienteClient(HttpClient httpClient, IHttpContextAccessor acessor)
        {
            _httpClient = httpClient;
            _acessor = acessor;

        }


        public async Task<Cliente> BuscarClientePorEmail(string email,string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"clientes/email/{email}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Cliente>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async Task<IEnumerable<Cliente>> BuscarTodosClientes(string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"clientes/");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Cliente>>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async Task<Cliente> BuscarCliente(Guid id, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"clientes/{id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Cliente>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async void Atualizar(Cliente cliente, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var resultado= await _httpClient.PutAsync($"clientes/{cliente.Id}",stringContent);
            resultado.EnsureSuccessStatusCode();
            
        }

        public async void Deletar(Cliente cliente, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.DeleteAsync($"clientes/{cliente.Id}");
            resultado.EnsureSuccessStatusCode();
        }

        public async void Criar(Cliente cliente, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync($"clientes/", stringContent);
            resultado.EnsureSuccessStatusCode();
        }
    }
}
