using SWCE.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos
{
    public record CuponMotoFijoDtoBase : DtoBase
    {
        public decimal Monto { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
