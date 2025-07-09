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
            public string Estado { get; set; }
            

            public Pedido(int id)
            {
                this.id = id;
                Estado = "Pendiente";
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
