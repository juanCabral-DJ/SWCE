using SWCE.Web.Models.Base;
using SWCE.Web.Models.Cupones.CuponPorcentaje;

namespace SWCE.Web.Services.Interfaces
{
    public interface ICuponPorcentajeHttpService 
    {
        Task<ModelResponse<List<CuponPorcentajeModel>>> GetAllCuponPorcentajeAsync();
        Task<ModelResponse<CuponPorcentajeModel>> GetCuponPorcentajeByIdAsync(int id);
        Task<ModelResponse<CreateCuponPorcentajeModel>> CreateCuponPorcentajeAsync(CreateCuponPorcentajeModel cuponPorcentaje);
        Task<ModelResponse<UpdateCuponPorcentajeModel>> UpdateCuponPorcentajeAsync(UpdateCuponPorcentajeModel cuponPorcentaje);
        Task<ModelResponse<CuponPorcentajeModel>> DeleteCuponPorcentajeAsync(int id);
    }
}
