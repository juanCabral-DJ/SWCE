using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class CuponPorcentaje : Base.Cupon
    {
        public decimal Porcentaje { get; private set; }

        public CuponPorcentaje(string codigo, decimal porcentaje, DateTime expiracion)
        {
            Codigo = codigo;
            Porcentaje = porcentaje;
            FechaExpiracion = expiracion;
        }

        //REVISAR LA LOGICA DE ESTE METODO
        public override decimal CalcularDescuento(decimal monto) =>
            monto * (Porcentaje / 100);
    }
}
