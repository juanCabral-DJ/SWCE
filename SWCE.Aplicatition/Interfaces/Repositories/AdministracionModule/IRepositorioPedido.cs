using SWCE.Application.Base;
using SWCE.Domain.Entities;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioPedido :  IRepositoryBase<Pedido>
    {
        public Task<List<Pedido>> ObtenerPedidosPorClienteAsync(int clienteId);
        public Task<Pedido> CancelarPedido(int pedidoId);
    }
}
