using SWCE.Web.Models.Categoria;

namespace SWCE.Web.Services.Interfaces
{
    public interface ICategoriaHttpService
    {
        Task<GetAllCategoriaResponse> GetAllCategoriasAsync();
        Task<GetCategoriaByIdResponse> GetCategoriaByIdAsync(int id);
        Task<CreateCategoriaResponse> CreateCategoriaAsync(CategoriaModel categoria);
        Task<UpdateCategoriaResponse> UpdateCategoriaAsync(UpdateCategoriaModel categoria);
        Task<DisableCategoriaResponse> DisableCategoriaAsync(int id);
    }
}
