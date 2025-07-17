using SWCE.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos
{
    public record CuponPorcentajeDtoBase : DtoBase
    {
        public decimal Porcentaje { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
