using Microsoft.EntityFrameworkCore;
using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.InfraStructure.DataAccess.Context
{
    public class FotografaContext:DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Album> Albuns { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<FotoProduto> FotoProdutos { get; set; }
        public DbSet<PedidoFotoProduto> PedidoFotoProdutos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        public FotografaContext()
        : base(CreateOptions(null))
        {
            
        }

        public FotografaContext(string connName)
            : base(CreateOptions(connName))
        {
        }

        private static DbContextOptions<FotografaContext> CreateOptions(string connName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FotografaContext>();
            optionsBuilder.UseSqlServer(Properties.Resources.ConnectionString);
            if (connName == null)
            {
                optionsBuilder.UseSqlServer(@"Server=.\;Integrated Security=True;Database=CompanyFormsContext");
            }
            else
            {
                optionsBuilder.UseSqlServer(@"Server=.\;Integrated Security=True;Database=" + connName);
            }
            return optionsBuilder.Options;
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Properties.Resources.ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Albuns)
            .WithOne(a => a.Cliente);

            modelBuilder.Entity<Album>()
            .HasMany(a => a.Fotos)
            .WithOne(f => f.Album);

        }
    }
}
