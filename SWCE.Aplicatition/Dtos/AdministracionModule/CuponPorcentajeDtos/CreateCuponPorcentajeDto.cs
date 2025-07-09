using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos
{
    public record CreateCuponPorcentajeDto
    {
        public int id { get; set; }
        public decimal Porcentaje { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
