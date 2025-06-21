using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Dtos.WishListItem
{
    public record CreateWishListItemDto
    {
        public int id_producto { get; set; }
        public int id_user { get; set; }
    }
}
