using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.ItemCarrito
{
    public record AddItemCarritoDto
    {
        public int CarritoId { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
