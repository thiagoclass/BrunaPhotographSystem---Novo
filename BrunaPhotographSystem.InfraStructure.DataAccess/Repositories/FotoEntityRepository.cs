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
    public class FotoEntityRepository : IFotoRepository
    {
        private readonly FotografaContext _db;

        public FotoEntityRepository(FotografaContext db)
        {
            _db = db;
        }
        public void Create(Foto entity)
        {
            _db.Entry<Album>(entity.Album).State = EntityState.Unchanged;
            _db.Fotos.Add(entity);
            _db.SaveChanges();
        }

        public void CreateWithId(Foto foto)
        {
            _db.Fotos.Add(foto);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _db.Fotos.Remove(Read(id));
            _db.SaveChanges();
        }

        public Foto Read(Guid id)
        {
            return _db.Fotos.Where(f=> f.Id == id).FirstOrDefault();
        }

        
        public IEnumerable<Foto> ReadAll()
        {
            return _db.Fotos;//.Include(f => f.Album);
        }

        public void Update(Foto entity)
        {
            _db.Fotos.Update(entity);
            _db.SaveChanges();
        }
    }
}