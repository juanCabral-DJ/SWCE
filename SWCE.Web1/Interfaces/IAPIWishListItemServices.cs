using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.WishListItem;

namespace SWCE.Web1.Interfaces
{
    public interface IAPIWishListItemServices 
    {
        Task<ModelResponse<ItemModel>> GetbyIdasync(int id);
        Task<ModelResponse<List<ItemModel>>> GetAllItemasync();
        Task<ModelResponse<CreateItemModel>> CreateItemasync(CreateItemModel entity);
        Task<ModelResponse<List<ItemModel>>> GetbyUserid(int userId);
        Task<ModelResponse<DisableItemModel>> DisableItemAsync(DisableItemModel entity);
    }
}
