using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SistemaFotografa.DomainModel.Entities;
using SistemaFotografa.DomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>().HasKey(T => T.Id);
            modelBuilder.Entity<Foto>().HasKey(T => T.Id);
            modelBuilder.Entity<Album>().HasKey(T => T.Id);
            modelBuilder.Entity<Login>().HasKey(T => T.Username);
        }

        public DbSet<SistemaFotografa.DomainModel.Entities.Cliente> Cliente { get; set; }
        public DbSet<SistemaFotografa.DomainModel.Entities.Foto> Foto { get; set; }
    }
}
