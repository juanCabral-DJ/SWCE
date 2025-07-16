using SWCE.Application.Base;
using SWCE.Application.Dtos.Carrito;
using SWCE.Domain.Base;

namespace SWCE.Application.Interfaces.Services
{
    public interface ICarritoService
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> CreateAsync(CreateCarritoDto dto);
        Task<OperationResult> UpdateAsync(UpdateCarritoDto dto);
        Task<OperationResult> DisableAsync(DisableCarritoDto dto);
        Task<OperationResult> GetActiveCarritoByUserIdAsync(int userId);
        Task<OperationResult> ClearCarritoAsync(int carritoId);
        Task UpdateCarritoTotal(int carritoId);
    }
}
