using Microsoft.AspNetCore.Http;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrunaPhotographSystem.DomainService.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IAlbumRepository _albumRepository; 


        public ClienteService(IClienteRepository clienteRepository, IAlbumRepository albumRepository)
        {
            _clienteRepository = clienteRepository;
            _albumRepository = albumRepository;

        }

        public void Criar(Cliente entity)
        {
            _clienteRepository.Create(entity);
        }

        public void Deletar(Guid id)
        {
            _clienteRepository.Delete(id);
        }

        public Cliente Buscar(Guid id)
        {
            return _clienteRepository.Read(id);
        }

        public IEnumerable<Cliente> BuscarTodos()
        {
            return _clienteRepository.ReadAll();
        }

        public void Atualizar(Cliente entity)
        {
            _clienteRepository.Update(entity);
        }

        public Cliente BuscarPeloUsuario(String Username)
        {
            return (Cliente)_clienteRepository.ReadAll().Where(c => c.Email == Username).FirstOrDefault();
        }

        

    }
}
