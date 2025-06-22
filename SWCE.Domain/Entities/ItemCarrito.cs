using SWCE.Domain.Base;
using SWCE.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public class ItemCarrito : EntityBase<int>
    {
        // Atributos
        public override int id { get; set; }
        public int IdProducto { get; private set; }
        public string NombreProducto { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        public int Cantidad { get; private set; }
        public decimal SubTotal { get; private set; }

        public int CarritoId { get; private set; }

        // Constructor
        public ItemCarrito(int id, int carritoId, Producto p, int cantidad)
        {
            if (p == null) throw new InvalidOperationException("Producto inválido");
            if (cantidad <= 0) throw new InvalidOperationException("Cantidad inválida");

            this.id = id;
            this.CarritoId = carritoId;
            IdProducto = p.id;
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
            Cantidad =+ cantidad;
            calcularSubTotal();
        }
        public void actualizarCantidad(int nuevaCantidad)
        {
            if (nuevaCantidad <= 0) throw new InvalidOperationException("Cantidad Invalida");
            Cantidad = nuevaCantidad;
            calcularSubTotal();
        }

        public decimal calcularSubTotal()
        {
            return PrecioUnitario * Cantidad;
        }
    }
}
