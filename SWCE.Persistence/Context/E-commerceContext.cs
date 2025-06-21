using Microsoft.EntityFrameworkCore;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Persistence.Context
{
    public class E_commerceContext : DbContext
    {
        public E_commerceContext(DbContextOptions<E_commerceContext> options) : base(options)
        {
        } 

        public DbSet<User> Usuarios {  get; set; }
        public DbSet<Address> Direcciones { get; set; }
        public DbSet<WishListItem> Lista_Deseos { get; set; }


    }
}
