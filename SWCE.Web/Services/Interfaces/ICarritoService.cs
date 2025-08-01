using SWCE.Web.Models.Base;
using SWCE.Web.Models.Carrito;

namespace SWCE.Web.Services.Interfaces
{
    public interface ICarritoService
    {
        Task<ModelResponse<List<CarritoModel>>> GetAllCarritosAsync();
        Task<ModelResponse<CarritoModel>> GetCarritoByIdAsync(int id);
        Task<ModelResponse<CreateCarritoModel>> CreateCarritoAsync(CreateCarritoModel model);
        Task<ModelResponse<EditCarritoModel>> UpdateCarritoAsync(EditCarritoModel model);
        Task<ModelResponse<DisableCarritoModel>> DeleteCarritoAsync(DisableCarritoModel model);
    }
}
