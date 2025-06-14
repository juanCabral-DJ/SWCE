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

        DbSet<User> Usuarios {  get; set; }
        DbSet<Address> Direcciones { get; set; }
        DbSet<WishListItem> Lista_Deseos { get; set; }


    }
}
