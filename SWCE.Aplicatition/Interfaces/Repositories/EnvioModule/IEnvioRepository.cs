using SWCE.Domain.Entities;
using SWCE.Domain.Base;

namespace SWCE.Aplicatition.Interfaces.Repositories.EnvioModule
{
    public interface IEnvioRepository
    {
        Task<OperationResult> GetByUserId(int userId);
        Task<OperationResult> GetByIdAsync(Guid id);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> CreateAsync(EnvioEntity envio);
        Task<OperationResult> UpdateAsync(EnvioEntity envio);
    }
}
