using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class EnvioEntity
    {
        public Guid Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaPedido { get; set; }
        public string? Estado { get; set; }
        public decimal Costo { get; set; }
        public string? TipoEnvio { get; set; }

        public EnvioEntity(int usuarioId)
        {
            UsuarioId = usuarioId;
            FechaPedido = DateTime.Now;
            Estado = "Pendiente";
        }

        public EnvioEntity()
        {
            FechaPedido = DateTime.Now;
            Estado = "Pendiente";
        }
    }
}
