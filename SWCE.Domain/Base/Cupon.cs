using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Base
{
    public abstract class Cupon
    {
        protected string? Codigo { get; set; }
        protected DateTime FechaExpiracion { get; set; }

        public bool EstaVigente() => FechaExpiracion >= DateTime.Today;

        public abstract decimal CalcularDescuento(decimal monto);
    }
}
