using SWCE.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities.Configuration
{
    internal class Carrito : ICarrito
    {
        private readonly List<ItemCarrito> productos = new();
        private decimal total;

        public int IdCarrito{ get; set; }
        public int IdUsuario{ get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }

        public Carrito(int IdUsuario) {
            this.IdUsuario= IdUsuario;
        }

        public void AgregarItem (ProductoMock p, int cantidad)
        {
            if (p == null) throw new InvalidOperationException("Producto inválido");
            if (cantidad <= 0) throw new InvalidOperationException("Cantidad inválida");

            var existeProducto = productos.FirstOrDefault(i => i.IdProducto == p.Id);

            if (existeProducto != null)
            {
                existeProducto.incrementarCantidad(cantidad);
            }
            else
            {
                productos.Add(new ItemCarrito(p, cantidad));
            }

            this.SubTotal = CalcularSubTotal();
        }
        
        public void EliminarItem (int IdProducto)
        {
            var producto = productos.FirstOrDefault(i => i.IdProducto == IdProducto);
            
            if (producto != null)
            {
                productos.Remove(producto);
            }
        }

        public decimal CalcularSubTotal()
        {
            decimal subt = 0m;

            foreach (var item in productos)
            {
                subt = + item.SubTotal;
            }

            return subt;
        }

        // Falta implementar mejor el apartado de cupones para verificar como hacer los descuentos
        public decimal CalcularTotal()
        {
            return total = SubTotal - Descuento;

        }

    }
}
