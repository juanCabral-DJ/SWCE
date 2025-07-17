using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SWCE.Domain.Entities;
using SWCE.Domain.Base;

namespace SWCE.Persistence.Context
{
    public class E_commerceContext : DbContext
    {
        public E_commerceContext(DbContextOptions<E_commerceContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cupon> Cupones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cupon>()
                .HasDiscriminator<string>("TipoCupon")
                .HasValue<CuponMontoFijo>("MontoFijo")
                .HasValue<CuponPorcentaje>("Porcentaje");

            modelBuilder.Entity<CuponMontoFijo>()
                        .Property(c => c.Monto)
                        .HasColumnName("Monto");

            modelBuilder.Entity<CuponPorcentaje>()
                        .Property(c => c.Porcentaje)
                        .HasColumnName("Porcentaje");

            modelBuilder.Entity<Producto>()
        .HasOne(p => p.Categoria)
        .WithMany()
        .HasForeignKey(p => p.IdCategoria);
        }
    }
}
