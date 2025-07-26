using SWCE.Application.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.ItemCarrito
{
    public record GetItemCarritoDto : ItemCarritoBaseDto
    {
        public int Id { get; set; }
        public string? NombreProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }

    }
}
