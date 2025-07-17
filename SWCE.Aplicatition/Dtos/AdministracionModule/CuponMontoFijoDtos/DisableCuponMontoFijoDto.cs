using SWCE.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos
{
    public record DisableCuponMontoFijoDto : DtoBase
    {
        // Hereda de DtoBase para incluir la propiedad Id
        public bool IsDisabled { get; set; }
    }
}
