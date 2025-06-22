using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.ProductoDtos
{
    public record UpdateProductoDto
    {
        public string? Nombre { get; set; }
        public string? Marca { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
