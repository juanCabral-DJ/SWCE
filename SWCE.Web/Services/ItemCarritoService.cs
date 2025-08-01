using SWCE.Model;
using SWCE.Web.Models.Base;
using SWCE.Web.Models.Carrito;
using SWCE.Web.Models.ItemCarrito;
using SWCE.Web.Services.Base;
using SWCE.Web.Services.Interfaces;
using System.Text.Json;

namespace SWCE.Web.Services
{
    public class ItemCarritoService : ServiceBase, IItemCarritoService
    {

        public ItemCarritoService (IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }
        public async Task<ModelResponse<AddItemCarritoModel>> AddItemCarritoAsync(AddItemCarritoModel model)
        {
            return await PostAsync<AddItemCarritoModel, AddItemCarritoModel>("itemscarrito/AddItem", model);
        }

        public async Task<ModelResponse<UpdateItemCantidadModel>> UpdateItemCarritoAsync(UpdateItemCantidadModel model)
        {
            return await PostAsync<UpdateItemCantidadModel, UpdateItemCantidadModel>("itemscarrito/UpdateItem", model);
        }

        public async Task<ModelResponse<DisableItemCarritoModel>> DeleteItemCarritoAsync(DisableItemCarritoModel model)
        {
            return await PostAsync<DisableItemCarritoModel, DisableItemCarritoModel>("itemscarrito/RemoveItem", model);
        }
    }
}
