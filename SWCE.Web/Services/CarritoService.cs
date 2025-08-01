using Microsoft.AspNetCore.Http;
using SWCE.Web.Models.Base;
using SWCE.Web.Models.Carrito;
using SWCE.Web.Services.Base;
using SWCE.Web.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;

namespace SWCE.Web.Services
{
    public class CarritoService : ServiceBase, ICarritoService
    {
        
        public CarritoService(IHttpClientFactory httpClientFactory) : base (httpClientFactory)
        {

        }

        public async Task<ModelResponse<List<CarritoModel>>> GetAllCarritosAsync()
        {
            return await GetAsync<List<CarritoModel>>("Carrito/GetAllCarts");
        }

        public async Task<ModelResponse<CarritoModel>> GetCarritoByIdAsync(int id)
        {
            return await GetAsync<CarritoModel>($"Carrito/GetCartById?id={id}");
        }

        public async Task<ModelResponse<CreateCarritoModel>> CreateCarritoAsync(CreateCarritoModel model)
        {
            return await PostAsync<CreateCarritoModel, CreateCarritoModel>("Carrito/CreateCart", model);
        }

        public async Task<ModelResponse<EditCarritoModel>> UpdateCarritoAsync(EditCarritoModel model)
        {
            return await PostAsync<EditCarritoModel, EditCarritoModel>("Carrito/UpdateCart", model);
        }

        public async Task<ModelResponse<DisableCarritoModel>> DeleteCarritoAsync(DisableCarritoModel model)
        {
            return await PostAsync<DisableCarritoModel, DisableCarritoModel>("Carrito/DisableCart", model);
        }

    }
}
