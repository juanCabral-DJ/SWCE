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
        public DbSet<Pedido> Envios { get; set; }
    }
}
