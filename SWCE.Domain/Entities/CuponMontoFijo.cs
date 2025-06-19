using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class CuponMontoFijo : Base.Cupon
    {
        public decimal Monto { get; private set; }

        public CuponMontoFijo(string codigo, decimal monto, DateTime expiracion)
        {
            Codigo = codigo;
            Monto = monto;
            FechaExpiracion = expiracion;
        }

        public override decimal CalcularDescuento(decimal montoBase) =>
        Math.Min(montoBase, Monto);
    }
}
