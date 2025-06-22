using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    internal interface ICarrito
    {
        public void AgregarItem(Producto p, int cantidad);
        public void EliminarItem(int IdProducto);
        public decimal CalcularSubTotal();
        public decimal CalcularTotal();
    }
}
