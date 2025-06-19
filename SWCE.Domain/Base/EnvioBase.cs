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
        public Pedido pedido {  get; set; }
        public string Estado {  get; set; }

        public EnvioBase(Pedido pedido)
        {
            this.pedido = pedido;
            this.Estado = "Pendiente";
        }

        public abstract decimal CalcularCosto();
        public abstract string GenerarGuia();
    }
}
