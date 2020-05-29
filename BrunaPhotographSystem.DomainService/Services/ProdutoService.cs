using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainService.Services
{
    public class ProdutoService : IProdutoService
    {
        private IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void Criar(Produto entity)
        {
            _produtoRepository.Create(entity);
        }

        public void Deletar(Guid id)
        {
            _produtoRepository.Delete(id);
        }

        public Produto Buscar(Guid id)
        {
            return _produtoRepository.Read(id);
        }

        public IEnumerable<Produto> BuscarTodos()
        {
            return _produtoRepository.ReadAll();
        }

        public void Atualizar(Produto entity)
        {
            _produtoRepository.Update(entity);
        }
    }
}
