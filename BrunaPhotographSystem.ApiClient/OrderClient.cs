using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BrunaPhotographSystem.ApiClient
{
    public class OrderClient
    {

        public void AddBearerToken(HttpClient _httpClient, IHttpContextAccessor _acessor, String token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public void AddBearerToken(HttpClient _httpClient, String token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
