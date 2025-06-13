using SWCE.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities.Configuration
{
    internal class ItemCarrito : IItemCarrito
    {
        // Atributos
        public int IdProducto { get; private set; }
        public string NombreProducto { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        public int Cantidad { get; private set; }
        public decimal SubTotal { get; private set; }

        // Constructor
        public ItemCarrito(ProductoMock p, int cantidad)
        {
            if (p == null) throw new InvalidOperationException("Producto inválido");
            if (cantidad <= 0) throw new InvalidOperationException("Cantidad inválida");

            IdProducto = p.Id;
            NombreProducto = p.Nombre;
            PrecioUnitario = p.Precio;
            Cantidad = cantidad;
            SubTotal = calcularSubTotal();
        }

        public void incrementarCantidad(int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new InvalidOperationException("Cantidad inválida");
            }
            this.Cantidad =+ cantidad;
            calcularSubTotal();
        }
        public void actualizarCantidad(int nuevaCantidad)
        {
            if (nuevaCantidad <= 0) throw new InvalidOperationException("Cantidad Invalida");
            this.Cantidad = nuevaCantidad;
            calcularSubTotal();
        }

        public decimal calcularSubTotal()
        {
            return PrecioUnitario * Cantidad;
        }
    }
}
