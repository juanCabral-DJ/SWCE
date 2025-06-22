using SWCE.Application.Dtos.ItemCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.Carrito
{
    public record UpdateCarritoDto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public bool IsDeleted { get; set; }
        public List<UpdateItemCarritoDto> Productos { get; set; } = new();
    }
}
