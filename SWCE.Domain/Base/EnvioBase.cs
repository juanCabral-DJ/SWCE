using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Base
{
    public abstract class EnvioBase
    {
        public Guid Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public decimal Costo { get; set; }
        protected EnvioBase(int usuarioId)
        {
            UsuarioId = usuarioId;
            FechaPedido = DateTime.Now;
            Estado = "Pendiente";
        }

        public abstract decimal CalcularCosto();
    }
}
