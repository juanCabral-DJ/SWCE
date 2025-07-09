using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.CategoriaDto
{
    public record DisableCategoriaDto
    {
        public int Id { get; set; }
        public bool IsDisabled { get; set; }
    }
}
