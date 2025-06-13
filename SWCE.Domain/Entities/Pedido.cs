using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class Pedido
    {
            public int Id { get; private set; }
            public DateTime Fecha { get; private set; }
            public string Estado { get; private set; }
            public List<Producto> Items { get; private set; }

            public Pedido()
            {
                Id = Id;
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
