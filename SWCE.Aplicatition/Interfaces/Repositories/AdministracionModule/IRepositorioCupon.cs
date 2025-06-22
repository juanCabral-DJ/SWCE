using SWCE.Application.Base;
using SWCE.Domain.Base;

namespace SWCE.Domain.Repository
{
    public interface IRepositorioCupon : IRepositoryBase<Cupon>
    {
        Task<Cupon> ObtenerPorCodigoAsync(string codigo);
    }
}
