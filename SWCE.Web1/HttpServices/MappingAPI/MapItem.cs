using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Models.WishListItem;

namespace SWCE.Web1.HttpServices.MappingAPI
{
    public class MapItem
    {
        public ItemModel MapToModel(WishListItem source)
        {
            return new ItemModel
            {
                id = source.id,
                id_producto = source.id_producto,
                id_Usuario = source.Id_Usuario
            };  
    }

        public List<ItemModel> MapToModelList(List<WishListItem> sourceList)
        {
            return sourceList.Select(MapToModel).ToList();
        }

        public CreateItemModel MapToCreateItemModel(WishListItem source)
        {
            return new CreateItemModel
            {
                id_producto = source.id_producto,
                id_Usuario = source.Id_Usuario
            };  
    }

        public DisableItemModel MapToDisableItemModel(WishListItem source)
        {
            return new DisableItemModel
            {
                id = source.id
            };  
    }

        public CreateItemDto MapToCreateDto(CreateItemModel model)
        {
            return new CreateItemDto
            {
                id_producto = model.id_producto,
                Id_Usuario = model.id_Usuario
            };  
    }

        public DisableItemDto MapToDisableDto(DisableItemModel model)
        {
            return new DisableItemDto
            {
                Id = model.id
            };  
    }
    }
}
