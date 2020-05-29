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
    public class CoreFotoClient:CoreClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;

        public CoreFotoClient(HttpClient httpClient, IHttpContextAccessor acessor)
        {
            _httpClient = httpClient;
            _acessor = acessor;

        }

        public async Task<IEnumerable<Foto>> BuscarTodosFotos(string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"fotos/");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Foto>>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async Task<Foto> BuscarFoto(Guid id, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"fotos/{id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Foto>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async void Atualizar(Foto foto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(foto), Encoding.UTF8, "application/json");
            var resultado= await _httpClient.PutAsync($"fotos/{foto.Id}",stringContent);
            resultado.EnsureSuccessStatusCode();
            
        }

        public async void Deletar(Foto foto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.DeleteAsync($"fotos/{foto.Id}");
            resultado.EnsureSuccessStatusCode();
        }

        public async void Criar(Foto foto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(foto), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync($"fotos/", stringContent);
            resultado.EnsureSuccessStatusCode();
        }

        public void CriarComId(Foto foto, string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(foto), Encoding.UTF8, "application/json");
            var resultado = _httpClient.PostAsync($"fotos/comid/{foto.Id}", stringContent).Result;
            resultado.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Foto>> BuscarTodosDoAlbum(Album album,string token)
        {
            AddBearerToken(_httpClient, _acessor, token);
            var resultado = await _httpClient.GetAsync($"fotos/album/{album.Id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Foto>>(resultado.Content.ReadAsStringAsync().Result);
        }
    }
}
