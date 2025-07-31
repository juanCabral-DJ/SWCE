using SWCE.Web.Models.Producto;

namespace SWCE.Web.Services.Interfaces
{
    public interface IProductoHttpService
    {
        Task<GetAllProductoResponse> GetAllProductosAsync();
        Task<GetProductoByIdResponse> GetProductoByIdAsync(int id);
        Task<CreateProductoResponse> CreateProductoAsync(CreateProductoModel producto);
        Task<UpdateProductoResponse> UpdateProductoAsync(UpdateProductoModel producto);
        Task<DisableProductoResponse> DisableProductoAsync(int id);
    }
}
