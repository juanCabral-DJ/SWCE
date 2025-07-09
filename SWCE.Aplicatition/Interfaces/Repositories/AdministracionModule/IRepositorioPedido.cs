using SWCE.Application.Base;
using SWCE.Domain.Entities;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioPedido :  IRepositoryBase<Pedido>
    {
        public Task<Pedido> CancelarPedido(int pedidoId);
    }
}
