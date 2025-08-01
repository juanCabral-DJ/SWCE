using SWCE.Web.Models.Base;
using SWCE.Web.Models.Cupones.CuponMontoFijo;
using SWCE.Web.Models.Cupones.CuponPorcentaje;
using SWCE.Web.Services.Base;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Services
{
    public class CuponPorcentajeHttpService : HttpServiceBase, ICuponPorcentajeHttpService
    {
        public CuponPorcentajeHttpService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<ModelResponse<List<CuponPorcentajeModel>>> GetAllCuponPorcentajeAsync()
        {
            return await GetAsync<List<CuponPorcentajeModel>>("CuponPorcentaje/GetAll");
        }

        public async Task<ModelResponse<CuponPorcentajeModel>> GetCuponPorcentajeByIdAsync(int id)
        {
            return await GetAsync<CuponPorcentajeModel>($"CuponPorcentaje/GetCuponPorcentajeById?id={id}");
        }

        public async Task<ModelResponse<CreateCuponPorcentajeModel>> CreateCuponPorcentajeAsync(CreateCuponPorcentajeModel cuponPorcentaje)
        {
            return await PostAsync<CreateCuponPorcentajeModel>("CuponPorcentaje/Create", cuponPorcentaje);
        }

        public async Task<ModelResponse<UpdateCuponPorcentajeModel>> UpdateCuponPorcentajeAsync(UpdateCuponPorcentajeModel cuponPorcentaje)
        {
            return await PutAsync<UpdateCuponPorcentajeModel>("CuponPorcentaje/Update", cuponPorcentaje);
        }

        public async Task<ModelResponse<CuponPorcentajeModel>> DeleteCuponPorcentajeAsync(int id)
        {
            return await PostAsync<CuponPorcentajeModel>($"CuponPorcentaje/DisableCuponPorcentaje?id={id}", null!);
        }
    }
}
