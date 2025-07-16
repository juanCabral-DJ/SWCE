using SWCE.Application.Base;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioProducto : IRepositoryBase<Producto>
    {
        Task<OperationResult> DisableAsync(int id);
    }
}