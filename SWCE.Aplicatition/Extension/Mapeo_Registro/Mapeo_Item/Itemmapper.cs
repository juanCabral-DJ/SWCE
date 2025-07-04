
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_Item
{

    public static class Itemmapper
    {
        public static WishListItem MapToEntityCreate(CreateItemDto dto)
        {
            return new WishListItem
            {
                Id_Usuario = dto.Id_Usuario,
                id_producto = dto.id_producto,
            };
        }
        public static WishListItem MapToEntity(DisableItemDto dto)
        {
            return new WishListItem
            {
                id = dto.Id,
            };
        }
    }
}
