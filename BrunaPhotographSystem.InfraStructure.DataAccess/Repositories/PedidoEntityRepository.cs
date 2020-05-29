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
    public class PedidoEntityRepository : IPedidoRepository
    {
        private readonly FotografaContext _db;

        public PedidoEntityRepository(FotografaContext db)
        {
            _db = db;
        }
        public void Create(Pedido entity)
        {
            
            _db.Pedidos.Add(entity);
            _db.Entry<Cliente>(entity.Cliente).State = EntityState.Unchanged;
            // _db.Entry(entity.Cliente.Pedidos.First()).State = EntityState.Unchanged;
            // _db.Entry<PedidoFotoProduto>().State = EntityState.Unchanged;
            //_db.Entry<Album>(entity.PedidoFotoProdutos.First().FotoProduto.Foto.Album).State = EntityState.Unchanged;
            //_db.Entry<Foto>(entity.PedidoFotoProdutos.First().FotoProduto.Foto).State = EntityState.Unchanged;
            //_db.Entry<Produto>(entity.PedidoFotoProdutos.First().FotoProduto.Produto).State = EntityState.Unchanged;
            //_db.Entry<FotoProduto>(entity.PedidoFotoProdutos.First().FotoProduto).State = EntityState.Unchanged;
            //_db.Entry<PedidoFotoProduto>(entity.PedidoFotoProdutos.First()).State = EntityState.Unchanged;
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _db.Pedidos.Remove(Read(id));
            _db.SaveChanges();
        }

        public Pedido Read(Guid id)
        {
            return _db.Pedidos.Where(p => p.Id == id).FirstOrDefault();
        }


        public IEnumerable<Pedido> ReadAll()
        {
            return _db.Pedidos;
        }

        public void Update(Pedido entity)
        {
            _db.Pedidos.Update(entity);
            _db.SaveChanges();
        }
    }
}
