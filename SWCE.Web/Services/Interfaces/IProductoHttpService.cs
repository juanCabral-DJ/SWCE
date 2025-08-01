using SWCE.Web.Models.Base;
using SWCE.Web.Models.Producto;

namespace SWCE.Web.Services.Interfaces
{
    public interface IProductoHttpService
    {
        Task<ModelResponse<List<ProductoModel>>> GetAllProductosAsync();
        Task<ModelResponse<ProductoModel>> GetProductoByIdAsync(int id);
        Task<ModelResponse<CreateProductoModel>> CreateProductoAsync(CreateProductoModel producto);
        Task<ModelResponse<UpdateProductoModel>> UpdateProductoAsync(UpdateProductoModel producto);
        Task<ModelResponse<DisableProductoModel>> DisableProductoAsync(int id);
    }
}
