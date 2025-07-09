using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class CuponMontoFijo : Base.Cupon
    {
        public decimal Monto { get; set; }

        public CuponMontoFijo() { }
        public CuponMontoFijo(int id, decimal monto, DateTime expiracion)
        {
            this.id = id;
            Monto = monto;
            FechaExpiracion = expiracion;
        }

        public override decimal CalcularDescuento(decimal montoBase) =>
        Math.Min(montoBase, Monto);
    }
}
