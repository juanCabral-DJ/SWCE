using SWCE.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.ProductoDtos
{
    public record ProductoDtoBase : DtoBase
    {
        public string? Nombre { get; set; }
        public string? Marca { get; set; }
        public int IdCategoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
