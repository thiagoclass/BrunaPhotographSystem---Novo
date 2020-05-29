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
    public class PedidoFotoProdutoEntityRepository : IPedidoFotoProdutoRepository
    {
        private readonly FotografaContext _db;

        public PedidoFotoProdutoEntityRepository(FotografaContext db)
        {
            _db = db;
        }
        public void Create(PedidoFotoProduto entity)
        {
            _db.Entry<Album>(entity.FotoProduto.Foto.Album).State = EntityState.Unchanged;
            _db.Entry<Foto>(entity.FotoProduto.Foto).State = EntityState.Unchanged;
            //_db.Entry<Cliente>(entity.FotoProduto.Foto.Album.Cliente).State = EntityState.Unchanged;
            _db.Entry<Produto>(entity.FotoProduto.Produto).State = EntityState.Unchanged;
            //_db.Entry<Pedido>(entity.Pedido).State = EntityState.Unchanged;
            _db.PedidoFotoProdutos.Add(entity);
            _db.Entry<FotoProduto>(entity.FotoProduto).State = EntityState.Unchanged;
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _db.PedidoFotoProdutos.Remove(Read(id));
            _db.SaveChanges();
        }

        public PedidoFotoProduto Read(Guid id)
        {
            return _db.PedidoFotoProdutos.Include(p => p.Pedido)
                                         .Include(f => f.FotoProduto)
                                            .ThenInclude(f => f.Foto)
                                         .Include(f => f.FotoProduto)
                                            .ThenInclude(p => p.Produto)
                                         .Where(p => p.Id == id).FirstOrDefault();
        }


        public IEnumerable<PedidoFotoProduto> ReadAll()
        {
            return _db.PedidoFotoProdutos.Include(p => p.Pedido)
                                         .Include(f=>f.FotoProduto)
                                            .ThenInclude(f=>f.Foto)
                                         .Include(f=>f.FotoProduto)
                                            .ThenInclude(p=>p.Produto);
        }

        public void Update(PedidoFotoProduto entity)
        {
            _db.PedidoFotoProdutos.Update(entity);
            _db.SaveChanges();
        }
    }
}
