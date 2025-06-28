using Riok.Mapperly.Abstractions;
using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
