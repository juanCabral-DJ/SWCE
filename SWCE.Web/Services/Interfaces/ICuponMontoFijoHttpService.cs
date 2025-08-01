using SWCE.Web.Models.Base;
using SWCE.Web.Models.Categoria;
using SWCE.Web.Models.Cupones.CuponMontoFijo;

namespace SWCE.Web.Services.Interfaces
{
    public interface ICuponMontoFijoHttpService
    {
        Task<ModelResponse<List<CuponMontoFijoModel>>> GetAllCuponMontoFijoAsync();
        Task<ModelResponse<CuponMontoFijoModel>> GetCuponMontoFijoByIdAsync(int id);
        Task<ModelResponse<CreateCuponMontoFijoModel>> CreateCuponMontoFijoAsync(CreateCuponMontoFijoModel cuponMontoFijoModel);
        Task<ModelResponse<UpdateCuponMontoFijoModel>> UpdateCuponMontoFijoAsync(UpdateCuponMontoFijoModel cuponMontoFijoModel);
        Task<ModelResponse<DisableCuponMontoFijoModel>> DisableCuponMontoFijoAsync(int id);
    }
}
