using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Base
{
    abstract class EnvioBase
    {
        public Pedido pedido {  get; set; }
        public Direccion DireccionDestino { get; set; }
        public string Estado {  get; set; }

        public EnvioBase(Pedido pedido, Direccion direccion) 
        {
            this.pedido = pedido;
            this.DireccionDestino = direccion;
            this.Estado = "Pendiente";
        }

        public abstract decimal CalcularCosto();
        public abstract string GenerarGuia();
    }
}
