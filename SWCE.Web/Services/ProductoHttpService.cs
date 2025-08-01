using SWCE.Web.Models.Base;
using SWCE.Web.Models.Producto;
using SWCE.Web.Services.Base;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Services
{
    public class ProductoHttpService : HttpServiceBase ,IProductoHttpService
    {
        public ProductoHttpService(IHttpClientFactory httpClientFactory) : base (httpClientFactory)
        {

        }
        public async Task<ModelResponse<List<ProductoModel>>> GetAllProductosAsync()
        {
            return await GetAsync<List<ProductoModel>>("Producto/GetAll");
        }

        public async Task<ModelResponse<ProductoModel>> GetProductoByIdAsync(int id)
        {
            return await GetAsync<ProductoModel>($"Producto/GetProductoById?id={id}");
        }

        public async Task<ModelResponse<CreateProductoModel>> CreateProductoAsync(CreateProductoModel producto)
        {
            return await PostAsync<CreateProductoModel>("Producto/CreateProducto", producto);
        }

        public async Task<ModelResponse<UpdateProductoModel>> UpdateProductoAsync(UpdateProductoModel producto)
        {
            return await PutAsync<UpdateProductoModel>("Producto/Update", producto);
        }

        public async Task<ModelResponse<DisableProductoModel>> DisableProductoAsync(int id)
        {
            return await PostAsync<DisableProductoModel>($"Producto/DisableProduct?id={id}", null!);
        }
    }
}