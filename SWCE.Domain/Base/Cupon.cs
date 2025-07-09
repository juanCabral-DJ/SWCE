using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Base
{
    public abstract class Cupon : EntityBase<int>
    {
        public override int id { get; set; }
        public DateTime FechaExpiracion { get; set; }

        public bool EstaVigente() => FechaExpiracion >= DateTime.Today;

        public abstract decimal CalcularDescuento(decimal monto);
    }
}
