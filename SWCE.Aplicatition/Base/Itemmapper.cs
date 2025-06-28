using Riok.Mapperly.Abstractions;
 
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Domain.Entities.Configuration.User_Perfil;
 

namespace SWCE.Aplicatition.Base
{
    [Mapper]
    public partial class Itemmapper
    {
        public WishListItem MapToEntityCreate(CreateItemDto dto)
        {
            return new WishListItem {
                Id_Usuario = dto.Id_Usuario,
                id_producto = dto.id_producto,
            };
        }
        public WishListItem MapToEntity(DisableItemDto dto)
        {
            return new WishListItem
            {
                id = dto.Id,
            };
        }
    }
}
