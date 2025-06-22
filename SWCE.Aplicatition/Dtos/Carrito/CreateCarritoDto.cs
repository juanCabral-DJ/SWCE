using SWCE.Application.Dtos.ItemCarrito;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.Carrito
{
    public record CreateCarritoDto
    {

        public int IdUsuario { get; init; }

        public List<CreateItemCarritoDto> Productos { get; init; } = new();
    }
}