using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioPedido
    {
        Pedido ObtenerPorId(int id);
        void Guardar(Pedido pedido);
        void Eliminar(int id);
    }
}
