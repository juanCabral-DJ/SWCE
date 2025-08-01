using SWCE.Web.Models.Base;
using SWCE.Web.Models.Categoria;
using SWCE.Web.Models.Cupones.CuponMontoFijo;
using SWCE.Web.Services.Base;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Services
{
    public class CuponMontoFijoHttpService : HttpServiceBase, ICuponMontoFijoHttpService
    {
        public CuponMontoFijoHttpService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            
        }

        public async Task<ModelResponse<List<CuponMontoFijoModel>>> GetAllCuponMontoFijoAsync()
        {
            return await GetAsync<List<CuponMontoFijoModel>>("CuponMontoFijo/GetAll");
        }

        public async Task<ModelResponse<CuponMontoFijoModel>> GetCuponMontoFijoByIdAsync(int id)
        {
            return await GetAsync<CuponMontoFijoModel>($"CuponMontoFijo/GetById?id={id}");
        }

        public async Task<ModelResponse<CreateCuponMontoFijoModel>> CreateCuponMontoFijoAsync(CreateCuponMontoFijoModel cuponMontoFijoModel)
        {
            return await PostAsync<CreateCuponMontoFijoModel>("CuponMontoFijo/Create", cuponMontoFijoModel);
        }

        public async Task<ModelResponse<UpdateCuponMontoFijoModel>> UpdateCuponMontoFijoAsync(UpdateCuponMontoFijoModel cuponMontoFijoModel)
        {
            return await PutAsync<UpdateCuponMontoFijoModel>("CuponMontoFijo/Update", cuponMontoFijoModel);
        }
        public async Task<ModelResponse<DisableCuponMontoFijoModel>> DisableCuponMontoFijoAsync(int id)
        {
            return await PostAsync<DisableCuponMontoFijoModel>($"CuponMontoFijo/DisableCuponMontoFijo?id={id}", null!);
        }
    }
}
