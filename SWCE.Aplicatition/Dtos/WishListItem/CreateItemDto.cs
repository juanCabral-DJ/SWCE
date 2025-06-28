using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Dtos.WishListItem
{
    public record CreateItemDto
    {
        public int id_producto { get; set; }
        public int Id_Usuario { get; set; }
    }
}
