using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class Categoria : EntityBase<int>
    {
        public override int id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

    public Categoria() { }

    public Categoria(int id, string nombre, string descripcion)
        {
            this.id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}
