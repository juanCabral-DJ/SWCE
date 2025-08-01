using SWCE.Web.Models.Base;
using SWCE.Web.Models.ItemCarrito;

namespace SWCE.Web.Services.Interfaces
{
    public interface IItemCarritoService
    {
        Task<ModelResponse<AddItemCarritoModel>> AddItemCarritoAsync(AddItemCarritoModel model);
        Task<ModelResponse<UpdateItemCantidadModel>> UpdateItemCarritoAsync(UpdateItemCantidadModel model);
        Task<ModelResponse<DisableItemCarritoModel>> DeleteItemCarritoAsync(DisableItemCarritoModel model);
    }
}
