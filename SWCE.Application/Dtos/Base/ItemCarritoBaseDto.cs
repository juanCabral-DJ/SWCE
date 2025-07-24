using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.Base
{
    public record ItemCarritoBaseDto
    {
        public int CarritoId { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
