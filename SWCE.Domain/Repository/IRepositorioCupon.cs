using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioCupon
    {
        bool ExisteCodigo(string codigo);
        Cupon ObtenerPorCodigo(string codigo);
        void Crear(Cupon cupon);
        void Eliminar(string codigo);
        void Editar(Cupon cupon);
    }
}
