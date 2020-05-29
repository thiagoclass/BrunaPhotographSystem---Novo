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
    public class CoreAlbumClient:CoreClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;

        public CoreAlbumClient(HttpClient httpClient, IHttpContextAccessor acessor)
        {
            _httpClient = httpClient;
            _acessor = acessor;
        }

        public async Task<IEnumerable<Album>> BuscarTodosAlbuns(string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"albuns/");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Album>>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async Task<Album> BuscarAlbum(Guid id, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"albuns/{id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Album>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async void Atualizar(Album album, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");
            var resultado= await _httpClient.PutAsync($"albuns/{album.Id}",stringContent);
            resultado.EnsureSuccessStatusCode();
            
        }

        public async void Deletar(Album album, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.DeleteAsync($"albuns/{album.Id}");
            resultado.EnsureSuccessStatusCode();
        }

        public async void Criar(Album album, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync($"albuns/", stringContent);
            resultado.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Album>> BuscarTodosDoCliente(Cliente cliente,String token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"albuns/cliente/{cliente.Id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Album>>(resultado.Content.ReadAsStringAsync().Result);
        }
    }
}
