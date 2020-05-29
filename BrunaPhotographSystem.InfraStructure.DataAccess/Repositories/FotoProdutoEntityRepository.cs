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
    public class FotoProdutoEntityRepository : IFotoProdutoRepository
    {
        public readonly IFotoRepository fotoRepository;
        public readonly IProdutoRepository produtoRepository;
        private readonly FotografaContext _db;

        public FotoProdutoEntityRepository(IFotoRepository fotoRepository, IProdutoRepository produtoRepository, FotografaContext db)
        {
            this.fotoRepository = fotoRepository;
            this.produtoRepository = produtoRepository;
            _db = db;
        }
        public void Create(FotoProduto entity)
        {
            
            _db.FotoProdutos.Add(entity);
            _db.Entry<Album>(entity.Foto.Album).State = EntityState.Unchanged;
            _db.Entry<Foto>(entity.Foto).State = EntityState.Unchanged;
            _db.Entry<Produto>(entity.Produto).State = EntityState.Unchanged;
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _db.FotoProdutos.Remove(Read(id));
            _db.SaveChanges();
        }

        public FotoProduto Read(Guid id)
        {
            return _db.FotoProdutos.Where(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<FotoProduto> ReadAll()
        {
            return _db.FotoProdutos;
        }

        public void Update(FotoProduto entity)
        {
            _db.FotoProdutos.Update(entity);
            _db.SaveChanges();
        }
    }
}
