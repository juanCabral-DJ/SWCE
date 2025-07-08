using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.ItemCarrito
{
    public record CreateItemCarritoDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int CarritoId { get; set; }
    }
}
