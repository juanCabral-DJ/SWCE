using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioCategoria
    {
        Categoria ObtenerPorId(int id);
        void Guardar(Categoria categoria);
        void Eliminar(int id);
        bool ExisteNombre(string nombre);
    }
}
