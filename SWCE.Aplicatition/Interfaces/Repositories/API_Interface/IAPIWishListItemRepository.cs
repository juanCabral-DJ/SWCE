using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Interfaces.Repositories.API_Interface
{
    public interface IAPIWishListItemRepository
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllItemasync();
        Task<OperationResult> CreateItemasync(CreateItemDto entity);
        Task<OperationResult> GetbyUserid(int userId);
        Task<OperationResult> DisableItemAsync(DisableItemDto entity);
    }
}
