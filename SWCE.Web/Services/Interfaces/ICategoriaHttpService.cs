using SWCE.Web.Models.Base;
using SWCE.Web.Models.Categoria;

namespace SWCE.Web.Services.Interfaces
{
    public interface ICategoriaHttpService
    {
        Task<ModelResponse<List<CategoriaModel>>> GetAllCategoriasAsync();
        Task<ModelResponse<CategoriaModel>> GetCategoriaByIdAsync(int id);
        Task<ModelResponse<CategoriaModel>> CreateCategoriaAsync(CategoriaModel categoria);
        Task<ModelResponse<UpdateCategoriaModel>> UpdateCategoriaAsync(UpdateCategoriaModel categoria);
        Task<ModelResponse<DisableCategoriaModel>> DisableCategoriaAsync(int id);
    }
}
