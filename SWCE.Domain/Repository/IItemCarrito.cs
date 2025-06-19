using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    internal interface IItemCarrito
    {
        public void incrementarCantidad(int cantidad);
        public void actualizarCantidad(int nuevaCantidad);
        public decimal calcularSubTotal();
    }
}
