using Microsoft.AspNetCore.Http;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainService.Services
{
    public class PedidoService : IPedidoService
    { 
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoFotoProdutoService _pedidoFotoProdutoService;
        private readonly IFotoProdutoService _fotoProdutoService;
        private readonly IProdutoService _produtoService;

        public PedidoService(IPedidoRepository pedidoRepository, IPedidoFotoProdutoService pedidoFotoProdutoService, IFotoProdutoService fotoProdutoService, IProdutoService produtoService)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoFotoProdutoService = pedidoFotoProdutoService;
            _fotoProdutoService = fotoProdutoService;
            _produtoService = produtoService;
        }

        public void Criar(Pedido entity)
        {
            _pedidoRepository.Create(entity);
        }

        public void Deletar(Guid id)
        {
            _pedidoRepository.Delete(id);
        }

        public Pedido Buscar(Guid id)
        {
            return _pedidoRepository.Read(id);
        }

        public IEnumerable<Pedido> BuscarTodos()
        {
            return _pedidoRepository.ReadAll();
        }

        public void Atualizar(Pedido entity)
        {
            _pedidoRepository.Update(entity);
        }


        public IEnumerable<PedidoFotoProduto> AssociarFotoAProdutoDeUmPedido(IEnumerable<Foto> fotos, IList<PedidoFotoProduto> fotosSelecionadas, Guid fotoAssociar, Guid produto, Guid pedido)
        {
            foreach (var foto in fotos)
            {
                if (foto.Id == fotoAssociar)
                {
                    FotoProduto fotoProduto = new FotoProduto();
                    fotoProduto.Foto = foto;
                    fotoProduto.Produto = _produtoService.Buscar(produto);

                    //_fotoProdutoService.Create(fotoProduto);
                    PedidoFotoProduto pedidoFotoProduto = new PedidoFotoProduto();
                    pedidoFotoProduto.FotoProduto = fotoProduto;
                    pedidoFotoProduto.Pedido = new Pedido() { Id = pedido };
                    fotosSelecionadas.Add(pedidoFotoProduto);
                    //_pedidoFotoProdutoService.Create(pedidoFotoProduto);
                }
            }
            return fotosSelecionadas;
        }

        public IEnumerable<PedidoFotoProduto> DesassociarFotoDeProdutoDeUmPedido(IEnumerable<Foto> fotos, IList<PedidoFotoProduto> fotosSelecionadas, Guid fotoDesassociar, Guid produto, Guid pedido)
        {
            List<PedidoFotoProduto> fotosSelecionadasReturn = new List<PedidoFotoProduto>(fotosSelecionadas);
            foreach (var fotoSelecionada in fotosSelecionadas)
            {
                if (fotoSelecionada.FotoProduto.Foto.Id == fotoDesassociar &&
                    fotoSelecionada.FotoProduto.Produto.Id == produto &&
                    fotoSelecionada.Pedido.Id == pedido)
                {
                    fotosSelecionadasReturn.Remove(fotoSelecionada);
                    //_pedidoFotoProdutoService.Delete(fotoSelecionada.Id);
                    //_fotoProdutoService.Delete(fotoSelecionada.FotoProduto.Id);
                }
            }
            return fotosSelecionadasReturn;
        }
    }
}
