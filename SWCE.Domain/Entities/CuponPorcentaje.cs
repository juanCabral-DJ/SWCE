using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class CuponPorcentaje : Base.Cupon
    {
        public decimal Porcentaje { get; set; }

        public CuponPorcentaje() { }
        public CuponPorcentaje(int id, decimal porcentaje, DateTime expiracion)
        {
            this.id = id;
            Porcentaje = porcentaje;
            FechaExpiracion = expiracion;
        }

        //REVISAR LA LOGICA DE ESTE METODO
        public override decimal CalcularDescuento(decimal monto) =>
            monto * (Porcentaje / 100);
    }
}
