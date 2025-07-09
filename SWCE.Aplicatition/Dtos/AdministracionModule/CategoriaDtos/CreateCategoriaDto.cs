using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.CategoriaDto
{
    public record CreateCategoriaDto
    {
        public int id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
