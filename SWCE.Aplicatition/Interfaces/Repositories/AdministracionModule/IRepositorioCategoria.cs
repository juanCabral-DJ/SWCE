using SWCE.Application.Base;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System.Linq.Expressions;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioCategoria : IRepositoryBase<Categoria>
    {
        Task<List<Categoria>> ObtenerActivasAsync();
        Task<OperationResult> DisableAsync(int id);
    }
}
