using SistemaFotografa.DomainModel.Entities;
using SistemaFotografa.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.InfraStructure.DataAccess.Repositories
{
    class ClienteMemDBRepository : IClienteRepository
    {

        private static readonly List<Cliente> _set = new List<Cliente>();

        public void Create(Cliente entity)
        {
            _set.Add(entity);
        }

        public void Delete(Guid id)
        {
            _set.Remove(Read(id));
        }

        public Cliente Read(Guid id)
        {
            return _set.Find(e => e.Id == id);
        }

        public IEnumerable<Cliente> ReadAll()
        {
            return _set;
        }

        public void Update(Cliente entity)
        {
            Delete(entity.Id);
            Create(entity);
        }

    }
}
