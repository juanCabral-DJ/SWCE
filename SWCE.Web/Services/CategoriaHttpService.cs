using SWCE.Web.Models.Base;
using SWCE.Web.Models.Categoria;
using SWCE.Web.Services.Base;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Services
{
    public class CategoriaHttpService : HttpServiceBase, ICategoriaHttpService
    {
        public CategoriaHttpService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            
        }

        public async Task<ModelResponse<List<CategoriaModel>>> GetAllCategoriasAsync()
        {
            return await GetAsync<List<CategoriaModel>>("Categoria/Getall");
        }

        public async Task<ModelResponse<CategoriaModel>> GetCategoriaByIdAsync(int id)
        {
            return await GetAsync<CategoriaModel>($"Categoria/GetCategoriaById?id={id}");
        }

        public async Task<ModelResponse<CategoriaModel>> CreateCategoriaAsync(CategoriaModel categoria)
        {
            return await PostAsync<CategoriaModel>("Categoria/CreateCategoria", categoria);
        }

        public async Task<ModelResponse<UpdateCategoriaModel>> UpdateCategoriaAsync(UpdateCategoriaModel categoria)
        {
            return await PutAsync<UpdateCategoriaModel>("Categoria/UpdateCategoria", categoria);
        }

        public async Task<ModelResponse<DisableCategoriaModel>> DisableCategoriaAsync(int id)
        {
            return await PostAsync<DisableCategoriaModel>($"Categoria/DisableCategoria?id={id}", null!);
        }
    }
}
