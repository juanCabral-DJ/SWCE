using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos
{
    public record CreateCuponMontoFijoDto
    {
        public int id { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
