using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    [Table("ItemCarrito")]
    public class ItemCarrito : EntityBase<int>
    {
        // Atributos
        [Column("Id")]
        public override int Id { get; set; }

        [Column("ID_Producto")]
        public int IdProducto { get;  set; }
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
        public decimal PrecioUnitario { get;  set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get;  set; }

        [Column("ID_Carrito")]
        public int CarritoId { get;  set; }

        // Constructor
        public ItemCarrito() { }
        public ItemCarrito(int id, int carritoId, Producto p, int cantidad)
        {
            if (p == null) throw new InvalidOperationException("Producto inválido");
            if (cantidad <= 0) throw new InvalidOperationException("Cantidad inválida");

            this.Id = id;
            this.CarritoId = carritoId;
            IdProducto = p.Id;
            PrecioUnitario = p.Precio;
            Cantidad = cantidad;
        }

        
    }
}
