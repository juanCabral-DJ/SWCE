using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.PedidoDtos
{
    public record CancelarPedidoDto
    {
        public int PedidoId { get; set; }
        public DateTime FechaCancelacion { get; set; }
    }
}
