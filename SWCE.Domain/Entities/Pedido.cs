using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class Pedido : EntityBase<int>
    {
            public override int id { get; set; }
            public DateTime Fecha { get; private set; }
            public string Estado { get; private set; }
            public List<Producto> Items { get; private set; }

            public Pedido(int id)
            {
                this.id = id;
                Fecha = DateTime.UtcNow;
                Estado = "Pendiente";
                Items = new List<Producto>();
            }

            public void Cancelar(string justificacion)
            {
                if (string.IsNullOrWhiteSpace(justificacion))
                {
                    throw new ArgumentException("Debe indicar una justificación válida.");
                }
                Estado = "Cancelado";
            }
    }
}
