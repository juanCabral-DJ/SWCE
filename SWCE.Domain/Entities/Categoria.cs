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
        public override int Id { get; set; }
        public string? Nombre { get; set; }
        public string Descripcion { get; set; }

        public Categoria(int id, string nombre, string descripcion)
        {
            this.Id = id;
            AsignarNombre(nombre);
            Descripcion = descripcion;
        }

        public void AsignarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            Nombre = nombre;
        }

        public void ModificarDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción de la categoría no puede estar vacía.");
            Descripcion = descripcion;
        }
    }
}