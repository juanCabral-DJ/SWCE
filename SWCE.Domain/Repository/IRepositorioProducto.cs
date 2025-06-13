using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioProducto
    {
        Producto ObtenerPorId(int id);
        void Crear(Producto producto);
        List<Producto> ObtenerTodos();
        void Eliminar(int id);
        void Editar(Producto producto);
    }
}
