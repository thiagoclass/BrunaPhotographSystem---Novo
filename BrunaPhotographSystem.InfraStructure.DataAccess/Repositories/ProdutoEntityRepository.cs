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
    public class ProdutoEntityRepository : IProdutoRepository
    {
        private readonly FotografaContext _db;

        public ProdutoEntityRepository(FotografaContext db)
        {
            _db = db;
        }
        public void Create(Produto entity)
        {
            //_db.Entry(entity.Album).State = EntityState.Unchanged;
            _db.Produtos.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _db.Produtos.Remove(Read(id));
            _db.SaveChanges();
        }

        public Produto Read(Guid id)
        {
            return _db.Produtos.Where(p => p.Id == id).FirstOrDefault();
        }


        public IEnumerable<Produto> ReadAll()
        {
            return _db.Produtos;
        }

        public void Update(Produto entity)
        {
            _db.Produtos.Update(entity);
            _db.SaveChanges();
        }
    }
}