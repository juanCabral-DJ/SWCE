using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class Carrito : EntityBase<int>
    {
        public Carrito() { }

        public List<ItemCarrito> productos { get; set; }

        public override int Id{ get; set; }
        public int ID_Usuario{ get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }

        public Carrito(int id, int ID_Usuario) {
            this.Id = id;
            this.ID_Usuario= ID_Usuario;
        }

    }
}
