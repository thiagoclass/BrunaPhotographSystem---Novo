using Microsoft.EntityFrameworkCore;
using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.InfraStructure.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
{
    public class AlbumEntityRepository : IAlbumRepository
    {
        private readonly FotografaContext _db;

        public AlbumEntityRepository(FotografaContext db)
        {
            _db = db;
            
        }

        public void Create(Album entity)
        {
            
            //_db.Clientes.Update(_db.Clientes.Find(entity.Cliente.Id));
            _db.Albuns.Add(entity);
            _db.Entry<Cliente>(entity.Cliente).State = EntityState.Unchanged;
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _db.Albuns.Remove(Read(id));
            _db.SaveChanges();
        }

        public Album Read(Guid id)
        {
            return _db.Albuns.Find(id);
        }

        public Album Read(string nomeAlbum)
        {
            return _db.Albuns.Where(a=>a.Nome==nomeAlbum).Include(c => c.Cliente).FirstOrDefault();
        }

        public IEnumerable<Album> ReadAll()
        {
            return _db.Albuns.Include(c=>c.Cliente);
        }

        public void Update(Album entity)
        {
            _db.Albuns.Update(entity);
            _db.SaveChanges();
        }
    }
}