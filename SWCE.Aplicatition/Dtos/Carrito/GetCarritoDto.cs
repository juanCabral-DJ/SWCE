using SWCE.Application.Dtos.Base;
using SWCE.Application.Dtos.ItemCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.Carrito
{
    public record GetCarritoDto : CarritoBaseDto
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateAt { get; set; }
        public bool? IsDeleted { get; set; }

        public List<GetItemCarritoDto> Productos { get; set; } = new();
    }
}
