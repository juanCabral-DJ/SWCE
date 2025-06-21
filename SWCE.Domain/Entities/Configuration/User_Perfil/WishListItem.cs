using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public sealed class WishListItem : Base.EntityBase<int>
    {
        public override int id { get; set; }
        public int id_producto { get; set; }
        public int id_user { get; set; }


        public WishListItem(int id, int id_producto, int id_user)
        {
            this.id = id;
            this.id_producto = id_producto;
            this.id_user = id_user;
        }

     }   

}
