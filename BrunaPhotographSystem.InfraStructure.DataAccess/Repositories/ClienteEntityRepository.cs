using Microsoft.EntityFrameworkCore;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.InfraStructure.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
{
    public class ClienteEntityRepository : IClienteRepository
    {
        private readonly FotografaContext _db;

        public ClienteEntityRepository(FotografaContext db)
        {
            _db = db;
        }

        public void Create(Cliente entity)
        {
            _db.Clientes.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _db.Clientes.Remove(Read(id));
            _db.SaveChanges();
        }

        public Cliente Read(Guid id)
        {
            return _db.Clientes.AsNoTracking().Where(c=> c.Id ==id).FirstOrDefault();
        }

        public IEnumerable<Cliente> ReadAll()
        {
            return _db.Clientes;
        }

        public void Update(Cliente entity)
        {
            _db.Clientes.Update(entity);
            _db.SaveChanges();
        }
    }
}
