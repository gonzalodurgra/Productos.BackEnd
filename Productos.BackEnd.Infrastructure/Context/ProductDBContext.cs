using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Productos.BackEnd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Infrastructure.Context
{
    /// <summary>
    /// Contexto de la base de datos, con los productos y las auditorías
    /// </summary>
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Audit> Audits { get; set; }
        /// <summary>
        /// Genera los campos y las validaciones de cada tabla
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.Property(p => p.Name).IsRequired().HasMaxLength(30);

                entity.Property(p => p.Price).IsRequired();

                entity.Property(p => p.Stock).IsRequired();
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.Property(a => a.Username).IsRequired().HasMaxLength(25);
                entity.Property(a => a.IpAddress).IsRequired().HasMaxLength(15);
                entity.Property(a => a.EntityName).IsRequired().HasMaxLength(20);
                entity.Property(a => a.Action).IsRequired().HasMaxLength(50);
                entity.Property(a => a.TimeStamp).IsRequired();
            });
        }
    }
}
