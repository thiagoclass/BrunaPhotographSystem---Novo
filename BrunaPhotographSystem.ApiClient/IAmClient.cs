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
    public class IAmClient
    {

        private readonly HttpClient _httpClient;

       

        public IAmClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
    public async Task<string> Login(string userName, string senha)
        {
            var resultado = _httpClient.PostAsync("login", new StringContent(JsonConvert.SerializeObject(new {Login = userName,Password = senha })
            , Encoding.UTF8, "application/json")).Result;
            resultado.EnsureSuccessStatusCode();
            return await resultado.Content.ReadAsStringAsync();
        }
    }
}
