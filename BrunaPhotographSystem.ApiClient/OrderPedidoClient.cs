using BrunaPhotographSystem.DomainModel.CQRS.Commands;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.CQRS;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BrunaPhotographSystem.ApiClient
{
    public class OrderPedidoClient:OrderClient
    {
        private readonly HttpClient _httpClient;
        private readonly IQueue _queue;

        public OrderPedidoClient(HttpClient httpClient, IQueue queue)
        {
            _httpClient = httpClient;
            _queue = queue;
        }

        public async Task<IEnumerable<Pedido>> BuscarTodosPedidos(string token)
        {
            AddBearerToken(_httpClient, token);
            var resultado = await _httpClient.GetAsync("pedidos");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Pedido>>(resultado.Content.ReadAsStringAsync().Result);
        }

        
        public async Task<Pedido> BuscarPedido(Guid id, string token)
        {
            AddBearerToken(_httpClient,token);
            var resultado = await _httpClient.GetAsync($"pedidos/{id}");
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Pedido>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async void Atualizar(Pedido pedido, string token)
        {
            AddBearerToken(_httpClient,  token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PutAsync($"pedidos/{pedido.Id}", stringContent);
            resultado.EnsureSuccessStatusCode();

        }

        public async void Deletar(Pedido pedido, string token)
        {
            AddBearerToken(_httpClient, token);
            var resultado = await _httpClient.DeleteAsync($"pedidos/{pedido.Id}");
            resultado.EnsureSuccessStatusCode();
        }

        public async Task Criar(Pedido pedido, string token)
        {
            AddBearerToken(_httpClient, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
            AddPedidoCommand command = new AddPedidoCommand(pedido);
            await _queue.EnqueueAsync(command);
            //var resultado = await _httpClient.PostAsync($"pedidos/", stringContent);
            //resultado.EnsureSuccessStatusCode();
            
        }
        public async Task CriarDireto(Pedido pedido, string token)
        {
            AddBearerToken(_httpClient, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
            AddPedidoCommand command = new AddPedidoCommand(pedido);
            var resultado = await _httpClient.PostAsync($"pedidos/", stringContent);
            resultado.EnsureSuccessStatusCode();
        }


        public async Task<IEnumerable<PedidoFotoProduto>> AssociarFotoAProdutoDeUmPedido(IEnumerable<Foto> fotos, List<PedidoFotoProduto> list, Guid fotoAssociar, Guid produto, Guid pedido, string token)
        {
            Associar associar = new Associar()
            {
                fotos = fotos,
                fotosSelecionadas=list,
                fotoAssociar = fotoAssociar,
                produto = produto,
                pedido = pedido
            };
            AddBearerToken(_httpClient, token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(associar), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync($"pedidos/associarfotoproduto/", stringContent);
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<PedidoFotoProduto>>(resultado.Content.ReadAsStringAsync().Result);
        }

        public async Task<IEnumerable<PedidoFotoProduto>> DesassociarFotoDeProdutoDeUmPedido(IEnumerable<Foto> fotos, List<PedidoFotoProduto> list, Guid fotoDesassociar, Guid produto, Guid pedido, string token)
        {
            Associar associar = new Associar()
            {
                fotos = fotos,
                fotosSelecionadas = list,
                fotoAssociar = fotoDesassociar,
                produto = produto,
                pedido = pedido
            };
            AddBearerToken(_httpClient,  token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(associar), Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync($"pedidos/desassociarfotoproduto/", stringContent);
            resultado.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<PedidoFotoProduto>>(resultado.Content.ReadAsStringAsync().Result);
        }
    }
}
