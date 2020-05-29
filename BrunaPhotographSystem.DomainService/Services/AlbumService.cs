using BrunaPhotographSystem.InfraStructure.AzureStorage;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrunaPhotographSystem.DomainService.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFotoRepository _fotoRepository;


        public AlbumService(IAlbumRepository albumRepository, IClienteRepository clienteRepository, IFotoRepository fotoRepository)
        {
            _albumRepository = albumRepository;
            _clienteRepository = clienteRepository;
            _fotoRepository = fotoRepository;
        }
        public void Criar(Album entity)
        {
            _albumRepository.Create(entity);
        }
        public List<Album> BuscarTodosDoCliente(Cliente cliente)
        {
            return BuscarTodos().Where(c => c.Cliente.Id == cliente.Id).ToList();
        }
        public void Deletar(Guid id)
        {
            _albumRepository.Delete(id);
        }

        public Album Buscar(Guid id)
        {
            return _albumRepository.Read(id);
        }

        public Album BuscarPeloNome(string nomeAlbum)
        {
            return _albumRepository.Read(nomeAlbum);
        }

        public IEnumerable<Album> BuscarTodos()
        {
            return _albumRepository.ReadAll();
        }
        

        public void Atualizar(Album entity)
        {
            _albumRepository.Update(entity);
        }
    }
}
