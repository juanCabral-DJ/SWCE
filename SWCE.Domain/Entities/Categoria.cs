using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class Categoria
    {
        public int Id { get; private set; }
        public string? Nombre { get; private set; }
        public string Descripcion { get; private set; }

        public Categoria(int id, string nombre, string descripcion)
        {
            Id = id;
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
