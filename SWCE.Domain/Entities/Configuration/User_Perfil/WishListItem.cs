using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public class WishListItem
    {
        private int id_item { get; set; }
        private int id_producto { get; set; }
        private int id_user { get; set; }
        public virtual User User { get; set; }

        public WishListItem(int id_item, int id_producto, int id_user)
        {
            this.id_item = id_item;
            this.id_producto = id_producto;
            this.id_user = id_user;
        }

     }   

}
