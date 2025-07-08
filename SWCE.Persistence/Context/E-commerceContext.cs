using Microsoft.EntityFrameworkCore;
using SWCE.Domain.Entities;

namespace SWCE.Persistence.Context
{
    public class E_commerceContext : DbContext
    {
        public E_commerceContext(DbContextOptions<E_commerceContext> options) : base(options)
        {
        }
       
        public DbSet<EnvioEntity> Envios { get; set; }

    }
}
